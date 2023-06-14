import { useContext, useEffect, useState } from "react";
import classes from "./AccountPage.module.css";
import axios from "axios";
import AuthContext from "../../store/authContext";
import { InfinitySpin } from "react-loader-spinner";

import PersonalInfo from "../../components/profile/PersonalInfo";
import TrainingInfo from "../../components/profile/TrainingInfo";

const AccountPage = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;

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
        <div className={classes["spinner"]}>
          <InfinitySpin width="200" color="#02C39A" />
        </div>
      ) : data ? (
        <div>
          <PersonalInfo data={data}/>
          <TrainingInfo />
        </div>
      ) : (
        <p>Error: Failed to fetch data</p>
      )}
      <div className={classes["button-center"]}>
        <button className={classes["update-profile-button"]}>Update Profile</button>
      </div>
    </div>
  );
};


export default AccountPage;
