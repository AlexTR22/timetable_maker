import React, { useState } from 'react';
import { Room } from '../models/RoomModel';
import axios from 'axios';
import { API_URL } from '../services/api';

interface EditRoomProps {
  room: Room;
  onClose: () => void;
}

const EditRoom: React.FC<EditRoomProps> = ({ room, onClose }) => {
  const [name, setName] = useState(room.name);
  const [capacity, setCapacity] = useState<number>(room.capacity);

  const handleEdit = async () => {
    const updatedRoom: Room = { ...room, name, capacity };
    await axios.put(`${API_URL}/room/${room.id}`, updatedRoom);
    onClose();
  };

  return (
    <div className='modal'>
      <div className='modal-content'>
        <h3>Editează Sală</h3>
        <input type="text" value={name} onChange={e => setName(e.target.value)} />
        <input type="number" value={capacity} onChange={e => setCapacity(Number(e.target.value))} />
        <button onClick={handleEdit}>Salvează</button>
        <button onClick={onClose}>Renunță</button>
      </div>
    </div>
  );
};

export default EditRoom;
