import React, { useEffect, useState } from 'react';
import axios from 'axios';
import '../css/TimetableComponent.css';

interface TimetableSlot {
  subject: string;
  professor: string;
  room: string;
  group: string;
}

interface TimetableGrid {
  weekdaySlots: { [key: number]: TimetableSlot[] };
}

interface GroupTimetable {
  groupName: string;
  timetable: TimetableGrid;
}

// Presupunem că avem 6 ore pe zi
const HOURS_PER_DAY = 6;

const TimetableComponent: React.FC = () => {
  const [timetables, setTimetables] = useState<GroupTimetable[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    axios.get('http://localhost:5261/timetable')
      .then(response => {
        setTimetables(response.data);
        setLoading(false);
      })
      .catch(error => {
        setError('Failed to fetch timetable data.');
        alert(error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading timetable...</p>;
  if (error) return <p>{error}</p>;

  const dayNames = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];

  return (
    <div className="timetable-container">
      {timetables.map((groupTimetable) => (
        <div key={groupTimetable.groupName}>
          <h2 className="timetable-title">{groupTimetable.groupName}</h2>
          <div className="timetable-grid">
            {/* Header Row */}
            <div className="header-cell"></div>
            {dayNames.map((day, index) => (
              <div key={index} className="header-cell">{day}</div>
            ))}

            {/* Rows by hour */}
            {Array.from({ length: HOURS_PER_DAY }).map((_, hourIndex) => (
              <React.Fragment key={hourIndex}>
                {/* Hour label */}
                <div className="time-cell"><p>{`${8 + hourIndex*2}:00` } - {`${10 + hourIndex*2}:00`}</p></div>

                {/* Slots for each day at this hour */}
                {dayNames.map((_, dayIndex) => {
                  const slot = groupTimetable.timetable.weekdaySlots[dayIndex]?.[hourIndex];

                  return (
                    <div key={dayIndex} className="timetable-cell">
                      {slot ? (
                        <>
                          <div>{slot.subject}</div>
                          <div>{slot.professor}</div>
                          <div className="classroom">{slot.room}</div>
                        </>
                      ) : (
                        <div style={{ opacity: 0.5 }}>—</div>
                      )}
                    </div>
                  );
                })}
              </React.Fragment>
            ))}
          </div>
        </div>
      ))}
    </div>
  );
};

export default TimetableComponent;
