import React, { Component } from 'react';
import ProjectList from './ProjectList';
import Pagination from './Pagination';
import TaskItem from './TaskItem';


export class TaskData extends Component {
    static displayName = TaskData.name;

    constructor(props) {
        super(props);
        this.state = { tasks: [], filteredTasks: [], loading: true, selectedProject:null };
        this.getTaskData = this.getTaskData.bind(this);
    }

    componentDidMount() {
        this.getTaskData();
    }
    selectedChange = (value) => {
        this.setState({selectedProject: value});        
        this.getTaskData();
    }

    renderTaskTable(tasks) {
        return (
            <div>
                <ProjectList selectedChange={this.selectedChange} />
                <Pagination data={this.state.filteredTasks} reload={this.getTaskData}/>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTaskTable(this.state.filteredTasks);

        return (
            <div>
                <h1 id="tableLabel">Task List</h1>
                {contents}
            </div>
        );
    }

    async getTaskData() {
        const response = await fetch('tasklist');
        const data = await response.json();
        if (!this.state.selectedProject || this.state.selectedProject === "none") {
            this.setState({ tasks: data, filteredTasks:data, loading: false });
        } else {
            const filtered = data.filter(item => item.projectId === this.state.selectedProject);
            this.setState({ tasks: data, filteredTasks:filtered, loading: false });
        }
    }
}
