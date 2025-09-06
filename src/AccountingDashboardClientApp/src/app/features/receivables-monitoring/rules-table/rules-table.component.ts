import { Component, effect, inject, input, OnInit, output, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';

import { ComponentBase } from 'app/shared/component.base';
import { Rule } from 'app/core/models';
import { RuleEditDialogComponent } from '../rule-edit-dialog/rule-edit-dialog.component';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-rules-table',
  templateUrl: './rules-table.component.html',
  styleUrls: ['./rules-table.component.scss'],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
  ],
})
export class RulesTableComponent extends ComponentBase implements OnInit {
  readonly rules = input.required<Rule[]>();
  readonly addRuleEvent = output<Rule>({ alias: 'addRule' });
  readonly editRuleEvent = output<Rule>({ alias: 'editRule' });
  readonly deleteRuleEvent = output<Rule>({ alias: 'deleteRule' });

  readonly paginator = viewChild(MatPaginator);
  readonly sort = viewChild(MatSort);

  readonly searchControl = new FormControl('');

  readonly displayedColumns: string[] = [
    'client',
    'program',
    'depositDestination',
    'updatedDate',
    'action',
  ];
  readonly dataSource = new MatTableDataSource<Rule>([]);

  private readonly dialog = inject(MatDialog);

  constructor() {
    super();

    effect(() => {
      this.dataSource.data = this.rules();
      this.paginator()!.length = this.rules().length;
    });
  }

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator();
    this.dataSource.sort = this.sort();

    this.dataSource.filterPredicate = (data, filter) => {
      const loweredFilter = filter.toLowerCase();

      return (
        data.client.toLowerCase().includes(loweredFilter) ||
        data.program.toLowerCase().includes(loweredFilter) ||
        data.depositDestination.toLowerCase().includes(loweredFilter)
      );
    };

    this.searchControl.valueChanges
      .pipe(this.takeUntilDestroyed(), debounceTime(500))
      .subscribe((filterValue) => (this.dataSource.filter = filterValue ?? ''));
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }

  editRule(rule: Rule | null) {
    const dialogRef = this.dialog.open(RuleEditDialogComponent, {
      width: '400px',
      data: { rule },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) {
        return;
      }

      if (rule) {
        this.editRuleEvent.emit(result);
      } else {
        this.addRuleEvent.emit(result);
      }
    });
  }

  deleteRule(rule: Rule) {
    const confirmed = window.confirm(`Delete rule for ${rule.client} (${rule.program})?`);
    if (confirmed) {
      this.deleteRuleEvent.emit(rule);
    }
  }
}
