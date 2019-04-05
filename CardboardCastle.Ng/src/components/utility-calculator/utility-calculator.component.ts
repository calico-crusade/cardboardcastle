import { Component, OnInit } from '@angular/core';
import * as _ from 'underscore';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IResident, IUtility, ObjectState } from './models';
import { EditUtilityComponent } from './edit-utility/edit-utility.component';

@Component({
    selector: 'utility-calculator-comp',
    templateUrl: './utility-calculator.template.html',
    styleUrls: [ './utility-calculator.style.scss' ]
})
export class UtilityCalculator implements OnInit {

    utilities: IUtility[] = [];
    residents: IResident[] = [];
    newResident: string = '';
    newType: string = '';
    newValue: number = 0;

    getSplit(util: IUtility, res: IResident) {
        return _.find(util.splits, t => t.residentId === res.residentId);
    }

    total(res: IResident) {
        var total = 0;

        for (var i = 0; i < this.utilities.length; i++) {

            var util = this.utilities[i];

            var split = this.getSplit(util, res);

            if (split) {
                total += util.value * split.split;
            }
        }

        return total;
    }

    getResident(id: number) {
        return _.find(this.residents, t => t.residentId === id);
    }

    addResident() {
        if (this.newResident == null || this.newResident.length <= 1)
            return;

        var id = 1;
        for (var i = 0; i < this.residents.length; i++) {
            var res = this.residents[i];

            if (id <= res.residentId)
                id = res.residentId + 1;
        }

        this.residents.push({
            residentId: id,
            name: this.newResident,
            state: ObjectState.Added
        });
        this.newResident = '';
    }

    addType() {
        if (this.newType == null || this.newType.length <= 1)
            return;

        if (this.newValue == null)
            return;

        var item = _.findIndex(this.utilities, t => t.type.toLowerCase().trim() == this.newType.toLowerCase().trim());

        if (item !== -1)
            return;

        var id = 1;
        for (var i = 0; i < this.utilities.length; i++) {
            var res = this.utilities[i];

            if (id <= res.utilityId)
                id = res.utilityId + 1;
        }

        this.utilities.push({
            utilityId: id,
            type: this.newType,
            value: this.newValue,
            splits: [],
            state: ObjectState.Added
        });

        this.newValue = 0;
        this.newType = '';
    }
    
    editUtility(util: IUtility) {
        var mu = JSON.parse(JSON.stringify(util));
        var ref = this.modalService.open(EditUtilityComponent);
        ref.componentInstance.utility = mu;
        ref.componentInstance.residents = this.residents;
        ref.result.then((result) => {
            console.log('Closed', result);
        }, (reason) => {
            console.log('Dismissed', reason);
        });
    }

    constructor(
        private modalService: NgbModal
    ) { }

    ngOnInit() {
        this.residents = [
            {
                residentId: 1,
                name: 'Alec',
                state: ObjectState.Unchanged
            },
            {
                residentId: 2,
                name: 'Matt',
                state: ObjectState.Unchanged
            },
            {
                residentId: 3,
                name: 'Thing',
                state: ObjectState.Unchanged
            }
        ];

        this.utilities = [
            {
                utilityId: 1,
                type: 'Rent',
                value: 1015.00,
                splits: [
                    {
                        splitId: 1,
                        residentId: 1,
                        split: 0.65,
                        state: ObjectState.Unchanged
                    },
                    {
                        splitId: 2,
                        residentId: 2,
                        split: 0.35,
                        state: ObjectState.Unchanged
                    }
                ],
                state: ObjectState.Unchanged
            },
            {
                utilityId: 2,
                type: 'Internet',
                value: 69.99,
                splits: [
                    {
                        splitId: 3,
                        residentId: 1,
                        split: 0.5,
                        state: ObjectState.Unchanged
                    },
                    {
                        splitId: 4,
                        residentId: 2,
                        split: 0.5,
                        state: ObjectState.Unchanged
                    }
                ],
                state: ObjectState.Unchanged
            },
            {
                utilityId: 3,
                type: 'Thing',
                value: 12.99,
                splits: [
                    {
                        splitId: 5,
                        residentId: 3,
                        split: 1,
                        state: ObjectState.Unchanged
                    }
                ],
                state: ObjectState.Unchanged
            }
        ];
    }
}