import { Injectable } from '@angular/core';
import { Actions, createEffect, Effect, ofType } from '@ngrx/effects';

//import * as EquipmentActions from './dashboard.actions';

import { Store } from '@ngrx/store';
import { DashboardService } from '../dashboard.service';
import { DashboardState } from './equipments.reducer'


@Injectable()
export class DashboardEffects {

    constructor(
        private actions$: Actions,
        private store: Store<DashboardState>,
        private dashboardService: DashboardService
      ) { }

    
}



