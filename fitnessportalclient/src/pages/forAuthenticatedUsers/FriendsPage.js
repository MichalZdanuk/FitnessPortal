import classes from "./FriendsPage.module.css";
import { useNavigate, useLocation } from "react-router-dom";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import PersonRemoveIcon from '@mui/icons-material/PersonRemove';
import { useContext, useEffect } from "react";
import { useState } from "react";
import AuthContext from "../../store/authContext";
import axios from "axios";
import { InfinitySpin } from "react-loader-spinner";


const FriendsPage = () => {
  const location = useLocation();
  const data = location.state;
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;
  //console.log(data);

  return (
    <div className={classes["friends-main-div"]}>
      <div className={classes["friend-request-panel"]}>
        <p className={classes["friend-request-header"]}>Friend Requests</p>
        <FriendRequests token={token}/>
      </div>
      <div className={classes["friend-list-panel"]}>
        <p className={classes["welcome-header"]}>Welcome to your personal web page where you can manage your friend list and easily add new friends.</p>
        <FriendsList friends={data} token={token}/>
      </div>
    </div>
  )
}

const FriendRequests = (props) => {
  const [friendRequestsList, setFriendRequestsList] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchRequestsList = async () => {
      try {
        const response = await axios.get("https://localhost:7087/api/friendship/friendship-requests",
        {
          headers:{
            Authorization: `Bearer ${props.token}`,
          },
        });
        setFriendRequestsList(response.data);
        setLoading(false);
        console.log(friendRequestsList);
        //navigate("/account/friendlist", { state: friendsData });
      } catch (error) {
        console.log(error);
      }
    };

    fetchRequestsList();
  }, []);

  const rejectRequest = async (requestId) => {
    try {
      await axios.delete(`https://localhost:7087/api/friendship/reject/${requestId}`,
      {
        headers:{
          Authorization: `Bearer ${props.token}`,
        },
      });
      const updatedList = friendRequestsList.filter(
        (request) => request.id !== requestId
      );
      setFriendRequestsList(updatedList);
    } catch (error) {
      console.log(error);
    }
  };

  const acceptRequest = async (requestId) => {
    //e.preventDefault();
  
    console.log("requestId: ", requestId);
    console.log("endpoint: ",`https://localhost:7087/api/friendship/accept/${requestId}`);
    try {
      await axios.post(`https://localhost:7087/api/friendship/accept/${requestId}`,{

      },
      {
        headers:{
          Authorization: `Bearer ${props.token}`,
        },
      });
      const updatedList = friendRequestsList.filter(
        (request) => request.id !== requestId
      );
      setFriendRequestsList(updatedList);

      window.location.reload();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
        <p className={classes["friend-request-motto"]}>Expand your social circle, accept new connections!</p>
        { loading ? (
          <div className={classes["spinner"]}>
          <InfinitySpin width="200" color="red" />
            </div>
        ) : friendRequestsList && friendRequestsList.length > 0 ? (
          friendRequestsList.map((request) => {
            return (
              <div key={request.id} className={classes["request-div"]}>
            <p>{request.senderName}</p>
            <div className={classes["icons-container"]}>
              <p>{request.sendDate.toString().substring(0,10)}</p>
              <p className={classes["icon"]}><PersonAddIcon onClick={() => acceptRequest(request.id)} className={classes["accept-request-icon"]}/></p>
              <p className={classes["icon"]}><PersonRemoveIcon onClick={() => rejectRequest(request.id)}className={classes["reject-request-icon"]}/></p>
            </div>
          </div>
              )
          })
        ) : (
          <p className={classes["no-friend-requests"]}>You don't have any friend requests at the moment</p>
        )

        }
    </div>
  )
};


const FriendsList = (props) => {
  const navigate = useNavigate();
  
  const [friendList, setFriendList] = useState(null);
  const [loading, setLoading] = useState(true);


  useEffect(() => {
    const fetchFriendsList = async () => {
      try {
        const response = await axios.get("https://localhost:7087/api/friendship/friends",
        {
          headers:{
            Authorization: `Bearer ${props.token}`,
          },
        });
        setFriendList(response.data);
        //console.log(friendList);
        setLoading(false);
        //navigate("/account/friendlist", { state: friendsData });
      } catch (error) {
        console.log(error);
      }
    };

    fetchFriendsList();
  }, []);

  const removeFriend = async (e) => {
    e.preventDefault();
    const friendId = e.currentTarget.getAttribute("data-friend-id");
  
    console.log("friendId: ", friendId);
    console.log("endpoint: ",`https://localhost:7087/api/friendship/remove/${friendId}`);
    try {
      await axios.delete(`https://localhost:7087/api/friendship/remove/${friendId}`,
      {
        headers:{
          Authorization: `Bearer ${props.token}`,
        },
      });
      //console.log(friendsData);
      window.location.reload();
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <ul>
        {loading ? (
          <div className={classes["spinner"]}>
            <InfinitySpin width="200" color="#02C39A" />
          </div>
        ) : friendList && friendList.length > 0 ? (
        friendList.map((friend) => {
          return (
            <div key={friend.id} className={classes["friend-div"]}>
              <div className={classes["contact-data"]}>
              <AccountCircleIcon/> {friend.username} {friend.email}
              </div>
              <div className={classes["manage-div"]}>
                <button className={classes["profile-button"]} onClick={(e) => {navigate(`/account/friendlist/${friend.email}`, {state:friend});}}><AccountBoxIcon/> Profile</button>
                <div className={classes["remove-icon"]} data-friend-id={friend.id} onClick={removeFriend}>
                  <PersonRemoveIcon/>  
                </div>
              </div>
            </div>
          );
        })) : (
        <p className={classes["no-data"]}>You don't have friends yet.</p>
        )
      }
      </ul>
    </div>
  )
}

export default FriendsPage;
