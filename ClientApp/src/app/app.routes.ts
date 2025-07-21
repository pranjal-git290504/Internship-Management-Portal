import { Routes } from '@angular/router';
import { DefaultLayoutComponent } from './layout';
import { authGuard, accountGuard } from './guards';
import { ProfileComponent } from './views/profile/profile.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: () => import('./views/dashboard/routes').then((m) => m.routes),
        canActivate: [authGuard]
      },
      {
        path: 'students',
        loadChildren: () => import('./views/student/routes').then((m) => m.routes),
        canActivate: [authGuard],
        data: {role : 'admin'}
      },
      {
        path: 'internships',
        loadChildren: () => import('./views/internship/routes').then((m) => m.routes),
        canActivate: [authGuard],
        data: {role : 'admin'}
      },
      {
        path: 'applications',
        loadChildren: () => import('./views/application/routes').then((m) => m.routes),
        canActivate: [authGuard],
        data: {role : 'admin'}
      },
      {
        path: 'profile',
        component: ProfileComponent
      },
      {
        path: 'pages',
        loadChildren: () => import('./views/pages/routes').then((m) => m.routes)
      }
    ]
  },
  {
    path: '404',
    loadComponent: () => import('./views/pages/page404/page404.component').then(m => m.Page404Component),
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    loadComponent: () => import('./views/pages/page500/page500.component').then(m => m.Page500Component),
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    loadComponent: () => import('./views/pages/login/login.component').then(m => m.LoginComponent),
    data: {
      title: 'Login Page'
    },
    canActivate: [accountGuard]
  },
  {
    path: 'register',
    loadComponent: () => import('./views/pages/register/register.component').then(m => m.RegisterComponent),
    data: {
      title: 'Register Page'
    },
    canActivate: [accountGuard]
  },
  {
    path: 'forgot-password',
    loadComponent: () => import('./views/pages/forgot-password/forgot-password.component').then(m => m.ForgotPasswordComponent),
    data: {
      title: 'Forgot Password'
    },
    canActivate: [accountGuard]
  },
  { path: '**', redirectTo: 'dashboard' }
];
