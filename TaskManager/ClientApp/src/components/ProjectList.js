import React, { Component } from 'react';

export default class ProjectList extends Component {
    static displayName = 'ProjectList';

    constructor(props) {
        super(props);
        this.state = { projects: [], loading: true, creation: "" };
        this.onCreate = this.onCreate.bind(this);
    }

    componentDidMount = () => {
        this.getProjectsListData();
    }

    onSelected(value) {
        this.props.selectedChange(value);
    }

    onCreation(value) {
        this.setState({ creation: value });
    }

    onCreate() {
        this.createProject(this.state.creation);
    }

    renderProjectList(projects) {
        return (
            <div className="row">
                <div className="col-5 form-floating">
                    <select
                        id="project-select"
                        className="form-select"
                        onChange={(e) => { this.onSelected(e.target.value) }}>
                        <option key="0"
                            value="none">
                            not selected
                        </option>
                        {projects.map(project => <option key={project.id} value={project.id}> {project.projectName} </option>)}
                    </select>
                    <label for="project-select">Choose project</label>
                </div>
                <div className="col-3  form-floating">
                    <input
                        type='text'
                        id="project-new-name"
                        className="form-control"
                        placeholder="create new project"
                        onChange={(e) => { this.onCreation(e.target.value) }}
                        value={this.state.creation} />
                    <label for="project-new-name">Create Project</label>
                </div>
                <div className="col-auto">
                    <button
                        className="btn btn-primary"
                        onClick={this.onCreate}>
                        Create
                    </button>
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
        this.setState({ projects: null, loading: true, creation: "" });
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
        const response = await fetch('projectslist');
        const data = await response.json();
        this.setState({ projects: data, loading: false, creation: "" });
    }
}
