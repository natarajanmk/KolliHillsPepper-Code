import { Router } from '@angular/router';
import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
 
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class RegisterComponent implements OnInit {
  
  requiredForm!: FormGroup;
  formSubmitted = false;

  constructor(private formBuilder: FormBuilder) {
    this.FrmValidation();
  }

  ngOnInit(): void {
     
  }
  onSubmit(event:any): void { 
    
    this.formSubmitted = true;
     console.log(this.requiredForm);
  }
  FrmValidation(){
    this.requiredForm = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname:  ['', Validators.required],
      password:  ['', Validators.required],
     // email:  ['', Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')],
      phonenumber:  ['', Validators.required],
    });
  }
}
