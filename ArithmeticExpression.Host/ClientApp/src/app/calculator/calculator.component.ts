import { Component, OnInit } from '@angular/core';
import { CalculatorService } from '../services/calculator.service';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit {
  expression: string | undefined;
  allowedSymbols: string[] | undefined;
  lastChar: string | undefined;
  lastCharAllowedSymbol: string | undefined;
  lastExpression: string | undefined;
  result: number = 0;
  history: string[] = [];

  constructor(private calculatorService: CalculatorService) { }

  ngOnInit(): void {
    this.allowedSymbols = ['+', '-', '*', '/', '.'];
  }

  clear() {
    this.expression = '';
  }

  addChar($event: any) {
    if(this.result !== 0){
      this.result = 0;
    }

    const char = $event.target.value;
    const isCharAllowedSymbol = this.allowedSymbols?.includes(char);
    if (this.expression === undefined && isCharAllowedSymbol) {
      return;
    }

    if (this.lastChar !== undefined && this.allowedSymbols?.includes(this.lastChar) && isCharAllowedSymbol) {
      return;
    }

    if (this.lastCharAllowedSymbol !== undefined && this.lastCharAllowedSymbol === '.' && char == '.') {
      return;
    }

    if (isCharAllowedSymbol) {
      this.lastCharAllowedSymbol = char;
    }

    this.lastChar = char;
    this.expression = (this.expression || '') + $event.target.value;
  }

  async evaluate() {
    if (this.expression && this.expression !== this.lastExpression ) {
      try {
        const response = await this.calculatorService.Calc(this.expression);
        console.log(response);
        if (!response.isSuccess) {
          console.error(response.error);
          alert(response.error);
          return;
        }

        this.result = response.result
        this.lastExpression = this.expression;
        this.history?.push(this.expression + ' = ' + response.result);
        this.clear();
      }
      catch (err: any) {
        console.error(err);
        alert('error');
      }
    }
  }
}
