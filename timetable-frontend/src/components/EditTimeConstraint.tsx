import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { API_URL } from '../services/api';
import { TimeConstraint } from '../models/TimeConstraintModel';
import '../css/AddEditComponents.css';
import { Professor } from '../models/ProfessorModel';

interface EditTimeConstraintProps {
  constraint: TimeConstraint;
  onClose: () => void;
}

export const EditTimeConstraint: React.FC<EditTimeConstraintProps> = ({ constraint, onClose }) => {
  const [day, setDay] = useState(constraint.day);
  const [startHour, setStartHour] = useState(constraint.fromHour);
  const [endHour, setEndHour] = useState(constraint.toHour);
  const [professorName, setProfessorName] = useState('');
  const id = constraint.professorId;

  useEffect(() => {
    const getProfessorName = async () => {
      try {
        const res = await axios.get<{ professor: Professor }>(`${API_URL}/professor/getProf/${id}`);
        setProfessorName(res.data.professor.name);

      } catch (error) {
        console.error("Eroare la obținerea profesorului:", error);
      }
    };

    getProfessorName();
  }, [constraint]);

  const handleSubmit = async () => {
    const res = await axios.get(`${API_URL}/professor/${encodeURIComponent(professorName)}`);
    constraint.day = day;
    constraint.fromHour = startHour;
    constraint.toHour = endHour;
    constraint.professorId = res.data.professorId;
    await axios.put(`${API_URL}/timeconstraints/${constraint.id}`, constraint);
    onClose();
  };

  return (
    <div className="modal">
      <div className='modal-content'>
        <h3>Editează Constrângere</h3>
        <input
          type="number"
          value={day}
          onChange={(e) => setDay(Number(e.target.value))}
        />
        <input
          type="number"
          value={startHour}
          onChange={(e) => setStartHour(Number(e.target.value))}
        />
        <input
          type="number"
          value={endHour}
          onChange={(e) => setEndHour(Number(e.target.value))}
        />
        <input
          type="text"
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
