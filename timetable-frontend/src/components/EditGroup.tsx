import React, { useState } from 'react';
import axios from 'axios';
import { API_URL } from '../services/api';
import { Group } from '../models/GroupModel';

interface EditGroupProps {
  group: Group;
  onClose: () => void;
}

export const EditGroup: React.FC<EditGroupProps> = ({ group, onClose }) => {
  const [name, setName] = useState(group.name);
  const [year, setYear] = useState(group.year);

  const handleSubmit = async () => {
    group.name = name;
    group.year = year;
    await axios.put(`${API_URL}/group/${group.id}`, group);
    onClose();
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h3>Editează Grupă</h3>
        <input
          type="text"
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
