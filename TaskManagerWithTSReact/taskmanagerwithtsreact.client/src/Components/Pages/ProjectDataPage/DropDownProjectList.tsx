import { Component } from 'react';
import { ProjectViewModel } from "../../../ViewModels/ProjectViewModel";
import { GUID_EMPTY } from '../../../Utils/Const'

type DropDownProjectListProps = {
  projects: ProjectViewModel[] | undefined;
  onSelectChange: (id: string, name: string) => void;
}



export default class DropDownProjectList extends Component<DropDownProjectListProps> {
  constructor(props: DropDownProjectListProps) {
    super(props)

  }

  handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const id: string = e.target.value;
    const name: string | undefined = this.props.projects?.find(p => p.projectId === id)?.projectName ?? "All";
    this.props.onSelectChange(id, name);
  }

  render() {
    const content = this.props.projects == undefined ?
      <option>None</option> :
      this.props.projects.map(project => <option key={project.projectId} value={project.projectId}> {project.projectName} </option>);
    return (
      <div className='row'>
        <div className="col-auto">
          <label htmlFor="dropDownProjectList"> Select Project</label>
        </div>
        <div className="col">
          <select
            className='form-control'
            id='dropDownProjectList'
            placeholder='select'
            onChange={(e) => this.handleSelectChange(e)}>
            <option value={GUID_EMPTY}>All</option> :
            {content}
          </select>
        </div>
      </div>
    );
  }
}