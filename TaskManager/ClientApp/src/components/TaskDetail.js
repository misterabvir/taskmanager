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

  reload(){
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
    if(!task){
      return(<NotFound/>);
    }
    return (
      <div>
        <div>
          <div className="row">
            <div className="col-auto"><h3>Project:</h3>  </div>
            <div className="col"><h3>{task.projectName}</h3></div>
          </div>
          <div className="row">
            <div className="col-auto"><h3>Task Name:</h3>  </div>
            <div className="col"><h3><EditableField data={task.taskName} save={(value) => this.saveTaskName(value, task.id)} /></h3></div>
          </div>
        </div>
        <table className="table">
          <thead>
            <tr>
              <th className="w-25">Detail</th>
              <th>Result</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Created</td>
              <td>{task.createDate}</td>
            </tr>
            <tr>
              <td>Started</td>
              <td>{task.startDate}</td>
            </tr>
            <tr>
              <td>Updated</td>
              <td>{task.updateDate}</td>
            </tr>
            <tr>
              <td>Canceled</td>
              <td>{task.cancelDate}</td>
            </tr>
            <tr>
              <td>In work</td>
              <td>{task.inWork}</td>
            </tr>
            <tr>
              <td>Status</td>
              <td>{task.state}</td>
            </tr>
            <tr>
              <td>Action</td>
              <td><TaskAction status={task.state} cancelAction={this.cancelAction} startAction={this.startAction} value={task.id} /></td>
            </tr>
          </tbody>
        </table>
        <TaskComments taskId={task.id} reload={this.reload}/>
      </div>

    );
  }


  render() {
    let content = this.state.loading ? <p><em>Loading...</em></p> : this.renderTask();

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
    
    if(response.ok){   
      const data = await response.json();
      this.setState({ task: data, loading: false });
    }
    else{
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