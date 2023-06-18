import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, TextOnlySnackBar } from '@angular/material/snack-bar';
import { SnackBarComponent } from 'src/app/common/snack-bar.component/snack-bar.component';

@Injectable({
    providedIn: 'root'
})
export class SnackBarService {
    constructor(private snack: MatSnackBar) { }

    public openMessage(message: string): MatSnackBarRef<TextOnlySnackBar> {
        return this.open({ message: message })
    }

    public openFromComponent(message: string): MatSnackBarRef<SnackBarComponent> {
        return this.snack.openFromComponent(SnackBarComponent, {
            duration: 5000,
            data: message
        });
    }

    public open(config: SnackConfig): MatSnackBarRef<TextOnlySnackBar> {
        let snackBarReference: MatSnackBarRef<TextOnlySnackBar> = this.snack.open(config.message, config.action ? config.actionName : '', {
            duration: config.durationMilliseconds ?? 3000,
            verticalPosition: config.position ?? 'bottom'
        });

        if (config.action && config.actionName) {
            snackBarReference
                .onAction()
                .subscribe(config.action);
        }

        return snackBarReference;
    }
}

export declare type SnackPosition = 'top' | 'bottom';

export declare class SnackConfig {
    public position?: SnackPosition;
    public durationMilliseconds?: number;
    public message: string;
    public actionName?: string;
    public action?: () => void;
}
