import React, { Component } from 'react';
import { Route, Switch } from 'react-router';
import { Layout } from './components/Layout';
import { Revendedor } from './components/Revendedor';
import { Compra } from './components/Compra';
import { Login } from './components/Login';
import { Inicio } from './components/Inicio';


export default class App extends Component {
    static displayName = App.name;
    render() {
        return (
            <Layout>
                <Switch>
                    <Route exact={true} path='/' component={Login} />
                    <Route path='/Revendedor' component={Revendedor} />
                    <Route path='/Compra' component={Compra} />
                    <Route path='/Inicio' component={Inicio} />
                </Switch>
            </Layout>
        );
    }
}
