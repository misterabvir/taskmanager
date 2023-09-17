import React, { Component } from 'react';
import Pagination from './Pagination';
import EditableField from './EditableField';
import { NotFound } from './NotFound';

export class ProjectDetail extends Component {
  static displayName = ProjectDetail.name;

  constructor(props) {
    super(props);
    this.state = {
      id: this.props.data,
      project: null,
      loading: true,
      creation: ""
    };
    this.onChangeNewTask = this.onChangeNewTaskName.bind(this);
    this.onCreate = this.onCreate.bind(this);
  }

  componentDidMount = () => {
    this.getProjectDetail();
  }

  onChangeNewTaskName(value) {
    this.setState({ creation: value });
  }

  onCreate() {
    this.createTask(this.state.creation, this.state.project.id);
  }

  renderProject() {
    const project = this.state.project;
    if(!project){
      return(<NotFound/>);
    }

    const tasks = project.tasks;
    return (
      <div>
        <a href="\" className='btn btn-link'>
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="24" fill="currentColor" className="bi bi-arrow-return-left" viewBox="0 0 16 16">
            <path fillRule="evenodd" d="M14.5 1.5a.5.5 0 0 1 .5.5v4.8a2.5 2.5 0 0 1-2.5 2.5H2.707l3.347 3.346a.5.5 0 0 1-.708.708l-4.2-4.2a.5.5 0 0 1 0-.708l4-4a.5.5 0 1 1 .708.708L2.707 8.3H12.5A1.5 1.5 0 0 0 14 6.8V2a.5.5 0 0 1 .5-.5z" />
          </svg>
          Back
        </a>
        <div>
          <div className="row">
            <div className="col-auto"><h3>Project Name:</h3>  </div>
            <div className="col"><h3><EditableField data={project.projectName} save={(value) => this.saveProjectName(value, project.id)} /></h3></div>
          </div>
        </div>
        <table className="table w-25">
          <thead>
            <tr>
              <th className="w-25">Detail</th>
              <th>Result</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Created</td>
              <td>{project.createDate}</td>
            </tr>
            <tr>
              <td>Updated</td>
              <td>{project.updateDate}</td>
            </tr>
          </tbody>
        </table>        
        <div className="row">
          <div className="col-3">
            <input
              type='text'
              className="form-control"
              placeholder="create new task"
              onChange={(e) => { this.onChangeNewTaskName(e.target.value) }}
              value={this.state.creation} />
          </div>
          <div className="col-auto">
            <button
              className="btn btn-primary"
              onClick={this.onCreate}>
              Create
            </button>
          </div>
        </div>
        <Pagination data={tasks} reload={() => { this.getProjectDetail() }} />
      </div>
    );
  }



  render() {
    let content = this.state.loading ? <p><em>Loading...</em></p> : this.renderProject();
    return (
      <div>{content} </div>
    );
  }

  async createTask(taskName, projectId){
    this.setState({creation:"" });
    await fetch('createTask', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        TaskName: taskName,
        ProjectId: projectId
      })
    });
    this.getProjectDetail();
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


  async saveProjectName(value, id) {
    this.setState({ project: null, loading: true, creation:"" });
    await fetch('saveProject', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: id,
        name: value
      })
    });
    this.getProjectDetail();
  }

  async getProjectDetail() {
    this.setState({ project: null, loading: true });
    const response = await fetch('projectDetail', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Id: this.state.id
      })
    });

    if(response.ok){   
      const data = await response.json();
      this.setState({ project: data, loading: false });
    }
    else{
      this.setState({ project: null, loading: false });
    }
  }
}