import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-login',
  imports: [RouterLink, CommonModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  loginForm: FormGroup;
  errorMessage: string | null = null;
  constructor(private fb: FormBuilder, private authService: Auth, private router: Router) {
    this.loginForm = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
      }
    )
  }
  hasError(controlName: string, errorName: string): boolean {
    const control = this.loginForm.get(controlName);
    return !!control && (control.touched || control.dirty) && control.hasError(errorName);
  }


  onSumbit() {
    this.errorMessage = null;
    if (this.loginForm.valid) {
      const signUp = this.loginForm.value;
      this.authService.login(signUp).subscribe({
        next: () => {
          this.router.navigate(['/transactions'])
        },
        error: (error) => {
          console.log('--- Error: ', error);
          this.errorMessage = error.error
        }
      })
    }
  }
}
