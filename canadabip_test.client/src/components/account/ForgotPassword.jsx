import { useState } from "react";

function ForgotPassword() {
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");

  // handle change events for input fields
  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "email") setEmail(value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!email) {
      setError("Please fill in email fields.");
    } else {
      setError("");

      fetch("/api/ManageUser/forgotPassword", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
        }),
      })
        .then((data) => {
          if (data.ok) {
            setError("Please check your email to reset your password.");            
          } else setError("Error Forgot Password.");
        })
        .catch((error) => {
          console.error(error);
          setError("Error Forgot Password.");
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

        <h3>Forgot pasword</h3>

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
            <button type="submit">Submit</button>
          </div>

          <div className="form__row text-center">
            {error && <p className="error text-center">{error}</p>}
          </div>
        </form>
      </div>
    </div>
  );
}

export default ForgotPassword;
