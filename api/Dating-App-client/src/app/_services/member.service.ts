import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Observable, of } from 'rxjs'
import { Member, MemberFilter } from '../_models/member'
@Injectable({
  providedIn: 'root'
})
export class MemberService {
  baseUrl = "https://localhost:7039/api/members"
  constructor(private httpClient: HttpClient) { }
  getMembers(memberFilter?: MemberFilter): Observable<Member[]> {
    return this.httpClient.get<Member[]>(`${this.baseUrl}?Keyword=${memberFilter?.keyword}`)
  }
  getMemberByUsername(username: string): Observable<Member> {
    return this.httpClient.get<Member>(`${this.baseUrl}/${username}`)
  }
}
