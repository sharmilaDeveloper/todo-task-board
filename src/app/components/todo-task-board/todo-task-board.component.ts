import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService, TaskItem } from '../../services/task.service';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'todo-task-board',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo-task-board.component.html',
  styleUrls: ['./todo-task-board.component.scss']
})
export class TaskBoardComponent implements OnInit {
  private taskService = inject(TaskService);
  private toast = inject(ToastService);
  
  tasks: TaskItem[] = [];
  showModal = false;
  isEditMode = false;
selectedTaskId: string | null = null;
  newTask = { title: '', description: '' };
  
  currentToast: { message: string, type: string } | null = null;

  columns = [
    { label: 'To Do', value: 0, color: '#94a3b8' },
    { label: 'In Progress', value: 1, color: '#3b82f6' },
    { label: 'Completed', value: 2, color: '#10b981' }
  ];

  ngOnInit() { 
    this.loadTasks();
    this.toast.toastEvents.subscribe(t => {
      this.currentToast = t;
      setTimeout(() => this.currentToast = null, 3000);
    });
  }

  loadTasks() {
    this.taskService.getTasks().subscribe({
      next: (data) => this.tasks = data,
      error: () => this.toast.show('Failed to load tasks', 'error')
    });
  }

  getTasksByStatus(status: any) {
    return this.tasks.filter(t => t.status === status);
  }

openCreateModal() {
  this.isEditMode = false;
  this.selectedTaskId = null;
  this.newTask = { title: '', description: '' };
  this.showModal = true;
}

  addTask() {
    if (!this.newTask.title.trim()) {
      this.toast.show('Title is required', 'error');
      return;
    }

    this.taskService.createTask({ 
      title: this.newTask.title, 
      description: this.newTask.description, 
      status: 0 
    }).subscribe({
      next: () => {
        this.toast.show('Task created successfully!');
        this.showModal = false;
        this.loadTasks();
      },
      error: () => this.toast.show('Error creating task', 'error')
    });
  }

  delete(id: string) {
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(id).subscribe({
        next: () => {
          this.toast.show('Task deleted');
          this.loadTasks();
        },
        error: () => this.toast.show('Delete failed', 'error')
      });
    }
  }

  changeStatus(id: string, status: number) {
    this.taskService.updateStatus(id, status).subscribe(() => this.loadTasks());
  }

  reopen(id: string) {
    this.taskService.reopenTask(id).subscribe(() => this.loadTasks());
  }

  openEditModal(task: TaskItem) {
  this.isEditMode = true;
  this.selectedTaskId = task.id;
  this.showModal = true;
  
  this.newTask = {
    title: task.title,
    description: task.description
  };
}


handleSave() {
  if (this.isEditMode && this.selectedTaskId) {
    this.taskService.updateTask(this.selectedTaskId, this.newTask).subscribe({
      next: () => {
        this.closeModal();
        this.loadTasks();
      },
      error: (err) => console.error(err)
    });
  } else {
    this.addTask(); 
  }
}

closeModal() {
  this.showModal = false;
  this.isEditMode = false;
  this.newTask = { title: '', description: '' };
}
  
}