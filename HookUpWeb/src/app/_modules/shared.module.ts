import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { HttpClientModule } from '@angular/common/http';
import { TabsModule } from "ngx-bootstrap/tabs";
import { NgxSpinnerModule } from "ngx-spinner";

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    ToastModule,
    HttpClientModule,
    TabsModule,
    NgxSpinnerModule.forRoot({ type: 'ball-atom' })
  ],
  exports: [
    BsDropdownModule,
    FormsModule,
    ToastModule,
    HttpClientModule,
    TabsModule,
    NgxSpinnerModule
  ]
})
export class SharedModule { }
