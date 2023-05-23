import classes from "./RegisterPage.module.css";
import extendedIcon from "../assets/images/extended.png";
import logoIcon from "../assets/images/logoIcon.png";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const RegisterPage = () => {
  return (
    <div className={classes["container"]}>
      <div className={classes["left-side"]}>
        <div className={classes["right-side-content"]}>
          <img className={classes["logo-img"]} src={logoIcon} alt="lotus" />
          <RegisterForm />
        </div>
      </div>
      <div className={classes["right-side"]}>
        <div className={classes["right-side-content"]}>
          <img className={classes["img"]} src={extendedIcon} alt="extended" />
          <p className={classes["quote-text"]}>
            "The only bad workout is the one that didn't happen."
          </p>
        </div>
      </div>
    </div>
  );
};

const RegisterForm = () => {
  const navigate = useNavigate("");
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [dateOfBirth, setDateOfBirth] = useState("");
  const [height, setHeight] = useState("");
  const [weight, setWeight] = useState("");

  const [step, setStep] = useState("1");

  const handleRegister = (e) => {
    e.preventDefault();

    if(step === "1"){
        setStep("2");
    }
    else if(step === "2"){
        navigate("/");
    }
  };

  const propsRegisterFormStep1 = {
    username,
    setUsername,
    email,
    setEmail,
    password,
    setPassword,
    confirmPassword,
    setConfirmPassword,
    handleRegister
  };

  const propsRegisterFormStep2 = {
    dateOfBirth,
    setDateOfBirth,
    height,
    setHeight,
    weight,
    setWeight,
    handleRegister
  };

  

  return (<div>
    {step === "1" && <FirstStepRegisterForm  {...propsRegisterFormStep1}/>}
    {step === "2" && <SecondStepRegisterForm  {...propsRegisterFormStep2}/>}

    </div>
  );
};

const FirstStepRegisterForm = ({
    username,
    setUsername,
    email,
    setEmail,
    password,
    setPassword,
    confirmPassword,
    setConfirmPassword,
    handleRegister
}) => {
    const navigate = useNavigate();
    return (
<div className={classes["form-container"]}>
      <form onSubmit={handleRegister}>
      <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Username</label>
          <input
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="username"
            className={classes["input-box"]}
          ></input>
        </div>
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
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Confirm Password</label>
          <input
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            type="password"
            placeholder="password"
            className={classes["input-box"]}
          ></input>
        </div>

        <input
          className={classes["submit-button"]}
          type="submit"
          value="NEXT"
        />
      </form>
      <p className={classes["bottom-text"]} onClick={(e) => {
                navigate("/login");
              }}>
        Already have an account?{" "}
        <emph className={classes["emph-text"]}>Log in NOW</emph>
      </p>
    </div>
    )
};

const SecondStepRegisterForm = ({
    dateOfBirth,
    setDateOfBirth,
    height,
    setHeight,
    weight,
    setWeight,
    handleRegister
}) => {
    const navigate = useNavigate();
    return (
<div className={classes["form-container"]}>
      <form onSubmit={handleRegister}>
      <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Date of Birth</label>
          <input
            value={dateOfBirth}
            onChange={(e) => setDateOfBirth(e.target.value)}
            placeholder="date of birth"
            className={classes["input-box"]}
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Height</label>
          <input
            value={height}
            onChange={(e) => setHeight(e.target.value)}
            placeholder="height"
            className={classes["input-box"]}
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Weight</label>
          <input
            value={weight}
            onChange={(e) => setWeight(e.target.value)}
            placeholder="weight"
            className={classes["input-box"]}
          ></input>
        </div>

        <input
          className={classes["submit-button"]}
          type="submit"
          value="REGISTER"
        />
      </form>
      <p className={classes["bottom-text"]} onClick={(e) => {
                navigate("/login");
              }}>
        Already have an account?{" "}
        <emph className={classes["emph-text"]}>Log in NOW</emph>
      </p>
    </div>
    )
};

export default RegisterPage;
