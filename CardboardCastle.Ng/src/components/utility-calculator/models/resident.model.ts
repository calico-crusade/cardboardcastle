import { IState } from './state.model';

export interface IResident extends IState {
    residentId: number;
    name: string;
}