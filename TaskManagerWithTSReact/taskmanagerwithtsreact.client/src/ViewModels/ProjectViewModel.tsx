import { TaskViewModel } from "./TaskViewModel";

export interface ProjectViewModel {
    projectId: string;
    projectName: string;
    createDate: Date;
    createDateFormat: string;
    updateDate: Date;
    updateDateFormat: string;
    tasks: TaskViewModel[]
};