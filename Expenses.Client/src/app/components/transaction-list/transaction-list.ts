import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionService } from '../../services/transaction';
import { Transaction } from '../../models/transaction';
import { Router } from '@angular/router';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css',
  imports: [CommonModule]
})
export class TransactionList implements OnInit {
  constructor(private transactionService: TransactionService, private router: Router) { }

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
    return this.transactions.filter(t => t.type === 'Expense').reduce((sum, t) => sum + t.amount, 0);
  }

  getNetBalance(): number {
    return this.getTotalIncome() - this.getTotalExpenses();
  }

  editTransaction(transaction: Transaction) {
    console.log('edit transaction - ', transaction);

    if (transaction.id) {
      this.router.navigate(['/edit/', transaction.id])
    }
  }
  deleteTransaction(transaction: Transaction) {
    console.log('delete transaction - ', transaction);
    if(confirm('Are you sure you want to delete this transaction?')) {
      this.transactionService.delete(transaction.id).subscribe({
      next: () => {
        this.loadTransaction();
      },
      error: (error) => {
        console.log('Error:', error);
      }
    });
    }
    
  }
}
