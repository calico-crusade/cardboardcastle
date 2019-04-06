import { LocalStorageService } from 'angular-2-local-storage';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

const STORAGE_ID: string = 'id';
const STORAGE_TOKEN: string = 'token';

@Injectable()
export class StorageService {

    private idSub = new Subject<number>();
    private tokenSub = new Subject<string>();

    public get tokenChanged() {
        return this.tokenSub.asObservable();
    }
    public get idChanged() {
        return this.idSub.asObservable();
    }

    constructor(
        private storage: LocalStorageService
    ) {}

    public get id(): number {
        return <number>this.storage.get(STORAGE_ID) || -1;
    }
    public set id(value: number) {
        if (value == null) {
            this.storage.remove(STORAGE_ID);
        }
        else {
            this.storage.set(STORAGE_ID, value);
        }
        this.idSub.next(value);
    }

    public get token(): string {
        return <string>this.storage.get(STORAGE_TOKEN) || null;
    }
    public set token(value: string) {
        if (value == null) {
            this.storage.remove(STORAGE_TOKEN);
        } else {
            this.storage.set(STORAGE_TOKEN, value);
        }
        this.tokenSub.next(value);
    }
}