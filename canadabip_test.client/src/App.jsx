import './App.css';

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import Login from './Pages/Login';
import RepresentativePage from './Pages/Representative';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/representative" element={<RepresentativePage />} />
            </Routes>
        </BrowserRouter>
    );

}
export default App;



