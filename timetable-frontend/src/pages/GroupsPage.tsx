import React, { useEffect, useState } from "react";
import TimetableComponent from "../components/TimetableComponent";

// Sample data structure simulating what you'd get from the backend.
interface Schedule {
  type: "course" | "lab" | "seminar";
  subject: string;
  teacher: string;
  day: string;
  time: string;
  classroom: string; // Added classroom field
}

interface Group {
  groupName: string;
  schedule: Schedule[];
}

const GroupsPage: React.FC = () => {
  const [groups, setGroups] = useState<Group[]>([]);

  // Simulating fetching data from backend
  useEffect(() => {
    // Mocked API response with classroom information
    const fetchedGroups: Group[] = [
      {
        groupName: "CS101",
        schedule: [
          { type: "course", subject: "Math", teacher: "Dr. Smith", day: "Monday", time: "08–10", classroom: "Room 101" },
          { type: "lab", subject: "Physics Lab", teacher: "Mr. Johnson", day: "Wednesday", time: "10–12", classroom: "Lab 2" },
        ],
      },
      {
        groupName: "CS102",
        schedule: [
          { type: "course", subject: "Algorithms", teacher: "Dr. Brown", day: "Tuesday", time: "08–10", classroom: "Room 202" },
          { type: "seminar", subject: "Data Science", teacher: "Dr. White", day: "Thursday", time: "14–16", classroom: "Room 303" },
        ],
      },
    ];
    setGroups(fetchedGroups);
  }, []);

  return (
    <div className="p-4">
      <h1 className="text-3xl font-bold text-center mb-8">University Timetables</h1>

      <div>
        {groups.map((group) => (
          <div key={group.groupName} className="mb-8">
            <h2 className="text-2xl font-semibold mb-4">{group.groupName}</h2>
            {/* Pass the schedule as props to the TimetableComponent */}
            <TimetableComponent/>
          </div>
        ))}
      </div>
    </div>
  );
};

export default GroupsPage;
