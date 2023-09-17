import { TaskData } from "./components/TaskData";
import { ProjectDetail } from "./components/ProjectDetail";
import { TaskDetail } from "./components/TaskDetail";

const AppRoutes = [
  {
    index: true,
    path: "/",
    element: <TaskData />
  },
  {
    path: "/project-detail/:id",
    element: <ProjectDetail />
  },
  {
    path: "/task-detail/:id",
    element: <TaskDetail />
  }
];

export default AppRoutes;
