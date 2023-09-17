import { TaskData } from "./components/TaskData";
import { ProjectDetailWrapper } from "./components/ProjectDetailWrapper";
import { TaskDetailWrapper } from "./components/TaskDetailWrapper";
import { NotFound } from "./components/NotFound";

const AppRoutes = [
  {
    path: "/",
    component: {TaskData},
    element: <TaskData/>
  },
  {
    path: "/project-detail/:id",
    element: <ProjectDetailWrapper/>,
  },
  {
    path: "/task-detail/:id",
    element: <TaskDetailWrapper/>,
    
  },
  {
    path: "*",
    element: <NotFound/>,    
  }
];

export default AppRoutes;
