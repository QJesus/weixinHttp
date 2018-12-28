import { Component, Inject, OnInit, } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-fetch-data',
    templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
    qrcode: string;
    login: boolean;
    users: User[];
    keys: string[];

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) { }

    async ngOnInit() {
        const t = await this.http.get<{ token: string, qrcode: string }>(`${this.baseUrl}api/WeChat/AccessToken`).toPromise();
        this.qrcode = t.qrcode;
        if (t.qrcode != null) {
            const r = await this.http.get<{ login: boolean, users: User[] }>(`${this.baseUrl}api/WeChat/Login?token=${t.token}`).toPromise();
            this.login = r.login;
            this.users = r.users;
            return;
        }
        this.login = true;
        const u = await this.http.get<{ users: User[] }>(`${this.baseUrl}api/WeChat/Users?token=${t.token}`).toPromise();
        this.users = u.users;
        this.keys = Object.keys(u.users[0]).filter(k => ![
            'userName'
        ].includes(k));
    }
}

class User {
    key: string;
    name: string;
    group: string;
}


