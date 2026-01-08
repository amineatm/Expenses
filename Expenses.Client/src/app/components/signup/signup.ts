import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-signup',
  imports: [RouterLink, CommonModule, ReactiveFormsModule],
  templateUrl: './signup.html',
  styleUrl: './signup.css',
})
export class Signup {
  signupForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: Auth, private router: Router) {
    this.signupForm = fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(15)]]
      },
      {
        validators: this.passwordMatchValidator
      }
    )
  }
  passwordMatchValidator(fg: FormGroup) {
    return fg.get('password')?.value === fg.get('confirmPassword')?.value
      ? null
      : { passwordMistmatch: true }
  }

  onSumbit() {
    if (this.signupForm.valid) {
      const signUp = this.signupForm.value;
      this.authService.register(signUp).subscribe({
        next: () => {
          this.router.navigate(['/transactions'])
        },
        error: (error) => {
          console.log('--- Error: ', error);
        }
      })
    }
  }

}
