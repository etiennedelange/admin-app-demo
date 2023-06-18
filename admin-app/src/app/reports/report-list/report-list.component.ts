import { Component, OnInit } from '@angular/core';
import * as dayjs from 'dayjs';
import { SnackBarService } from 'src/app/shared/services/snack-bar.service';
import { Report, ReportService } from '../report.service';
import * as FileSaver from 'file-saver';

@Component({
    templateUrl: './report-list.component.html',
    styleUrls: ['./report-list.component.less']
})
export class ReportListComponent implements OnInit {
    public reports: Report[];

    constructor(
        private reportService: ReportService,
        private snack: SnackBarService
    ) { }

    ngOnInit(): void {
        this.reports = [{
            displayName: "Firm Activation Summary",
            name: "activationsummary",
        },
        {
            displayName: "Active User",
            name: "activeuser",
        }];
    }

    public downloadReport(report: Report) {
        this.reportService.download(report.name)
            .subscribe({
                next: (res) => {
                    let currentDate: string = dayjs().format('YYYY-MM-DD');
                    let fileName = `${report.displayName.replace(/[\s]/g, '')}Report-${currentDate}.csv`;

                    FileSaver.saveAs(res, fileName);
                },
                error: (error) => {
                    this.snack.open({ message: error.error });
                },
            });
    }
}