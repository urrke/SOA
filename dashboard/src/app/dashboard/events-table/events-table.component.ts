import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { EventType, SensorEvent } from "src/app/models/sensor-event";
import { DashboardService } from "../dashboard.service";

@Component({
    selector: 'events-table',
    templateUrl: './events-table.component.html',
    styleUrls: ['./events-table.component.scss']
  })
export class EventsTableComponent implements OnInit, OnChanges {
    @Input() dataSource: SensorEvent[];
    displayedColumns: string[] = ['eventType', 'value', 'timestamp', 'BeachName'];
    eventType = EventType;

    constructor(private dashboardService: DashboardService) {}

    ngOnChanges(changes: SimpleChanges): void {
        this.dataSource = changes.dataSource.currentValue;
    }
    
    ngOnInit(): void {
        // this.dashboardService.getEvents().subscribe(data => {
        //     this.dataSource = data;
        // })
    }
}