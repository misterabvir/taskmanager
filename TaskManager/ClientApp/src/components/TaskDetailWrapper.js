import React from 'react';
import { useParams } from 'react-router-dom';
import { TaskDetail } from './TaskDetail';

export  function TaskDetailWrapper(){
  const { id } = useParams(); 
  return <TaskDetail data={id} />;
};
