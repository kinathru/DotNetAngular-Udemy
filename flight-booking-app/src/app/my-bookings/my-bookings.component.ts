import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BookingRm } from '../api/models';
import { BookingService } from '../api/services';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-my-bookings',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css',
})
export class MyBookingsComponent implements OnInit {
  bookings!: BookingRm[];

  constructor(
    private bookingService: BookingService,
    private authService: AuthService
  ) {}
  ngOnInit(): void {
    this.bookingService
      .listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe({
        next: (res) => {
          this.bookings = res;
        },
        error: (err) => {
          this.handleError(err);
        },
      });
  }

  private handleError(err: any) {
    console.log('Response Error, Status: ', err.status);
    console.log('Response Error, Status Text: ', err.statusText);
    console.log(err);
  }

  cancel(booking: BookingRm) {}
}
