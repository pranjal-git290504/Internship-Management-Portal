# Function to check if a port is in use
function Test-PortInUse {
    param([int]$Port)
    $listener = $null
    try {
        $listener = New-Object System.Net.Sockets.TcpListener([System.Net.IPAddress]::Loopback, $Port)
        $listener.Start()
        return $false
    }
    catch {
        return $true
    }
    finally {
        if ($listener) {
            $listener.Stop()
        }
    }
}

function Stop-ProcessOnPort {
    param([int]$Port)
    try {
        $processInfo = netstat -ano | findstr ":$Port"
        if ($processInfo) {
            $processId = $processInfo.Split()[-1]
            $process = Get-Process -Id $processId -ErrorAction SilentlyContinue
            if ($process) {
                Write-Host "Stopping process on port $Port (PID: $processId)"
                Stop-Process -Id $processId -Force
                Start-Sleep -Seconds 2
            }
        }
    }
    catch {
        Write-Host "No process found using port $Port"
    }
}

# Kill any existing dotnet processes that might be hanging
Get-Process | Where-Object { $_.ProcessName -eq "dotnet" } | ForEach-Object {
    try {
        $_ | Stop-Process -Force -ErrorAction SilentlyContinue
        Write-Host "Stopped existing dotnet process (PID: $($_.Id))"
    }
    catch {
        Write-Host "Could not stop process (PID: $($_.Id))"
    }
}

# Check and free ports if needed
$backendPort = 5056
$frontendPort = 4200

if (Test-PortInUse -Port $backendPort) {
    Write-Host "Port $backendPort is in use. Attempting to free it..."
    Stop-ProcessOnPort -Port $backendPort
}

if (Test-PortInUse -Port $frontendPort) {
    Write-Host "Port $frontendPort is in use. Attempting to free it..."
    Stop-ProcessOnPort -Port $frontendPort
}

# Start backend
Write-Host "Starting backend..." -ForegroundColor Yellow
$backendPath = Join-Path $PSScriptRoot "Intern"
Set-Location $backendPath

# First build the project
Write-Host "Building backend project..." -ForegroundColor Yellow
$buildResult = dotnet build
if ($LASTEXITCODE -ne 0) {
    Write-Host "Backend build failed:" -ForegroundColor Red
    Write-Host $buildResult
    exit 1
}

# Start the backend with detailed output
Write-Host "Running backend..." -ForegroundColor Yellow
$backendProcess = Start-Process "dotnet" -ArgumentList "run --verbosity detailed" -PassThru -NoNewWindow -RedirectStandardOutput "backend.log" -RedirectStandardError "backend.error.log"

# Wait for backend to be ready
$timeout = 60 # 60 seconds timeout
$elapsed = 0
$interval = 2
$ready = $false

Write-Host "Waiting for backend to start (timeout: $timeout seconds)..." -ForegroundColor Yellow
while (-not $ready -and $elapsed -lt $timeout) {
    if ($backendProcess.HasExited) {
        Write-Host "Backend process exited prematurely!" -ForegroundColor Red
        Write-Host "Error log:" -ForegroundColor Red
        Get-Content "backend.error.log"
        Write-Host "Output log:" -ForegroundColor Yellow
        Get-Content "backend.log"
        exit 1
    }

    try {
        $response = Invoke-WebRequest "http://localhost:$backendPort/swagger/index.html" -UseBasicParsing -ErrorAction SilentlyContinue
        if ($response.StatusCode -eq 200) {
            $ready = $true
            Write-Host "Backend is ready!" -ForegroundColor Green
        }
    }
    catch {
        Write-Host "Waiting for backend to start... ($elapsed seconds elapsed)" -ForegroundColor Yellow
        Start-Sleep -Seconds $interval
        $elapsed += $interval
    }
}

if (-not $ready) {
    Write-Host "Error: Backend failed to start on port $backendPort after $timeout seconds" -ForegroundColor Red
    Write-Host "Error log:" -ForegroundColor Red
    Get-Content "backend.error.log"
    Write-Host "Output log:" -ForegroundColor Yellow
    Get-Content "backend.log"
    if ($backendProcess -and -not $backendProcess.HasExited) {
        Stop-Process -Id $backendProcess.Id -Force -ErrorAction SilentlyContinue
    }
    exit 1
}

# Start frontend
Write-Host "Starting frontend..." -ForegroundColor Yellow
Set-Location (Join-Path $PSScriptRoot "ClientApp")

# Use the full path to npm
$npmPath = "C:\Program Files\nodejs\npm.cmd"
$frontendProcess = Start-Process $npmPath -ArgumentList "start" -PassThru -NoNewWindow -RedirectStandardOutput "frontend.log" -RedirectStandardError "frontend.error.log"

# Register cleanup on script exit
$null = Register-ObjectEvent -InputObject $backendProcess -EventName Exited -Action {
    if ($frontendProcess -and -not $frontendProcess.HasExited) {
        Stop-Process -Id $frontendProcess.Id -Force -ErrorAction SilentlyContinue
    }
}

$null = Register-ObjectEvent -InputObject $frontendProcess -EventName Exited -Action {
    if ($backendProcess -and -not $backendProcess.HasExited) {
        Stop-Process -Id $backendProcess.Id -Force -ErrorAction SilentlyContinue
    }
}

Write-Host "`nApplication is running!" -ForegroundColor Green
Write-Host "Backend URL: http://localhost:$backendPort" -ForegroundColor Cyan
Write-Host "Frontend URL: http://localhost:$frontendPort" -ForegroundColor Cyan
Write-Host "Press Ctrl+C to stop both services`n" -ForegroundColor Yellow

try {
    Wait-Process -Id $frontendProcess.Id
}
catch {
    Write-Host "`nStopping services..." -ForegroundColor Yellow
}
finally {
    if ($backendProcess -and -not $backendProcess.HasExited) {
        Stop-Process -Id $backendProcess.Id -Force -ErrorAction SilentlyContinue
    }
    if ($frontendProcess -and -not $frontendProcess.HasExited) {
        Stop-Process -Id $frontendProcess.Id -Force -ErrorAction SilentlyContinue
    }
} 