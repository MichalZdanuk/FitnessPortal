import classes from "./LeftSidePanel.module.css"
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import Diversity3Icon from '@mui/icons-material/Diversity3';
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter';
import ScaleIcon from '@mui/icons-material/Scale';
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { useContext } from "react";
import AuthContext from "../../store/authContext";

const LeftSidePanel = () => {
    const navigate = useNavigate();
    const authCtx = useContext(AuthContext);
    const token = authCtx.tokenJWT;

    const fetchFriends = async (e) => {
        e.preventDefault();
        try {
          const response = await axios.get("https://localhost:7087/api/friendship/friends",
          {
            headers:{
              Authorization: `Bearer ${token}`,
            },
          });
          const friendsData = response.data;
          console.log(friendsData);
          navigate("/account/friendlist", { state: friendsData });
        } catch (error) {
          console.log(error);
        }
      };

    return (
        <div className={classes["panel-div"]}>
            <p className={classes["panel-title"]}>Manage your account</p>
            <hr/>
            <ul className={classes["ul-blank"]}>
                <li className={classes["li-item"]} onClick={() => {navigate("/account")}}><AccountBoxIcon/> My Profile</li>
                <li className={classes["li-item"]} onClick={() => {navigate("/account/friendlist")}}><Diversity3Icon/> My Friends</li>
                <li className={classes["li-item"]} onClick={() => {navigate("/account/traininghistory")}}><FitnessCenterIcon/> Training History</li>
                <li className={classes["li-item"]} onClick={() => {navigate("/account/bmihistory")}}><ScaleIcon/> BMI History</li>
            </ul>
        </div>
    )
};

export default LeftSidePanel