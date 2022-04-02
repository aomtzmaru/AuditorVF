import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatRadioModule } from '@angular/material/radio';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {ClipboardModule} from '@angular/cdk/clipboard';

const MaterialComponent = [
  MatDatepickerModule,
  MatNativeDateModule,
  MatButtonModule,
  MatIconModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatTooltipModule,
  MatRadioModule,
  MatCheckboxModule,
  MatSlideToggleModule,
  ClipboardModule
];

@NgModule({
  declarations: [],
  imports: [
    MaterialComponent
  ],
  exports: [
    MaterialComponent
  ]
})
export class MaterialModule { }
