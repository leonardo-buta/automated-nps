import { Component, Injector, Inject, OnInit, Optional } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA
} from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import {
  PermissionDto,
  CampaignDto,
  CampaignServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-campaign-dialog.component.html'
})
export class EditCampaignDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  campaign: CampaignDto = new CampaignDto();
  permissions: PermissionDto[] = [];
  grantedPermissionNames: string[] = [];
  checkedPermissionsMap: { [key: string]: boolean } = {};

  constructor(
    injector: Injector,
    private _campaignService: CampaignServiceProxy,
    private _dialogRef: MatDialogRef<EditCampaignDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._campaignService
      .get(this._id)
      .subscribe((result: CampaignDto) => {
        this.campaign.init(result);
      });
  }

  save(): void {
    this.saving = true;

    this._campaignService
      .update(this.campaign)
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
