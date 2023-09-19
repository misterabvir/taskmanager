import { Component } from 'react';

type ProjectNewNameProps = {
  onCreated: (name: string) => void;
}

type ProjectNewNameState = {
  name: string;
}

export default class ProjectNewNameComponent extends Component<ProjectNewNameProps, ProjectNewNameState> {
  constructor(props: ProjectNewNameProps) {
    super(props)
    this.state = { name: "" };
  }

  handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    this.setState({ name: e.target.value });
  }

  handleSend = () => {
    this.props.onCreated(this.state.name);
    this.setState({ name: "" });
  }

  render() {
    return (
      <div className='row'>
        <div className="col-auto">
          <button className='btn btn-primary' onClick={this.handleSend} disabled={this.state.name === ""}>Create</button>
        </div>
        <div className="col">
          <input className='form-control' placeholder='type new name' type='text' value={this.state.name} onChange={(e) => this.handleChange(e)} />     
        </div>
      </div>
    );
  }
}