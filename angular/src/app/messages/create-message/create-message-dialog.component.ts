import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CreateMessageInput,
  MessageServiceProxy,
  CampaignDto,
  CampaignServiceProxy,
  CampaignDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-message-dialog.component.html'
})
export class CreateMessageDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  campaigns: CampaignDto[] = [];

  constructor(
    injector: Injector,
    private _messageService: MessageServiceProxy,
    private _campaignService: CampaignServiceProxy,
    private _dialogRef: MatDialogRef<CreateMessageDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._campaignService.getAll(undefined, undefined, undefined)
    .subscribe((result: CampaignDtoPagedResultDto) => {
      this.campaigns = result.items;
    });
  }

  save(): void {
    this.saving = true;

    const message = new CreateMessageInput();
    message.init(this.message);

    this._messageService
      .create(message)
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
