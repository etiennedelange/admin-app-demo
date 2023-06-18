import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatInputModule } from "@angular/material/input";
import { MatCardModule } from "@angular/material/card";
import { MatSortModule } from "@angular/material/sort";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatDialogModule } from "@angular/material/dialog";
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTabsModule, MAT_TABS_CONFIG } from '@angular/material/tabs';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSelectModule } from "@angular/material/select";
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider'
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSidenavModule } from '@angular/material/sidenav';

@NgModule({
    imports: [
        MatButtonModule,
        MatExpansionModule,
        MatTableModule,
        MatPaginatorModule,
        MatFormFieldModule,
        MatInputModule,
        MatCardModule,
        MatSortModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatDialogModule,
        MatToolbarModule,
        MatListModule,
        MatMenuModule,
        MatBadgeModule,
        MatTabsModule,
        MatAutocompleteModule,
        MatSelectModule,
        MatCheckboxModule,
        MatIconModule,
        MatDividerModule,
        MatDatepickerModule,
        MatTooltipModule,
        MatSidenavModule
    ],
    exports: [
        MatButtonModule,
        MatExpansionModule,
        MatTableModule,
        MatPaginatorModule,
        MatFormFieldModule,
        MatInputModule,
        MatCardModule,
        MatSortModule,
        MatProgressSpinnerModule,
        MatProgressBarModule,
        MatDialogModule,
        MatToolbarModule,
        MatListModule,
        MatMenuModule,
        MatBadgeModule,
        MatTabsModule,
        MatAutocompleteModule,
        MatSelectModule,
        MatCheckboxModule,
        MatIconModule,
        MatDividerModule,
        MatDatepickerModule,
        MatTooltipModule,
        MatSidenavModule
    ],
    providers: [
        { provide: MAT_TABS_CONFIG, useValue: { animationDuration: "0ms" } }
    ]
})
export class AppMaterialModule { }
