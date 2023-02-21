import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Pagination } from './Models/Pagination';
import { Product } from './Models/Product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

// onInitialization is another lifecycle hook
// It's provided as an interface
export class AppComponent implements OnInit{
  title = 'Skinet';
  // Property to store what we get returned from api
  products: Product[] = [];

// Lifecycle hooks. Various stages an entity goes through
// Inject something into constructor
// Make it private so we can only use http in this class

  constructor(private http: HttpClient) {}

  // Normally considered too early to call this inside constructor
  // Typically use constructor for dependency injection
  ngOnInit(): void {
    // Will return an observable of the response body as a js object
    // Must subscribe to observable
    // will auto-unsubscribe when complete
    this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products?PageSize=50').subscribe({
    //   <Pagination<Product[]>> allows us to use type safety with typescript in out methods. I.E. no need to use "any"
    next: (response) => this.products = response.data, 
      error: error => console.log(error),
      complete: () => {
        console.log('request completed');
        console.log('Extra statement');
      }
    })
  }
}

