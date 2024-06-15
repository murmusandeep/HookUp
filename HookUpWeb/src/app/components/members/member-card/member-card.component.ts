import { Component, Input, OnInit } from '@angular/core';
import { Member } from "../../../_model/member";
import { MemberService } from 'src/app/_services/member.service';
import { MessageService } from 'primeng/api';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() member: Member | undefined;

  constructor(private memberService: MemberService, private messageService: MessageService, public presenceService: PresenceService) { }

  ngOnInit(): void { }

  addLike(member: Member) {
    this.memberService.addLike(member.userName).subscribe({
      next: () => this.messageService.add({ severity: 'success', summary: 'Success', detail: 'You have Liked ' + member.knownAs })
    })
  }
}
