import { Component } from 'react';
import Modal from 'react-modal';

const modalStyles = {
  content: {
    width: '300px',
    height: '180px',
    margin: 'auto',
    padding: '20px',
  },
};

interface ConfirmationModalProps {
  isOpen: boolean;
  onRequestClose: () => void;
  onConfirm: () => void;
};

export default class ConfirmationModal extends Component<ConfirmationModalProps>{
  constructor(props: ConfirmationModalProps) {
    super(props);
  }

  render = () => {

    return (
      <Modal
        isOpen={this.props.isOpen}
        onRequestClose={this.props.onRequestClose}
        style={modalStyles}
        ariaHideApp={false}>
        <h2>Confirm Action</h2>
        <p>Are you sure?</p>
        <button className='btn btn-danger me-2' onClick={this.props.onConfirm}>I'm sure</button>
        <button className='btn btn-secondary' onClick={this.props.onRequestClose}>Cancel</button>
      </Modal>
    );
  }
}

