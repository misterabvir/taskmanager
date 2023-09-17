import React, { Component } from 'react';

export class NotFound extends Component {
  static displayName = NotFound.name;

  constructor(props) {
    super(props);
  }

  render() {

    return (
      <div>
        <h2>404 - Not Found</h2>
        <p>Sorry, the page you're looking for doesn't exist.</p>
      </div>
    );
  }
}