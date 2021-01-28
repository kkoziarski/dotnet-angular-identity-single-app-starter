import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Shell } from '@shell';
import { FetchDataComponent } from './fetch-data.component';

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
