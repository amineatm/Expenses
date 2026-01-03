import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TransactionService } from '../../services/transaction';
import { Transaction } from '../../models/transaction';

@Component({
  selector: 'app-transaction-form',
  imports: [ReactiveFormsModule],
  templateUrl: './transaction-form.html',
  styleUrl: './transaction-form.css',
})
export class TransactionForm implements OnInit {
  transactionForm: FormGroup;

  incomeCategories = [
    'Salary', 'Freelance', 'Investment'
  ];

  expensesCategories = [
    'Food', 'Transaportation', 'Entertainment'
  ];

  availableCategories: string[] = [];

  editMode = false;
  transactionId?: number;

  constructor(private fb: FormBuilder,
    private router: Router,
    private transactionService: TransactionService,
    private activatedRoute: ActivatedRoute
  ) {
    this.transactionForm = this.fb.group({
      type: ['Expense', Validators.required],
      category: ['', Validators.required],
      amount: ['', [Validators.min(0)]],
    })
  }
  ngOnInit(): void {
    const type = this.transactionForm.get('type')?.value;
    this.updateAvailableCategories(type, false);
    const id = this.activatedRoute.snapshot.paramMap.get("id");
    if (id) {
      this.editMode = true;
      this.transactionId = +id;
      this.loadtransaction(this.transactionId);
    }
  }
  loadtransaction(id: number) {
    this.transactionService.getById(id).subscribe({
      next: (transaction) => {
        console.log('loadtransaction', transaction);
        this.updateAvailableCategories(transaction.type, false);

        this.transactionForm.patchValue({
          type: transaction.type,
          amount: transaction.amount,
          category: transaction.category
        });
      },
      error: (error) => {
        console.log("error :", error);
      }
    });
  }
  onTypeChange() {
    const type = this.transactionForm.get('type')?.value;
    this.updateAvailableCategories(type, true);
  }

  updateAvailableCategories(type: string, resetCategory: boolean) {
    this.availableCategories = type === 'Expense' ? this.expensesCategories : this.incomeCategories;

    if (resetCategory) {
      this.transactionForm.patchValue({ category: '' });
    }
  }

  cancel() {
    this.router.navigate(['/transactions']);
  }

  OnSubmit() {
    if (this.transactionForm.valid) {
      const transaction = this.transactionForm.value;
      console.log(transaction);

      if (this.editMode && this.transactionId) {
        this.transactionService.update(this.transactionId, transaction).subscribe({
          next: () => {
            this.router.navigate(['/transactions']);
          },
          error: (error) => {
            console.log('Error:', error);
          }
        });
      }
      else {
        this.transactionService.create(transaction).subscribe({
          next: () => {
            this.router.navigate(['/transactions']);
          },
          error: (error) => {
            console.log('Error:', error);
          }
        });
      }
    }
  }
}
