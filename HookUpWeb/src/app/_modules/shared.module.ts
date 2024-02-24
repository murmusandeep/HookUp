import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    FormsModule,
    ToastModule,
    HttpClientModule,
  ],
  exports: [
    BsDropdownModule,
    FormsModule,
    ToastModule,
    HttpClientModule,
  ]
})
export class SharedModule { }
