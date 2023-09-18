import React, { Component } from 'react';
import TaskItem from './TaskItem';


export default class TaskPagination extends Component {
  static displayName = TaskPagination.name;

  constructor(props) {
    super(props);
    this.state = { currentPage: 1, itemsPerPage: 3 };
    this.handlePageClick = this.handlePageClick.bind(this);
  }

  handlePageClick = (event) => {
    this.setState({
      currentPage: Number(event.target.id),
    });
  };


  handleItemsInPerPageChange(value) {
    this.setState({ itemsPerPage: value });
  }

  render() {
    const { data } = this.props;
    const { currentPage, itemsPerPage } = this.state;
    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = data.slice(indexOfFirstItem, indexOfLastItem);
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(data.length / itemsPerPage); i++) {
      pageNumbers.push(i);
    }
    return (
      <div>
        <div className='row'>
          <div className="col-auto">
            <label>Items per page: {this.state.itemsPerPage}</label> 
            </div>
          <div className="col">
            <input
            type="range"
            class="form-range"
            min="1"
            max="10"
            step="1"
            value={this.state.itemsPerPage}
            onChange={(e) => this.handleItemsInPerPageChange(e.target.value)}
            id="sliderInput" /> 
            </div>
        </div>
        <ul className="pagination">
          {pageNumbers.map((number) => (
            <li
              key={number}
              className={number === currentPage ?
                'active page-item user-select-none' :
                'page-item user-select-none'}>
              <a className="page-link"
                id={number}
                onClick={this.handlePageClick}>
                {number}
              </a>
            </li>
          ))}
        </ul>
        <table className="table">
          <thead>
            <tr>
              <th>#</th>
              <th>Detail</th>
              <th className="w-75">Task</th>
            </tr>
          </thead>
          <tbody>
            {
              currentItems.map((task, index) =>
                <TaskItem
                  key={task.id}
                  item={task}
                  index={index - this.state.itemsPerPage + (this.state.itemsPerPage) * this.state.currentPage}
                  reload={this.props.reload} />)
            }
          </tbody>
        </table>

      </div>
    );
  }
}
