import React, { Component } from 'react';
import TaskItem from './TaskItem';
import { NotFound } from './NotFound';

export default class Pagination extends Component {
  static displayName = Pagination.name;

  constructor(props) {
    super(props);
    this.state = { currentPage: 1, itemsPerPage: 3 };
    this.pageClick = this.pageClick.bind(this);
  }

  
  pageClick = (event) => {
    this.setState({
      currentPage: Number(event.target.id),
    });
  };

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
        <table className="table">
          <thead>
            <tr>
              <th>#</th>
              <th>InWork</th>
              <th>Project</th>
              <th className="w-50">Task</th>
              <th>State</th>
              <th>Action</th>
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
        <ul className="pagination">
          {pageNumbers.map((number) => (
            <li
              key={number}
              className={number === currentPage ? 'active page-item user-select-none' : 'page-item user-select-none'}>
              <a className="page-link" id={number} onClick={this.pageClick}>{number}</a>
            </li>
          ))}
        </ul>
      </div>
    );
  }
}
