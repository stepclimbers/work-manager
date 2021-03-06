import React, { Component } from 'react';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import Register from './user/Register';
import Login from './user/Login';
import Header from './navigation/Header';
import { Layout } from 'antd';

const { Content, Footer } = Layout;

interface IAppProps {}
interface IAppState {
    data: string[];
}

class App extends Component<Readonly<IAppProps>, IAppState> {
    constructor(props: Readonly<IAppProps>) {
        super(props);

        this.state = {
            data: []
        };
    }

    componentDidMount(): void {
        fetch('http://localhost:5000/api/values')
            .then((response): Promise<string[]> => response.json())
            .then((data): void => this.setState({ data }));
    }

    render(): JSX.Element {
        return (
            <Router>
                <Layout className="layout">
                    <Header />
                    <Content style={{ padding: '0 50px' }}>
                        <div style={{ background: '#fff', padding: 24, minHeight: 280 }}>
                            <Route path="/user/register" exact component={Register} />
                            <Route path="/user/login" exact component={Login} />
                        </div>
                    </Content>
                    <Footer style={{ textAlign: 'center' }}>©2019 Work Manager</Footer>
                </Layout>
            </Router>
        );
    }
}

export default App;
