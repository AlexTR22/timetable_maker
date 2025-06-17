import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';
import { API_URL } from '../services/api';
import { Room } from '../models/RoomModel';
import AddRoom from '../components/AddRoom';
import EditRoom from '../components/EditRoom';
import '../css/DataPage.css'

const RoomPage: React.FC = () => {
  const {universityId} = useAuth();
  const [rooms, setRooms] = useState<Room[]>([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedRoom, setSelectedRoom] = useState<Room | null>(null);
  const { token } = useAuth();

   if (token === null) {
      return <Navigate to="/login" replace />;
    }

  const loadRooms = async () => {
    await axios.get(`${API_URL}/room/${universityId}`)
      .then(response => setRooms(response.data))
      .catch(error => alert(error));
  };

  useEffect(() => {
    loadRooms();
  }, [universityId]);

  const handleDelete = async (id: number) => {
    await axios.delete(`${API_URL}/room/${id}`)
      .catch(error => alert(error));
    loadRooms();
  };

  return (
    <div className="data-container">
      <h2 className="blue-text">Săli</h2>
      <button onClick={() => setShowAddModal(true)}>Adaugă Sală</button>

      <ul>
        {rooms.map((room) => (
          <li
            key={room.id}
            className={selectedRoom?.id === room.id ? 'selected' : ''}
            onClick={() => setSelectedRoom(room)}
          >
            {room.name}
            {selectedRoom?.id === room.id && (
              <span className="actions">
                <button onClick={() => setShowEditModal(true)}>Edit</button>
                <button onClick={() => handleDelete(room.id)}>Delete</button>
              </span>
            )}
          </li>
        ))}
      </ul>

       {showAddModal && (
        <AddRoom
          facultyId={universityId}
          onClose={() => {
            setShowAddModal(false);
            loadRooms();
          }}
        />
      )}

      {showEditModal && selectedRoom && (
        <EditRoom
          room={selectedRoom}
          onClose={() => {
            setShowEditModal(false);
            loadRooms();
          }}
        />
      )} 
    </div>
  );
};

export default RoomPage;
