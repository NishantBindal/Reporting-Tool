import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  menuClosed: boolean = true;
  @Input() navbarBrand: string;
  constructor() {}

  ngOnInit() {

  }
  toggleMenu() {
    this.menuClosed = !this.menuClosed;
  }
}
