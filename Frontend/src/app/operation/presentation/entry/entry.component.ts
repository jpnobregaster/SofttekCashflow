import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CashflowModel } from '@app/operation/domain/cashflow/model/cashflow.model';
import { CashflowServiceContract } from '@app/operation/domain/cashflow/service/cashflow.service.contract';
import { AlertServiceConstract } from '@app/shared/domain/service/alert.service.contract';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-entry',
  templateUrl: './entry.component.html',
})
export class EntryComponent implements OnInit, OnDestroy {

    entryForm!: FormGroup;
    save$ = new Subscription();
    submitted = false;

    get fields() { 
        return this.entryForm.controls; 
    }

    constructor(private formBuilder: FormBuilder,
        private alertService: AlertServiceConstract,
        private cashFlowService: CashflowServiceContract) {
    }

    ngOnInit() {
        this.entryForm = this.formBuilder.group({
            entry: ['', Validators.required]
        });
    }

    onClickSubmit() {
        this.submitted = true;

        if(!this.entryForm.valid) {
            return;
        }

        this.alertService.loading();

        const cashflow = CashflowModel.CashflowModelBuilder.Create()
            .withValue(this.entryForm.value.entry)
            .Make();

        this.save$ = this.cashFlowService.addObservable(cashflow).subscribe(() => {
            this.submitted = false;
            this.entryForm.reset();
            this.alertService.success(`Lançamento adicionado com sucesso.`)
        });
    }

    ngOnDestroy(): void {
        this.save$.unsubscribe();
    }
}
