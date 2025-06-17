import React, { useState } from 'react';
import { Room } from '../models/RoomModel';
import axios from 'axios';
import { API_URL } from '../services/api';

interface AddRoomProps {
  facultyId: number | null;
  onClose: () => void;
}

const AddRoom: React.FC<AddRoomProps> = ({ facultyId, onClose }) => {
  const [name, setName] = useState('');
  const [capacity, setCapacity] = useState<number>(0);
  const collegeId =facultyId ?? 0;
  const handleAdd = async () => {
    alert(facultyId);
    const newRoom: Room = { id: 0, name, capacity, collegeId: collegeId };
    await axios.post(`${API_URL}/room`, newRoom);
    onClose();
  };

  return (
    <div className='modal'>
      <div className='modal-content'>
        <h3>Adaugă Sală</h3>
        <input type="text" placeholder="Nume" value={name} onChange={e => setName(e.target.value)} />
        <input type="number" placeholder="Capacitate" value={capacity} onChange={e => setCapacity(Number(e.target.value))} />
        <button onClick={handleAdd}>Adaugă</button>
        <button onClick={onClose}>Renunță</button>
      </div>
    </div>
  );
};

export default AddRoom;
