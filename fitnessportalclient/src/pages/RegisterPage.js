import classes from "./RegisterPage.module.css";
import extendedIcon from "../assets/images/extended.png";
import logoIcon from "../assets/images/letterHicon.png";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import 'bootstrap/dist/css/bootstrap.css';
import { Alert } from "react-bootstrap";

const RegisterPage = () => {
  const navigate = useNavigate();
  return (
    <div className={classes["container"]}>
      <div className={classes["left-side"]}>
        <div className={classes["right-side-content"]}>
          <img onClick={(e) => {navigate("/")}} className={classes["logo-img"]} src={logoIcon} alt="lotus" />
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
  const [registerInput, setRegisterInput] = useState({
    username: "",
    email: "",
    password: "",
    confirmPassword: "",
    dateOfBirth: "",
    height: "",
    weight: ""
  });

  const onRegisterInputChange = (e) => {
    e.persist();
    setRegisterInput({ ...registerInput, [e.target.name]: e.target.value});
  };

  const [registerError, setRegisterError] = useState("");
  const [registerSuccess, setRegisterSuccess] = useState(false);
  const [step, setStep] = useState("1");

  const handleRegister = async (e) => {
    e.preventDefault();

    if(step === "1"){
        setStep("2");
    }
    else if(step === "2"){
      if(!validateInputs()) return;
      await axios
        .post("https://localhost:7087/api/account/register",{
          username: registerInput.username,
          email: registerInput.email,
          password: registerInput.password,
          confirmPassword: registerInput.confirmPassword,
          dateOfBirth: registerInput.dateOfBirth,
          height: registerInput.height,
          weight: registerInput.weight,
        })
        .then((response) => {
          if(response.status === 200) setRegisterSuccess(true);
        })
        .catch((error) => {
          if(error.response && error.response.status === 400){
            setRegisterError("User already exists");
            console.log(error.response.data);
          }
          //else console.log(error);
        });
    }
  };

  const validateInputs = () => {
    if(registerInput.password.length < 6){
      setRegisterError("Password's minimum length is 6.")
      return false;
    }
    if(registerInput.confirmPassword !== registerInput.password){
      setRegisterError("Password and Confirm Password does not match.");
      return false;
    }

    return true;
  }

  const propsRegisterForm = {
    registerInput,
    handleRegister,
    onRegisterInputChange,
    registerSuccess
  };

  return (<div>
      {step === "1" && <FirstStepRegisterForm  {...propsRegisterForm}/>}
      {step === "2" && <SecondStepRegisterForm  {...propsRegisterForm}/>}
      {registerError && (
        <div className={classes["alert-div"]}>
          <Alert variant={'danger'}>{registerError}</Alert>
        </div>
      )}
      {registerSuccess && (
        <div className={classes["alert-div"]}>
          <Alert variant={'success'}>Successful registration <Alert.Link href="/login">log in NOW</Alert.Link></Alert>
        </div>
      )}
    </div>
  );
};

const FirstStepRegisterForm = ({
  registerInput,
  handleRegister,
  onRegisterInputChange
}) => {
    const navigate = useNavigate();
    return (
<div className={classes["form-container"]}>
      <form onSubmit={handleRegister}>
      <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Username</label>
          <input
            name="username"
            value={registerInput.username}
            onChange={onRegisterInputChange}
            placeholder="username"
            className={classes["input-box"]}
            required
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Email</label>
          <input
            name="email"
            value={registerInput.email}
            onChange={onRegisterInputChange}
            placeholder="email"
            className={classes["input-box"]}
            required
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Password</label>
          <input
            name="password"
            value={registerInput.password}
            onChange={onRegisterInputChange}
            type="password"
            placeholder="password"
            className={classes["input-box"]}
            required
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Confirm Password</label>
          <input
            name="confirmPassword"
            value={registerInput.confirmPassword}
            onChange={onRegisterInputChange}
            type="password"
            placeholder="password"
            className={classes["input-box"]}
            required
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
        <span className={classes["emph-text"]}>Log in NOW</span>
      </p>
    </div>
    )
};

const SecondStepRegisterForm = ({
  registerInput,
  handleRegister,
  onRegisterInputChange,
  registerSuccess
}) => {
    const navigate = useNavigate();
    return (
<div className={classes["form-container"]}>
      <form onSubmit={handleRegister}>
      <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Date of Birth</label>
          <input
            name="dateOfBirth"
            value={registerInput.dateOfBirth}
            onChange={onRegisterInputChange}
            placeholder="date of birth"
            className={classes["input-box"]}
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Height</label>
          <input
            name="height"
            value={registerInput.height}
            onChange={onRegisterInputChange}
            placeholder="height"
            className={classes["input-box"]}
          ></input>
        </div>
        <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Weight</label>
          <input
            name="weight"
            value={registerInput.weight}
            onChange={onRegisterInputChange}
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
      {!registerSuccess && <p className={classes["bottom-text"]} onClick={(e) => {
                navigate("/login");
              }}>
        Already have an account?{" "}
        <span className={classes["emph-text"]}>Log in NOW</span>
      </p>}
    </div>
    )
};

export default RegisterPage;
