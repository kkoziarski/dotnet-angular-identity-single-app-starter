import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Shell } from '@shell';
import { CounterComponent } from './counter.component';

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
