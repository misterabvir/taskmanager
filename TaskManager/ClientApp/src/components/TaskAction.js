import React from 'react';

export default function TaskAction(props) {
  const content =
    props.status === 'canceled' ?
      <button className="btn btn-secondary h-100" disabled>completed</button> :
      props.status === 'started' ?
        <button className="btn btn-danger h-100"
          onClick={() => { props.cancelAction(props.value) }}>
          Cancel
        </button> :
        <button className="btn btn-primary h-100"
          onClick={() => { props.startAction(props.value) }}>
          Start
        </button>;
  return (
    <span>{content}</span>
  );
}
