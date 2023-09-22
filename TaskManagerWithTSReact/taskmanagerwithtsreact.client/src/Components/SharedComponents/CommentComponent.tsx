import { Component } from 'react';
import { CommentViewModel } from '../../ViewModels/CommentViewModel';
import ConfirmationModal from './ConfirmationModalComponent';
import EditableTextAreaComponent from './EditableTextAreaComponent';
import { COMMENT_DELETE, COMMENT_UPDATE } from '../../Utils/Const';
import { Delete, Put } from '../RequestsToServer/Requests';

type CommentComponentProps = {
  comment: CommentViewModel;
  reload: () => void;
}

type CommentComponentSate = {
  comment: CommentViewModel;
  isModalOpen: boolean;
}

export default class CommentComponent extends Component<CommentComponentProps, CommentComponentSate>{
  constructor(props: CommentComponentProps) {
    super(props);
    this.state = { comment: this.props.comment, isModalOpen: false }
  }

  handleCommentChanged = async (value: string, commentId: string) => {
    await Put(COMMENT_UPDATE, { CommentId: commentId, Content: value });
    await this.props.reload();
  };

  handleOpenModal = () => {
    this.setState({ isModalOpen: true });
  }

  handleCloseModal = () => {
    this.setState({ isModalOpen: false });
  }

  handleConfirmAction = async (commentId: string) => {
    this.handleCloseModal();
    await Delete(COMMENT_DELETE, { CommentId: commentId });
    await this.props.reload();
  }

  render() {
    return (
      <div key={this.state.comment.commentId}>
        <div className="card  mt-2" >
          <div className="card-header">
            <div className="row small opacity-75">
              <div className="col">{this.state.comment.createdFormat}</div>
              <div className="col-auto">
                <button className="btn-close" onClick={this.handleOpenModal}></button>
                <ConfirmationModal
                  isOpen={this.state.isModalOpen}
                  onRequestClose={this.handleCloseModal}
                  onConfirm={() => this.handleConfirmAction(this.state.comment.commentId)}
                />
              </div>
            </div>
          </div>
          <div className="card-body">
            <div className="row">
              <EditableTextAreaComponent 
                data={this.state.comment.content} 
                save={(value) => this.handleCommentChanged(value, this.state.comment.commentId)} />
            </div>
          </div>
        </div>
      </div>
    );
  }
}