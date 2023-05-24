import classes from "./LoginPage.module.css";
import lotusIcon from "../assets/images/lotus.png";
import logoIcon from "../assets/images/letterHicon.png";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const LoginPage = () => {
  const navigate = useNavigate();
  return (
    <div className={classes["container"]}>
      <div className={classes["left-side"]}>
        <div className={classes["right-side-content"]}>
          <img onClick={(e) => {navigate("/")}} className={classes["logo-img"]} src={logoIcon} alt="lotus" />
          <LoginForm />
        </div>
      </div>
      <div className={classes["right-side"]}>
        <div className={classes["right-side-content"]}>
          <img className={classes["img"]} src={lotusIcon} alt="lotus" />
          <p className={classes["quote-text"]}>
            "The pain you feel today will be the stegth you feel tomorrow."
          </p>
        </div>
      </div>
    </div>
  );
};

const LoginForm = () => {
  const navigate = useNavigate("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = (e) => {
    e.preventDefault();
    navigate("/");
  };

  return (
    <div className={classes["form-container"]}>
      <form onSubmit={handleLogin}>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Email</label>
          <input
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="email"
            className={classes["input-box"]}
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Password</label>
          <input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type="password"
            placeholder="password"
            className={classes["input-box"]}
          ></input>
        </div>

        <input
          className={classes["submit-button"]}
          type="submit"
          value="LOG IN"
        />
      </form>
      <p className={classes["bottom-text"]} onClick={(e) => {
                navigate("/register");
              }}>
        Do not have an account?{" "}
        <emph className={classes["emph-text"]}>Create NOW</emph>
      </p>
    </div>
  );
};

export default LoginPage;
