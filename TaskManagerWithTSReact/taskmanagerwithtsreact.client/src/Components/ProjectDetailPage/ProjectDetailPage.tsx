import { Component } from 'react';
import { useParams } from 'react-router-dom';
import NotFoundPage from '../NotFoundPage/NotFoundPage';
import { ProjectViewModel } from '../../ViewModels/ProjectViewModel';
import EditableFieldComponent from '../SharedComponents/EditableFieldComponent';
import TaskTableItems from '../SharedComponents/TaskTable/TaskTableItems';
import InputComponent from '../SharedComponents/InputComponent';
import { PostRequest, PostRequestReturnable } from '../FetchServer/FetchServer';

export default function ProjectDetailPage() {
  let { id } = useParams();
  let content = id ? <ProjectDetail projectId={id?.toString() ?? ""} /> : <NotFoundPage />
  return (
    <div className='container'>{content}</div>
  );
}

type ProjectDetailProps = {
  projectId: string | undefined;
}

type ProjectDetailState = {
  project: ProjectViewModel | null;
  isLoading: boolean;
}

class ProjectDetail extends Component<ProjectDetailProps, ProjectDetailState>{
  constructor(props: ProjectDetailProps) {
    super(props);
    this.state = { project: null, isLoading: true }
  }

  componentDidMount() {
    this.getProjectDetail();
  }

  handleProjectNameChange = async (name: string) => {
    await PostRequest('/saveProjectName', { ProjectId: this.state.project?.projectId, Name: name });
  }

  handleNewTaskCreate = async (name: string) => {
    await PostRequest('/createTask', { ProjectId: this.state.project?.projectId, TaskName: name },);
    await this.getProjectDetail();
  };

  renderContent() {
    if (!this.state.project?.projectId) return <NotFoundPage />

    const project = this.state.project;
    const tasks = project.tasks;
    tasks.sort((t1, t2) => new Date(t2.updateDate).getTime() - new Date(t1.updateDate).getTime())
    return (
      <div>
        <h3>Project detail for <EditableFieldComponent data={project.projectName} save={this.handleProjectNameChange} /></h3>
        <table className='table small'>
          <thead>
            <tr>
              <th className='w-25'>Info</th>
              <th>Data</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Created:</td>
              <td>{project.createDateFormat}</td>
            </tr>
            <tr>
              <td>Updated:</td>
              <td>{project.updateDateFormat}</td>
            </tr>
          </tbody>
        </table>
        <div><InputComponent onCreated={this.handleNewTaskCreate} /></div>
        <div className="mt-2"><TaskTableItems tasks={project.tasks} reload={this.getProjectDetail} /></div>
      </div>);
  }

  render() {
    const contents = this.state.isLoading ? <p>Loading... </p> : this.renderContent();
    return (
      <div className="container">
        <a href='/' className='btn'> <h1>Task Manager</h1> </a>
        {contents}
      </div>
    );
  }

  getProjectDetail = async () => {
    const data = await PostRequestReturnable('/projectDetail', { ProjectId: this.props.projectId });
    this.setState({ project: data, isLoading: false });
  }
}
