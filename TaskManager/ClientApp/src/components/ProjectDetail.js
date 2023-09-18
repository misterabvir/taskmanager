import React, { Component } from 'react';
import TaskPagination from './TaskPagination';
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
    if (!project) {
      return (<NotFound />);
    }
    const tasks = project.tasks;
    return (
      <div>
        <figure>
          <blockquote className="blockquote">
            <h4>Project Name: <EditableField data={project.projectName} save={(value) => this.saveProjectName(value, project.id)} /></h4>
          </blockquote>
          <figcaption class="figure-caption">
            <span>Created: <cite>{project.createDateFormat}</cite></span> <br/>
            <span>Updated: <cite>{project.updateDateFormat}</cite></span>
          </figcaption>
        </figure>       
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
        <TaskPagination data={tasks} reload={() => { this.getProjectDetail() }} />
      </div>
    );
  }

  render() {
    let content = this.state.loading ? <p><em>Loading...</em></p> : this.renderProject();
    return (
      <div>{content} </div>
    );
  }

  async createTask(taskName, projectId) {
    this.setState({ creation: "" });
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
    this.setState({ project: null, loading: true, creation: "" });
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

    if (response.ok) {
      const data = await response.json();
      this.setState({ project: data, loading: false });
    }
    else {
      this.setState({ project: null, loading: false });
    }
  }
}