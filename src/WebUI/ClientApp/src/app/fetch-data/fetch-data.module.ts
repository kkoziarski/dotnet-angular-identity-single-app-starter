import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SharedModule } from '@shared';
import { FetchDataRoutingModule } from './fetch-data-routing.module';
import { FetchDataComponent } from './fetch-data.component';

@NgModule({
  imports: [CommonModule, FormsModule, TranslateModule, FontAwesomeModule, SharedModule, FetchDataRoutingModule],
  declarations: [FetchDataComponent],
})
export class FetchDataModule {}
