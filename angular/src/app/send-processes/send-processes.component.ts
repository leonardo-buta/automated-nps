import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  SendProcessDto,
  SendProcessServiceProxy,
  SendProcessDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateSendProcessDialogComponent } from './create-send-process/create-send-process-dialog.component';
import { EditSendProcessDialogComponent } from './edit-send-process/edit-send-process-dialog.component';

class PagedSendProcessesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './send-processes.component.html',
  styleUrls: ['./send-processes.component.css'],
  animations: [appModuleAnimation()]
})
export class SendProcessesComponent extends PagedListingComponentBase<SendProcessDto> {
  sendProcesses: SendProcessDto[] = [];
  keyword = '';

  constructor(
    injector: Injector,
    private _sendProcessService: SendProcessServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  list(
    request: PagedSendProcessesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {

    request.keyword = this.keyword;

    this._sendProcessService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: SendProcessDtoPagedResultDto) => {
        this.sendProcesses = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(sendProcess: SendProcessDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', sendProcess.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._sendProcessService
            .delete(sendProcess.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => { });
        }
      }
    );
  }

  createSendProcess(): void {
    this.showCreateOrEditSendProcessDialog();
  }

  editProcess(sendProcess: SendProcessDto): void {
    this.showCreateOrEditSendProcessDialog(sendProcess.id);
  }

  showCreateOrEditSendProcessDialog(id?: number): void {
    let createOrEditSendProcessDialog;
    if (id === undefined || id <= 0) {
      createOrEditSendProcessDialog = this._dialog.open(CreateSendProcessDialogComponent);
    } else {
      createOrEditSendProcessDialog = this._dialog.open(EditSendProcessDialogComponent, {
        data: id
      });
    }

    createOrEditSendProcessDialog.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
}
