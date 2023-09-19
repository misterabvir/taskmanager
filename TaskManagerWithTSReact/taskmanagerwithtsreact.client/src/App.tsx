import { BrowserRouter as Router, Route } from 'react-router-dom';

import ProjectData from './Components/ProjectData';

function App() {
  return (
    <Router>
      <Route path="/" Component={ProjectData} />
      <Route path="task-detail/:id" Component={ProjectData} />
      <Route path="project-detail/:id" Component={ProjectData} />
    </Router>
  );
}

export default App;