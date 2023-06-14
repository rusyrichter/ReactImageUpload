﻿import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Home = () => {

    const [people, setPeople] = useState([]);

    useEffect(() => {
        const onLoadConfirmed = async () => {
            const { data } = await axios.get('/api/people/getData');
            setPeople(Object.values(data));
        }
        onLoadConfirmed();
    }, [])

    const onDeleteClick = async () => {
        await axios.post('/api/people/deleteall');
        const { data } = await axios.get('/api/people/getData');
        setPeople(Object.values(data));
    }

    return (
        <div className="container" style={{ marginTop: '60px' }}>
            <div className="row">
                <div className="col-md-6 offset-md-3 mt-5">
                    <button onClick={onDeleteClick} className="btn btn-danger btn-lg w-100">Delete All</button>
                </div>
            </div>
            <table className="table table-hover table-striped table-bordered mt-5">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map(p => (
                        <tr key={p.id}>
                            <td>{p.id}</td>
                            <td>{p.firstName}</td>
                            <td>{p.lastName}</td>
                            <td>{p.age}</td>
                            <td>{p.address}</td>
                            <td>{p.email}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
        )
}
export default Home;