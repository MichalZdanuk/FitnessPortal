import axios from "axios";
import classes from "./UpdateProfilePage.module.css";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import AuthContext from "../../store/authContext";
import { Alert } from "react-bootstrap";
import { useEffect } from "react";

const UpdateProfilePage = () => {
  return <div className={classes["updateProfile-main-div"]}>
    <p className={classes["update-header"]}>Update Profile</p>
      <hr />
    <UpdateForm />
  </div>;
};

const UpdateForm = () => {
    const navigate = useNavigate("");
    const authCtx = useContext(AuthContext);
    const token = authCtx.tokenJWT;
    const [updateError, setUpdateError] = useState("");

    const [updateInput, setUpdateInput] = useState({
        username: "",
        email: "",
        dateOfBirth: "",
        height: "",
        weight: ""
      });

      const onUpdateInputChange = (e) => {
        e.persist();
        setUpdateInput({ ...updateInput, [e.target.name]: e.target.value});
      };

    useEffect(() => {
        fetchProfileInfo();
    },[token]);

    const fetchProfileInfo = async () => {
        try {
            const response = await axios.get(
              "https://localhost:7087/api/account/profile-info",
              {
                headers: {
                  Authorization: `Bearer ${token}`,
                },
              }
            );

            setInitialInputs(response);
            console.log(response.data);
          } catch (error) {
            console.log(error);
          }
    };

    const setInitialInputs = (response) => {
        setUpdateInput({...updateInput, 
            username: response.data.username,
            email: response.data.email,
            dateOfBirth: response.data.dateOfBirth.slice(0,10),
            weight: response.data.weight,
            height: response.data.height
        });
    };
  
    const handleUpdate = async (e) => {
      e.preventDefault();
      await axios
        .post("https://localhost:7087/api/account/update-profile",{
          username: updateInput.username,
          email: updateInput.email,
          dateOfBirth: updateInput.dateOfBirth,
          height: updateInput.height,
          weight: updateInput.weight
        },
        {
            headers: {
                Authorization: `Bearer ${token}`,
              },
        })
        .then((response) => {
          if(response.data){
            //console.log(response.data);
            navigate("/");
            authCtx.logout();
            authCtx.login(response.data);
            navigate("/account");
          }
        })
        .catch((error) => {
          if(error.response && error.response.status === 400)
          setUpdateError(error.response.data);
        });
    };
  
    return (
      <div className={classes["form-container"]}>
        <form onSubmit={handleUpdate}>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Username</label>
            <input
              name="username"
              value={updateInput.username}
              onChange={onUpdateInputChange}
              placeholder="email"
              className={classes["input-box"]}
              required
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Email</label>
            <input
              name="email"
              value={updateInput.email}
              onChange={onUpdateInputChange}
              placeholder="email"
              className={classes["input-box"]}
              required
            ></input>
          </div>
          <div className={classes["input-container"]}>
          <label className={classes["form-label"]}>Date of Birth</label>
          <input
            name="dateOfBirth"
            value={updateInput.dateOfBirth}
            onChange={onUpdateInputChange}
            placeholder="date of birth"
            className={classes["input-box"]}
          ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Weight</label>
            <input
              name="weight"
              value={updateInput.weight}
              onChange={onUpdateInputChange}
              placeholder="weight"
              className={classes["input-box"]}
              required
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Height</label>
            <input
              name="height"
              value={updateInput.height}
              onChange={onUpdateInputChange}
              placeholder="height"
              className={classes["input-box"]}
              required
            ></input>
          </div>
  
          <div className={classes["button-div"]}>
          <input
            className={classes["submit-button"]}
            type="submit"
            value="UPDATE"
            />
          </div>
        </form>
        {updateError && (
          <div className={classes["alert-div"]}>
            <Alert variant={'danger'}>{updateError}</Alert>
          </div>
        )}
      </div>
    );
  };

export default UpdateProfilePage;
