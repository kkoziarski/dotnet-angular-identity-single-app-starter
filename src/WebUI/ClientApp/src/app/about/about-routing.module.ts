import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { marker } from '@biesbjerg/ngx-translate-extract-marker';
import { Shell } from '@shell';
import { AboutComponent } from './about.component';

const routes: Routes = [
  Shell.childRoutes([{ path: 'about', component: AboutComponent, data: { title: marker('About') } }], false),
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [],
})
export class AboutRoutingModule {}
