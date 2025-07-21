import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroupDirective, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ButtonDirective, CardBodyComponent, CardComponent, CardFooterComponent, CardHeaderComponent, ColComponent, ColDirective, FormCheckComponent, FormCheckInputDirective, FormCheckLabelDirective, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, InputGroupComponent, InputGroupTextDirective, PageItemDirective, PageLinkDirective, PaginationComponent, ProgressBarComponent, ProgressComponent, RowComponent, TextColorDirective, ToastBodyComponent, ToastComponent, ToasterComponent, ToastHeaderComponent } from '@coreui/angular';
import { IconDirective } from '@coreui/icons-angular';
import { User } from 'src/app/models';
import { AuthService, UserService } from 'src/app/services';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [RowComponent, ColComponent, TextColorDirective, CardComponent, CardHeaderComponent, CardBodyComponent, FormControlDirective, 
    ReactiveFormsModule, FormsModule, FormDirective, FormLabelDirective, FormSelectDirective, FormCheckComponent, 
    FormCheckInputDirective, FormCheckLabelDirective, ButtonDirective, ColDirective, InputGroupComponent, 
    InputGroupTextDirective, CommonModule, PaginationComponent, PageItemDirective, PageLinkDirective, RouterLink, IconDirective,
    ToasterComponent, ToastComponent,ToastHeaderComponent, ToastBodyComponent, ProgressComponent, ProgressBarComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {

  constructor(private authService: AuthService, private userService: UserService) { }

  ngOnInit(): void {
    const userId = this.authService.getUserId();
    this.getByUserId(userId);
  }
  user: User = new User();

  updateProfile() {
    // Update the user profile
  }

  getByUserId(userId: number) {
    // Get the user by id
    this.userService.getById(userId).subscribe(response => {
      if (response.success) {
        this.user.setData(response.data);
        console.log(this.user);
      }
    });
  }
}
