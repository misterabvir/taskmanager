import React from 'react';

export default function TaskAction(props) {
  const content =
    props.status === 'canceled' ?
      <button className="btn btn-secondary align-middle" disabled>completed</button> :
      props.status === 'started' ?
        <button className="btn btn-danger align-middle"
          onClick={() => { props.cancelAction(props.value) }}>
          Cancel
        </button> :
        <button className="btn btn-primary align-middle"
          onClick={() => { props.startAction(props.value) }}>
          Start
        </button>;
  return (
    <span>{content}</span>
  );
}
