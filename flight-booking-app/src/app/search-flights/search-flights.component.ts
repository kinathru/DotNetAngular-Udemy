import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css',
})
export class SearchFlightsComponent implements OnInit {
  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService) {}

  ngOnInit(): void {}

  search() {
    this.flightService.searchFlight().subscribe({
      next: (response) => {
        this.searchResult = response;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
