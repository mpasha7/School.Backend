import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { CdkAccordionModule } from '@angular/cdk/accordion';

import { SendMessageComponent } from './components/send-message/send-message.component';
import { ShowResultComponent } from './components/show-result/show-result.component';
import { SendFeedbackComponent } from './components/send-feedback/send-feedback.component';
import { ShowAssessmentComponent } from './components/show-assessment/show-assessment.component';
import { SendCommentComponent } from './components/send-comment/send-comment.component';
import { SafePipe } from './pipes/safe.pipe';


@NgModule({
  declarations: [
    SendMessageComponent,
    ShowResultComponent,
    SendFeedbackComponent,
    ShowAssessmentComponent,
    SendCommentComponent,
    SafePipe
  ],
  imports: [
    CommonModule,    
    FormsModule,
    ReactiveFormsModule,

    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSlideToggleModule,
    CdkAccordionModule
  ],
  exports: [
    CommonModule,

    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatGridListModule,
    MatSidenavModule,
    MatDialogModule,
    MatSlideToggleModule,
    CdkAccordionModule,
    SafePipe
  ]
})
export class SharedModule { }
