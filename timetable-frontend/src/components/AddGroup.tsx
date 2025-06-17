import React, { useState } from 'react';
import axios from 'axios';
import { API_URL } from '../services/api';
import { Group } from '../models/GroupModel';
import "../css/AddEditComponents.css"

interface AddGroupProps {
  facultyId: number | null;
  onClose: () => void;
}

export const AddGroup: React.FC<AddGroupProps> = ({ facultyId, onClose }) => {
  const [name, setName] = useState('');
  const [year, setYear] = useState(0);
  const collegeId =facultyId ?? 0;
  const handleSubmit = async () => {
    const group: Group =
    {
      id: 0,
      name: name,
      year: year,
      collegeId: collegeId  
    }
    await axios.post(`${API_URL}/group`, group);
    onClose();
  };

  return (
    <div className="modal">
      <div className='modal-content'>
        <h3>Adaugă Grupă</h3>
        <input
          type="text"
          placeholder="Nume grupă"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <input
          type="number"
          placeholder="An grupă"
          value={year}
          onChange={(e) => setYear(Number(e.target.value))}
        />
        <button onClick={handleSubmit}>Salvează</button>
        <button onClick={onClose}>Anulează</button>
      </div>
    </div>
  );
};
