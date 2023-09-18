import React, { Component } from 'react';
import ProjectList from './ProjectList';
import TaskPagination from './TaskPagination';
import DateTimePicker from './DateTimePicker';

export class TaskData extends Component {
  static displayName = TaskData.name;

  constructor(props) {
    super(props);
    this.state = { tasks: [], filteredTasks: [], loading: true, selectedProject: null, start: null, end: null };
    this.getTaskData = this.getTaskData.bind(this);
  }

  componentDidMount() {
    this.getTaskData();
  }

  selectedChange = (value) => {
    this.setState({ selectedProject: value });
    this.getTaskData();
  }

  handleStartChangeTime(value) {
    this.setState({ start: value._d });
    this.getTaskData();
  }

  handleEndChangeTime(value) {
    this.setState({ end: value._d });
    this.getTaskData();
  }


  getStartDate(data) {
    let start = new Date();
    data.forEach(item => {
      if (start > new Date(item.updateDate))
        start = new Date(item.updateDate);
    });
    return start;
  }

  getEndDate(data) {
    let end = new Date();
    data.forEach(item => {
      if (end < new Date(item.updateDate))
        end = new Date(item.updateDate);
    });
    return end;
  }

  renderTaskTable() {
    return (
      <div>
        <ProjectList selectedChange={this.selectedChange} />
        <div className="row mt-2">
          <div className="col">
            <DateTimePicker
              datetime={this.state.start}
              placeHolder="select a start date-time"
              timeChange={(value) => this.handleStartChangeTime(value)} />
          </div>
          <div className="col">
            <DateTimePicker
              datetime={this.state.end}
              placeHolder="select a end date-time"
              timeChange={(value) => this.handleEndChangeTime(value)} />
          </div>
        </div>
        <TaskPagination 
          data={this.state.filteredTasks} 
          reload={this.getTaskData} />
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderTaskTable();

    return (
      <div>
        <h1 id="tableLabel">Task List</h1>
        {contents}
      </div>
    );
  }

  getFiltered(data) {
    let filtered = data;
    if (this.state.selectedProject && this.state.selectedProject !== "none") {
      filtered = filtered.filter(item => item.projectId === this.state.selectedProject);
    }

    if (this.state.start) {
      filtered = filtered.filter(item => new Date(item.updateDate).getTime() >= this.state.start.getTime());
    }

    if (this.state.end) {
      filtered = filtered.filter(item => new Date(item.updateDate).getTime() <= this.state.end.getTime());
    }
    return filtered;
  }


  async getTaskData() {
    this.setState({ tasks: null, filteredTasks: null, loading: true, start: null, end: null });
    const response = await fetch('tasklist');
    const data = await response.json();
    const filtered = this.getFiltered(data);
    const start = this.getStartDate(filtered);
    const end = this.getEndDate(filtered);
    this.setState({ tasks: data, filteredTasks: filtered, loading: false, start: start, end: end });
  }
}
