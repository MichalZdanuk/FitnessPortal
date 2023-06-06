import { useLocation } from "react-router-dom";
import classes from "./FriendProfilePage.module.css";
import { RequiredAuth } from "../store/authContext";

const FriendProfilePage = () => {
  const location = useLocation();
  const data = location.state;
    // console.log(data);
  return (
    <RequiredAuth>
      <div className={classes["friend-profile"]}>
        <h1>{data.username} profile</h1>
        <h5>email: {data.email}</h5>
        <p className={classes["red"]}>to be implemented</p>
      </div>
    </RequiredAuth>
  );
};

export default FriendProfilePage;
