
// File: src/components/EditProfessorModal.tsx
import React, { useState } from 'react';
//import { updateProfessor } from '../services/api';
import { Professor } from '../models/ProfessorModel';
import '../css/AddEditComponents.css';
import axios from 'axios';
import { API_URL } from '../services/api';

type Props = {
    professor: Professor;
    onClose: () => void;
};

const EditProfessor: React.FC<Props> = ({ professor, onClose }) => {
    const [name, setName] = useState(professor.name);
    //const [collegeId, setCollegeName] = useState("");
    professor.name = name;

    const handleSubmit = async () => {
        //aici o sa trebuiasca sa fac cumva sa pot sa schimb facultatea
        await axios.put(`${API_URL}/professor`, professor).catch(error => {
            alert(error);
        });;
        onClose();
    };

    return (
        <div className="modal">
            <div className="modal-content">
                <h3>Editare Profesor</h3>
                <input
                    type="text"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <button onClick={handleSubmit}>Salvează</button>
                <button onClick={onClose}>Anulează</button>
            </div>
        </div>
    );
};

export default EditProfessor;