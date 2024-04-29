import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_model/member';
import { Pagination } from 'src/app/_model/pagination';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  members: Member[] | undefined;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 3;
  pagination: Pagination | undefined;

  constructor(private memberService: MemberService) {
  }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    });
  }

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.loadLikes();
    }
  }

}
