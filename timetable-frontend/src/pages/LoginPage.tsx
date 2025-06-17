
import { useState } from "react";
import "../css/LoginPage.css";
import { useNavigate } from 'react-router-dom';

import axiosApi from '../services/axiosInstance';
import { useAuth } from '../context/AuthContext';
import { API_URL } from "../services/api";
import axios from "axios";

function LoginPage()
{
    const navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const { login } = useAuth();
    const { role } = useAuth();
    
    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            const res = await axios.post(`${API_URL}/authentification/login`, { email, username, password });
            if(res.data.token as string!=null)
            {
                login(res.data.token as string, res.data.universityId as number);
                navigate('/homePage');
            }

            /* const response = await fetch('http://localhost:5000/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ username, email, password })
            });
            
            const data=await response.json();
            if (!response.ok) {
                setError(data.message);
            }
            else
            {
                login(data);
                navigate(`/mainpPage`);
            } */
           
        }
        catch (error) {
            setError("Nume, Email sau Parola incorectă!");
            
        }
    };

    return (
        <div className="login-container">
            <form className="login-form" onSubmit={handleLogin}>
                <h1>Login</h1>
                <input
                    type="text"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                />
                <button type="submit">Login</button>
                 {error && <p style={{ color: "red", fontWeight: "bold" }}>{error}</p>}
               {/* <label> - or -</label>
                <button onClick={() => navigate("/register")}>Signin</button> */}
            </form>
        </div>
    );
};

export default LoginPage;