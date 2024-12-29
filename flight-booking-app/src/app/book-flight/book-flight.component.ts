import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { CommonModule } from '@angular/common';

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
    private flightService: FlightService
  ) {}

  filightId: string = 'not loaded';
  flight: FlightRm = {};
  number: number = 0;

  ngOnInit(): void {
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
        }

        console.log('Response Error. Status : ', err.status);
        console.log(err);
      },
    });
  };
}
