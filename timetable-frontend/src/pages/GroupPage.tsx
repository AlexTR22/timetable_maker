


function GroupPage() {
    //const { universitate } = useParams<{ universitate: string }>();
    //const [profesori, setProfesori] = useState<Profesor[]>([]);
    //const [loading, setLoading] = useState(true);
    //const [error, setError] = useState<string | null>(null);
    //const [selected, setSelected] = useState<Profesor | null>(null);
    //const [editing, setEditing] = useState<Profesor | null>(null);

    // Încarcă lista
    /* useEffect(() => {
      if (!universitate) return;
      setLoading(true);
      api<Profesor[]>(
        `profesori?universitate=${encodeURIComponent(universitate)}`,
      )
        .then(setProfesori)
        .catch(() => setError("Eroare la încărcarea profesorilor"))
        .finally(() => setLoading(false));
    }, [universitate]);
  
    
    const reload = () => {
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
            <h2>Groups – { }</h2>

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

            {/* 
      

      
      {loading ? (<p>Se încarcă...</p>) : error ? ( <p >{error}</p>) :
       (
        <div>
          {groups.map((p) => (
            <div key={p.id} onClick={() => setSelected(p)}>
              {p.nume}
            </div>
          ))}
          {gorups.length === 0 && <p style={{ padding: "1rem" }}>Nu există grupe.</p>}
        </div>
      )} 
       */}
         </div>
    );
}
export default GroupPage;