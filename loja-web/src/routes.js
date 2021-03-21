import React from 'react'
import { BrowserRouter, Route, Switch } from 'react-router-dom'
import ProductList from './pages/Product/List';
import ProductSave from './pages/Product/Save';

export default function Routes() {
    return (
        <BrowserRouter>
            <Switch>
            <Route path="/produto/:id" component={ProductSave}></Route>
            <Route path="/produto" component={ProductSave}></Route>
                <Route path="/" component={ProductList}></Route>
            </Switch>
        </BrowserRouter>
    );
}