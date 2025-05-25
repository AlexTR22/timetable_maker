// SessionContext.tsx
import React, {
  createContext,
  useContext,
  useState,
  ReactNode,
  //Dispatch,
  //SetStateAction,
} from "react";


export interface User {
  id: string;         
  username: string;
  universityId: number;
  role?: string;
}

interface SessionContextType {
  user: User | null;
  login: (userInfo: User) => void;
  logout: () => void;
}

const SessionContext = createContext<SessionContextType | undefined>(undefined);

export const useSession = (): SessionContextType => {
  const ctx = useContext(SessionContext);
  if (!ctx) {
    throw new Error("useSession must be used inside a SessionProvider");
  }
  return ctx;
};

interface ProviderProps {
  children: ReactNode;
}

export const SessionProvider: React.FC<ProviderProps> = ({ children }) => {
  /* user */
  const [user, setUser] = useState<User | null>(() => {
    const saved = localStorage.getItem("user");
    return saved ? (JSON.parse(saved) as User) : null;
  });

  /* auth */
  const login = (userInfo: User) => {
    setUser(userInfo);
    localStorage.setItem("user", JSON.stringify(userInfo));
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem("user");
  };

  /* context value */
  const value: SessionContextType = {
    user,
    login,
    logout,
  };

  return (
    <SessionContext.Provider value={value}>
        {children}
    </SessionContext.Provider>
  );
};
