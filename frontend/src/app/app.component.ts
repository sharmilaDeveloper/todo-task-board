import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TaskBoardComponent } from './components/todo-task-board/todo-task-board.component';

@Component({
  selector: 'app-root',
  imports: [TaskBoardComponent],
  template: '<todo-task-board></todo-task-board>'
})
export class AppComponent {
  title = 'frontend';
}
