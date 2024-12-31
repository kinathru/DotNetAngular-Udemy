import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth/auth.service';
import { FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
})
export class BookFlightComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  filightId: string = 'not loaded';
  flight: FlightRm = {};

  form = this.fb.group({
    number: [1],
  });

  ngOnInit(): void {
    if (!this.authService.currentUser) {
      this.router.navigate(['/register-passenger']);
    }

    this.route.paramMap.subscribe((p) => this.findFlight(p.get('flightId')));
  }

  private findFlight = (flightId: string | null) => {
    this.filightId = flightId ?? 'not passed';

    this.flightService.findFlight({ flightId: this.filightId }).subscribe({
      next: (response) => {
        this.flight = response;
      },
      error: (err) => {
        if (err.status == 404) {
          alert('Flight Not Found');
          this.router.navigate(['/search-flights']);
        }

        console.log('Response Error. Status : ', err.status);
        console.log(err);
      },
    });
  };

  book() {
    console.log(
      `Booking ${this.form.get('number')?.value} passengers for the flight ${
        this.flight.id
      }`
    );

    const booking = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value ?? 0,
    };
    this.flightService.bookFlight({ body: booking }).subscribe({
      next: () => {
        this.router.navigate(['/my-booking']);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
