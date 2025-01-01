import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlightService } from '../api/services';
import { FlightRm } from '../api/models';
import { RouterModule } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SearchFlight$Params } from '../api/fn/flight/search-flight';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, FormsModule],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css',
})
export class SearchFlightsComponent implements OnInit {
  searchResult: FlightRm[] = [];

  constructor(private flightService: FlightService, private fb: FormBuilder) {}

  searchForm = this.fb.group({
    from: [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassengers: [1],
  });

  ngOnInit(): void {}

  search() {
    const searchCriteria: SearchFlight$Params = {
      from: this.searchForm.get('from')?.value ?? undefined,
      destination: this.searchForm.get('destination')?.value ?? undefined,
      fromDate: this.searchForm.get('fromDate')?.value ?? undefined,
      toDate: this.searchForm.get('toDate')?.value ?? undefined,
      numberOfPassengers:
        this.searchForm.get('numberOfPassengers')?.value ?? undefined,
    };

    this.flightService.searchFlight(searchCriteria).subscribe({
      next: (response) => {
        this.searchResult = response;
      },
      error: (err) => {
        console.log('Response Error. Status : ', err.status);
        console.log(err);
      },
    });
  }
}
