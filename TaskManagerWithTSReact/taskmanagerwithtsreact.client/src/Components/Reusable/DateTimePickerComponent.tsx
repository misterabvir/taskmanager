import { Component } from 'react';
import Datetime from 'react-datetime';
import moment from 'moment';
import 'react-datetime/css/react-datetime.css';


type DateTimePickerProps = {
  message: string;
  selectedDate: Date | string;
  selectedDateChanged: (date: Date) => void;
}


export default class DateTimePicker extends Component<DateTimePickerProps> {
  constructor(props: DateTimePickerProps) {
    super(props)
    
  }

  handleDateChange(e: string | moment.Moment) {

    const date = this.convertToType(e)
    this.props.selectedDateChanged(date);
  }

  convertToType(value: string | moment.Moment): Date {
    if (moment.isMoment(value)) {
      return value.toDate();
    } else if (typeof value === 'string') {
      const date = new Date(value);
      if (!isNaN(date.getTime())) {
        return date;
      }
    }
    return new Date();
  }


  render() {
    return (
      <Datetime
        value={this.props.selectedDate}
        onChange={(e) => this.handleDateChange(e)}
        inputProps={{ placeholder: this.props.message }}
      />
    );
  }

}

