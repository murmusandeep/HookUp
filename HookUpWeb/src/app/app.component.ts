import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'HookUpWeb';
  users: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('https://localhost:5000/api/Users/GetUsers').subscribe({
      next: (response) => {
        this.users = response;
        console.log(response);
      },
      error: () => { },
      complete: () => console.log("Request is completed")
    });
  }
}
