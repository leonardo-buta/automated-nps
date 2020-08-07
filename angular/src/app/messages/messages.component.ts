import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  MessageDto,
  MessageDtoPagedResultDto,
  MessageServiceProxy
} from '@shared/service-proxies/service-proxies';
import { CreateMessageDialogComponent } from './create-message/create-message-dialog.component';
import { EditMessageDialogComponent } from './edit-message/edit-message-dialog.component';

class PagedMessagesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
  animations: [appModuleAnimation()]
})
export class MessagesComponent extends PagedListingComponentBase<MessageDto> {
  messages: MessageDto[] = [];

  keyword = '';

  constructor(
    injector: Injector,
    private _messagesService: MessageServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  list(
    request: PagedMessagesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {

    request.keyword = this.keyword;

    this._messagesService
      .getAll('', '', '', request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: MessageDtoPagedResultDto) => {
        this.messages = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(message: MessageDto): void {
    abp.message.confirm(
      this.l('MessageDeleteWarningMessage', message.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._messagesService
            .delete(message.id)
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

  createMessage(): void {
    this.showCreateOrEditMessageDialog();
  }

  editMessage(message: MessageDto): void {
    this.showCreateOrEditMessageDialog(message.id);
  }

  showCreateOrEditMessageDialog(id?: number): void {
    let createOrEditMessageDialog;
    if (id === undefined || id <= 0) {
      createOrEditMessageDialog = this._dialog.open(CreateMessageDialogComponent);
    } else {
      createOrEditMessageDialog = this._dialog.open(EditMessageDialogComponent, {
        data: id
      });
    }

    createOrEditMessageDialog.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
}
