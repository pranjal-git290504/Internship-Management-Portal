import { Component, inject, DestroyRef } from '@angular/core';
import { NgStyle } from '@angular/common';
import { IconDirective } from '@coreui/icons-angular';
import { ContainerComponent, RowComponent, ColComponent, CardGroupComponent, TextColorDirective, CardComponent, CardBodyComponent, FormDirective, InputGroupComponent, InputGroupTextDirective, FormControlDirective, ButtonDirective } from '@coreui/angular';
import { AuthService } from '../../../services/auth.service';
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    standalone: true,
    imports: [
      FormsModule,
      RouterModule,
      ContainerComponent,
      RowComponent, 
      ColComponent,
      CardGroupComponent,
      TextColorDirective,
      CardComponent,
      CardBodyComponent,
      FormDirective, 
      InputGroupComponent,
      InputGroupTextDirective,
      IconDirective, 
      FormControlDirective,
      ButtonDirective,
      NgStyle
    ]
})
export class LoginComponent {
  private readonly destroyRef = inject(DestroyRef);
  user = { Username: '', Password: '' };
  constructor(private authService: AuthService) { }

  login() {
    this.authService.login(this.user).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe();
  }

  onRegisterClick() {
    console.log('Register button clicked');
  }
}
