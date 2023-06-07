import classes from "./FriendsPage.module.css";
import friendsImg from "../../assets/images/friends.png";
import addFriendIcon from "../../assets/images/add-user.png";
import userIcon from "../../assets/images/user.png";
import {friendsList} from "../../mocks/mockedData"
import { useNavigate } from "react-router-dom";
import { RequiredAuth } from "../../store/authContext";

const FriendsPage = () => {
  return (
    <RequiredAuth>
      <div className={classes["container"]}>
        <div className={classes["left-side"]}>
          <img
            src={friendsImg}
            className={classes["friends-img"]}
            alt="friends"
          />
          <div className={classes["input-container"]}>
            <label className={classes["add-friend-label"]}>Friends email</label>
            <input
              placeholder="friend@wp.pl"
              className={classes["input-box"]}
            ></input>
          </div>
          <button className={classes["add-friend-button"]}>
            ADD FRIEND{" "}
            <img src={addFriendIcon} className={classes["add-img"]} alt="add" />
          </button>
        </div>

        <div className={classes["right-side"]}>
          <p className={classes["list-of-friends-label"]}>List of friends</p>
          <FriendsList friendsData={friendsList} />
          <button className={classes["show-more-button"]}>Show more</button>
        </div>
      </div>
    </RequiredAuth>
  );
};

const FriendsList = (props) => {
    const navigate = useNavigate();
    const listOfFriends = props.friendsData.map((friend) => {
      return (
        <div key={friend.id} className={classes["friend-row"]}>
          <div className={classes["friend-info"]}>
            <img src={userIcon} className={classes["user-img"]} alt="user" />
            <p className={classes["friend-elem"]}><span className={classes["friend-username"]}>{friend.username}</span> {friend.email}</p>
          </div>
          <button className={classes["profile-button"]} onClick={(e) => {navigate(`/friends/${friend.email}`, {state:friend});
}}>Profile</button>
        </div>
      );
    });
  
    return <div>{listOfFriends}</div>;
  };

export default FriendsPage;
