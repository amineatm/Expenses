import { Component } from '@angular/core';
import { Transaction } from '../../models/transaction';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.html',
  styleUrl: './transaction-list.css',
  imports:[CommonModule]
})
export class TransactionList {
  transactions: Transaction[] = [
    {
      id: 1,
      type:'Expenses' ,
      category: 'Food',
      amount: 120,
      createdAt: new Date(),
      updatedAt: new Date()
    }
  ];
}
