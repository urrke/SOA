import { Component, OnInit } from '@angular/core';
import { combineLatest } from 'rxjs';
import { WaterConditionSensorData } from '../models/water-conditions-sensor-data';
import { WavesSensorData } from '../models/waves-sensor-data';
import { DashboardService } from './dashboard.service';
import { first } from 'rxjs/operators';
import * as Highcharts from 'highcharts';
import more from 'highcharts/highcharts-more';
import { SignalRService } from './signalr.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EventType, SensorEvent } from '../models/sensor-event';
more(Highcharts);

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    allBeaches: string[];
    CurrentBeach: string = null;
    tempSignal: number[][] = [];
    turbiditySignal: number[][] = [];
    waveHeightSignal: number[][] = [];
    wavePeriodSignal: number[][] = [];
    batteryLifeSignal: number[][] = [];
    allEvents: SensorEvent[];

    constructor(private dashboardService: DashboardService, 
                private signalRService: SignalRService,
                private snackBar: MatSnackBar) {}

    ngOnInit(): void {
        this.dashboardService.getAllBeachNames().subscribe(data => {
            this.allBeaches = data
        });

        this.dashboardService.getEvents().subscribe(data => {
          this.allEvents = data;
        });

        this.signalRService.startConnection();
        this.signalRService.hubConnection.on('new_event', (data) => {
          const {eventType, beachName, timestamp} = data as SensorEvent;
          const message = `Event: ${EventType[eventType]}, Beach: ${beachName}`;
          this.snackBar.open(message, 'Ok', {duration: 2000});
          this.allEvents = [data, ...this.allEvents];
        });
    }

    onBeachSelected(beachName: string): void {
        this.CurrentBeach = beachName;
        const waterQualitySensorData$ = this.dashboardService.getWaterQualitySensorData(beachName);
        const wavesSensorData$ = this.dashboardService.getWavesSensorData(beachName);

        combineLatest(waterQualitySensorData$, wavesSensorData$).pipe(first()).subscribe(
            ([data1, data2]) => {
                this.parseWaterQualitySensorDataToSignal(data1);
                this.parseWavesSensorDataToSignal(data2);

                let options = this.getOptions("Temperature", this.tempSignal, "C");
                Highcharts.chart("chart-container1", options);
                options = this.getOptions("Turbidity", this.turbiditySignal, "");
                Highcharts.chart("chart-container2", options);
                options = this.getOptions("Wave Height", this.waveHeightSignal, "cm");
                Highcharts.chart("chart-container3", options);
                options = this.getOptions("Wave Period", this.wavePeriodSignal, "hz");
                Highcharts.chart("chart-container4", options);
                options = this.getOptions("Battery Life", this.batteryLifeSignal, "%");
                Highcharts.chart("chart-container5", options);
            }
        )
    }
  
    private parseWaterQualitySensorDataToSignal(data: WaterConditionSensorData[]): void {
      this.tempSignal = [];
      this.turbiditySignal = [];
        data.forEach(val => {
            this.tempSignal.push([new Date(val.Timestamp).getTime(), val.Temperature]);
            this.turbiditySignal.push([new Date(val.Timestamp).getTime(), val.Turbidity]);
        })
    }

    private parseWavesSensorDataToSignal(data: WavesSensorData[]): void {
      this.waveHeightSignal = [];
      this.wavePeriodSignal = [];
      this.batteryLifeSignal = []; 
        data.forEach(val => {
            this.waveHeightSignal.push([new Date(val.Timestamp).getTime(), val.WaveHeight]);
            this.wavePeriodSignal.push([new Date(val.Timestamp).getTime(), val.WavePeriod]);
            this.batteryLifeSignal.push([new Date(val.Timestamp).getTime(), val.BatteryLife]);
        })
    }

    private getOptions(title: string, data: number[][], uom: string): any {
        let options: any = {
            chart: {
              type: 'spline',
              height: 500,
              width: 700
            },
            title: {
              text: title
            },
            credits: {
              enabled: false
            },
            tooltip: {
              formatter: function() {
                return 'x: ' + Highcharts.dateFormat('%e %b %y %H:%M:%S', this.x) + ' y: ' + this.y.toFixed(2);
              }
            },
            xAxis: {
              type: 'datetime',
              gridLineWidth: 0,
              labels: {
                formatter: function() {
                  return Highcharts.dateFormat('%e %b %H:%M', this.value);
                }
              }
            },
            series: [
              {
                name: uom,
                data: data
              },
            ]
          }
        return options;
    }
}

