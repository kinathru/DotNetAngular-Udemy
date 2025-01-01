import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PassengerService } from '../api/services';
import { NewPassengerDto } from '../api/models';
import {
  FormsModule,
  ReactiveFormsModule,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './register-passenger.component.html',
  styleUrl: './register-passenger.component.css',
})
export class RegisterPassengerComponent implements OnInit {
  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  form = this.fb.group({
    email: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]),
    ],
    firstName: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(35),
      ]),
    ],
    lastName: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(35),
      ]),
    ],
    isFemale: [true, Validators.required],
  });

  ngOnInit(): void {}

  checkPassenger(): void {
    if (this.form.get('email')?.value) {
      const params = { email: this.form.get('email')?.value! };
      this.passengerService.findPassenger(params).subscribe({
        next: this.login,
        error: (err) => {
          console.log('Hello');
        },
      });
    }
  }

  register() {
    if (this.form.invalid) {
      return;
    }

    console.log('Form Values : ', this.form.value);
    this.passengerService
      .registerPassenger({ body: this.form.value })
      .subscribe({
        next: this.login,
        error: (err) => {
          console.log('Error registering user' + err);
        },
      });
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value! });
    this.router.navigate(['/search-flights']);
  };
}
