import classes from "./FriendsPage.module.css";
import { useNavigate, useLocation } from "react-router-dom";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import PersonRemoveIcon from '@mui/icons-material/PersonRemove';
import { useContext } from "react";
import AuthContext from "../../store/authContext";
import axios from "axios";

const FriendsPage = () => {
  const location = useLocation();
  const data = location.state;
  //console.log(data);

  return (
    <div className={classes["friends-main-div"]}>
      <div className={classes["friend-request-panel"]}>
        <p className={classes["friend-request-header"]}>Friend Requests</p>
        <p className={classes["friend-request-motto"]}>Expand your social circle, accept new connections!</p>
        {/* <p>user1 XXX</p>
        <p>user2 XXX</p>
        <p>user3 XXX</p> */}
        <p className={classes["no-friend-requests"]}>You don't have any friend requests at the moment</p>
      </div>
      <div className={classes["friend-list-panel"]}>
        <p className={classes["welcome-header"]}>Welcome to your personal web page where you can manage your friend list and easily add new friends.</p>
        <FriendsList friends={data}/>
      </div>
    </div>
  )
}

const FriendsList = (props) => {
  const navigate = useNavigate();
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;

  const removeFriend = async (e) => {
    e.preventDefault();
    const friendId = e.currentTarget.getAttribute("data-friend-id");
  
    console.log("friendId: ", friendId);
    console.log("endpoint: ",`https://localhost:7087/api/friendship/remove/${friendId}`);
    try {
      await axios.delete(`https://localhost:7087/api/friendship/remove/${friendId}`,
      {
        headers:{
          Authorization: `Bearer ${token}`,
        },
      });
      //console.log(friendsData);
      navigate("/account/");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <ul>
        {props.friends.map((friend) => {
          return (
            <div key={friend.id} className={classes["friend-div"]}>
              <div className={classes["contact-data"]}>
              <AccountCircleIcon/> {friend.username} {friend.email}
              </div>
              <div className={classes["manage-div"]}>
                <button className={classes["profile-button"]} 
                onClick={(e) => {navigate(`/account/friendlist/${friend.email}`, {state:friend});
}}><AccountBoxIcon/> Profile</button>
                <div className={classes["remove-icon"]} data-friend-id={friend.id} onClick={removeFriend}>
                  <PersonRemoveIcon/>  
                </div>
              </div>
            </div>
          );
        })}
      </ul>
    </div>
  )
}

export default FriendsPage;
