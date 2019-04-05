import { IState } from './state.model';

export interface ISplit extends IState {
    splitId: number;
    residentId: number;
    split: number;
}