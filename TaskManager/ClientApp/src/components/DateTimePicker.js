import React, { useState } from 'react';
import Datetime from 'react-datetime';
import 'react-datetime/css/react-datetime.css';

function DateTimePicker(props) {
  const [selectedDate, setSelectedDate] = useState(props.datetime);
  const handleDateChange = (date) => {
    setSelectedDate(date);
    props.timeChange(date);
  };

  return (         
      <Datetime
        value={selectedDate}
        onChange={handleDateChange}
        inputProps={{ placeholder: props.placeHolder }}
      />   
  );
}

export default DateTimePicker;