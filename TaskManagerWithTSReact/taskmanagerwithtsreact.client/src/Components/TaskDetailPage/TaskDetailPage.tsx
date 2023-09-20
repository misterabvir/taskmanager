import { useParams } from "react-router-dom";
import { TaskViewModel } from "../../ViewModels/TaskViewModel";
import { Component } from "react";
import NotFoundPage from "../NotFoundPage/NotFoundPage";
import { PostRequest, PostRequestReturnable } from "../FetchServer/FetchServer";
import EditableFieldComponent from "../SharedComponents/EditableFieldComponent";
import InputComponent from "../SharedComponents/InputComponent";
import TaskItemAction from "../SharedComponents/TaskTable/TaskItemAction";


export default function TaskDetailPage() {
  let { id } = useParams();
  let content = id ? <TaskDetail taskId={id?.toString() ?? ""} /> : <NotFoundPage />
  return (
    <div className='container'>{content}</div>
  );
}

type TaskDetailProps = {
  taskId: string | undefined;
}

type TaskDetailState = {
  task: TaskViewModel | null;
  isLoading: boolean;
}

class TaskDetail extends Component<TaskDetailProps, TaskDetailState>{

  constructor(props: TaskDetailProps) {
    super(props);
    this.state = { task: null, isLoading: true }
  }

  componentDidMount(): void {
    this.getTaskDetail();
  }

  handleTaskNameChanged = async (value: string) => {
    await PostRequest('/saveTaskName', { TaskId: this.state.task?.taskId, Name: value });
    await this.getTaskDetail();
  }

  handleTaskDescriptionChanged = async (value: string) => {
    await PostRequest('/saveDescription', { TaskId: this.state.task?.taskId, Description: value });
    await this.getTaskDetail();
  }

  handleNewCommentCreated = async (value: string) => {
    await PostRequest('/createComment', { TaskId: this.state.task?.taskId, Content: value });
    await this.getTaskDetail();
  }

  handleTaskCanceled = async (taskId: string) => {
    await PostRequest('/cancelTask', { TaskId: taskId });
    await this.getTaskDetail();
  }

  handleTaskStarted = async (taskId: string) => {
    await PostRequest('/startTask', { TaskId: taskId });
    await this.getTaskDetail();
  }

  renderContent() {
    if (!this.state.task?.taskId) return (<NotFoundPage />);
    const task = this.state.task;
    const comments = task.comments;
    console.log(comments);
    return (
      <div>
        <div className="row">
          <div className="col-md-3 col-sd-12">
            <table className="table small">
              <thead>
                <tr>
                  <th className="w-25">Info</th>
                  <th>Data</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Project</td>
                  <td><a href={"/project-detail/" + task.projectId}>{task.projectName}</a></td>
                </tr>
                <tr>
                  <td>Created</td>
                  <td>{task.createDateFormat}</td>
                </tr>
                <tr>
                  <td>Started</td>
                  <td>{task.startDateFormat}</td>
                </tr>
                <tr>
                  <td>Updated</td>
                  <td>{task.updateDateFormat}</td>
                </tr>
                <tr>
                  <td>Canceled</td>
                  <td>{task.cancelDateFormat}</td>
                </tr>
                <tr>
                  <td>In work</td>
                  <td>{task.inWork}</td>
                </tr>
                <tr>
                  <td colSpan={2}><TaskItemAction status={task.state} value={task.taskId} cancelAction={this.handleTaskCanceled}
                  startAction={this.handleTaskStarted} /></td>
                </tr>
              </tbody>
            </table>
          </div>
          <div className="col">
            <div className="card mb-2">
              <div className="card-header ">
                <div className="row">
                  <div className="col h4 d-flex">
                    <EditableFieldComponent data={task.taskName} save={this.handleTaskNameChanged} />
                  </div>
                  <div className='col-auto text-danger'>
                    {task.state}
                  </div>
                  <div className='col-auto'>
                    {task.inWork}
                  </div>
                </div>
              </div>
              <div className="card-body">
                <div className="row">
                  <EditableFieldComponent data={task.description} save={this.handleTaskDescriptionChanged} />
                </div>
              </div>
            </div>
            
            <InputComponent onCreated={this.handleNewCommentCreated} />
            
            {comments.map((comment) => (            
                <div className="card key={comment.commentId} mt-2">
                  <div className="card-body">
                    <div className="row">
                      {comment.content}
                    </div>
                  </div>
                  <div className="card-footer">
                    <div className="row small opacity-75">
                      {comment.createdFormat}
                    </div>
                  </div>
                </div>))}

          </div>
        </div>

      </div>
    );
  }

  render() {
    const contents = this.state.isLoading ? <p>Loading... </p> : this.renderContent();
    return (
      <div className="container">
        <a href='/' className='nav-link'> <h1>Task Manager</h1> </a>
        {contents}
      </div>
    );
  }


  async getTaskDetail() {
    const data = await PostRequestReturnable('/taskDetail', { TaskId: this.props.taskId })
    this.setState({ task: data, isLoading: false });
  }

}