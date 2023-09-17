import React from 'react';
import { useParams } from 'react-router-dom';
import { ProjectDetail } from './ProjectDetail';

export function ProjectDetailWrapper(){
  const { id } = useParams(); 
  return <ProjectDetail data={id} />;
};
