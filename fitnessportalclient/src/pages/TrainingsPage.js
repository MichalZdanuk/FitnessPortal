import classes from "./TrainingPage.module.css";
import workout from "../assets/images/workout.png";

const TrainingsPage = () => {
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
    <div className={classes["container"]}>
      <div className={classes["left-side"]}>
        <img src={workout} className={classes["workout-img"]} alt="workout" />
        <button className={classes["add-training-button"]}>
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
  );
};

const trainings = [
  {
    id: 3,
    date: "18.03.2023",
    numOfSeries: 4,
    listOfExercises: [
      { name: "biceps curl", reps: "8", weight: "18" },
      { name: "squat", reps: "8", weight: "120" },
      { name: "push-up", reps: "20", weight: "" },
      { name: "bench press", reps: "6", weight: "80" },
      { name: "pull-up", reps: "12", weight: "" },
      { name: "deadlift", reps: "8", weight: "110" },
      { name: "overhead press", reps: "8", weight: "45" },
    ],
  },
  {
    id: 2,
    date: "21.03.2023",
    numOfSeries: 3,
    listOfExercises: [
      { name: "push-up", reps: "20", weight: "" },
      { name: "biceps curl", reps: "8", weight: "20" },
      { name: "squat", reps: "8", weight: "120" },
      { name: "bench press", reps: "6", weight: "75" },
      { name: "pull-up", reps: "12", weight: "" },
      { name: "deadlift", reps: "8", weight: "115" },
      { name: "overhead press", reps: "8", weight: "47.5" },
    ],
  },
  {
    id: 1,
    date: "25.03.2023",
    numOfSeries: 4,
    listOfExercises: [
      { name: "push-up", reps: "22", weight: "" },
      { name: "biceps curl", reps: "7", weight: "22" },
      { name: "squat", reps: "8", weight: "125" },
      { name: "pull-up", reps: "12", weight: "" },
      { name: "deadlift", reps: "8", weight: "117.5" },
      { name: "overhead press", reps: "8", weight: "50" },
    ],
  },
];

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

const exercises = [
  { name: "squats", reps: "100" },
  { name: "push-up", reps: "90" },
  { name: "pull-up", reps: "90" },
];

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
