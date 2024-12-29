import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-flight',
  standalone: true,
  imports: [],
  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css',
})
export class BookFlightComponent implements OnInit {
  constructor(private route: ActivatedRoute) {}

  filightId: string = 'not loaded';

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (p) => (this.filightId = p.get('flightId') ?? 'not passed')
    );
  }
}
