import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Revendedor } from './components/Revendedor';
import { Compra } from './components/Compra';
import { Login } from './components/Login';



export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact={true} path='/' component={Login} />
                <Route path='/Revendedor' component={Revendedor} />
                <Route path='/Compra' component={Compra} />
            </Layout>
        );
    }
}
