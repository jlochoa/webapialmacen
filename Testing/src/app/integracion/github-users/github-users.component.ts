import { Component, OnInit } from '@angular/core';
import { GithubUsersService } from '../github-users.service';

@Component({
  selector: 'app-github-users',
  templateUrl: './github-users.component.html',
  styleUrls: ['./github-users.component.css']
})
export class GithubUsersComponent implements OnInit {
  users: any[]=[];

  constructor(private service: GithubUsersService) {}

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
    this.service.getUsers().subscribe((users) => (this.users = users));
  }
}
