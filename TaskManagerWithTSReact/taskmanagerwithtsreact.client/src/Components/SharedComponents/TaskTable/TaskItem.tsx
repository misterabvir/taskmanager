import { Component } from 'react';
import { TaskViewModel } from "../../../ViewModels/TaskViewModel";
import EditableTextAreaComponent from '../EditableTextAreaComponent';
import TaskItemAction from './TaskItemAction';
import { Put } from '../../RequestsToServer/Requests';
import { PAGE_PROJECT_DETAIL, PAGE_TASK_DETAIL, TASK_UPDATE_DESCRIPTION } from '../../../Utils/Const';

type TaskTableItemsProps = {
  task: TaskViewModel,
  index: number,
  reload: () => void
}

export default class TaskItem extends Component<TaskTableItemsProps>{
  constructor(props: TaskTableItemsProps) {
    super(props)
  }

  async handleTaskDescriptionChanged(taskDescription: string, taskId: string) {
    await Put(TASK_UPDATE_DESCRIPTION, { TaskId: taskId, Description: taskDescription });
  }

  render() {
    const task = this.props.task;
    const index = this.props.index + 1;
    return (
      <tr>
        <td>{index}</td>
        <td className='small'>
          <span className='small'>
            Project: <cite> <a className='text-dark badge-pill' href={`${PAGE_PROJECT_DETAIL}/${task.projectId}`}>{task.projectName}</a></cite>
          </span>
          <br />
          <span className='small'>
            In work: <cite> {task.inWork} </cite>
          </span>
          <br />
          <span className='small'>
            Updated:  <cite> {task.updateDateFormat} </cite>
          </span>
        </td>
        <td>
          <div className="card">
            <div className="card-header ">
              <div className="row">
                <div className="col h4 d-flex">
                  <a className='btn-link' href={`${PAGE_TASK_DETAIL}/${task.taskId}`}>{task.taskName}</a>
                </div>
                <div className='col-auto'>{task.state}</div>
              </div>
            </div>
            <div className="card-body">
              <div className="row">
                <EditableTextAreaComponent
                  data={task.description}
                  save={(value: string) => this.handleTaskDescriptionChanged(value, task.taskId)} />
              </div>
            </div>
            <div className="card-footer text-muted d-flex">
              <div className="col">
                <TaskItemAction
                  status={task.state}
                  reload={this.props.reload}
                  taskId={task.taskId} />
              </div>
              <div className="col-auto mt-1">{task.inWork}</div>
            </div>
          </div>
        </td>
      </tr>
    );
  }
}