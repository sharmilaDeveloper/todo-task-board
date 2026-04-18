import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TaskItem {
  id: string;
  title: string;
  description: string;
  status: number;
  createdAt?: Date;
}

@Injectable({ providedIn: 'root' })
export class TaskService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5186/api/tasks'; 

  getTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl);
  }

  createTask(task: Partial<TaskItem>): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, task);
  }

  updateTask(id: string, task: any): Observable<any> {
  return this.http.put(`${this.apiUrl}/${id}`, { ...task, id });
}

  updateStatus(id: string, newStatus: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}/status`, { id, newStatus });
  }

  deleteTask(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  reopenTask(id: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}/reopen`, {});
  }

  
}