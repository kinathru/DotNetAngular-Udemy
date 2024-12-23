import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../employee.service';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css',
})
export class EmployeeFormComponent implements OnInit {
  employee: Employee = {
    id: 0,
    firstName: '',
    lastName: '',
    phone: '',
    email: '',
    position: '',
  };
  errorMessage: string = '';
  isEditing: boolean = false;

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((result) => {
      console.log(result);
      const id = result.get('id');
      if (id) {
        // Edit employee
        this.isEditing = true;
        this.employeeService.getEmployeeById(Number(id)).subscribe({
          next: (response) => {
            this.employee = response;
          },
          error: (err) => {
            console.error('Error loading employee', err);
            this.errorMessage = `Error occured: ${err.status} - ${err.message}`;
          },
        });
      }
    });
  }

  onSubmit(): void {
    console.log(this.employee);

    if (this.isEditing) {
      this.employeeService.editEmployee(this.employee).subscribe({
        next: (response) => {
          this.router.navigate(['/']);
        },
        error: (err) => {
          console.error(err);
          this.errorMessage = `Error occured during update: ${err.status} - ${err.message}`;
        },
      });
    } else {
      // logic to create new employee
      this.employeeService.createEmployee(this.employee).subscribe({
        next: (response) => {
          this.router.navigate(['/']);
        },
        error: (err) => {
          console.error(err);
          this.errorMessage = `Error occured during creation: ${err.status} - ${err.message}`;
        },
      });
    }
  }
}
