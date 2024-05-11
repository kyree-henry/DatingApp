import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/members';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-members-list',
  templateUrl: './members-list.component.html',
  styleUrls: ['./members-list.component.css']
})
export class MembersListComponent {
  members$: Observable<Member[]>;

  constructor(private memberService: MembersService) {}

  ngOnInit(): void{
    this.members$ = this.memberService.getMembers();
  }

}
