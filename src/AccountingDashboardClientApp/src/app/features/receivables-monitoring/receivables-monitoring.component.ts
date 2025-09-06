import { Component, inject, OnInit, signal } from '@angular/core';
import { startWith, Subject, switchMap } from 'rxjs';

import { ComponentBase } from 'app/shared/component.base';
import { RulesService } from 'app/core/services/rules.service';
import { Rule } from 'app/core/models';
import { RulesTableComponent } from './rules-table/rules-table.component';

@Component({
  selector: 'app-receivables-monitoring',
  standalone: true,
  templateUrl: './receivables-monitoring.component.html',
  imports: [RulesTableComponent],
})
export class ReceivablesMonitoringComponent extends ComponentBase implements OnInit {
  private readonly rulesService = inject(RulesService);

  private reloadDataSubject = new Subject<void>();

  rules = signal<Rule[]>([]);

  ngOnInit(): void {
    this.reloadDataSubject
      .pipe(
        startWith(null),
        this.takeUntilDestroyed(),
        switchMap(() => this.rulesService.getRules())
      )
      .subscribe((rules) => this.rules.set(rules));
  }

  addRule(rule: Rule) {
    this.rulesService
      .addRule(rule)
      .pipe(this.takeUntilDestroyed())
      .subscribe(() => this.reloadDataSubject.next());
  }

  editRule(rule: Rule) {
    this.rulesService
      .updateRule(rule.id, rule)
      .pipe(this.takeUntilDestroyed())
      .subscribe(() => this.reloadDataSubject.next());
  }

  deleteRule(rule: Rule) {
    this.rulesService
      .deleteRule(rule.id)
      .pipe(this.takeUntilDestroyed())
      .subscribe(() => this.reloadDataSubject.next());
  }
}
