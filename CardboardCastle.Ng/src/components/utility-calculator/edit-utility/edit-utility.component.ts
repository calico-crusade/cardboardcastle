import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IUtility, IResident, ISplit, ObjectState } from './../models';
import * as _ from 'underscore';

@Component({
    selector: 'edit-utility',
    templateUrl: './edit-utility.template.html',
    styleUrls: ['./edit-utility.style.scss']
})
export class EditUtilityComponent {

    private _utility: IUtility = null;
    private _residents: IResident[] = [];

    @Input()
    public set utility(util: IUtility) {
        this._utility = util;
        this.refreshSplits();
    }
    public get utility() {
        return this._utility;
    }

    @Input()
    public set residents(res: IResident[]) {
        this._residents = res;
        this.refreshSplits();
    }
    public get residents() {
        return this._residents;
    }

    public splits: ISplit[] = [];

    public getResident(id: number) {
        return _.find(this.residents, t => t.residentId === id);
    }

    private refreshSplits() {
        this.splits = [];
        for (var i = 0; i < this.residents.length; i++) {
            var res = this.residents[i];
            var split = _.find(this.utility.splits, t => t.residentId === res.residentId);

            if (split) {
                var s = this.clone(split);
                s.split *= 100;
                this.splits.push(s);
            } else {
                this.splits.push({
                    splitId: -1,
                    residentId: res.residentId,
                    split: 0,
                    state: ObjectState.Added
                });
            }
        }
    }

    private clone<T>(value: T): T {
        return JSON.parse(JSON.stringify(value));
    }

    constructor(
        public modal: NgbActiveModal
    ) { }

}