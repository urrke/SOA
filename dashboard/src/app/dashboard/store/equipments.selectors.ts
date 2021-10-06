import { createFeatureSelector, createSelector } from '@ngrx/store';
import { EquipmentsState } from './equipments.reducer';

const getEquipmentFeatureState = createFeatureSelector<EquipmentsState>('equipments');
export const getIDs = createSelector(
    getEquipmentFeatureState,
    state => state.ids
);

export const getErrorMessage = createSelector(
    getEquipmentFeatureState,
    state=> state.errorMessage
)

export const getLoadingFlag = createSelector(
    getEquipmentFeatureState,
    state => state.loading
)

export const getTmpEquipmentInfo = createSelector(
    getEquipmentFeatureState,
    state => state.currentEquipmentInfo
)

export const getSelectedEquipment = createSelector(
    getEquipmentFeatureState,
    getTmpEquipmentInfo,
    (state, currentEquipment) => state.equipments.find(e=>e.equipmentID == currentEquipment.equipmentID)
)

export const getSelectedEquipmentMetaInf = createSelector(
    getEquipmentFeatureState,
    getTmpEquipmentInfo,
    (state, currentEquipment) => state.metaInf.find(i=>i.equipmentID == currentEquipment.equipmentID)
)
