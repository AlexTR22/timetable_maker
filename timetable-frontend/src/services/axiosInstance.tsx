import axios from 'axios';
//import { useAuth } from '../context/AuthContext';

const axiosInstance = axios.create({ baseURL: 'http://localhost:5261' });

// Interceptor to attach JWT
axiosInstance.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers = config.headers ?? {};
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
});

export default axiosInstance;