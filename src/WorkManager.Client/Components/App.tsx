import React, { Component } from 'react';

interface IAppProps {}
interface IAppState {
    data: string[];
}

class App extends Component<Readonly<IAppProps>, IAppState> {
    public constructor(props: Readonly<IAppProps>) {
        super(props);

        this.state = {
            data: []
        };
    }

    public componentDidMount(): void {
        fetch('http://localhost:5000/api/values')
            .then((response): Promise<string[]> => response.json())
            .then((data): void => this.setState({ data }));
    }

    public render(): JSX.Element {
        console.log(this.state.data);
        return <div>test</div>;
    }
}

export default App;
