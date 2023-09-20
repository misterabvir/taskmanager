import { Component } from 'react';

type TaskItemActionProps = {
  status: string;
  value: string;
  cancelAction: (value: string) => void;
  startAction: (value: string) => void;
}

export default class TaskItemAction extends Component<TaskItemActionProps> {
  constructor(props: TaskItemActionProps) {
    super(props)
  }
  render() {
    const content =
      this.props.status === 'canceled' ?
        <button className="btn btn-secondary" disabled>completed</button> :
        this.props.status === 'started' ?
          <button className="btn btn-danger"
            onClick={() => { this.props.cancelAction(this.props.value) }}>
            Cancel
          </button> :
          <button className="btn btn-primary"
            onClick={() => { this.props.startAction(this.props.value) }}>
            Start
          </button>;
    return (
      <span>{content}</span>
    );
  }
}
