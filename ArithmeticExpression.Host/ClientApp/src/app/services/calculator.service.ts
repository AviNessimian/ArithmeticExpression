import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';
import { CalculationResponse } from './dtos';

@Injectable({
    providedIn: 'root'
})
export class CalculatorService {
    private apiURL = 'http://localhost:5191/api';
    private httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        }),
    };

    constructor(private http: HttpClient) { }

    Calc(expression: string): Promise<CalculationResponse> {
        console.log('posting expression', expression);
        var $observable = this.http
            .post<CalculationResponse>(
                this.apiURL + '/calculator',
                JSON.stringify(expression),
                this.httpOptions
            );

        const $promise = lastValueFrom($observable);
        return $promise;
    }
}

