import { Component, Input } from '@angular/core';
import { Member } from 'src/app/_models/members';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {
  @Input() member: Member;

  constructor () {}
}
