import classes from "./FriendsPage.module.css";
import friendsImg from "../assets/images/friends.png";
import addFriendIcon from "../assets/images/add-user.png";
import userIcon from "../assets/images/user.png";
import {friendsList} from "../mocks/mockedData"

const FriendsPage = () => {
  return (
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
  );
};

const FriendsList = (props) => {
    const listOfFriends = props.friendsData.map((friend) => {
      return (
        <div className={classes["friend-row"]}>
          <div className={classes["friend-info"]}>
            <img src={userIcon} className={classes["user-img"]} alt="user" />
            <p className={classes["friend-elem"]}><emph className={classes["friend-username"]}>{friend.username}</emph> {friend.email}</p>
          </div>
          <button className={classes["profile-button"]}>Profile</button>
        </div>
      );
    });
  
    return <div>{listOfFriends}</div>;
  };

export default FriendsPage;
