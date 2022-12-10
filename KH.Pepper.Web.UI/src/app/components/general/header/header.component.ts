  
import { Component, ViewEncapsulation, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class HeaderComponent implements OnInit {
  constructor( ) {}

  ngOnInit(): void {}
  logout(): void {
    
  }
}
