import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { MessagesComponent } from './messages/messages.component';
import { SendProcessesComponent } from './send-processes/send-processes.component';
import { SendProcessReportsComponent } from './send-process-reports/send-process-reports.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'campaigns', component: CampaignsComponent, canActivate: [AppRouteGuard] },
                    { path: 'messages', component: MessagesComponent, canActivate: [AppRouteGuard] },
                    { path: 'send-processes', component: SendProcessesComponent, canActivate: [AppRouteGuard] },
                    { path: 'send-process-reports', component: SendProcessReportsComponent, canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    // { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    // { path: 'about', component: AboutComponent },
                    { path: 'update-password', component: ChangePasswordComponent }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
