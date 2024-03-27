/* eslint-disable react/prop-types */
import { Container } from 'reactstrap';
import AuthorizeView from "../components/AuthorizeView.jsx";
import { NavMenu } from '../components/NavMenu';
import { Budget } from '../components/budget/index.jsx';

function Home(props) {
    console.log('Home props', props)
    return (
        <div>
            <AuthorizeView>
                <NavMenu />
                <Container>
                    <Budget />
                </Container>
            </AuthorizeView>
        </div>
    );
}

export default Home;