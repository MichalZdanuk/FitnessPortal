import classes from "./TrainingPage.module.css";
import workout from "../../assets/images/workout.png";
import { useNavigate } from "react-router-dom";
import {exercises} from "../../mocks/mockedData"
import AuthContext, { RequiredAuth } from "../../store/authContext";
import { useContext, useEffect, useState } from "react";
import axios from "axios";

import TrainingCard from "../../components/training/TrainingCard";
import MySpinner from "../../components/spinner/MySpinner";
const TrainingsPage = () => {
  const navigate = useNavigate();
  return (
    <RequiredAuth>
      <div className={classes["container"]}>
        <div className={classes["left-side"]}>
          <img src={workout} className={classes["workout-img"]} alt="workout" />
          <button onClick={(e) => {
          navigate(`/addTraining`);

        }} className={classes["add-training-button"]}>
            Add new training
          </button>
          <MostFrequentExercisesTable />
        </div>
        <div className={classes["right-side"]}>
          <p className={classes["training-history-label"]}>Last trainings</p>
              {/* {trainingList} */}
              <TrainingHistory />
              {/* needs to be fixed, if empty list then white stripe at bottom */}
        
        </div>
      </div>
    </RequiredAuth>
  );
};

const TrainingHistory = (props) => {
  const [data, setData] = useState(null);
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("https://localhost:7087/api/training", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
          params: {
            PageNumber: 1,
            PageSize: 3
          }
        });

        // Check if the response data is null and update the state accordingly
        setData(response.data.items || []);
        console.log(response.data.items);
        console.log(response.data.items.length);
        setLoading(false);
      } catch(error) {
        console.log(error);
      }
    };

    fetchData();
  }, []);

  return (<div className={classes["training-history-div"]}>
    {loading ? (
      <MySpinner />
    ): data && data.length > 0 ? (
      <>
      {data.map((training) => {
        return (
          <TrainingCard
          key={training.id}
          id={training.id}
          date={training.dateOfTraining}
          totalPayload={training.totalPayload}
          numOfSeries={training.numberOfSeries}
          listOfExercises={training.exercises}
          />
          )
        })}
        <div className={classes["show-more-div"]}>
          <button className={classes["show-more-button"]} onClick={(e) => {navigate("/account/traininghistory")}}>Show more</button>
        </div>
        </>
      
    ) : (<div className={classes["no-trainings-div"]}>No trainings yet.</div>)}
  </div>);
};

const MostFrequentExercisesTable = () => {
  return (
    <div className={classes["most-exercises-table"]}>
      <p className={classes["most-exercises-title"]}>
        You last most frequent exercises
      </p>
      <p className={classes["most-exercises-label"]}>
        (based on your last 3 training sessions)
      </p>
      <p>
        {exercises.map((ex) => {
          return (
            <li key={ex.id} className={classes["most-exercise-single-exercise"]}>
              {ex.name} reps: {ex.reps}
            </li>
          );
        })}
      </p>
    </div>
  );
};

export default TrainingsPage;
