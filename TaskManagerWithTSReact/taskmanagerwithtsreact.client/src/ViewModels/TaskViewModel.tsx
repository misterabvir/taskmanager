import { CommentViewModel } from "./CommentViewModel";

export interface TaskViewModel {
    taskId: string;
    taskName: string;
    description: string;
    projectId: string;
    createDate: Date;
    projectName: string;
    updateDate: Date;
    startDate: Date;
    cancelDate: Date;
    state:string;
    inWork:string;
    createDateFormat:string;
    startDateFormat:string;
    cancelDateFormat:string;
    updateDateFormat:string;
    comments: CommentViewModel[];
  };