import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { SensorEvent } from "../models/sensor-event";
import { WaterConditionSensorData } from "../models/water-conditions-sensor-data";
import { WavesSensorData } from "../models/waves-sensor-data";
import * as signalR from "@microsoft/signalr";

@Injectable({
    providedIn: 'root'
  })
  export class SignalRService {
    baseUrl = "http://localhost:50954/apigateway"
    hubConnection: signalR.HubConnection
  
    constructor(private http: HttpClient) { }
  
    
    public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('http://localhost:50954/beach-water-hub')
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  /*public addTransferChartDataListener = () => {
    this.hubConnection.on('new_event', (data) => {

      console.log(data);
    });
  }*/ 
    
}