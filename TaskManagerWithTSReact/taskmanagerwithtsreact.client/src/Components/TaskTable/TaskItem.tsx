import { Component } from 'react';
import { TaskViewModel } from "../../Interfaces/TaskViewModel";
import EditableFieldComponent from '../Reusable/EditableFieldComponent';
import TaskItemAction from './TaskItemAction';

type TaskTableItemsProps = {
  task: TaskViewModel,
  index: number,
  reload: () => void
}

export default class TaskItem extends Component<TaskTableItemsProps>{
  constructor(props: TaskTableItemsProps) {
    super(props)
  }

  saveTaskName(taskId: string, taskName: string) {
    this.updateTaskName(taskName, taskId);
  }

  cancelAction = (value: string) => {
    this.actionTask(value, 'cancelTask')
  }

  startAction = (value: string) => {
    this.actionTask(value, 'startTask')
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
          <div className="row h-100">
            <div className="col-auto bg-secondary rounded-start p-0">
              <a className='btn btn-primary h-100 align-middle' href={linkTask} >Detail:</a>
            </div>
            <div className="col border">
              <EditableFieldComponent
                data={task.taskName}
                save={(value: string) => this.saveTaskName(value, task.taskId)} />
            </div>
            <div className="col-auto bg-secondary rounded-end p-0">
              <TaskItemAction
                status={task.state}
                cancelAction={this.cancelAction}
                startAction={this.startAction}
                value={task.taskId} />
            </div>
          </div>
        </td>
        <td>          
          <span className="badge bg-secondary mt-2">{task.state}</span>
        </td>
      </tr>
    );
  }

  updateTaskName = async (taskId: string, taskName: string) => {
    try {
      await fetch('saveTaskName', {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify({
          Id: taskId,
          Name: taskName
        })
      });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  }

  actionTask = async (taskId: string, url: string) => {
    try {
      await fetch(url, {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify({
          Id: taskId
        })
      });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    this.props.reload();
  }
}