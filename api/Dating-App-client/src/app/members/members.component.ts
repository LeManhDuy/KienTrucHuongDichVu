import { Component, OnInit } from '@angular/core';
import { Member, MemberFilter } from '../_models/member';
import { MemberService } from '../_services/member.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  members: Member[] = [];
  memberFilter: MemberFilter = new MemberFilter();
  constructor(private memberService: MemberService) { }

  ngOnInit(): void {
    this.memberService.getMembers(this.memberFilter).subscribe(
      (response) => console.log(this.members = response),
      (err) => console.log(err)
    )
  }
  search(): void {
    this.memberService.getMembers(this.memberFilter).subscribe(
      (response) => console.log(this.members = response),
      (err) => console.log(err)
    )
  }
}
