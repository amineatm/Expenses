import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Header } from "./header/header";
import { Footer } from "./footer/footer";
import { TransactionForm } from "./transaction-form/transaction-form";
import { TransactionList } from "./transaction-list/transaction-list";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Footer, TransactionForm, TransactionList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Expenses.Client');
}
