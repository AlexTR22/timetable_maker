import React, { createContext, useState, useEffect, useContext } from 'react';
import { jwtDecode } from 'jwt-decode';

interface AuthContextProps {
  token: string | null;
  role: 'admin' | 'client' | null;
  universityId: number | null;
  login: (t: string, uniId: number | null) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextProps>({
  token: null,
  role: null,
  universityId: null,
  login: () => { },
  logout: () => { }
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
  const [role, setRole] = useState<'admin' | 'client' | null>(null);
  const [universityId, setUniversityId] = useState<number | null>(Number(localStorage.getItem("universityId")));

  useEffect(() => {
    if (token) {
      localStorage.setItem('token', token);
      const decoded: any = jwtDecode(token);
      setRole(decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);
    } else {
      localStorage.removeItem('token');
      setRole(null);
    }
  }, [token]);

  useEffect(() => {
    if (universityId) {
      localStorage.setItem('universityId', universityId.toString());
    } else {
      localStorage.removeItem('universityId');
    }
  }, [universityId]);

  const login = (t: string, uniId: number | null = null) => {
    setToken(t);
    if (uniId !== null) setUniversityId(uniId);
  };
  const logout = () => {
    setToken(null);
    setUniversityId(null);
  };

  return (
    <AuthContext.Provider value={{ token, role, universityId, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
