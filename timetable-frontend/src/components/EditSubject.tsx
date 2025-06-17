import React, { useEffect, useState } from 'react';
import { Subject } from '../models/SubjectModel';
import axios from 'axios';
import { API_URL } from '../services/api';
import { Professor } from '../models/ProfessorModel';
import '../css/AddEditComponents.css'
interface EditSubjectProps {
    subject: Subject;
    onClose: () => void;
}

export const EditSubject: React.FC<EditSubjectProps> = ({ subject, onClose }) => {
    const [name, setName] = useState(subject.name);
    const [year, setYear] = useState(subject.year);
    const [professorName, setProfessorName] = useState('');
    const id = subject.professorId;

    useEffect(() => {
        const getProfessorName = async () => {
            try {
                const res = await axios.get<{ professor: Professor }>(`${API_URL}/professor/getProf/${id}`);
                setProfessorName(res.data.professor.name);

            } catch (error) {
                console.error("Eroare la obținerea profesorului:", error);
            }
        };

        getProfessorName();
    }, [subject]);

    const handleSubmit = async () => {
        const res = await axios.get(`${API_URL}/professor/${encodeURIComponent(professorName)}`);
        subject.professorId = res.data.professorId;
        subject.name = name;
        subject.year = year;
        await axios.put(`${API_URL}/subject/${subject.id}`, subject);
        onClose();
    };

    return (
        <div className="modal">
            <div className='modal-content'>
                <h3>Editează Materie</h3>
                <input value={name} onChange={(e) => setName(e.target.value)} placeholder="Nume" />
                <input type="number" value={year} onChange={(e) => setYear(Number(e.target.value))} placeholder="An" />
                <input value={professorName} onChange={(e) => setProfessorName(e.target.value)} placeholder="Nume Profesor" />
                <button onClick={handleSubmit}>Salvează</button>
                <button onClick={onClose}>Anulează</button>
            </div>
        </div>
    );
};