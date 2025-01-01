import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BookingRm } from '../api/models';
import { BookingService } from '../api/services';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

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
    private authService: AuthService,
    private router: Router
  ) {}
  ngOnInit(): void {
    if (!this.authService.currentUser?.email) {
      this.router.navigate(['/register-passenger']);
    }

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
