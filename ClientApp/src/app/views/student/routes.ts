import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./student.component').then(m => m.StudentComponent),
    data: {
      title: `Students`
    }
  }
];

