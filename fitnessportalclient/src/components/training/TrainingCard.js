import classes from "./TrainingCard.module.css"

const TrainingCard = (props) => {
    const middleIndex = Math.ceil(props.listOfExercises.length / 2);
    
    return (
      <div className={classes["training-card-div"]}>
        <p className={classes["training-title"]}>
          Training: {props.date.substring(0,10)}
        </p>
        <p className={classes["num-of-series-label"]}>Number of series: {props.numOfSeries}</p>
        <div className={classes["exercises-container"]}>
        <div className={classes["column"]}>
          {props.listOfExercises.slice(0, middleIndex).map((exercise, index) => (
            <div key={index}>
              <li className={classes["exercise-elem"]}>{exercise.name} reps: {exercise.numberOfReps} weight: {exercise.payload}</li>
            </div>
          ))}
        </div>
        <div className={classes["column"]}>
          {props.listOfExercises.slice(middleIndex).map((exercise, index) => (
            <div key={index}>
              <li className={classes["exercise-elem"]}>{exercise.name} reps: {exercise.numberOfReps} weight: {exercise.payload}</li>
            </div>
          ))}
        </div>
      </div>
      </div>
    );
  };

export default TrainingCard