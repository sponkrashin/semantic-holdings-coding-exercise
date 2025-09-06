import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'receivables-monitoring',
        loadChildren: () => import('./features/receivables-monitoring/receivables-monitoring.routes')
    },
    {
        path: 'commission-validation',
        loadChildren: () => import('./features/commission-validation/commission-validation.routes')
    },
    {
        path: 'cpc-spend-monitoring',
        loadChildren: () => import('./features/cpc-spend-monitoring/cpc-spend-monitoring.routes')
    },
    { path: '**', redirectTo: 'receivables-monitoring', pathMatch: 'full' },

];
