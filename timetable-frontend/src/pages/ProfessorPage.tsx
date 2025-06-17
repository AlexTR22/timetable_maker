import React, { useEffect, useState } from 'react';
//import { useParams } from 'react-router-dom';
import { Professor } from '../models/ProfessorModel';
import AddProfessor from '../components/AddProfessor';
import EditProfessor from '../components/EditProfessor';
import '../css/DataPage.css';
import { API_URL } from '../services/api';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';
import { Navigate } from 'react-router-dom';

const ProfessorsPage: React.FC = () => {
  const {universityId}=useAuth();
  const [professors, setProfessors] = useState<Professor[]>([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedProfessor, setSelectedProfessor] = useState<Professor | null>(null);
  const { token } = useAuth();
  
  if (token === null) {
    
    return <Navigate to="/login" replace />;
  }
  const loadProfessors = async () => {
      await axios.get(`${API_URL}/professor/${universityId}`) // aici nu e bine inca o sa trebuiasca sa fie cu slash nu cu semnu intrebarii
      .then(response => {
        setProfessors(response.data);
      })
      .catch(error => {
        
        alert(error);
      });
  };

  useEffect(() => {
    loadProfessors();
  }, [universityId]);

  const handleDelete = async (id: number) => {
    await axios.delete(`${API_URL}/professor/${id}`)
      .catch(error => {
        alert(error);
      });
    loadProfessors();
  };

  return (
    <div className="data-container">
      <h2 className="blue-text">Profesori</h2>
      <button onClick={() => setShowAddModal(true)}>Adaugă Profesor</button>

      <ul>
        {professors.map((prof) => (
          <li
            key={prof.id}
            className={selectedProfessor?.id === prof.id ? 'selected' : ''}
            onClick={() => {setSelectedProfessor(prof);}}
          >
            {prof.name}
            {selectedProfessor?.id === prof.id && (
              <span className="actions">
                <button onClick={() => setShowEditModal(true)}>Edit</button>
                <button onClick={() => handleDelete(prof.id)}>Delete</button>
              </span>
            )}
          </li>
        ))}
      </ul>

      {showAddModal && (
        <AddProfessor
          facultyId={universityId}
          onClose={() => {
            setShowAddModal(false);
            loadProfessors();
          }}
        />
      )}

      {showEditModal && selectedProfessor && (
        <EditProfessor
          professor={selectedProfessor}
          onClose={() => {
            setShowEditModal(false);
            loadProfessors();
          }}
        />
      )}
    </div>
  );
};

export default ProfessorsPage;