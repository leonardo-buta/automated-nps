import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  CampaignDto,
  CampaignServiceProxy,
  CampaignDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateCampaignDialogComponent } from './create-campaign/create-campaign-dialog.component';
import { EditCampaignDialogComponent } from './edit-campaign/edit-campaign-dialog.component';

class PagedCampaignsRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './campaigns.component.html',
  animations: [appModuleAnimation()]
})
export class CampaignsComponent extends PagedListingComponentBase<CampaignDto> {
  campaigns: CampaignDto[] = [];
  keyword = '';

  constructor(
    injector: Injector,
    private _campaignsService: CampaignServiceProxy,
    private _dialog: MatDialog
  ) {
    super(injector);
  }

  list(
    request: PagedCampaignsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {

    request.keyword = this.keyword;

    this._campaignsService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: CampaignDtoPagedResultDto) => {
        this.campaigns = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(campaign: CampaignDto): void {
    abp.message.confirm(
      this.l('RoleDeleteWarningMessage', campaign.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._campaignsService
            .delete(campaign.id)
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

  createCampaign(): void {
    this.showCreateOrEditCampaignDialog();
  }

  editCampaign(campaign: CampaignDto): void {
    this.showCreateOrEditCampaignDialog(campaign.id);
  }

  showCreateOrEditCampaignDialog(id?: number): void {
    let createOrEditCampaignDialog;
    if (id === undefined || id <= 0) {
      createOrEditCampaignDialog = this._dialog.open(CreateCampaignDialogComponent);
    } else {
      createOrEditCampaignDialog = this._dialog.open(EditCampaignDialogComponent, {
        data: id
      });
    }

    createOrEditCampaignDialog.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
}
