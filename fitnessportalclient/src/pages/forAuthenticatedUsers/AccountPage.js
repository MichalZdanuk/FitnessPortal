import { useContext, useEffect, useState } from "react";
import classes from "./AccountPage.module.css";
import axios from "axios";
import AuthContext from "../../store/authContext";
import { InfinitySpin } from "react-loader-spinner";

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
        <div className={classes["container"]}>
          <div className={classes["personal-div"]}>
            <p className={classes["panel-label"]}>Personal</p>
            <p className={classes["property-label"]}>
              username: {data.username}
            </p>
            <p className={classes["property-label"]}>email: {data.email}</p>
          </div>
          <hr className={classes["vertical-line"]} />
          <div className={classes["physical-div"]}>
            <p className={classes["panel-label"]}>Physical</p>
            <p className={classes["property-label"]}>
              date of birth: {data.dateOfBirth.substring(0,10)}
            </p>
            <p className={classes["property-label"]}>
              weight: {data.weight} kg
            </p>
            <p className={classes["property-label"]}>
              height: {data.height} cm
            </p>
          </div>
        </div>
      ) : (
        <p>Error: Failed to fetch data</p>
      )}

      <button className={classes["show-more-button"]}>Update Profile</button>
    </div>
  );
};

export default AccountPage;
