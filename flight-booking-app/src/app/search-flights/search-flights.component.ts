import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search-flights',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css',
})
export class SearchFlightsComponent implements OnInit {
  searchResult: any = ['American Airlines', 'British Airways', 'Lufthansa'];

  ngOnInit(): void {}
}
