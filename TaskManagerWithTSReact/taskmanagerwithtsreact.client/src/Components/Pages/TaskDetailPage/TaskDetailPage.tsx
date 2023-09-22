import { Component } from "react";
import { useParams } from "react-router-dom";
import { Get, Post, Put } from "../../RequestsToServer/Requests";
import { COMMENT_CREATE, TASK_UPDATE_DESCRIPTION, TASK_UPDATE_NAME, TASK_DETAIL, PAGE_PROJECT_DETAIL } from "../../../Utils/Const";
import { TaskViewModel } from "../../../ViewModels/TaskViewModel";
import NotFoundPage from "../NotFoundPage/NotFoundPage";
import EditableTextAreaComponent from "../../SharedComponents/EditableTextAreaComponent";
import InputComponent from "../../SharedComponents/InputComponent";
import TaskItemAction from "../../SharedComponents/TaskTable/TaskItemAction";
import EditableInputComponent from "../../SharedComponents/EditableInputComponent";
import CommentComponent from "../../SharedComponents/CommentComponent";


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

  async componentDidMount(): Promise<void> {
    await this.getTaskDetail();
  }

  handleTaskNameChanged = async (value: string) => {
    await Put(TASK_UPDATE_NAME, { TaskId: this.state.task?.taskId, Name: value });
    await this.getTaskDetail();
  }

  handleTaskDescriptionChanged = async (value: string) => {
    await Put(TASK_UPDATE_DESCRIPTION, { TaskId: this.state.task?.taskId, Description: value });
    await this.getTaskDetail();
  }

  handleNewCommentCreated = async (value: string) => {

    await Post(COMMENT_CREATE, { TaskId: this.state.task?.taskId, Content: value });
    await this.getTaskDetail();
  }



  renderContent() {
    if (!this.state.task?.taskId) return (<NotFoundPage />);
    const task = this.state.task;
    const comments = task.comments;
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
                  <td><a href={`${PAGE_PROJECT_DETAIL}/${task.projectId}`}>{task.projectName}</a></td>
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
                  <td colSpan={2}>
                    <TaskItemAction
                      status={task.state}
                      taskId={task.taskId}
                      reload={this.getTaskDetail} />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div className="col">
            <div className="card mb-2" key={task.projectId}>
              <div className="card-header">
                <div className="row">
                  <div className="col h4 d-flex">
                    <EditableInputComponent data={task.taskName} save={this.handleTaskNameChanged} />
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
                  <EditableTextAreaComponent data={task.description} save={this.handleTaskDescriptionChanged} />
                </div>
              </div>
            </div>
            <div>
              <InputComponent key={task.taskId} onCreated={this.handleNewCommentCreated} />
            </div>
            <div>
              {comments.map(comment => (<CommentComponent key={comment.commentId} comment={comment} reload={this.getTaskDetail} />))}
            </div>
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

  getTaskDetail = async () => {
    const data = await Get(`${TASK_DETAIL}/${this.props.taskId}`);
    this.setState({ task: data, isLoading: false });
  }
}