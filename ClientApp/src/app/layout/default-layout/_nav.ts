import { INavDataExtended } from './nav-data';
import {AuthService} from "../../services/auth.service";

export const navItems: INavDataExtended[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' }
  },
  {
    name: 'Students',
    url: '/students',
    iconComponent: { name: 'cil-user' },
    role: 'Admin'
  },
  {
    name: 'Internship',
    url: '/internships',
    iconComponent: { name: 'cil-user' },
    role: 'Admin'
  },
  {
    name: 'Application',
    url: '/applications',
    iconComponent: { name: 'cil-user' },
    role: 'Admin'
  }
];
