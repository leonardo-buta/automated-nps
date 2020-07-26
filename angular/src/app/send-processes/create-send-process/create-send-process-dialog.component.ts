import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatDatepickerInputEvent } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';
import {
  SendProcessServiceProxy,
  CreateSendProcessInput,
  MessageDto,
  MessageServiceProxy,
  MessageDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-send-process-dialog.component.html',
  styleUrls: ['./create-send-process-dialog.component.css']
})
export class CreateSendProcessDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  sendProcess: CreateSendProcessInput = new CreateSendProcessInput();
  messages: MessageDto[] = [];
  inputStartDate: Date = new Date();

  constructor(
    injector: Injector,
    private _sendProcessService: SendProcessServiceProxy,
    private _messageService: MessageServiceProxy,
    private _dialogRef: MatDialogRef<CreateSendProcessDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._messageService.getAll(undefined, undefined, undefined, undefined, undefined)
    .subscribe((result: MessageDtoPagedResultDto) => {
      this.messages = result.items;
    });
  }

  onChangeDate(event: MatDatepickerInputEvent<Date>) {
    this.sendProcess.scheduleDate = moment(event.value);
  }

  save(): void {
    this.saving = true;

    const sendProcess = new CreateSendProcessInput();
    sendProcess.init(this.sendProcess);

    this._sendProcessService
      .create(sendProcess)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
