import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:50707/api/'
})

export default api;