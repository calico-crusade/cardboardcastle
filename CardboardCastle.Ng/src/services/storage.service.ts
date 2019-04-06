import { LocalStorageService } from 'angular-2-local-storage';
import { Injectable } from '@angular/core';

const STORAGE_ID: string = 'id';
const STORAGE_TOKEN: string = 'token';

@Injectable()
export class StorageService {

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
    }
}