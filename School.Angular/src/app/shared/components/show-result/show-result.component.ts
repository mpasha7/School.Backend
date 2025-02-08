import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-show-result',
  templateUrl: './show-result.component.html',
  styleUrl: './show-result.component.css'
})
export class ShowResultComponent implements OnInit {
 resultText: string = '';
 result: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<ShowResultComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    this.result = this.data.result;
    if (this.result) {
      this.resultText = this.data.resultText + ' прошло успешно';
    }
    else {
      this.resultText = this.data.resultText + ' отменено из-за ошибки';
    }
  }

  closePopup() {
    this.dialogRef.close();
  }
}
