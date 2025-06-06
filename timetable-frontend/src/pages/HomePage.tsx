import { useNavigate } from 'react-router-dom';


function Home() 
{
    const navigate = useNavigate();
    return (
    <div className="flex flex-col items-center justify-center min-h-screen gap-4">
        <h1 className="text-3xl font-bold">Admin Portal</h1>
        <button onClick={ ()=>navigate(`/professorPage`)}>Adaugă Profesor</button>
        <button onClick={ ()=>navigate(`/groupPage`)}>Adaugă Grupă</button>
        <button onClick={ ()=>navigate(`/subjectPage`)}>Adaugă Materie</button>
    </div>
    );
};

export default Home;