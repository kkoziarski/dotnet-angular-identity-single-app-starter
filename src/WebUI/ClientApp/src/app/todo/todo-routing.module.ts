import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Shell } from '@shell';
import { TodoComponent } from './todo.component';

const routes: Routes = [
  Shell.childRoutes(
    [
      { path: '', redirectTo: '/todo', pathMatch: 'full' },
      { path: 'todo', component: TodoComponent, data: { title: marker('Todos') } },
    ],
    true
  ),
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class TodoRoutingModule {}
