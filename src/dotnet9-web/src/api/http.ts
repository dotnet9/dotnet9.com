import axios from 'axios';

const service = axios.create({
    baseURL: "http://localhost:5000/api",
    timeout: 15000,
});

export default service;