import { Component } from 'react';

type EditableFieldComponentProps = {
    save: (value: string) => void;
    data: string;
}

type EditableFieldComponentState = {
    isEditing: boolean;
    value: string;
}

export default class EditableFieldComponent extends Component<EditableFieldComponentProps, EditableFieldComponentState>{
    constructor(props: EditableFieldComponentProps) {
        super(props);
        this.state = { isEditing: false, value: this.props.data }
    }

    handleStartEdit = () => {
        this.setState({ isEditing: true });
    }

    handleValueChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        this.setState({ value: e.target.value });
    }

    handleEndEdit = () => {
        this.setState({ isEditing: false });
        this.props.save(this.state.value);
    }

    render() {
        
            
        const content = this.state.isEditing ?
            <input className='form-control d-inline' type='text' value={this.state.value} onChange={this.handleValueChange} onBlur={this.handleEndEdit} /> :
            <span onClick={this.handleStartEdit}>
                {this.state.value}
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil ms-2" viewBox="0 0 16 16">
                <path d="M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207 11.207 2.5zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293l6.5-6.5zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z" />
            </svg>
            </span>;
        return (
            <span>{content}</span>
        );
    }
}