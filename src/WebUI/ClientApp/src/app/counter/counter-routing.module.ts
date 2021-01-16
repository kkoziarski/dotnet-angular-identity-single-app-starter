import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

import { CounterComponent } from './counter.component';
import { Shell } from '@app/shell/shell.service';

const routes: Routes = [
  Shell.childRoutes(
    [
      { path: '', redirectTo: '/counter', pathMatch: 'full' },
      { path: 'counter', component: CounterComponent, data: { title: marker('Counter') } },
    ],
    false
  ),
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class CounterRoutingModule {}
