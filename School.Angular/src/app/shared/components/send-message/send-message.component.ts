import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { AppState } from '../../../redux/store';
import { CourseLookupDto, PublicCourseLookupDto } from '../../../core/models/course.model';
import { MessageLookupDto } from '../../../core/models/message.model';
import { createMessage } from '../../../redux/messages/messages.actions';

@Component({
  selector: 'app-send-message',
  templateUrl: './send-message.component.html',
  styleUrl: './send-message.component.css'
})
export class SendMessageComponent implements OnInit {
  messageForm: FormGroup;
  course!: PublicCourseLookupDto | CourseLookupDto;
  question!: MessageLookupDto | null;
  isAuthenticated!: boolean;

  constructor(
    private dialogRef: MatDialogRef<SendMessageComponent>,
    private store: Store<AppState>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.messageForm = new FormGroup({
      email: new FormControl<string>('', [Validators.email]),
      phone: new FormControl<string>(''),  // , [Validators.pattern('\+7[0-9]{10}')] не работает RegEx
      text: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)])
    });
  }

  ngOnInit(): void {
    this.course = this.data.course;
    this.question = this.data.question;
    this.isAuthenticated = this.data.isAuthenticated;
  }

  closePopup() {
    this.dialogRef.close();
  }

  saveMessage() {
    if (this.messageForm.valid) {
      // студент спрашивает
      let createMessageDto = {
        theme: this.course.title,
        text: this.messageForm.value.text,
        email: this.messageForm.value.email,
        phone: this.messageForm.value.phone,
        courseId: this.course.id,
        recipientGuid: '',
        questionId: 0
      }

      if (this.question != null) {
        // тренер отвечает
        createMessageDto.recipientGuid = this.question.senderGuid;
        createMessageDto.questionId = this.question.id;
      }

      this.store.dispatch(createMessage({createMessageDto: createMessageDto}));
      this.closePopup();
    }
  }
}
