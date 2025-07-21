import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Application, PaginatedResult } from '../../models';
import { ApplicationService } from '../../services/application.service';
import { TableDirective, TableColorDirective, TableActiveDirective, PaginationComponent, PageItemDirective, PageLinkDirective}  from '@coreui/angular';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-application',
  standalone: true,
  imports: [TableDirective, TableColorDirective, TableActiveDirective, PaginationComponent, PageItemDirective, PageLinkDirective, RouterLink, CommonModule],
  templateUrl: './application.component.html',
  styleUrl: './application.component.scss'
})
export class ApplicationComponent {
  constructor(private applicationService: ApplicationService) { }
  paginatedResult: PaginatedResult<Application> = new PaginatedResult<Application>([], 0, 1, 5);
  applications: Array<Application> = [];
  ngOnInit(): void {
    this.getAllApplications();
  }

  getAllApplications() {
    this.applicationService.getAllApplications(this.paginatedResult.pageNumber, this.paginatedResult.pageSize).subscribe(response => {
      this.paginatedResult = response;
    });
  }

  onPageChange(pageNumber: number) {
    this.paginatedResult.pageNumber = pageNumber;
    this.getAllApplications();
  }

  getPages(totalPages: number): number[] {
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }
}
