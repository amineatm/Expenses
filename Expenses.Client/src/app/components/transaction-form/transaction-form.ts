import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-transaction-form',
  imports: [ ReactiveFormsModule],
  templateUrl: './transaction-form.html',
  styleUrl: './transaction-form.css',
})
export class TransactionForm implements OnInit {
  transactionForm: FormGroup;

  incomeCategories = [
    'Salary', 'Freelance', 'Investment'
  ];

  ExpensesCategories = [
    'Food', 'Transaportation', 'Entertainment'
  ];

  availableCategories: string[] = [];

  constructor(private fb: FormBuilder) {
    this.transactionForm = this.fb.group({
      type: ['Expenses', Validators.required],
      category: ['', Validators.required],
      amount: ['', [Validators.min(0)]],
      createdAt: [new Date(), Validators.required],
    })
  }
  ngOnInit(): void {
    const type = this.transactionForm.get('type')?.value;
    this.availableCategories = type === 'Expenses' ? this.ExpensesCategories : this.incomeCategories;
    this.transactionForm.patchValue({ category: '' })
  }
  onTypeChange() {

  }

  cancel() {

  }
  OnSubmit() {

  }
}
