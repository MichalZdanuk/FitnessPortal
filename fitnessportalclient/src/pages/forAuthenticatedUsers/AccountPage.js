import { useContext, useEffect, useState } from "react";
import classes from "./AccountPage.module.css";
import axios from "axios";
import AuthContext from "../../store/authContext";

import PersonalInfo from "../../components/profile/PersonalInfo";
import TrainingInfo from "../../components/profile/TrainingInfo";
import MySpinner from "../../components/spinner/MySpinner";
import { useNavigate } from "react-router-dom";

const AccountPage = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7087/api/account/profile-info",
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setData(response.data);
        setLoading(false);
      } catch (error) {
        console.log(error);
      }
    };

    fetchData();
  }, []);

  console.log(data);
  return (
    <div className={classes["acount-main-div"]}>
      <p className={classes["account-header"]}>Account Panel</p>
      {loading ? (
        <MySpinner />
      ) : data ? (
        <div>
          <PersonalInfo data={data}/>
          <TrainingInfo />
        </div>
      ) : (
        <p>Error: Failed to fetch data</p>
      )}
      <div className={classes["button-center"]}>
        <button className={classes["update-profile-button"]} onClick={() => {navigate("/account/updateProfile")}}>Update Profile</button>
      </div>
    </div>
  );
};


export default AccountPage;
