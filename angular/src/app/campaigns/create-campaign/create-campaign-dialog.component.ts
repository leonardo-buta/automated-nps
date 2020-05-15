import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  PermissionDto,
  CampaignDto,
  CampaignServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-campaign-dialog.component.html',
  styleUrls: ['./create-campaign-dialog.component.css']
})
export class CreateCampaignDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  campaign: CampaignDto = new CampaignDto();
  permissions: PermissionDto[] = [];
  grantedPermissionNames: string[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;

  constructor(
    injector: Injector,
    private _campaignService: CampaignServiceProxy,
    private _dialogRef: MatDialogRef<CreateCampaignDialogComponent>
  ) {
    super(injector);
  }

  ngOnInit(): void {
    
  }

  save(): void {
    this.saving = true;
    const campaign = new CampaignDto();
    campaign.init(this.campaign);

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
