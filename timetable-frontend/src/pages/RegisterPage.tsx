
import { useState } from "react";
import "../css/RegisterPage.css";
import { useNavigate } from 'react-router-dom';
import { useSession } from '../context/SessionProvider';

function RegisterPage()
{
    const navigate = useNavigate();

    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const { login } = useSession();

    const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        try {
            
            const response = await fetch('http://localhost:5000/register', {
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
            }
           
        }
        catch (error) {
            throw(error);
        }
    };

    return (
        <div className="register-container">
            <form className="register-form" onSubmit={handleLogin}>
                <h1>Register</h1>
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
                {error && <p style={{ color: "red", fontWeight: "bold" }}>{error}</p>}
                <button type="submit">Signin</button>
                <label> - or -</label>
                <button onClick={() => navigate("/login")}>Login</button>
            </form>
        </div>
    );
}
export default RegisterPage;