import React from 'react';
import Home from './Home'
import Layout from './Layout'
import Upload from './Upload'
import { Route, Routes } from 'react-router-dom';
import Generate from './Generate'

class App extends React.Component {

   

    render() {
        return (
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/upload' element={<Upload />} />
                    <Route exact path='/generate' element={<Generate />} />                 
                </Routes>              
            </Layout>
        );
    }
};

export default App;