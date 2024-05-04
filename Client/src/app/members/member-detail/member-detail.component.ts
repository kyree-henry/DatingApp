import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/members';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent {
  member: Member;

  constructor(
    private memberService: MembersService,
    private route: ActivatedRoute)
    {}

    ngOnInit(): void{
      this.loadMember();
    }

    loadMember(){
      this.memberService.getMember(this.route.snapshot.paramMap.get('userName')).subscribe(member => {
        this.member = member;
      })
    }
}
