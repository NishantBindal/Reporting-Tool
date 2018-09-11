import { Component, OnInit } from '@angular/core';
import { LoginComponent } from '../login/login.component';
import { SignupComponent } from '../signup/signup.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
  }

  showLoginForm() {
    this.modalService.open(LoginComponent)
  }
  showSignupForm() {
    this.modalService.open(SignupComponent)
  }
}
