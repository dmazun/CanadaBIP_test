import { useState } from "react";

function ForgotPassword() {
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirm, setPasswordConfirm] = useState("");

  // handle change events for input fields
  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
    if (name === "password") setPassword(value);
    if (name === "passwordConfirm") setPasswordConfirm(value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const resetCode = "";

    if (!email || !password || !passwordConfirm) {
      setError("Please fill in all fields.");
      return;
    }
    
    if (passwordConfirm !== password) {
      setError("Passwords are not the same.");
      return;
    } 

    setError("");

    fetch("/account/resetPassword", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email,
        resetCode: resetCode,
        newPassword: password,
      }),
    })
      .then((data) => {
        if (data.ok) {
          const htmlString = "Your password has been reset. <br /> Please <a href='/login'>click here to login</a>";
          const theObj = {__html:htmlString};
          setError(theObj);
        } else setError("Error Reset Password.");
      })
      .catch((error) => {
        console.error(error);
        setError("Error Reset Password.");
      });
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

        <h3>Reset pasword</h3>
        
        <form onSubmit={handleSubmit} className="form">
          <div className="form__row">
            <div>
              <label className="forminput" htmlFor="email">
                Email:
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
            <div>
              <label htmlFor="passwordConfirm">Confirm password:</label>
            </div>
            <input
              type="password"
              id="passwordConfirm"
              name="passwordConfirm"
              value={passwordConfirm}
              onChange={handleChange}
            />
          </div>

          <div className="form__row">
            <button type="submit">Reset Password</button>
          </div>

          <div className="form__row text-center">
            {error && <p className="error text-center" dangerouslySetInnerHTML={error} />}
            {error && <p className="error text-center">{error}</p>}
          </div>
        </form>
      </div>
    </div>
  );
}

export default ForgotPassword;
