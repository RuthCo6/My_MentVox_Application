import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  users: { username: string }[] = [];

  addUser() {
    if (this.username && this.password) {
      this.users.push({ username: this.username });
      this.username = '';
      this.password = '';
    }
  }

  deleteUser(index: number) {
    this.users.splice(index, 1);
  }
}
