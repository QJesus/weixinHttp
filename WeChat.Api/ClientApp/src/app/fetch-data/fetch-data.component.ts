import { Component, Inject, OnInit, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@aspnet/signalr';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
    qrcode: string;
    login: boolean;
    users: User[];
    keys: string[];

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    async ngOnInit() {
        const t = await this.http.get<{ token: string, qrcode: string }>(`${this.baseUrl}api/WeChat/AccessToken`).toPromise();
        this.qrcode = t.qrcode;
        if (t.qrcode != null) {
            const r = await this.http.get<{ login: boolean, users: User[] }>(`${this.baseUrl}api/WeChat/Login?token=${t.token}`).toPromise();
            this.login = r.login;
            this.users = r.users;
        } else {
            const u = await this.http.get<{ users: User[] }>(`${this.baseUrl}api/WeChat/Users?token=${t.token}`).toPromise();
            this.login = true;
            this.users = u.users;
        }
        this.keys = Object.keys(this.users[0]).filter(k => !['userName'].includes(k));
        const divMessages: HTMLDivElement = document.querySelector('#divMessages');
        const tbMessage: HTMLInputElement = document.querySelector('#tbMessage');
        const btnSend: HTMLButtonElement = document.querySelector('#btnSend');
        const time = new Date().getTime();

        const connection = new signalR.HubConnectionBuilder().withUrl('/hub').build();
        connection.start().catch(err => console.log(err));

        connection.on('messageReceived', (username: string, message: string) => {
            const m = document.createElement('div');
            m.innerHTML = `<div class="message-author">${username}</div><div>${message}</div>`;
            divMessages.appendChild(m);
            divMessages.scrollTop = divMessages.scrollHeight;
        });

        tbMessage.addEventListener('keyup', (e: KeyboardEvent) => {
            if (e.keyCode === 13) {
                send();
            }
        });
        btnSend.addEventListener('click', send);
        function send() {
            connection.send('newMessage', time, tbMessage.value)
                .then(() => tbMessage.value = '');
        }
    }
}

class User {
    userName: string;
    province: string;
    city: string;
    headImgUrl: string;
    nickName: string;
    pyInitial: string;
    pyQuanPin: string;
    remarkName: string;
    remarkPYInitial: string;
    remarkPYQuanPin: string;
    sex: number;
    signature: string;
    memberCount: number;
    memberList: Member[];
    group: boolean;
}

class Member {
    UserName: string;
    DisplayName: string;
    AttrStatus: number;
    KeyWord: string;
    Uin: number;
    NickName: string;
    PYInitial: string;
    PYQuanPin: string;
    MemberStatus: number;
    RemarkPYInitial: string;
    RemarkPYQuanPin: string;
}
