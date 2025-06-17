//import React from "react";
import { Navigate } from "react-router-dom";
import TimetableComponent from "../components/TimetableComponent";
import { useAuth } from "../context/AuthContext";
import '../css/TimetablePage.css'
import { useState } from "react";

function TimetablePage() {
  const { universityId } = useAuth();
  const { token } = useAuth();
  const [startGenerating, setStartGenerating] = useState(false);


  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return (
    <div className="timetable-page">
      <h1>Orar </h1>
      {!startGenerating&& (<button onClick={() => setStartGenerating(true)}> Porneste generare </button>)}
       {startGenerating && (
         <TimetableComponent facultyId={universityId} />
              
            )}
    </div>
  );
};

export default TimetablePage;
