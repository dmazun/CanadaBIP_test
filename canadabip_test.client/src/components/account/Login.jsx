import { useState } from "react";

function Login() {
  // state variables for email and passwords
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberme, setRememberme] = useState(false);
  const [error, setError] = useState("");

  // handle change events for input fields
  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
    if (name === "rememberme") setRememberme(e.target.checked);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // validate email and passwords
    if (!email || !password) {
      setError("Please fill in all fields.");
    } else {
      setError("");

      var loginurl = "";
      if (rememberme == true) loginurl = "/account/login?useCookies=true";
      else loginurl = "/account/login?useSessionCookies=true";

      fetch(loginurl, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
          password: password,
        }),
      })
        .then((data) => {
          if (data.ok) {
            setError("Successful Login.");
            window.location.href = "/";
          } else setError("Error Logging In.");
        })
        .catch((error) => {
          console.error(error);
          setError("Error Logging in.");
        });
    }
  };

  return (
    <div className="containerbox">
      <div className="containerbox__back"></div>

      <div className="login">
        <div className="login__header">
          <figure className="logo">
            <img src="src/assets/Pfizer_logo2022.png" alt="Pfizer logo" />
          </figure>

          <h1>CANADA</h1>
          <h2>Budget tracker</h2>
        </div>

        <h3>Login</h3>
        <form onSubmit={handleSubmit} className="form">
          <div className="form__row">
            <div>
              <label className="forminput" htmlFor="email">
                Username:
              </label>
            </div>

            <div>
              <input
                type="email"
                id="email"
                name="email"
                value={email}
                onChange={handleChange}
              />
            </div>
          </div>

          <div className="form__row">
            <div>
              <label htmlFor="password">Password:</label>
            </div>
            <input
              type="password"
              id="password"
              name="password"
              value={password}
              onChange={handleChange}
            />
          </div>

          <div className="form__row">
            <input
              type="checkbox"
              id="rememberme"
              name="rememberme"
              className="rememberme__input"
              checked={rememberme}
              onChange={handleChange}
            />
            <label className="rememberme__label" htmlFor="rememberme">
              Remember Me
            </label>
          </div>

          <div className="form__row">
            <button type="submit">Login</button>
          </div>

          <div className="form__row">
            <a className="link" href="/forgot-password">
              Forgot password?
            </a>
          </div>

          <div className="form__row">
            {error && <p className="error">{error}</p>}
          </div>
        </form>
      </div>
    </div>
  );
}

export default Login;
