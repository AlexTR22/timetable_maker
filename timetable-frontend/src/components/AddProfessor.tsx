
// File: src/components/AddProfessorModal.tsx
import React, { useState } from 'react';
//import { addProfessor } from '../services/api';
import '../css/AddEditComponents.css';
import axios from 'axios';
import { API_URL } from '../services/api';
import { CreateProfessor } from '../models/ProfessorModel';

type Props = {
    facultyId: number | null;
    onClose: () => void;
};

const AddProfessor: React.FC<Props> = ({ facultyId, onClose }) => {
    const [name, setName] = useState('');
    const collegeId =facultyId ?? 0;

    const handleSubmit = async () => {
        const professor: CreateProfessor ={
            name: name,
            collegeId: collegeId,
        }
        await axios.post(`${API_URL}/professor`, professor);
        onClose();
    };

    return (
        <div className="modal">
            <div className="modal-content">
                <h3>Adaugă Profesor</h3>
                <input
                    type="text"
                    placeholder="Nume profesor"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <button onClick={handleSubmit}>Salvează</button>
                <button onClick={onClose}>Anulează</button>
            </div>
        </div>
    );
};

export default AddProfessor;