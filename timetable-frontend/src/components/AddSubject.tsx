import React, { useState } from 'react';
import { Subject } from '../models/SubjectModel';
import axios from 'axios';
import { API_URL } from '../services/api';
import '../css/AddEditComponents.css'
interface AddSubjectProps {
  facultyId: number | null;
  onClose: () => void;
}


export const AddSubject: React.FC<AddSubjectProps> = ({ facultyId, onClose }) => {
  const [name, setName] = useState('');
  const [year, setYear] = useState(0);
  const [professorName, setProfessorName] = useState('');


  const collegeId =facultyId ?? 0;

  const handleSubmit = async () => {

    try {
      const res = await axios.get(`${API_URL}/professor/${encodeURIComponent(professorName)}`);
      if (res.data != null)
        if (res.data.professorId > 0) {
          const subject: Subject =
          {
            id: 0,
            name: name,
            year: year,
            collegeId: collegeId,
            professorId: res.data.professorId,
            semester: false,
          }
          await axios.post(`${API_URL}/subject`, subject).catch(err => { alert(err) });
        }
        else {
          throw ("No valid professor");
        }

    }
    catch (err) {
      alert(err);
    }
    onClose();
  };

  return (
    <div className="modal">
      <div className='modal-content'>
        <h3>Adaugă Materie</h3>
        <input value={name} onChange={(e) => setName(e.target.value)} placeholder="Nume" />
        <input type="number" value={year} onChange={(e) => setYear(Number(e.target.value))} placeholder="An" />
        <input value={professorName} onChange={(e) => setProfessorName(e.target.value)} placeholder="Nume Profesor" />
        <button onClick={handleSubmit}>Salvează</button>
        <button onClick={onClose}>Anulează</button>
      </div>
    </div>
  );
};
