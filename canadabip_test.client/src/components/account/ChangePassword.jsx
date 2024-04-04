import { useState } from "react";

function ChangePassword() {
  const [oldPassword, setOldPassword] = useState("");
  const [oldPasswordConfirm, setOldPasswordConfirm] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");

  // todo: Set current password to oldPassword

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name === "oldPasswordConfirm") setOldPasswordConfirm(value);
    if (name === "newPassword") setNewPassword(value);
    if (name === "confirmPassword") setConfirmPassword(value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (oldPassword !== oldPasswordConfirm) {
      setError("Entered password is wrong.");
      return
    } 
    
    if (confirmPassword !== newPassword) {
      setError("Passwords are not the same.");
      return
    } 
    
    // todo: Validate new password

    setError("");

    fetch("/account/changePassword", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        newPassword: newPassword,
      }),
    })
      .then((data) => {
        if (data.ok) {
          const htmlString = "Your password has been change. <br /> Please <a href='/login'>click here to login</a>";
          const theObj = {__html:htmlString};
          setError(theObj);
        } else setError("Error Change Password.");
      })
      .catch((error) => {
        console.error(error);
        setError("Error Change Password.");
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

        <h3>Change password</h3>
        
        <form onSubmit={handleSubmit} className="form">
          <div className="form__row">
            <div>
              <label htmlFor="oldPassword">Old Password:</label>
            </div>
            <input
              type="password"
              id="oldPassword"
              name="oldPassword"
              value={oldPassword}
              onChange={handleChange}
            />
          </div>

          <div className="form__row">
            <div>
              <label htmlFor="password">New Password:</label>
            </div>
            <input
              type="password"
              id="newPassword"
              name="newPassword"
              value={newPassword}
              onChange={handleChange}
            />
          </div>

          <div className="form__row">
            <div>
              <label htmlFor="confirmPassword">Confirm password:</label>
            </div>
            <input
              type="password"
              id="confirmPassword"
              name="confirmPassword"
              value={confirmPassword}
              onChange={handleChange}
            />
          </div>

          <div className="form__row">
            <button type="submit">Change Password</button>
          </div>

          <div className="form__row">
            <p className="hint">Rules for Password Validation:</p>
            <p className="hint">- Minimum length of 8 characters is required.</p>
            <p className="hint">- At least one special character (!, #, $ etc.) is required.</p>
            <p className="hint">- At least one numeric digit (0-9) is required.</p>
            <p className="hint">- At least one lowercase letter is required.</p>
            <p className="hint">- At least one uppercase letter is required.</p>
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

export default ChangePassword;
