import './App.css';

import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import Login from './Pages/Login';
import { Budget } from './components/budget/Budget';


function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                {/* <Route path="/budget" element={<Budget />} /> */}
            </Routes>
        </BrowserRouter>
    );

}
export default App;



