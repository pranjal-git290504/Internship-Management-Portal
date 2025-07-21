export class Application {
    id: number;
    fkStudentId: number;
    fkInternshipId: number;
    fkApplicationStatusId: number;
    internshipTitle: string
    course: string;
    firstName: string;
    lastName: string;
    email: string;
    applicationStatus: string;
    createdAt?: Date;
    updatedAt?: Date;

    constructor() {
        this.id = 0;
        this.fkStudentId = 0;
        this.fkInternshipId = 0;
        this.fkApplicationStatusId = 0;
        this.internshipTitle = '';
        this.course = '';
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.applicationStatus = '';
        this.createdAt = undefined;
        this.updatedAt = undefined;
    }
}
