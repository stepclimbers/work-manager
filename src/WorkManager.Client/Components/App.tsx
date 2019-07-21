import React, { Component } from "react";

class App extends Component<any, any> {
  constructor(props: any) {
    super(props);

    this.state = {
      data: null,
    };
  }

  componentDidMount() {
    fetch("http://localhost:52257/api/values")
      .then(response => response.json())
      .then(data => this.setState({ data }));
  }

  render() {
    console.log(this.state.data);
    return <div>test</div>;
  }
}

export default App;
