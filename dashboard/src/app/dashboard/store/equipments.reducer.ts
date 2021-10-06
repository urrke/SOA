import { createReducer } from "@ngrx/store";



export interface DashboardState {
    CurrentBeach: string;
    tempSignal: number[][];
    turbiditySignal: number[][];
    waveHeightSignal: number[][];
    wavePeriodSignal: number[][];
    batteryLifeSignal: number[][];
    /*ids: string[];
    equipments: Equipment[];
    errorMessage: string;
    metaInf: {equipmentID: string, info: SignalInfo[]}[]
    loading: boolean;
    currentEquipmentInfo: {equipmentID: string, startDate: Date, endDate: Date};*/
}

const initialState: DashboardState = {
    CurrentBeach: '',
    tempSignal: [],
    turbiditySignal: [],
    waveHeightSignal: [],
    wavePeriodSignal: [],
    batteryLifeSignal: [],
    /*ids: [],
    equipments: [],
    errorMessage: null,
    metaInf: [],
    loading: false,
    currentEquipmentInfo: null*/
}

export const dashboardReducer = createReducer<DashboardState>(
    initialState,
    /*on(EquipmentActions.SetEquipmentSignalsData, (state, action): EquipmentsState => {
        const newNode: Equipment = {
            equipmentID: state.currentEquipmentInfo.equipmentID,
            signals: action.signals
        };
        return { ...state, equipments: [...state.equipments, newNode] }
    }),*/


);
