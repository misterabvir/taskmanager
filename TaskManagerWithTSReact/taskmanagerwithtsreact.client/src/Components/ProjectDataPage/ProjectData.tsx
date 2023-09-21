import { Component } from 'react';
import { ALL, CREATE_PROJECT, GUID_EMPTY, PROJECT_LIST, SAVE_PROJECT_NAME } from '../../Utils/Const'
import { TaskViewModel } from '../../ViewModels/TaskViewModel';
import { ProjectViewModel } from "../../ViewModels/ProjectViewModel";
import DropDownProjectList from "./DropDownProjectList";
import TaskTableItems from '../SharedComponents/TaskTable/TaskTableItems';
import InputComponent from '../SharedComponents/InputComponent';
import DateTimePicker from '../SharedComponents/DateTimePickerComponent';
import { GetRequest, PostRequest } from '../FetchServer/FetchServer';

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
      selectedProjectName: ALL,
      startDate: new Date(),
      endDate: new Date()
    };
  }

  componentDidMount() {
    this.getProjects();
  }
  handleProjectNameChanged = async (value: string) => {
    await PostRequest(SAVE_PROJECT_NAME, { ProjectId: this.state.selectedProjectId, Name: value })
    await this.getProjects();
  };

  handleNewNameCreated = async (newProjectName: string) => {
    await PostRequest(CREATE_PROJECT, { ProjectName: newProjectName })
    await this.getProjects();
  }

  handleSelectChange = (projectId: string, projectName: string) => {
    this.setState({ selectedProjectId: projectId, selectedProjectName: projectName });
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
    const header = this.state.selectedProjectName === ALL ?
      <h3>Showed tasks for All projects</h3> :
      <h3>Showed tasks for <a className='text-dark badge-pill' href={"/project-detail/" + this.state.selectedProjectId}>{this.state.selectedProjectName}</a> </h3>
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
            <InputComponent
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

  getFilteredTasks = (tasks: TaskViewModel[]) => {
    if (this.state.selectedProjectId !== GUID_EMPTY)
      tasks = tasks.filter(task => task.projectId == this.state.selectedProjectId)
    tasks = tasks.filter(task => new Date(task.updateDate).getTime() >= this.state.startDate.getTime())
    tasks = tasks.filter(task => new Date(task.updateDate).getTime() <= this.state.endDate.getTime())
    return tasks;
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
        <a href='/' className='btn'> <h1>Task Manager</h1> </a>
        {contents}
      </div>
    );
  }

  getProjects = async () => {
    this.setState({ isLoading: true });
    const data = await GetRequest(PROJECT_LIST);
    this.setState({ projects: data, isLoading: false, startDate: this.searchMinDate(data), endDate: this.searchMaxDate(data) });
  }
}
