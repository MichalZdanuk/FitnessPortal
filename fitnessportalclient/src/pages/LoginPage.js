import classes from "./LoginPage.module.css";
import lotusIcon from "../assets/images/lotus.png";
import logoIcon from "../assets/images/letterHicon.png";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import AuthContext from "../store/authContext";
import axios from "axios";
// import 'bootstrap/dist/css/bootstrap.css';
import { Alert } from "react-bootstrap";

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
  const authCtx = useContext(AuthContext)

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loginError, setLoginError] = useState("");

  const handleLogin = async (e) => {
    e.preventDefault();
    await axios
      .post("https://localhost:7087/api/account/login",{
        email: email,
        password: password,
      })
      .then((response) => {
        if(response.data){
          console.log(response.data);
          authCtx.login(response.data);
          navigate("/");
        }
      })
      .catch((error) => {
        if(error.response && error.response.status === 400)
          setLoginError(error.response.data);
      });
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
            required
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
            required
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
        <span className={classes["emph-text"]}>Create NOW</span>
      </p>
      {loginError && (
        <div className={classes["alert-div"]}>
          <Alert variant={'danger'}>{loginError}</Alert>
        </div>
      )}
    </div>
  );
};

export default LoginPage;
