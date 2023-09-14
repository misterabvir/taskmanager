import React, { Component } from 'react';
import TaskAction from './TaskAction';

export default class TaskItem extends Component {
    static displayName = TaskItem.name;
    
    constructor(props) {
        super(props);
        this.cancelAction = this.cancelAction.bind(this);
        this.startAction = this.startAction.bind(this);
        this.cancelTask = this.cancelTask.bind(this);
        this.startTask = this.startTask.bind(this);
    }


    cancelAction(value){
        this.cancelTask(value);
    }

    startAction(value){
        this.startTask(value);
    }

    render() {
        let task = this.props.item;
        let index = this.props.index + 1;       
        return (           
            <tr>
             <td>{index}</td>
             <td>{task.inWork}</td>
             <td>{task.projectName}</td>
             <td>{task.taskName}</td>
             <td>{task.state}</td>
             <td>
                 <TaskAction status={task.state} cancelAction={this.cancelAction} startAction={this.startAction} value={task.id}/>
             </td>
         </tr>          
        );
    }

    async startTask(id) {       
        this.setState({ tasks: null, filteredTasks:null, loading: true });
        await fetch('startTask', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({
                Id: id
              })
        });
        this.props.reload();
    }

    async cancelTask(id) {       
        this.setState({ tasks: null, filteredTasks:null, loading: true });
        await fetch('cancelTask', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({
                Id: id
              })
        });
        this.props.reload();
    }
}