import React, { Component } from 'react';

export default class TaskComments extends Component {
  static displayName = TaskComments.name;

  constructor(props) {
    super(props);
    this.state = { taskId: this.props.taskId, comments: null, loading: true, newComment: "" };
    this.handleChangeNewComment = this.handleChangeNewComment.bind(this);
    this.handleCreateComment = this.handleCreateComment.bind(this);
  }

  componentDidMount = () => {
    this.getComments();
  }

  handleChangeNewComment(value) {
    this.setState({ newComment: value })
  }

  handleCreateComment() {
    this.createComment(this.state.newComment, this.state.taskId);
  }

  renderComments() {
    const comments = this.state.comments;
    return (
      <div className="container">
        <div className="row mb-5">
          <textarea
            className="form-control mb-1 mt-1"
            placeholder="Write comment"
            onChange={(e) => { this.handleChangeNewComment(e.target.value) }}
            value={this.state.newComment} />
          <button
            className="btn btn-primary"
            onClick={this.handleCreateComment}>
            Create
          </button>
        </div>
        <div className='row'>{
          comments.map(comment =>
            <div key={comment.id} className="row p-0 mb-2">
              <div className="col-auto small bg-secondary rounded-start">
                <span className='text-white small' >{comment.createdFormat}</span>
              </div>
              <div className="col border rounded-end">{comment.content}</div>
            </div>
          )}
        </div>
      </div>);
  }


  render() {
    let content = this.state.loading ?
      <p><em>Loading...</em></p> :
      this.renderComments();

    return (
      <div>{content}</div>
    );
  }

  async createComment(content, taskId) {
    this.setState({ newComment: "" });
    await fetch('createComment', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        Content: content,
        TaskId: taskId
      })
    });
    this.props.reload();
    this.getComments();
  }

  async getComments() {
    this.setState({ comments: null, loading: true });
    const response = await fetch('getComments', {
      method: 'POST',
      headers: { 'Content-type': 'application/json' },
      body: JSON.stringify({
        TaskId: this.state.taskId
      })
    });
    const data = await response.json();
    this.setState({ comments: data, loading: false });
  }
}