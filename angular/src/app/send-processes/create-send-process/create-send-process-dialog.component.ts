import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  PermissionDto,
  SendProcessDto,
  SendProcessServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-send-process-dialog.component.html',
  styleUrls: ['./create-send-process-dialog.component.css']
})
export class CreateSendProcessDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  sendProcess: SendProcessDto = new SendProcessDto();
  permissions: PermissionDto[] = [];
  grantedPermissionNames: string[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  constructor(
    injector: Injector,
    private _sendProcessService: SendProcessServiceProxy,
    private _dialogRef: MatDialogRef<CreateSendProcessDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {
    
  }

  save(): void {
    this.saving = true;
    const sendProcess = new SendProcessDto();
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
