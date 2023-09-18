import React, { Component } from 'react';
import EditableField from './EditableField';
import TaskAction from './TaskAction';
import TaskComments from './TaskComments';
import { NotFound } from './NotFound';

export class TaskDetail extends Component {
  static displayName = TaskDetail.name;

  constructor(props) {
    super(props);
    this.state = {
      id: this.props.data,
      task: null,
      loading: true
    };
    this.cancelAction = this.cancelAction.bind(this);
    this.startAction = this.startAction.bind(this);
    this.cancelTask = this.cancelTask.bind(this);
    this.startTask = this.startTask.bind(this);
    this.reload = this.reload.bind(this);
  }

  reload() {
    this.getTaskDetail();
  }

  componentDidMount = () => {
    this.getTaskDetail();
  }

  cancelAction(value) {
    this.cancelTask(value);
  }

  startAction(value) {
    this.startTask(value);
  }

  renderTask() {
    const task = this.state.task;
    if (!task) {
      return (<NotFound />);
    }
    return (
      <div>
        <figure>
          <blockquote className="blockquote">
            <h4>Project Name: {task.projectName}</h4>
            <h4>Task Name:
              <EditableField
                data={task.taskName}
                save={(value) => this.saveTaskName(value, task.id)} />
            </h4>
          </blockquote>
          <figcaption class="figure-caption">
            <span>Created: <cite>{task.createDateFormat}</cite></span> <br />
            <span>Started: <cite>{task.startDateFormat}</cite></span> <br />
            <span>Updated: <cite>{task.updateDateFormat}</cite></span> <br />
            <span>Canceled:<cite>{task.cancelDateFormat}</cite></span> <br />
            <span>In work: <cite>{task.inWork}</cite></span> <br />
            <span>Status:  <cite>{task.state}</cite></span>
          </figcaption>
        </figure>
        <div className='row'>
          <TaskAction status={task.state} cancelAction={this.cancelAction} startAction={this.startAction} value={task.id} />
        </div>
        <TaskComments taskId={task.id} reload={this.reload} />
      </div>

    );
  }

  render() {
    let content = this.state.loading ?
      <p><em>Loading...</em></p> :
      this.renderTask();
    return (
      <div>{content}</div>
    );
  }

  async saveTaskName(value, id) {
    this.setState({ task: null, loading: true });
    await fetch('saveTask', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: id,
        name: value
      })
    });
    this.getTaskDetail();
  }

  async getTaskDetail() {
    this.setState({ task: null, loading: true });
    const response = await fetch('getTask', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: this.state.id
      })
    });

    if (response.ok) {
      const data = await response.json();
      this.setState({ task: data, loading: false });
    }
    else {
      this.setState({ task: null, loading: false });
    }
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
    this.getTaskDetail();
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
    this.getTaskDetail();
  }
}