import React, { Component } from 'react';

export default class TaskAction extends Component {
    static displayName = TaskAction.name;
    
    constructor(props) {
        super(props);
    }

    render() {
        let content = 
            this.props.status === 'canceled' ? 
            <p></p> : 
            this.props.status === 'started'? 
            <button className="btn btn-danger" onClick={()=>{this.props.cancelAction(this.props.value)}}>Cancel</button> :
            <button className="btn btn-primary" onClick={()=>{this.props.startAction(this.props.value)}}>Start</button>;
        
        return (           
            <div> {content}</div>           
        );
    }
}