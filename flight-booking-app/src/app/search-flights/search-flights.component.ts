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
  searchResult: FlightRm[] = [
    {
      airline: 'Airways Alpha',
      arrival: { place: 'New York', time: '2024-12-28T14:30:00' },
      departure: { place: 'Los Angeles', time: '2024-12-28T10:00:00' },
      price: '$450',
      remainingNumberOfSeats: 20,
    },
    {
      airline: 'Bravo Airlines',
      arrival: { place: 'Chicago', time: '2024-12-28T18:00:00' },
      departure: { place: 'Houston', time: '2024-12-28T15:00:00' },
      price: '$320',
      remainingNumberOfSeats: 15,
    },
    {
      airline: 'SkyHigh',
      arrival: { place: 'San Francisco', time: '2024-12-28T13:45:00' },
      departure: { place: 'Seattle', time: '2024-12-28T11:00:00' },
      price: '$280',
      remainingNumberOfSeats: 25,
    },
    {
      airline: 'Delta Express',
      arrival: { place: 'Miami', time: '2024-12-28T19:30:00' },
      departure: { place: 'Atlanta', time: '2024-12-28T17:00:00' },
      price: '$400',
      remainingNumberOfSeats: 10,
    },
    {
      airline: 'Echo Flights',
      arrival: { place: 'Boston', time: '2024-12-28T20:15:00' },
      departure: { place: 'Denver', time: '2024-12-28T16:30:00' },
      price: '$380',
      remainingNumberOfSeats: 5,
    },
    {
      airline: 'Foxtrot Wings',
      arrival: { place: 'Las Vegas', time: '2024-12-28T12:30:00' },
      departure: { place: 'Phoenix', time: '2024-12-28T10:00:00' },
      price: '$150',
      remainingNumberOfSeats: 30,
    },
    {
      airline: 'Global Air',
      arrival: { place: 'Orlando', time: '2024-12-28T21:00:00' },
      departure: { place: 'Dallas', time: '2024-12-28T18:00:00' },
      price: '$310',
      remainingNumberOfSeats: 8,
    },
    {
      airline: 'Horizon',
      arrival: { place: 'Salt Lake City', time: '2024-12-28T15:45:00' },
      departure: { place: 'Portland', time: '2024-12-28T13:00:00' },
      price: '$220',
      remainingNumberOfSeats: 12,
    },
    {
      airline: 'Inland Airways',
      arrival: { place: 'Washington D.C.', time: '2024-12-28T22:30:00' },
      departure: { place: 'Charlotte', time: '2024-12-28T20:00:00' },
      price: '$330',
      remainingNumberOfSeats: 7,
    },
    {
      airline: 'Jet Stream',
      arrival: { place: 'Minneapolis', time: '2024-12-28T23:45:00' },
      departure: { place: 'Detroit', time: '2024-12-28T21:15:00' },
      price: '$270',
      remainingNumberOfSeats: 18,
    },
  ];

  ngOnInit(): void {}
}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingNumberOfSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}
