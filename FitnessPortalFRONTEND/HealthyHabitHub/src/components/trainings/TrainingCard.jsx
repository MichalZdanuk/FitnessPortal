import classes from './TrainingCard.module.css'
import { formatPayload } from '../../utils/formatTrainingPayload'

const TrainingCard = ({ listOfExercises, date, totalPayload, numOfSeries }) => {
  const middleIndex = Math.ceil(listOfExercises.length / 2)

  return (
    <div className={classes['training-card-container']}>
      <div className={classes['general-info-container']}>
        <h3 className={classes['training-title']}>
          Training: {date.substring(0, 10)}
        </h3>
        <p>
          Series: {numOfSeries} Payload: {formatPayload(totalPayload)}
        </p>
      </div>
      <div className={classes['exercises-container']}>
        <div className={classes['column-left']}>
          {listOfExercises.slice(0, middleIndex).map((exercise, index) => (
            // eslint-disable-next-line react/no-array-index-key
            <li key={index}>
              {exercise.name}: {exercise.numberOfReps} x {exercise.payload}kg
            </li>
          ))}
        </div>
        <div className={classes['column-right']}>
          {listOfExercises.slice(middleIndex).map((exercise, index) => (
            // eslint-disable-next-line react/no-array-index-key
            <li key={index}>
              {exercise.name}: {exercise.numberOfReps} x {exercise.payload}kg
            </li>
          ))}
        </div>
      </div>
    </div>
  )
}

export default TrainingCard
