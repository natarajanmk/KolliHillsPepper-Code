import { Component, ViewEncapsulation, OnInit } from '@angular/core';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']   ,
  encapsulation: ViewEncapsulation.None,
})
export class ProductComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
