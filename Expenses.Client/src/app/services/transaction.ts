import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Transaction {
  private apiUrl = environment.apiHost + '/transactions';
  /**
   *
   */
  constructor(private http: HttpClient) { }

  getAll(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(this.apiUrl + '/GetAll');
  }

  getById(TransactionId: number): Observable<Transaction> {
    return this.http.get<Transaction>(this.apiUrl + '/Details/' + TransactionId);
  }

  create(transaction: Transaction): Observable<Transaction> {
    return this.http.post<Transaction>(this.apiUrl + '/Create', transaction);
  }

  update(TransactionId: number, transaction: Transaction): Observable<Transaction> {
    return this.http.put<Transaction>(this.apiUrl + '/Update/' + TransactionId, transaction);
  }

  delete(id: number): Observable<Transaction> {
    return this.http.delete<Transaction>(this.apiUrl + '/Delete/' + id);
  }
}
