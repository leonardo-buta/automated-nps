import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  SendProcessReportDto,
  SendProcessReportServiceProxy,
  SendProcessReportDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';

class PagedCampaignsRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  templateUrl: './send-process-reports.component.html',
  animations: [appModuleAnimation()]
})
export class SendProcessReportsComponent extends PagedListingComponentBase<SendProcessReportDto> {
  sendProcessReports: SendProcessReportDto[] = [];
  keyword = '';

  constructor(
    injector: Injector,
    private _sendProcessReportsService: SendProcessReportServiceProxy
  ) {
    super(injector);
  }

  list(
    request: PagedCampaignsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {

    request.keyword = this.keyword;

    this._sendProcessReportsService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: SendProcessReportDtoPagedResultDto) => {
        this.sendProcessReports = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(sendProcessReport: SendProcessReportDto): void {
    
  }
}
