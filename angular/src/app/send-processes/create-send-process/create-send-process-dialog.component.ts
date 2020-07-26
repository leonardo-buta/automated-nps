import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatDatepickerInputEvent } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import * as moment from 'moment';
import {
  SendProcessServiceProxy,
  CreateSendProcessInput,
  MessageDto,
  MessageServiceProxy,
  MessageDtoPagedResultDto,
  SendProcessDto
} from '@shared/service-proxies/service-proxies';
import { AppConsts } from '@shared/AppConsts';

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
  fileToUpload: File = null;

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

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  save(): void {
    abp.ui.setBusy();

    this.saving = true;
    const sendProcess = new CreateSendProcessInput();
    sendProcess.init(this.sendProcess);    

    this._sendProcessService
      .create(sendProcess)
      .subscribe((result: SendProcessDto) => {
        const form: FormData = new FormData();
        form.append('sendProcessId', result.id.toString());
        form.append('separator', this.sendProcess.separator);
        form.append('formFile', this.fileToUpload, this.fileToUpload.name);

        abp.ajax({
          url: `${AppConsts.remoteServiceBaseUrl}/Upload/Mailing`,
          data: form,
          contentType: false,
          processData: false
        }).done(() => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close(true);
          this.saving = false;
          abp.ui.clearBusy();
        });
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
