import classes from "./TrainingPage.module.css";
import workout from "../assets/images/workout.png";
import { useNavigate } from "react-router-dom";
import {trainings, exercises} from "../mocks/mockedData"
import { RequiredAuth } from "../store/authContext";

const TrainingsPage = () => {
  const navigate = useNavigate();
  const trainingList = trainings.map((training) => {
    return (
      <TrainingCard
        id={training.id}
        date={training.date}
        numOfSeries={training.numOfSeries}
        listOfExercises={training.listOfExercises}
      />
    );
  });
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
          <p className={classes["training-history-label"]}>Training history</p>
              {trainingList}
              {/* needs to be fixed, if empty list then white stripe at bottom */}
        <button className={classes["show-more-button"]}>Show more</button>
        </div>
      </div>
    </RequiredAuth>
  );
};

const TrainingCard = (props) => {
  const middleIndex = Math.ceil(props.listOfExercises.length / 2);
  
  return (
    <div className={classes["training-div"]}>
      <p className={classes["training-title"]}>
        Training {props.id} : {props.date}
      </p>
      <p className={classes["num-of-series-label"]}>Number of series: {props.numOfSeries}</p>
      <div className={classes["exercises-container"]}>
      <div className={classes["column"]}>
        {props.listOfExercises.slice(0, middleIndex).map((exercise, index) => (
          <div key={index}>
            <li className={classes["exercise-elem"]}>{exercise.name} reps: {exercise.reps} weight: {exercise.weight}</li>
          </div>
        ))}
      </div>
      <div className={classes["column"]}>
        {props.listOfExercises.slice(middleIndex).map((exercise, index) => (
          <div key={index}>
            <li className={classes["exercise-elem"]}>{exercise.name} reps: {exercise.reps} weight: {exercise.weight}</li>
          </div>
        ))}
      </div>
    </div>
    </div>
  );
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
            <li className={classes["most-exercise-single-exercise"]}>
              {ex.name} reps: {ex.reps}
            </li>
          );
        })}
      </p>
    </div>
  );
};

export default TrainingsPage;
