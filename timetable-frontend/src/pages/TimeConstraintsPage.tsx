import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';
import { API_URL } from '../services/api';
import { TimeConstraint } from '../models/TimeConstraintModel';
import '../css/DataPage.css'
import { EditTimeConstraint } from '../components/EditTimeConstraint';
import { AddTimeConstraint } from '../components/AddTimeConstraint';

const TimeConstraintPage: React.FC = () => {
  const {universityId} = useAuth();
  const [constraints, setConstraints] = useState<TimeConstraint[]>([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedConstraint, setSelectedConstraint] = useState<TimeConstraint | null>(null);
  const { token } = useAuth();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  const loadConstraints = async () => {
    await axios.get(`${API_URL}/timeconstraints/${universityId}`)
      .then(response => setConstraints(response.data))
      .catch(error => alert(error));
  };

  useEffect(() => {
    loadConstraints();  
  }, [universityId]);

  const handleDelete = async (id: number) => {
    await axios.delete(`${API_URL}/timeconstraints/${id}`)
      .catch(error => alert(error));
    loadConstraints();
  };

  return (
    <div className="data-container">
      <h2 className="blue-text">Constrângeri de Timp</h2>
      <button onClick={() => setShowAddModal(true)}>Adaugă Constrângere</button>

      <ul>
        {constraints.map((constr) => (
          <li
            key={constr.id}
            className={selectedConstraint?.id === constr.id ? 'selected' : ''}
            onClick={() => setSelectedConstraint(constr)}
          >
            {constr.day} - {constr.fromHour}:00 - {constr.toHour}:00
            {selectedConstraint?.id === constr.id && (
              <span className="actions">
                <button onClick={() => setShowEditModal(true)}>Edit</button>
                <button onClick={() => handleDelete(constr.id)}>Delete</button>
              </span>
            )}
          </li>
        ))}
      </ul>

       {showAddModal && (
        <AddTimeConstraint
          facultyId={universityId}
          onClose={() => {
            setShowAddModal(false);
            loadConstraints();
          }}
        />
      )}

      {showEditModal && selectedConstraint && (
        <EditTimeConstraint
          constraint={selectedConstraint}
          onClose={() => {
            setShowEditModal(false);
            loadConstraints();
          }}
        />
      )} 
    </div>
  );
};

export default TimeConstraintPage;
