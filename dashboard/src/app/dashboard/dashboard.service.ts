import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SensorEvent } from "../models/sensor-event";
import { WaterConditionSensorData } from "../models/water-conditions-sensor-data";
import { WavesSensorData } from "../models/waves-sensor-data";

@Injectable({
    providedIn: 'root'
  })
  export class DashboardService {
    baseUrl = "http://localhost:50954/apigateway"
   
    constructor(private http: HttpClient) { }
  
    /*getSignals(tmpEquipmentID: string, dataPointCount: number, startDate: Date, endDate: Date) {
      return this.http.get<DataPoint[]>('/api/signal-data/' + tmpEquipmentID + '/' + dataPointCount + '/'+ moment(startDate).format() +'/'+ moment(endDate).format());
    }*/

    getWaterQualitySensorData(beachName: string): Observable<WaterConditionSensorData[]> {
        const url = this.baseUrl + `/water-conditions-sensor/${beachName}`;
        return this.http.get<WaterConditionSensorData[]>(url);
    }

    getWavesSensorData(beachName: string): Observable<WavesSensorData[]> {
        const url = this.baseUrl + `/waves-sensor/${beachName}`;
        return this.http.get<WavesSensorData[]>(url);
    }

    getAllBeachNames(): Observable<string[]> {
        const url = this.baseUrl + '/beaches';
        return this.http.get<string[]>(url);
    }

    getEvents(): Observable<SensorEvent[]> {
      const url = this.baseUrl + '/events';
      return this.http.get<SensorEvent[]>(url); 
    }


}