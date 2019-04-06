import { Injectable } from '@angular/core';

@Injectable()
export class HelperService {

    validateEmail(email: string): boolean {
        if (email == null ||
            email == "" ||
            email.length <= 5)
            return false;

        if (email.indexOf('@') === -1)
            return false;

        if (email.indexOf('.') === -1)
            return false;

        var regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

        return regexp.test(email);
    }

    validatePassword(password: string): boolean {
        if (password == null ||
            password == "" ||
            password.length <= 10)
            return false;

        var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");
        return strongRegex.test(password);
    }

    validateNickname(nickname: string): boolean {
        if (nickname == null ||
            nickname == "" ||
            nickname.length < 4)
            return false;

        return true;
    }
}