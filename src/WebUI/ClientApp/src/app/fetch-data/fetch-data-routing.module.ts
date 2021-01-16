import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';

import { FetchDataComponent } from './fetch-data.component';
import { Shell } from '@app/shell/shell.service';

const routes: Routes = [
  Shell.childRoutes(
    [
      { path: '', redirectTo: '/fetch-data', pathMatch: 'full' },
      { path: 'fetch-data', component: FetchDataComponent, data: { title: marker('Todo') } },
    ],
    false
  ),
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class FetchDataRoutingModule {}
