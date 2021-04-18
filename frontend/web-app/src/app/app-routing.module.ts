import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth-guard';
import { CronComponent } from './components/pages/cron/cron.component';
import { HomeComponent } from './components/pages/home/home.component';
import { LoginComponent } from './components/pages/login/login.component';
import { SambaComponent } from './components/pages/samba/samba.component';
import { TransmissionComponent } from './components/pages/transmission/transmission.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'samba', component: SambaComponent, canActivate: [AuthGuard] },
  { path: 'cron', component: CronComponent, canActivate: [AuthGuard] },
  { path: 'transmission', component: TransmissionComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
