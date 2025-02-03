import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { PublicCourseDetailsVm } from '../../core/models/course.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { loadPublicCourse } from '../../redux/home/home.actions';
import { selectPublicCourse } from '../../redux/home/home.selector';
import { MatDialog } from '@angular/material/dialog';
import { SendMessageComponent } from '../../shared/components/send-message/send-message.component';
import { AuthService } from '../../core/services/auth/auth.service';
import { MessageLookupDto } from '../../core/models/message.model';
import { deleteMessage, loadMessageList } from '../../redux/messages/messages.actions';
import { selectMessageList } from '../../redux/messages/messages.selector';
import { FormsModule } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-public-course',
  standalone: true,
  imports: [SharedModule, FormsModule],
  templateUrl: './public-course.component.html',
  styleUrl: './public-course.component.css'
})
export class PublicCourseComponent implements OnInit {
  selectedCourse!: PublicCourseDetailsVm | null;
  courseId!: number;
  userRole: string = '';
  messageList!: MessageLookupDto[];
  showMessages: boolean = false;

  constructor(
    private store: Store<AppState>,
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private authService: AuthService
  ) {
    this.activatedRoute.params.subscribe(params => this.courseId = params['id']);
    this.getRole();
  }

  ngOnInit(): void {
    this.store.dispatch(loadPublicCourse({id: this.courseId}));
    this.store.select(selectPublicCourse).subscribe((data) => {
      this.selectedCourse = data;
    });
    
  }

  getRole() {
    return this.authService.getUserRole().then(role => {
      this.userRole = role;
    });
  }

  sendQuestion() {
    this.dialog.open(
      SendMessageComponent,
      {
        width: '50%',
        data: {
          course: this.selectedCourse,
          question: null,
          isAuthenticated: this.userRole == 'Student'
        }
      }
    );
  }

  sendApply() {

  }

  onToggleChange($event: MatSlideToggleChange) {
    if (this.userRole == 'Student' && $event.checked) {
      this.store.dispatch(loadMessageList({courseId: this.courseId}));
      this.store.select(selectMessageList).subscribe((data) => {
        this.messageList = data;
      });
    }
  }

  onDeleteMessage(id: number) {
    if (confirm("Вы хотите удалить это вопрос и ответы на него?")) {
      this.store.dispatch(deleteMessage({id: id, courseId: this.courseId}));
    }
  }
}
