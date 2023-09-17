import React, { Component } from 'react';

export default class TaskComments extends Component {
  static displayName = TaskComments.name;

  constructor(props) {
    super(props);
    this.state = { taskId: this.props.taskId, comments: null, loading: true, newComment: "" };
    this.onChangeNewComment = this.onChangeNewComment.bind(this);
    this.onCreateComment = this.onCreateComment.bind(this);  
    this.createComment = this.createComment.bind(this);
  }

  componentDidMount = () => {
    this.getComments();
  }

  onChangeNewComment(value) {
    this.setState({ newComment: value })
  }
  onCreateComment() {
    this.createComment(this.state.newComment, this.state.taskId);
  }

  renderComments() {
    const comments = this.state.comments;

    return (
      <div className="container">
        <div className="row mb-5">

          <textarea
            className="form-control mb-1"
            placeholder="Write comment"
            onChange={(e) => { this.onChangeNewComment(e.target.value) }}
            value={this.state.newComment} />

          <button
            className="btn btn-primary"
            onClick={this.onCreateComment}>
            Create
          </button>

        </div>
        <div className='row'>{
          comments.map(comment =>
            <div key={comment.id} className="card border-light mb-3 row">
              <div className="card-header small text-dark opacity-75">#{comment.id}</div>
              <div className="card-body row">
                <div className='col'>
                  <p className="card-text">
                    {comment.content}
                  </p>
                </div>
                <div className='col-auto'>{comment.created}</div>
              </div>

            </div>
          )}
        </div>
      </div>);
  }


  render() {
    let content = this.state.loading ? <p><em>Loading...</em></p> : this.renderComments();

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