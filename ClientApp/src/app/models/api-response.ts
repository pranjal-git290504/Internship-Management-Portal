export class ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;

    constructor(apiResponse: ApiResponse<T>) {
        this.success = apiResponse.success;
        this.message = apiResponse.message;
        this.data = apiResponse.data;
    }
}