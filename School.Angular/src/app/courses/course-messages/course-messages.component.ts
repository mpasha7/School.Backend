import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { deleteMessage, loadMessageList } from '../../redux/messages/messages.actions';
import { selectMessageList } from '../../redux/messages/messages.selector';
import { MessageLookupDto } from '../../core/models/message.model';
import { AuthService } from '../../core/services/auth/auth.service';
import { SendMessageComponent } from '../../shared/components/send-message/send-message.component';
import { CourseLookupDto } from '../../core/models/course.model';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-course-messages',
  standalone: true,
  imports: [RouterLink, SharedModule],
  templateUrl: './course-messages.component.html',
  styleUrl: './course-messages.component.css'
})
export class CourseMessagesComponent implements OnInit {
  courseId!: number;
  courseTitle!: string;
  messageList!: MessageLookupDto[];
  isCoach: boolean = false;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private dialog: MatDialog
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
    this.activatedRoute.queryParams.subscribe((queryParams) => {
      this.courseTitle = queryParams["courseTitle"];
    });
    this.getRole();
  }

  ngOnInit(): void {
    this.store.dispatch(loadMessageList({courseId: this.courseId}));
    this.store.select(selectMessageList).subscribe((data) => {
      this.messageList = data;
    });
  }

  getRole() {
    return this.authService.getUserRole().then(role => {
      if (role === 'Coach'){
        this.isCoach = true;
      }
    });
  }

  onSendQuestion() {
    this.openDialog(null);
  }

  onSendAnswer(question: MessageLookupDto) {
    this.openDialog(question);
  }

  openDialog(question: MessageLookupDto | null) {
    this.dialog.open(
      SendMessageComponent,
      {
        width: '50%',
        data: {
          course: {id: this.courseId, title: this.courseTitle},
          question: question,
          isAuthenticated: true
        }
      }
    );
  }

  onDeleteMessage(id: number) {
    let confirmText: string = '';
    if (this.isCoach) {
      confirmText = 'Вы хотите удалить этот ответ?';
    }
    else {
      confirmText = 'Вы хотите удалить этот вопрос и ответы на него?';
    }

    if (confirm(confirmText)) {
      this.store.dispatch(deleteMessage({id: id, courseId: this.courseId}));
    }
  }
}
