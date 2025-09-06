import { Component, inject, signal } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

import { Rule } from 'app/core/models';
import { ComponentBase } from 'app/shared/component.base';

export interface RuleEditDialogData {
  rule?: Rule;
}

@Component({
  selector: 'app-rule-edit-dialog',
  templateUrl: './rule-edit-dialog.component.html',
  styleUrls: ['./rule-edit-dialog.component.scss'],
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
})
export class RuleEditDialogComponent extends ComponentBase {
  private readonly fb = inject(FormBuilder);
  private readonly dialogRef = inject(MatDialogRef<RuleEditDialogComponent>);
  private readonly data = inject<RuleEditDialogData>(MAT_DIALOG_DATA);

  isEditForm = signal(false);
  form: FormGroup;

  constructor() {
    super();

    this.isEditForm.set(!!this.data.rule);

    this.form = this.fb.group({
      client: [this.data.rule?.client ?? '', Validators.required],
      program: [this.data.rule?.program ?? '', Validators.required],
      depositDestination: [
        this.data.rule?.depositDestination ?? '',
        [Validators.required, Validators.pattern(/^[A-Za-z]+ \d{4}$/)],
      ],
    });
  }

  save() {
    if (this.form.valid) {
      this.dialogRef.close({ ...this.data.rule, ...this.form.value });
    }
  }

  cancel() {
    this.dialogRef.close();
  }
}
