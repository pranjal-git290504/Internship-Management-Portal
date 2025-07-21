export class Student {
    id: number;
    firstName: string;
    lastName: string;
    username: string;
    email: string;
    collegeName: string;
    fkCollegeId: number;
    course: string;
    yearOfStudy: number;
    resumeBase64: string;
    createdAt?: Date;
    updatedAt?: Date;

    constructor() {
        this.id = 0;
        this.firstName = '';
        this.lastName = '';
        this.username = '';
        this.email = '';
        this.collegeName = '';
        this.fkCollegeId = 0;
        this.course = '';
        this.yearOfStudy = 0;
        this.resumeBase64 = '';
        this.createdAt = undefined;
        this.updatedAt = undefined
    }
}
