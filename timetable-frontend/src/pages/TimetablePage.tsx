//import React from "react";
import TimetableComponent from "../components/TimetableComponent";


function TimetablePage()  {
  

  return (
    <div className="p-4">
      <h1 className="text-3xl font-bold text-center mb-8">University Timetables</h1>

      <div>
        <TimetableComponent/>
      </div>
    </div>
  );
};

export default TimetablePage;
