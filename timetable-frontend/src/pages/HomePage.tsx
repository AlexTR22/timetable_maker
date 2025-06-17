import { useNavigate } from 'react-router-dom';
import "../css/HomePage.css"

function Home() {
    const navigate = useNavigate();
    return (
        <div className="home-container">
            <h1 className='home-title'> Pagina Principala </h1>
            <div className="button-grid">
                <button className="home-button" onClick={() => navigate(`/timetablePage`)}>Generează Orar</button>
                <button className="home-button" onClick={() => navigate(`/professorPage`)}>Adaugă Profesor</button>
                <button className="home-button" onClick={() => navigate(`/groupPage`)}>Adaugă Grupă</button>
                <button className="home-button" onClick={() => navigate(`/subjectPage`)}>Adaugă Materie</button>
                <button className="home-button" onClick={() => navigate(`/roomPage`)}>Adaugă Sală</button>
                <button className="home-button" onClick={() => navigate(`/timeConstraintsPage`)}>Adaugă Constrangeri</button>
            </div>

            <div className='home-footer'>
                <button className='home-button'><a
                    href={`https://github.com/AlexTR22/timetable_maker`}
                    className=''
                    role="button" 
                    >
                    Documentație
                </a>
                </button>
                <p> Versiunea aplicației: 1.0 </p>
            </div>
        </div>
    );
};

export default Home;