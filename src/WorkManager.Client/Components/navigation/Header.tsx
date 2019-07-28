import React, { Component } from 'react';
import { Menu, Layout } from 'antd';
import { Link } from 'react-router-dom';

const { Header: AntHeader } = Layout;
interface HeaderProps {}
interface HeaderState {}

export default class Header extends Component<Readonly<HeaderProps>, HeaderState> {
    constructor(props: Readonly<HeaderProps>) {
        super(props);
    }

    render(): JSX.Element {
        return (
            <AntHeader>
                <div className="logo" />
                <Menu
                    theme="dark"
                    mode="horizontal"
                    defaultSelectedKeys={['1']}
                    style={{ lineHeight: '64px' }}>
                    <Menu.Item key="1">
                        <Link to="/user/register">Register</Link>
                    </Menu.Item>
                    <Menu.Item key="2">
                        <Link to="/user/login">Login</Link>
                    </Menu.Item>
                </Menu>
            </AntHeader>
        );
    }
}
