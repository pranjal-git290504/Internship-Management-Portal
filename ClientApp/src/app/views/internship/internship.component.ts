import { Component, ViewChild } from '@angular/core';
import { Internship, PaginatedResult } from '../../models';
import { InternshipService } from '../../services/internship.service';
import { TableDirective, TableColorDirective, TableActiveDirective, PaginationComponent, PageItemDirective, 
  PageLinkDirective, CardComponent, ColComponent, RowComponent, FormCheckComponent, TextColorDirective, CardHeaderComponent,
   CardBodyComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, FormCheckInputDirective, 
   FormCheckLabelDirective, ButtonDirective, ColDirective, InputGroupComponent, InputGroupTextDirective, ModalComponent,
    ModalHeaderComponent, ModalBodyComponent, ModalFooterComponent, ButtonCloseDirective, ButtonGroupComponent,
    ToasterComponent,
    ToastComponent,
    ToastHeaderComponent,
    ToastBodyComponent,
    ProgressComponent,
    ProgressBarComponent,
    ToasterPlacement}  from '@coreui/angular';
import { RouterLink } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { IconDirective } from '@coreui/icons-angular';
import { NotificationComponent } from '../shared/notification/notification.component';

@Component({
  selector: 'app-internship',
  standalone: true,
  providers: [DatePipe],
  imports: [RowComponent, ColComponent, TextColorDirective, CardComponent, CardHeaderComponent, CardBodyComponent, FormControlDirective, 
    ReactiveFormsModule, FormsModule, FormDirective, FormLabelDirective, FormSelectDirective, FormCheckComponent, 
    FormCheckInputDirective, FormCheckLabelDirective, ButtonDirective, ColDirective, InputGroupComponent, 
    InputGroupTextDirective, CommonModule, PaginationComponent, PageItemDirective, PageLinkDirective, RouterLink, 
    TableDirective, TableColorDirective, TableActiveDirective, ModalComponent, ModalHeaderComponent, ModalBodyComponent, 
    ModalFooterComponent, ButtonCloseDirective, ButtonGroupComponent, IconDirective,
    ToasterComponent, ToastComponent,ToastHeaderComponent, ToastBodyComponent, ProgressComponent, ProgressBarComponent],
  templateUrl: './internship.component.html',
  styleUrl: './internship.component.scss'
})
export class InternshipComponent {
  constructor(private internshipService: InternshipService, private datePipe: DatePipe) { }
  paginatedResult: PaginatedResult<Internship> = new PaginatedResult<Internship>([], 0, 1, 5);
  internships: Array<Internship> = [];
  internship: Internship = new Internship();
  showAddEditModal = false;

  placement = ToasterPlacement.TopEnd;

  @ViewChild(ToasterComponent) toaster!: ToasterComponent;

  ngOnInit(): void {
    this.getAllInternships();
    
    if(this.internship?.id > 0){
      // Get the internship by id
    } else {
      // Initialize Start and End Date
      const today = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
      this.internship.startDate = today ? today : '';
      this.internship.endDate = today ? today : '';
    }
  }

  addToast(content: string, color: string) {
    const options = {
      content: content,
      delay: 3000,
      placement: this.placement,
      color: color,
      autohide: true
    };
    const componentRef = this.toaster.addToast(NotificationComponent, { ...options });
  }

  getAllInternships() {
    this.internshipService.getAll(this.paginatedResult.pageNumber, this.paginatedResult.pageSize).subscribe(response => {
      this.paginatedResult = response;
    });
  }

  onPageChange(pageNumber: number) {
    this.paginatedResult.pageNumber = pageNumber;
    this.getAllInternships();
  }

  getPages(totalPages: number): number[] {
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }

  toggleAddEditModal() {
    this.showAddEditModal = !this.showAddEditModal;
  }

  handleAddEditModalChange(event: any) {
    this.showAddEditModal = event;
  }

  save() {
    console.log(this.internship);
    if(this.internship.title && this.internship.location && this.internship.startDate && this.internship.endDate && this.internship.description) {
      this.internshipService.upsert(this.internship).subscribe(response => {
        if(response.success) {
          this.addToast(`Internship ${(this.internship.id > 0 ? 'updated' : 'saved')} successfully`, 'success');
          this.getAllInternships();
          this.resetModel();
        } else {
          // Add Toastr
          console.error(response.message);
        }
      });
    } else {
      this.addToast(`Please fill all the required fields`, 'warning');
    }
  }

  resetModel(){
    this.internship = new Internship();
    this.toggleAddEditModal();
  }

  addModal(){
    this.internship = new Internship();
    this.toggleAddEditModal();

  }

  updateModal(internship: Internship) {
    this.internship.setData(internship);
    this.toggleAddEditModal();
  }
  remove(id: number) {
    this.internshipService.remove(id).subscribe(response => {
      if(response.success) {
        this.addToast(`Internship removed successfully`, 'success');
        this.getAllInternships();
      } else {
        // Add Toastr
        console.error(response.message);
      }
    });
  }
}
