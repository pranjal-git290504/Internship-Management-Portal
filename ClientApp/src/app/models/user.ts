export class User {
    id: number;
    firstName: string = '';
    lastName: string = '';
    email: string = '';
    username: string;

    constructor() {
        this.id = 0;
        this.firstName = '';
        this.lastName = '';
        this.email = '';
        this.username = '';
    }

    setData(user: User) {
        this.id = user.id;
        this.firstName = user.firstName;
        this.lastName = user.lastName;
        this.email = user.email;
        this.username = user.username;
    }   
}
