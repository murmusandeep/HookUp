import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../_model/member';
import { environment } from 'src/environments/environment';
import { map, of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MemberService {

    baseUrl = environment.apiUrl;
    members: Member[] = [];

    constructor(private http: HttpClient) { }

    getMembers() {
        if (this.members.length > 0) return of(this.members);
        return this.http.get<Member[]>(this.baseUrl + 'Users').pipe(
            map(members => {
                this.members = members;
                return members;
            })
        );
    }

    getMember(username: string) {
        const member = this.members.find(x => x.userName === username);
        if (member) return of(member);
        return this.http.get<Member>(this.baseUrl + 'Users/' + username);
    }

    updateMember(member: Member) {
        return this.http.put(this.baseUrl + 'Users', member).pipe(
            map(() => {
                const index = this.members.indexOf(member);
                this.members[index] = { ...this.members[index], ...member }
            })
        );
    }

    setMainPhoto(photoId: number) {
        return this.http.put(this.baseUrl + 'Users/set-main-photo/' + photoId, {});
    }

    deletePhoto(photoId: number) {
        return this.http.delete(this.baseUrl + 'Users/delete-photo/' + photoId);
    }
}
