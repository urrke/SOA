export interface SensorEvent {
    eventType: EventType,
    value: number;
    timestamp: Date;
    beachName: string;
}

export enum EventType
{
    HotWaterAlert,
    ColdWaterAlert,
    TurbidWaterAlert,
    ClearWaterAlert,
    LowBatteryAlert,
    FullBatteryAlert
}