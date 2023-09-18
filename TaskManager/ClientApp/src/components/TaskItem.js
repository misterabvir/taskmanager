import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import TaskAction from './TaskAction';
import EditableField from './EditableField';

export default class TaskItem extends Component {
  static displayName = TaskItem.name;

  constructor(props) {
    super(props);
    this.cancelAction = this.cancelAction.bind(this);
    this.startAction = this.startAction.bind(this);
    this.cancelTask = this.cancelTask.bind(this);
    this.startTask = this.startTask.bind(this);
  }

  cancelAction(value) {
    this.cancelTask(value);
  }

  startAction(value) {
    this.startTask(value);
  }

  render() {
    let task = this.props.item;
    let index = this.props.index + 1;
    let linkProject = "/project-detail/" + task.projectId;
    let linkTask = "/task-detail/" + task.id;
    return (
      <tr>
        <td>{index}</td>
        <td className='small'>
          <span className='small'>
              Project: <cite> <Link className='text-dark badge-pill' to={linkProject}>{task.projectName}</Link></cite>
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
              <Link
                className='btn btn-primary h-100 align-middle'
                to={linkTask} >Detail:</Link>
            </div>
            <div className="col border">
              <EditableField
                data={task.taskName}
                save={(value) => this.saveTaskName(value, task.id)} />
            </div>
            <div className="col-auto bg-secondary rounded-end p-0">
              <TaskAction
                status={task.state}
                cancelAction={this.cancelAction}
                startAction={this.startAction}
                value={task.id} />
            </div>
          </div>
        </td>
        <td>

        </td>
      </tr>
    );
  }

  async saveTaskName(value, id) {
    this.setState({ tasks: null, filteredTasks: null, loading: true });
    await fetch('saveTask', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: id,
        name: value
      })
    });
    this.props.reload();
  }

  async startTask(id) {
    this.setState({ tasks: null, filteredTasks: null, loading: true });
    await fetch('startTask', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: id
      })
    });
    this.props.reload();
  }

  async cancelTask(id) {
    this.setState({ tasks: null, filteredTasks: null, loading: true });
    await fetch('cancelTask', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: id
      })
    });
    this.props.reload();
  }
}