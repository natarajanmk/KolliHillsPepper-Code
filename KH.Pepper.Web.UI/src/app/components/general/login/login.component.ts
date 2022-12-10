import { Router } from '@angular/router';
import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
//import { faLock } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {
  
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
      emailOrMobile: ['', Validators.required],
      password:  ['', Validators.required],
     // email:  ['', Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')],
    });
  }
}
