import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { SharedModule } from '@shared';
import { CounterRoutingModule } from './counter-routing.module';
import { CounterComponent } from './counter.component';

@NgModule({
  imports: [CommonModule, FormsModule, TranslateModule, FontAwesomeModule, SharedModule, CounterRoutingModule],
  declarations: [CounterComponent],
})
export class CounterModule {}
