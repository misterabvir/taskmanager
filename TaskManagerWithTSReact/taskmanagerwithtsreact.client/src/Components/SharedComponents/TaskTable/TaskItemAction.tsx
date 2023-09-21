import { Component } from 'react';
import { PostRequest } from '../../FetchServer/FetchServer';
import { CANCEL_TASK, START_TASK } from '../../../Utils/Const';

type TaskItemActionProps = {
  status: string;
  taskId: string;
  reload: () => void;
}

export default class TaskItemAction extends Component<TaskItemActionProps> {
  constructor(props: TaskItemActionProps) {
    super(props)
  }

  
  handleTaskCanceled = async () => {
    await PostRequest(CANCEL_TASK, { TaskId: this.props.taskId });
    await this.props.reload();
  }

  handleTaskStarted = async () => {
    await PostRequest(START_TASK, { TaskId: this.props.taskId });
    await this.props.reload();
  }

  render() {
    const content =
      this.props.status === 'canceled' ?
        <button className="btn btn-secondary" disabled>completed</button> :
        this.props.status === 'started' ?
          <button className="btn btn-danger"
            onClick={this.handleTaskCanceled}>
            Cancel
          </button> :
          <button className="btn btn-primary"
            onClick={this.handleTaskStarted}>
            Start
          </button>;
    return (
      <span>{content}</span>
    );
  }
}
