import React, { useEffect, useState, useCallback } from 'react'; // Adaugă useCallback
import axios from 'axios';
import * as XLSX from 'xlsx'; // Importă biblioteca xlsx
import '../css/TimetableComponent.css'; // Asigură-te că acest CSS există și este corect
import { API_URL } from '../services/api';
import { useAuth } from '../context/AuthContext'; // Presupun că vrei să folosești token-ul de aici

interface TimetableProps {
  facultyId: number | null;
}

interface TimetableSlot {
  subject: string;
  professor: string;
  room: string;
  // Am eliminat 'group' de aici, deoarece orarul este deja per 'groupName'
  // Dacă 'group' este diferit în cadrul unui 'groupTimetable', trebuie ajustat
}

interface TimetableGrid {
  // Structura ta originală era: { [key: number]: TimetableSlot[] }
  // Acest lucru ar însemna că weekdaySlots este un obiect unde cheile sunt numere (0-4 pentru zile)
  // și valorile sunt array-uri de sloturi pentru acea zi.
  // Pentru maparea directă în grilă, un array de array-uri e mai comun:
  // weekdaySlots: (TimetableSlot | null | undefined)[][]; // [dayIndex][hourIndex]
  // Voi adapta codul pentru ambele posibilități, dar prefer array-ul de array-uri.
  // Voi folosi varianta ta originală și o voi adapta în funcția de export.
  weekdaySlots: { [dayIndex: number]: (TimetableSlot | null | undefined)[] };
}

interface GroupTimetable {
  groupName: string;
  timetable: TimetableGrid;
}

const HOURS_PER_DAY = 6; // Presupunem 6 sloturi de 2 ore

