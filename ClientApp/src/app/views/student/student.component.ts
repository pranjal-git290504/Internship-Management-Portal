import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { StudentService } from '../../services';
import { TableDirective, TableColorDirective, TableActiveDirective, PaginationComponent, PageItemDirective, PageLinkDirective, CardComponent, ColComponent, RowComponent, FormCheckComponent, TextColorDirective, CardHeaderComponent, CardBodyComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, FormCheckInputDirective, FormCheckLabelDirective, ButtonDirective, ColDirective, InputGroupComponent, InputGroupTextDirective}  from '@coreui/angular';
import { PaginatedResult, Student } from 'src/app/models';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [RowComponent, ColComponent, TextColorDirective, CardComponent, CardHeaderComponent, CardBodyComponent, FormControlDirective, 
    ReactiveFormsModule, FormsModule, FormDirective, FormLabelDirective, FormSelectDirective, FormCheckComponent, 
    FormCheckInputDirective, FormCheckLabelDirective, ButtonDirective, ColDirective, InputGroupComponent, 
    InputGroupTextDirective, CommonModule, PaginationComponent, PageItemDirective, PageLinkDirective, RouterLink, TableDirective, TableColorDirective, TableActiveDirective],
  templateUrl: './student.component.html',
  styleUrl: './student.component.scss'
})
export class StudentComponent implements OnInit {
  constructor(private studentService: StudentService) { }
  paginatedResult: PaginatedResult<Student> = new PaginatedResult<Student>([], 0, 1, 5);
  students: Array<Student> = [];
  ngOnInit(): void {
    this.getAllStudents();
  }

  getAllStudents() {
    this.studentService.getAllStudents(this.paginatedResult.pageNumber, this.paginatedResult.pageSize).subscribe(response => {
      this.paginatedResult = response;
    });
  }

  onPageChange(pageNumber: number) {
    this.paginatedResult.pageNumber = pageNumber;
    this.getAllStudents();
  }

  getPages(totalPages: number): number[] {
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }
}
