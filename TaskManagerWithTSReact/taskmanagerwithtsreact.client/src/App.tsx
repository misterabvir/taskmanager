import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import ProjectData from './Components/Pages/ProjectDataPage/ProjectData';
import ProjectDetailPage from './Components/Pages/ProjectDetailPage/ProjectDetailPage';
import TaskDetailPage from './Components/Pages/TaskDetailPage/TaskDetailPage';
import NotFoundPage from './Components/Pages/NotFoundPage/NotFoundPage';
import { PAGE_404, PAGE_HOME, PAGE_PROJECT_DETAIL, PAGE_TASK_DETAIL } from './Utils/Const';

function App() {
  return (
    <Router>
      <Routes>
        <Route path={PAGE_HOME} Component={ProjectData} />
        <Route path={`${PAGE_PROJECT_DETAIL}:id`} Component={ProjectDetailPage} />
        <Route path={`${PAGE_TASK_DETAIL}:id`} Component={TaskDetailPage} />
        <Route path={PAGE_404} Component={NotFoundPage} />
      </Routes>
    </Router>
  );
}

export default App;