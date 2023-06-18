import { Component, Inject, Input, OnInit } from "@angular/core";
import { MAT_SNACK_BAR_DATA } from '@angular/material/snack-bar';

@Component({
    template: `{{data}}`,
})
export class SnackBarComponent {
    constructor(@Inject(MAT_SNACK_BAR_DATA) public data: string) {
    }
}