import LogoutLink from "../components/LogoutLink.jsx";
import AuthorizeView, { AuthorizedUser } from "../components/AuthorizeView.jsx";

function Home() {
    return (
        <div>
            <p>HOME</p>
            <AuthorizeView>
                <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
            </AuthorizeView>
        </div>
    );
}

export default Home;