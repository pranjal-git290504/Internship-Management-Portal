import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./application.component').then(m => m.ApplicationComponent),
    data: {
      title: `Applications`
    }
  }
];

