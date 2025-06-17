import React, { useCallback, useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';
import { API_URL } from '../services/api';
import { Subject } from '../models/SubjectModel';
import { AddSubject } from '../components/AddSubject';
import { EditSubject } from '../components/EditSubject';
import '../css/DataPage.css'

const SubjectPage: React.FC = () => {
  const { universityId } = useAuth(); 
  const [subjects, setSubjects] = useState<Subject[]>([]);
  const [showAddModal, setShowAddModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedSubject, setSelectedSubject] = useState<Subject | null>(null);
  const { token } = useAuth();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  const loadSubjects = useCallback(async () => {
    if (!universityId) { 
        setSubjects([]);
        return;
    }
    try {
        const response = await axios.get(`${API_URL}/subject/${universityId}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        setSubjects(response.data);
    } catch (error) {
        console.error("Error loading subjects:", error);
    }
}, [universityId, token]); 

useEffect(() => {
    loadSubjects();
}, [loadSubjects]); 
  const handleDelete = async (id: number) => {
    await axios.delete(`${API_URL}/subject/${id}`)
      .catch(error => alert(error));
    loadSubjects();
  };

  return (
    <div className="data-container">
      <h2 className="blue-text">Materii</h2>
      <button onClick={() => setShowAddModal(true)}>Adaugă Materie</button>

      <ul>
        {subjects.map((subj) => (
          <li
            key={subj.id}
            className={selectedSubject?.id === subj.id ? 'selected' : ''}
            onClick={() => setSelectedSubject(subj)}
          >
            {subj.name}
            {selectedSubject?.id === subj.id && (
              <span className="actions">
                <button onClick={() => setShowEditModal(true)}>Edit</button>
                <button onClick={() => handleDelete(subj.id)}>Delete</button>
              </span>
            )}
          </li>
        ))}
      </ul>

      {showAddModal && (
        <AddSubject
          facultyId={universityId}
          onClose={() => {
            setShowAddModal(false);
            loadSubjects();
          }}
        />
      )}

      {showEditModal && selectedSubject && (
        <EditSubject
          subject={selectedSubject}
          onClose={() => {
            setShowEditModal(false);
            loadSubjects();
          }}
        />
      )}
    </div>
  );
};

export default SubjectPage;
