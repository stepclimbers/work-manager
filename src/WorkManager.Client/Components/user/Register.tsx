import React, { Component } from 'react';

interface RegisterProps {}
interface RegisterState {}

export default class Register extends Component<Readonly<RegisterProps>, RegisterState> {
    constructor(props: Readonly<RegisterProps>) {
        super(props);
    }

    public render(): JSX.Element {
        return <div>Register</div>;
    }
}
