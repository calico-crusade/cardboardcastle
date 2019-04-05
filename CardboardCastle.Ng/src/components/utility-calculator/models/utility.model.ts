import { ISplit } from './split.model';
import { IState } from './state.model';

export interface IUtility extends IState {
    utilityId: number;
    type: string;
    value: number;
    splits: ISplit[];
}