import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../_model/member';
import { environment } from 'src/environments/environment';
import { map, of, take } from 'rxjs';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';
import { UserParams } from '../_model/userParams';
import { AccountService } from './account.service';
import { User } from '../_model/user';

@Injectable({
    providedIn: 'root'
})
export class MemberService {

    baseUrl = environment.apiUrl;
    members: Member[] = [];
    memberCache = new Map();
    userParams: UserParams | undefined;
    user: User | undefined;

    constructor(private http: HttpClient, private accountService: AccountService) {
        this.accountService.currentUser$.pipe().subscribe({
            next: user => {
                if (user) {
                    this.userParams = new UserParams(user);
                    this.user = user;
                }
            }
        });
    }

    getUserParams() {
        return this.userParams;
    }

    setUserParams(params: UserParams) {
        this.userParams = params;
    }

    resetUserParams() {
        if (this.user) {
            this.userParams = new UserParams(this.user);
            return this.userParams;
        }
        return;
    }

    getMembers(userParams: UserParams) {

        var response = this.memberCache.get(Object.values(userParams).join('-'));

        if (response) return of(response);

        let params = getPaginationHeader(userParams.pageNumber, userParams.pageSize);

        params = params.append('minAge', userParams.minAge);
        params = params.append('maxAge', userParams.maxAge);
        params = params.append('gender', userParams.gender);
        params = params.append("orderBy", userParams.orderBy);

        return getPaginatedResult<Member[]>(this.baseUrl + 'Users', params, this.http).pipe(
            map(response => {
                this.memberCache.set(Object.values(userParams).join('-'), response);
                return response;
            })
        );
    }

    getMember(username: string) {
        const member = [...this.memberCache.values()]
            .reduce((arr, elem) => arr.concat(elem.result), [])
            .find((member: Member) => member.userName === username);
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

    addLike(username: string) {
        return this.http.post(this.baseUrl + 'likes/' + username, {});
    }

    getLikes(predicate: string, pageNumber: number, pageSize: number) {
        let params = getPaginationHeader(pageNumber, pageSize);
        params = params.append('predicate', predicate)
        return getPaginatedResult<Member[]>(this.baseUrl + 'likes', params, this.http);
    }
}
