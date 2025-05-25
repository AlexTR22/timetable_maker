import { useEffect, useState } from "react";

import { useSession } from "../context/SessionProvider";

import { Professor } from "../models/ProfessorModel";
import axios from "axios";

function ProfessorPage() {
  //const { universitate } = useParams<{ universitate: string }>();
  const [professors, setProfessors] = useState<Professor[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selected, setSelected] = useState<Professor>();

  const { user } = useSession();

  useEffect(() => {
    axios.get('http://localhost:5261/professor')
      .then(response => {
        setProfessors(response.data);
        setLoading(false);
      })
      .catch(error => {
        setError('Failed to fetch timetable data.');
        alert(error);
        setLoading(false);
      });
    /*  if (!user?.universityId) return;
     setLoading(true);
     const fetchProfessors= async() =>{
      try {
           const userId=user.id;
           const response = await fetch(`http://localhost:5261/professor`, {
               method: 'Get',
               headers: { 'Content-Type': 'application/json' },
               body: JSON.stringify({ userId })
           });
           
           const data=await response.json();
           if (!response.ok) {
               setError(data.message);
           }
           else
           {
               setProfessors(data);
           }
          
       }
       catch (error) {
           throw(error);
       }
     }
     fetchProfessors(); */
  }, []);


  /*const reload = () => {
    if (!universitate) return;
    api<Profesor[]>(
      `profesori?universitate=${encodeURIComponent(universitate)}`,
    ).then(setProfesori);
  };
 
  const handleSave = async () => {
    if (!editing) return;
    const isEdit = Boolean(editing.id);
    await api<Profesor>(
      `profesori${isEdit ? `/${editing.id}` : ""}`,
      isEdit ? "PUT" : "POST",
      editing,
    );
    setEditing(null);
    setSelected(null);
    reload();
  };
 
  const handleDelete = async () => {
    if (!selected?.id) return;
    if (!window.confirm("Sigur ștergeți profesorul?")) return;
    await api<void>(`profesori/${selected.id}`, "DELETE");
    setSelected(null);
    reload();
  };
 
  if (!universitate) return <p>Lipsește universitatea în URL.</p>; */

  return (
    <div style={{ maxWidth: "40rem", margin: "2rem auto" }}>
      <h2>Profesori – { }</h2>

      {/* Butoane Add / Edit / Delete */}
      <div style={{ display: "flex", gap: "0.5rem", margin: "1rem 0" }}>
        <button /*onClick={ () => setEditing({ nume: "", universitate } )}*/>Add</button>
        <button
        /*  onClick={() => selected && setEditing(selected)}
         disabled={!selected} */
        >
          Edit
        </button>
        <button /*onClick={ handleDelete }  disabled={!selected} */>Delete</button>
      </div>





      {loading ? (<p>Se încarcă...</p>) : error ? (<p >{error}</p>) :
        (
          <div>
            {professors.map((p) => (
              <div key={p.id} onClick={() => setSelected(p)}>
                {p.name}
              </div>
            ))}
            {professors.length === 0 && <p style={{ padding: "1rem" }}>Nu există profesori.</p>}
          </div>
        )}

    </div>
  );
}
export default ProfessorPage;