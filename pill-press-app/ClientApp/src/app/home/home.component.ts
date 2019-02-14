import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { faPhone, faEnvelope } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  window = window;
  busy: Subscription;
  faPhone = faPhone;
  faEnvelope = faEnvelope;

  constructor(private titleService: Title) { }

  ngOnInit() {
    this.titleService.setTitle('Home - Pill Press Registry');
  }
}