const TimetableComponent: React.FC<TimetableProps> = ({ facultyId }) => {
  const [timetables, setTimetables] = useState<GroupTimetable[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');
  const { token } = useAuth(); // Preia token-ul pentru apelurile API

  const loadTimetableData = useCallback(async () => {
    if (!facultyId) {
      setError('ID-ul facultății nu este specificat.');
      setLoading(false);
      setTimetables([]);
      return;
    }
    if (!token) {
      setError('Utilizatorul nu este autentificat.'); // Sau redirecționează la login
      setLoading(false);
      setTimetables([]);
      return;
    }

    setLoading(true);
    setError('');
    try {
      const response = await axios.get(`${API_URL}/timetable/${facultyId}`, { // Am ajustat endpoint-ul
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      // Verifică structura response.data. Ar trebui să fie GroupTimetable[]
      if (Array.isArray(response.data)) {
        setTimetables(response.data);
      } else {
        // Dacă API-ul returnează un singur orar sau o structură diferită, trebuie ajustat aici
        console.warn("Formatul datelor primite de la API nu este un array de GroupTimetable:", response.data);
        // setTimetables([response.data]); // Exemplu dacă API-ul returnează un singur GroupTimetable
        setError('Formatul datelor orarului este neașteptat.');
        setTimetables([]);
      }
    } catch (err: any) {
      console.error('Failed to fetch timetable data:', err.response ? err.response.data : err.message);
      setError('Nu s-au putut încărca datele orarului.');

    } finally {
      setLoading(false);
    }
  }, [facultyId, token]);

  useEffect(() => {
    loadTimetableData();
  }, [loadTimetableData]);

  const dayNames = ['Luni', 'Marți', 'Miercuri', 'Joi', 'Vineri'];

  const formatHourForDisplay = (hourIndex: number): string => {
    const startHour = 8 + hourIndex * 2;
    const endHour = 10 + hourIndex * 2;
    return `${String(startHour).padStart(2, '0')}:00 - ${String(endHour).padStart(2, '0')}:00`;
  };

  const exportToExcel = () => {
    if (!timetables || timetables.length === 0) {
      alert("Nu există date de orar pentru export.");
      return;
    }

    const wb = XLSX.utils.book_new();

    timetables.forEach((groupTimetable) => {
      const ws_data: (string | null)[][] = [];


      const headerRow: string[] = ["Ora"];
      dayNames.forEach(day => headerRow.push(day));
      ws_data.push(headerRow);


      for (let hourIndex = 0; hourIndex < HOURS_PER_DAY; hourIndex++) {
        const row: (string | null)[] = [formatHourForDisplay(hourIndex)];

        for (let dayIndex = 0; dayIndex < dayNames.length; dayIndex++) {
          const daySlots = groupTimetable.timetable.weekdaySlots[dayIndex];
          const slot = daySlots ? daySlots[hourIndex] : null;

          if (slot) {
            row.push(`Materie: ${slot.subject}\n Profesor: ${slot.professor}\n Sala:  (${slot.room})`);
          } else if (!slot) {
            row.push("-");
          }
        }
        ws_data.push(row);
      }

      const ws = XLSX.utils.aoa_to_sheet(ws_data);

      const columnWidths = [
        { wch: 18 },
        ...dayNames.map(() => ({ wch: 35 }))
      ];
      ws['!cols'] = columnWidths;

      const safeSheetName = groupTimetable.groupName.replace(/[\/\\?*[\]]/g, '').substring(0, 30);
      XLSX.utils.book_append_sheet(wb, ws, safeSheetName || `Orar_${timetables.indexOf(groupTimetable) + 1}`);
    });

    XLSX.writeFile(wb, "Orare_Facultate.xlsx");
  };

  if (loading) return <p>Se încarcă orarul...</p>;
  if (error) return <p style={{ color: 'red' }}>{error}</p>;
  if (!timetables || timetables.length === 0) return <p>Nu există orare de afișat pentru această facultate.</p>;

  return (
    <div className="timetable-export-page-container"> {/* Un container general pentru pagină */}
      <button
        onClick={exportToExcel}
        disabled={!timetables || timetables.length === 0}
        style={{
          display: 'block',
          margin: '0 auto 20px auto', // Centrează butonul și adaugă spațiu sub el
          padding: '10px 20px',
          fontSize: '1rem',
          cursor: 'pointer'
        }}
      >
        Exportă Toate Orarele în Excel
      </button>

      <div className="timetable-container"> {/* Containerul pentru lista de orare */}
        {timetables.map((groupTimetable) => (
          // Am schimbat div-ul exterior cu li pentru coerență semantică, presupunând că timetable-container este un ul
          // Dacă timetable-container nu este ul, poți păstra div
          <div key={groupTimetable.groupName} className="group-timetable-item">
            <h2 className="timetable-title"> {groupTimetable.groupName}</h2>
            <div className="timetable-grid">
              {/* Header Row */}
              <div className="header-cell"></div> {/* Colț gol */}
              {dayNames.map((day, index) => (
                <div key={index} className="header-cell">{day}</div>
              ))}

              {/* Rows by hour */}
              {Array.from({ length: HOURS_PER_DAY }).map((_, hourIndex) => (
                <React.Fragment key={hourIndex}>
                  {/* Hour label */}
                  <div className="time-cell">
                    <p>{formatHourForDisplay(hourIndex)}</p>
                  </div>

                  {/* Slots for each day at this hour */}
                  {dayNames.map((_, dayIndex) => {
                    // Adaptare pentru structura ta originală weekdaySlots
                    const daySlots = groupTimetable.timetable.weekdaySlots[dayIndex];
                    const slot = daySlots ? daySlots[hourIndex] : null;

                    return (
                      <div key={dayIndex} className="timetable-cell">
                        {slot ? (
                          <>
                            <div>{slot.subject}</div>
                            <div>{slot.professor}</div>
                            <div className="classroom">{slot.room}</div>
                          </>
                        ) : (
                          <div>—</div>
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
    </div>
  );
};

export default TimetableComponent;