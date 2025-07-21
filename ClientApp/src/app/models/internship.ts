import {  DatePipe } from '@angular/common';
const datePipe  = new DatePipe('en-US');

export class Internship {
    id: number;
    title: string = '';
    description: string = '';
    location: string = '';
    startDate: string;
    endDate: string;
    createdAt?: Date;
    updatedAt?: Date;

    constructor() {
        this.id = 0;
        this.title = '';
        this.description = '';
        this.location = '';
        this.startDate = '';
        this.endDate = '';
        this.createdAt = undefined;
        this.updatedAt = undefined;
    }

    setData(internship: Internship) {
        const startDate = datePipe.transform(internship.startDate, 'yyyy-MM-dd');
        const endDate = datePipe.transform(internship.endDate, 'yyyy-MM-dd');
        this.id = internship.id;
        this.title = internship.title;
        this.description = internship.description;
        this.location = internship.location;
        this.startDate = startDate ? startDate : '';
        this.endDate = endDate ? endDate : '';
        this.createdAt = internship.createdAt;
        this.updatedAt = internship.updatedAt;
    }   
}
