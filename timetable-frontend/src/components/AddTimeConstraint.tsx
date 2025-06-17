import React, { useState } from 'react';
import axios from 'axios';
import { API_URL } from '../services/api';
import '../css/AddEditComponents.css';
import { TimeConstraint } from '../models/TimeConstraintModel';
interface AddTimeConstraintProps {
  facultyId: number | null;
  onClose: () => void;
}

export const AddTimeConstraint: React.FC<AddTimeConstraintProps> = ({ facultyId, onClose }) => {
  const [day, setDay] = useState(0);
  const [startHour, setStartHour] = useState(0);
  const [endHour, setEndHour] = useState(0);
  const [professorName, setProfessorName] = useState('');
  const collegeId = facultyId ?? 0;

  const handleSubmit = async () => {
    const res = await axios.get(`${API_URL}/professor/${encodeURIComponent(professorName)}`);
    
    const timeConstraint: TimeConstraint = {
      id: 0,
      professorId: res.data.professorId,
      fromHour: startHour,
      toHour: endHour,
      day: day,
      collegeId: collegeId
    }

    await axios.post(`${API_URL}/timeconstraints`, timeConstraint).catch(err=>{alert(err)});
    onClose();
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h3>Adaugă Constrângere de Timp</h3>
        <input
          type="number"
          placeholder="Zi (ex: 0 for Monday, 1 for Thursday)"
          value={day}
          onChange={(e) => setDay(Number(e.target.value))}
        />
        <input
          type="number"
          placeholder="Ora de început"
          value={startHour}
          onChange={(e) => setStartHour(Number(e.target.value))}
        />
        <input
          type="number"
          placeholder="Ora de sfârșit"
          value={endHour}
          onChange={(e) => setEndHour(Number(e.target.value))}
        />
        <input
          
          placeholder="Profesor"
          value={professorName}
          onChange={(e) => setProfessorName(e.target.value)}
        />
        <button onClick={handleSubmit}>Salvează</button>
        <button onClick={onClose}>Anulează</button>
      </div>
    </div>
  );
};
