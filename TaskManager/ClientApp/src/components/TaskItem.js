import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link, useParams } from 'react-router-dom';
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
        <td>{task.inWork}</td>
        <td><Link className='text-dark' to={linkProject}>{task.projectName}</Link></td>
        <td>
          <div className="row">
            <div className="col-auto"><Link className='text-dark' to={linkTask} >Detail:</Link>  </div>
            <div className="col"><EditableField data={task.taskName} save={(value) => this.saveTaskName(value, task.id)} /></div>
                  
            
          </div>
    
        </td>
        <td>{task.state}</td>
        <td>
          <TaskAction status={task.state} cancelAction={this.cancelAction} startAction={this.startAction} value={task.id} />
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