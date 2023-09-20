import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import ProjectData from './Components/ProjectDataPage/ProjectData';
import ProjectDetailPage from './Components/ProjectDetailPage/ProjectDetailPage';
import NotFoundPage from './Components/NotFoundPage/NotFoundPage';
import TaskDetailPage from './Components/TaskDetailPage/TaskDetailPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" Component={ProjectData} />
        <Route path="project-detail/:id" Component={ProjectDetailPage} />
        <Route path="task-detail/:id" Component={TaskDetailPage} />
        <Route path="*" Component={NotFoundPage} />
      </Routes>
    </Router>
  );
}

export default App;