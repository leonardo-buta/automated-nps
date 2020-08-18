import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatDatepickerInputEvent } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  PermissionDto,
  CampaignDto,
  CampaignServiceProxy
} from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';

@Component({
  templateUrl: 'create-campaign-dialog.component.html',
  styleUrls: ['./create-campaign-dialog.component.css']
})
export class CreateCampaignDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  campaign: CampaignDto = new CampaignDto();
  inputStartDate: Date = new Date();

  constructor(
    injector: Injector,
    private _campaignService: CampaignServiceProxy,
    private _dialogRef: MatDialogRef<CreateCampaignDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.campaign.startDate = moment(this.inputStartDate).startOf('day').utc(false);
  }

  onChangeDate(event: MatDatepickerInputEvent<Date>) {
    let momentDate = moment(event.value).startOf('day').utc(false);

    if (event.targetElement.id === 'startDate') {
      this.campaign.startDate = momentDate;
    } else {
      this.campaign.endDate = momentDate;
    }
  }

  save(): void {
    this.saving = true;
    const campaign = new CampaignDto();
    campaign.init(this.campaign);
    campaign.active = true;

    this._campaignService
      .create(campaign)
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
