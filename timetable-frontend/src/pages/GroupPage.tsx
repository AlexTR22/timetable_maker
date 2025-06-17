import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';
import { API_URL } from '../services/api';
import { Group } from '../models/GroupModel';
import { AddGroup } from '../components/AddGroup';
import { EditGroup } from '../components/EditGroup';
import "../css/DataPage.css"

const GroupPage: React.FC = () => {
   const {universityId}=useAuth();
  const [groups, setGroups] = useState<Group[]>([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedGroup, setSelectedGroup] = useState<Group | null>(null);
  const { token } = useAuth();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  const loadGroups = async () => {
    await axios.get(`${API_URL}/group/${universityId}`)
      .then(response => setGroups(response.data))
      .catch(error => alert(error));
  };

  useEffect(() => {
    loadGroups();
  }, [universityId]);

  const handleDelete = async (id: number) => {
    await axios.delete(`${API_URL}/group/${id}`)
      .catch(error => alert(error));
    loadGroups();
  };

  return (
    <div className="data-container">
      <h2 className="blue-text">Grupe</h2>
      <button onClick={() => setShowAddModal(true)}>Adaugă Grupă</button>

      <ul>
        {groups.map((group) => (
          <li
            key={group.id}
            className={selectedGroup?.id === group.id ? 'selected' : ''}
            onClick={() => setSelectedGroup(group)}
          >
            {group.name}
            {selectedGroup?.id === group.id && (
              <span className="actions">
                <button onClick={() => setShowEditModal(true)}>Edit</button>
                <button onClick={() => handleDelete(group.id)}>Delete</button>
              </span>
            )}
          </li>
        ))}
      </ul>

      {showAddModal && (
        <AddGroup
          facultyId={universityId}
          onClose={() => {
            setShowAddModal(false);
            loadGroups();
          }}
        />
      )}

      {showEditModal && selectedGroup && (
        <EditGroup
          group={selectedGroup}
          onClose={() => {
            setShowEditModal(false);
            loadGroups();
          }}
        />
      )}
    </div>
  );
};

export default GroupPage;
