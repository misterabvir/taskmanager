import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export default class ProjectList extends Component {
  static displayName = 'ProjectList';

  constructor(props) {
    super(props);
    this.state = { projects: [], loading: true, creation: "", selected: null };
    this.handleCreate = this.handleCreate.bind(this);
  }

  componentDidMount = () => {
    this.getProjectsListData();
  }

  handleSelected(value) {
    this.setState({ selected: value });
    this.props.selectedChange(value);
  }

  handleNewProjectNameInput(value) {
    this.setState({ creation: value });
  }

  handleCreate() {
    this.createProject(this.state.creation);
  }

  renderProjectList(projects) {
    let selected = this.state.selected;
    let link = "/project-detail/" + selected;
    let project = this.state.projects.find(item => item.id === selected);
    let content = project ? 
      <h4> For project  <Link className='text-dark' to={link}>{project.projectName}</Link></h4> : 
      <h4>For all projects</h4>;
    
    return (
      <div>
        <div className="row">
          {content}
        </div>
        <div className="row">
          <div className="col-auto">
            <select
              id="project-select"
              className="form-select"
              onChange={(e) => { this.handleSelected(e.target.value) }}>
              <option key="0"
                value="none">
                not selected
              </option>
              {projects.map(project => <option key={project.id} value={project.id}> {project.projectName} </option>)}
            </select>
          </div>
          <div className="col-auto">
            <input
              type='text'
              id="project-new-name"
              className="form-control"
              placeholder="create new project"
              onChange={(e) => { this.handleNewProjectNameInput(e.target.value) }}
              value={this.state.creation} />
          </div>
          <div className="col-auto">
            <button
              className="btn btn-primary "
              onClick={this.handleCreate}>
              Create
            </button>
          </div>
        </div>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderProjectList(this.state.projects);
    return (
      <div>
        {contents}
      </div>
    );
  }


  async createProject(name) {
    this.setState({ projects: null, loading: true, creation: "", selected: null });
    await fetch('createProject', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        ProjectName: name
      })
    });
    this.getProjectsListData();
  }

  async getProjectsListData() {
    const response = await fetch('projectsList');
    const data = await response.json();
    this.setState({ projects: data, loading: false, selected: null });
  }
}
