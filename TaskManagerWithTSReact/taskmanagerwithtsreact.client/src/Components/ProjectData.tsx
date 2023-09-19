import { Component } from 'react';
import { GUID_EMPTY } from '../Utils/Const'
import { TaskViewModel } from '../Interfaces/TaskViewModel';
import { ProjectViewModel } from "../Interfaces/ProjectViewModel";
import DropDownProjectList from "./DropDownProjectList";
import TaskTableItems from './TaskTable/TaskTableItems';
import NewNameComponent from './Reusable/NewNameComponent';
import DateTimePicker from './Reusable/DateTimePickerComponent';
import EditableFieldComponent from './Reusable/EditableFieldComponent';

interface ProjectDataState {
  projects: ProjectViewModel[];
  filtered: ProjectViewModel[];
  isLoading: boolean;
  selectedProjectId: string;
  selectedProjectName: string;
  startDate: Date;
  endDate: Date;
}

export default class ProjectData extends Component<{}, ProjectDataState> {

  constructor(props: {}) {
    super(props);
    this.state = {
      projects: [],
      filtered: [],
      isLoading: false,
      selectedProjectId: GUID_EMPTY,
      selectedProjectName: "All",
      startDate: new Date(),
      endDate: new Date()
    };
  }

  componentDidMount() {
    this.getProjects();
  }
  handleProjectNameChanged = (value: string) => {
    this.saveProjectName(this.state.selectedProjectId, value);
  };

  handleNewNameCreated = (newProjectName: string) => {
    this.createProject(newProjectName);
  }

  handleSelectChange = (projectId: string, projectName: string) => {
    this.setState({ selectedProjectId: projectId, selectedProjectName: projectName });
  }

  getFilteredTasks = (tasks: TaskViewModel[]) => {
    if (this.state.selectedProjectId !== GUID_EMPTY)
      tasks = tasks.filter(task => task.projectId == this.state.selectedProjectId)
    tasks = tasks.filter(task => new Date(task.updateDate).getTime() >= this.state.startDate.getTime())
    tasks = tasks.filter(task => new Date(task.updateDate).getTime() <= this.state.endDate.getTime())
    return tasks;
  }

  handleSelectedDateStartChanged = (date: Date) => {
    if (date <= this.state.endDate)
      this.setState({ startDate: date });
  }

  handleSelectedDateEndChanged = (date: Date) => {
    if (date >= this.state.startDate)
      this.setState({ endDate: date });
  }

  renderContent = () => {
    const projects = this.state.projects;
    let tasks: TaskViewModel[] = [];
    projects.forEach(p => tasks.push(...p.tasks));
    tasks = this.getFilteredTasks(tasks);
    const header = this.state.selectedProjectName === 'All' ?
      <h3>Showed tasks for All projects</h3> :
      <h3>Showed tasks for <EditableFieldComponent
        data={this.state.selectedProjectName}
        save={this.handleProjectNameChanged} /> </h3>
    return (
      <div>
        {header}
        <div className='row m-2'>
          <div className="col-sd-12 col-md-6">
            <DropDownProjectList
              projects={projects}
              onSelectChange={this.handleSelectChange} />
          </div>
          <div className="col-sd-12 col-md-6">
            <NewNameComponent
              onCreated={this.handleNewNameCreated} />
          </div>
        </div>

        <div className="row m-2">
          <div className="col">
            <DateTimePicker
              message='select start date'
              selectedDateChanged={this.handleSelectedDateStartChanged}
              selectedDate={this.state.startDate} />
          </div>
          <div className="col">
            <DateTimePicker
              message='select end date'
              selectedDateChanged={this.handleSelectedDateEndChanged}
              selectedDate={this.state.endDate} />
          </div>
        </div>
        <div className="row m-2">
          <TaskTableItems
            tasks={tasks}
            reload={this.getProjects} />
        </div>


      </div>
    );
  }

  searchMinDate(projects: ProjectViewModel[]): Date {
    let dates: Date[] = [];
    projects.forEach(p => p.tasks.forEach(t => dates.push(new Date(t.updateDate))));
    return new Date(Math.min(...dates.map(date => date.getTime())));;
  }

  searchMaxDate(projects: ProjectViewModel[]): Date {
    let dates: Date[] = [];
    projects.forEach(p => p.tasks.forEach(t => dates.push(new Date(t.updateDate))));
    return new Date(Math.max(...dates.map(date => date.getTime())));;
  }

  render() {
    const contents = this.state.projects === undefined
      ? <p>Loading... </p>
      : this.renderContent();
    return (
      <div className="container">
        <h1>Task Manager</h1>

        {contents}
      </div>
    );
  }

  createProject = async (name: string) => {
    try {
      await fetch('createProject', {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify({
          ProjectName: name
        })
      });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    this.getProjects();
  }

  saveProjectName = async (projectId: string, projectName: string) => {
    try {
      await fetch('saveProjectName', {
        method: 'POST',
        headers: { 'Content-type': 'application/json' },
        body: JSON.stringify({
          Name: projectName,
          Id: projectId
        })
      });
    } catch (error) {
      console.error('Error fetching data:', error);
    }
    this.getProjects();
  }

  getProjects = async () => {
    this.setState({ isLoading: true });
    try {
      const response = await fetch('projectsList');
      const data = await response.json();
      this.setState({
        projects: data, isLoading: false, startDate: this.searchMinDate(data), endDate: this.searchMaxDate(data)
      });
    } catch (error) {
      console.error('Error fetching data:', error);
      this.setState({
        isLoading: false
      });
    }

  }
}
