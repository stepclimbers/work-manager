import React, { Component } from 'react';

interface LoginProps {}
interface LoginState {}

export default class Login extends Component<Readonly<LoginProps>, LoginState> {
    constructor(props: Readonly<LoginProps>) {
        super(props);
    }

    public render(): JSX.Element {
        return <div>Login</div>;
    }
}
