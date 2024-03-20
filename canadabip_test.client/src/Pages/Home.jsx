/* eslint-disable react/prop-types */
import { Container } from 'reactstrap';
import AuthorizeView from "../components/AuthorizeView.jsx";
import { NavMenu } from '../components/NavMenu';

function Home(props) {
    return (
        <div>
            <AuthorizeView>
                <NavMenu />
                <Container>
                    {props?.children}
                </Container>
            </AuthorizeView>
        </div>
    );
}

export default Home;