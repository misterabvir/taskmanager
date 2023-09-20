import { Component } from 'react';
import { TaskViewModel } from "../../../ViewModels/TaskViewModel";
import EditableFieldComponent from '../EditableFieldComponent';
import TaskItemAction from './TaskItemAction';
import { PostRequest } from '../../FetchServer/FetchServer';

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
    console.log(taskId);
    await PostRequest('/saveDescription', { TaskId: taskId, Description: taskDescription });
  }

  handleTaskCanceled = async (taskId: string) => {
    await PostRequest('/cancelTask', { TaskId: taskId });
    this.props.reload();
  }

  handleTaskStarted = async (taskId: string) => {
    await PostRequest('/startTask', { TaskId: taskId });
    this.props.reload();
  }

  render() {
    const task = this.props.task;
    const index = this.props.index + 1;
    const linkProject = "/project-detail/" + task.projectId;
    const linkTask = "/task-detail/" + task.taskId;
    return (
      <tr>
        <td>{index}</td>
        <td className='small'>
          <span className='small'>
            Project: <cite> <a className='text-dark badge-pill' href={linkProject}>{task.projectName}</a></cite>
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
                  <a className='btn-link' href={linkTask}>{task.taskName}</a>
                </div>
                <div className='col-auto'>{task.state}</div>
              </div>
            </div>
            <div className="card-body">
              <div className="row">
                <EditableFieldComponent
                  data={task.description}
                  save={(value: string) => this.handleTaskDescriptionChanged(value, task.taskId)} />
              </div>
            </div>
            <div className="card-footer text-muted d-flex">
              <div className="col">
                <TaskItemAction
                  status={task.state}
                  cancelAction={this.handleTaskCanceled}
                  startAction={this.handleTaskStarted}
                  value={task.taskId} />
              </div>
              <div className="col-auto mt-1">{task.inWork}</div>
            </div>
          </div>
        </td>
      </tr>
    );
  }
}