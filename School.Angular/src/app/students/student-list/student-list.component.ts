import { Component, Inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { Student, StudentLookupDto, StudentsIdsVm } from '../../core/models/student.model';
import { Store } from '@ngrx/store';
import { AppState } from '../../redux/store';
import { loadStudentIds, loadStudentListByIds, loadStudentListBySearch } from '../../redux/students/students.actions';
import { selectStudentsIds, selectStudentsList } from '../../redux/students/students.selector';
import { FormsModule } from '@angular/forms';
import { DOCUMENT, JsonPipe } from '@angular/common';
import { Constants } from '../../shared/constants/constants';

@Component({
  selector: 'app-student-list',
  standalone: true,
  imports: [SharedModule, FormsModule],
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css'
})
export class StudentListComponent implements OnInit {
  search: string = '';
  studentIds!: any;
  idsString: string = '';
  studentList!: Student[];

  constructor(
    private store: Store<AppState>,
    @Inject(DOCUMENT) private document: Document
  ) { }

  ngOnInit(): void {
    this.getIds();
  }

  getIds() {
    this.store.dispatch(loadStudentIds());
    this.store.select(selectStudentsIds).subscribe((data) => {
      this.studentIds = {...data};
      this.document.location.href = Constants.idpAuthority + `/students/list/${JSON.stringify(this.studentIds)}`;
    });
  }

  getStudents() {
    // this.studentIds.forEach(id => {
    //   this.idsString += `,${id}`;
    // });

    // for (let id of this.studentIds) {
    //   this.idsString += `,${id}`;
    // }

    // this.idsString = `${this.studentIds[0]},${this.studentIds[1]}`; 
    this.store.dispatch(loadStudentListByIds({ids: '77be0187-1d57-42dd-8d76-145c36c51bed,acc53bf2-c3f6-442b-99c0-da2cf971516e'}));

    // if (this.search != '') {
    //   this.store.dispatch(loadStudentListBySearch({search: this.search}));
    // }
    // else {
    //   this.store.dispatch(loadStudentListByIds({ids: this.idsString}));
    // }
    this.store.select(selectStudentsList).subscribe((data) => {
      this.studentList = data;
    });
  }
}
