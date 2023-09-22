import { Component } from 'react';
import { Put } from '../../RequestsToServer/Requests';
import { TASK_CANCEL, TASK_START } from '../../../Utils/Const';

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
    await Put(TASK_CANCEL, { TaskId: this.props.taskId });
    await this.props.reload();
  }

  handleTaskStarted = async () => {
    await Put(TASK_START, { TaskId: this.props.taskId });
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
