import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { CommonModule } from '@angular/common';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
})
export class BookFlightComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService
  ) {}

  filightId: string = 'not loaded';
  flight: FlightRm = {};
  number: number = 0;

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
}
