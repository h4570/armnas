import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CronComponent } from './components/pages/cron/cron.component';
import { HomeComponent } from './components/pages/home/home.component';
import { SambaComponent } from './components/pages/samba/samba.component';
import { TransmissionComponent } from './components/pages/transmission/transmission.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'samba', component: SambaComponent },
  { path: 'cron', component: CronComponent },
  { path: 'transmission', component: TransmissionComponent },
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
