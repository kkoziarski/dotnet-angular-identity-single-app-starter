import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CoreModule } from '@core';
import { SharedModule } from '@shared';
import { FetchDataComponent } from './fetch-data.component';
import { QuoteService } from './quote.service';

describe('FetchDataComponent', () => {
  let component: FetchDataComponent;
  let fixture: ComponentFixture<FetchDataComponent>;

  beforeEach(
    waitForAsync(() => {
      TestBed.configureTestingModule({
        imports: [CoreModule, SharedModule, HttpClientTestingModule],
        declarations: [FetchDataComponent],
        providers: [QuoteService],
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(FetchDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
