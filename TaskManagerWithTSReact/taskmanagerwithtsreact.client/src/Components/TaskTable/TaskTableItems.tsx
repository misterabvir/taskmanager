import { Component } from 'react';
import { TaskViewModel } from "../../Interfaces/TaskViewModel";
import TaskItem from './TaskItem';

type TaskTableItemsProps = {
  tasks: TaskViewModel[],
  reload: () => void
}

type TaskTableItemsState = {
  currentPage: number,
  itemsPerPage: number,
}

export default class TaskTableItems extends Component<TaskTableItemsProps, TaskTableItemsState> {
  constructor(props: TaskTableItemsProps) {
    super(props)
    this.state = {
      currentPage: 1,
      itemsPerPage: 2,
    };
  }

  handleClick = (event: React.MouseEvent<HTMLAnchorElement>) => {
    this.setState({
      currentPage: Number(event.currentTarget.id),
    });
  };

  handleItemsInPerPageChange(value: number) {
    this.setState({ itemsPerPage: value });
  }

  handleResultChange = () => {
    this.props.reload();
  }

  renderContent() {
    const tasks = this.props.tasks;
    const { currentPage, itemsPerPage } = this.state;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = tasks.slice(indexOfFirstItem, indexOfLastItem);
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(tasks.length / itemsPerPage); i++) {
      pageNumbers.push(i);
    }

    return (
      <div>
        <div className='row'>
          <div className="col-auto  mt-2">
            <label>Items per page: {this.state.itemsPerPage}</label>
          </div>
          <div className="col-auto mt-2">
            <input
              type="range"
              className="form-range"
              min="1"
              max="10"
              step="1"
              value={this.state.itemsPerPage}
              onChange={(e) => this.handleItemsInPerPageChange(Number.parseInt(e.target.value))}
              id="sliderInput" />
          </div>
        
        <div className="col">
          <ul id="page-numbers" className="pagination">
            {pageNumbers.map((number) => (
              <li
                key={number}
                id={number.toString()}

                className={number === currentPage ? 'active page-item user-select-none' : 'page-item user-select-none'}
              >
                <a className="page-link"
                  id={number.toString()}
                  onClick={this.handleClick}>
                  {number}
                </a>
              </li>
            ))}
          </ul>
        </div>
        </div>



        <table className='table'>
          <thead>
            <tr>
              <th> # </th>
              <th> Info </th>
              <th className='w-75'> Task </th>
              <th> Status </th>
            </tr>
          </thead>
          <tbody>
            {
              currentItems.map((task, index) =>
                <TaskItem
                  key={task.taskId}
                  task={task} index={index - this.state.itemsPerPage + (this.state.itemsPerPage) * this.state.currentPage}
                  reload={this.handleResultChange} />
              )
            }
          </tbody>
        </table>
      </div>
    );
  }

  render() {
    const content = this.props.tasks == undefined ?
      <div>empty...</div> :
      this.renderContent();
    return (
      <div>{content}</div>
    );
  }
}