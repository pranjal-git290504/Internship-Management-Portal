import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./internship.component').then(m => m.InternshipComponent),
    data: {
      title: `Internships`
    }
  }
];

