import classes from "./FriendProfilePage.module.css";
import { useLocation } from "react-router-dom";
import { RequiredAuth } from "../../store/authContext";
import TrainingInfo from "../../components/profile/TrainingInfo";
import PersonalInfo from "../../components/profile/PersonalInfo";
import CalculatorInfo from "../../components/profile/CalculatorInfo";
const FriendProfilePage = () => {
  const location = useLocation();
  const data = location.state;
  // console.log(data);
  const mocked= {
    username: "username",
    email: "email@email.com",
    dateOfBirth: "2023-06-13T20:38:06.607Z",
    weight: 0,
    height: 0
  };

  return (
    <RequiredAuth>
      <div className={classes["friend-profile"]}>
        <p className={classes["username"]}>{data.username}</p>
        <PersonalInfo data={mocked}/>
        <TrainingInfo />
        <CalculatorInfo />
      </div>
    </RequiredAuth>
  );
};

export default FriendProfilePage;
