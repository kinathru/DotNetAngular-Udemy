import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css',
})
export class SearchFlightsComponent implements OnInit {
  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService) {}

  ngOnInit(): void {}

  search() {
    this.flightService.flightGet().subscribe((response) => {
      this.searchResult = response;
    });
  }
}
