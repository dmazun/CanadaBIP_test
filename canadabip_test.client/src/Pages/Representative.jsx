/* eslint-disable react/prop-types */
import { Container } from 'reactstrap';
import AuthorizeView from "../components/AuthorizeView.jsx";
import { NavMenu } from '../components/NavMenu.jsx';
import Representative from '../components/representative/index.jsx';

function RepresentativePage() {
    return (
        <div>
            <AuthorizeView>
                <NavMenu />
                <Container>
                    <Representative />
                </Container>
            </AuthorizeView>
        </div>
    );
}

export default RepresentativePage;