import { IUser } from './user.model';

export interface ILoginResult {
    token: string;
    user: IUser;
}