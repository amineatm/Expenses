import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction';
import { Transaction } from '../../models/transaction';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css',
  imports: [CommonModule]
})
export class TransactionList implements OnInit {
  constructor(private transactionService: TransactionService) { }

  transactions: Transaction[] = [];

  ngOnInit(): void {
    this.loadTransaction();
  }

  loadTransaction(): void {
    this.transactionService.getAll().subscribe(transactions => this.transactions = transactions);
  }

  getTotalIncome(): number {
    return this.transactions.filter(t => t.type === 'Income').reduce((sum, t) => sum + t.amount, 0);
  }

  getTotalExpenses(): number {
    return this.transactions.filter(t => t.type === 'Expenses').reduce((sum, t) => sum + t.amount, 0);
  }

  getNetBalance(): number {
    return this.getTotalIncome() - this.getTotalExpenses();
  }

}
