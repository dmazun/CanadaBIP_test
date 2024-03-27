/* eslint-disable react/prop-types */
/* eslint-disable no-empty */

import { useNavigate } from "react-router-dom";
import { Button } from 'reactstrap';
import { AuthorizedUser } from "./AuthorizeView.jsx";

function Logout() {
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch("/logout", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: ""
        })
            .then((data) => {
                if (data.ok) {
                    navigate("/login");
                }
            })
            .catch((error) => {
                console.error(error);
            })
    };

    return (
        <>
            <Button onClick={handleSubmit}>Logout <AuthorizedUser value="email" /></Button>
        </>
    );
}

export default Logout;
