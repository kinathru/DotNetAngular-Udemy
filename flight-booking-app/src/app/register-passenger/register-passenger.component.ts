import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PassengerService } from '../api/services';
import { NewPassengerDto } from '../api/models';
import { FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';

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
    private fb: FormBuilder
  ) {}

  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true],
  });

  ngOnInit(): void {}

  register() {
    console.log('Form Values : ', this.form.value);
    this.passengerService
      .registerPassenger({ body: this.form.value })
      .subscribe((_) => console.log('form posted to server'));
  }
}
