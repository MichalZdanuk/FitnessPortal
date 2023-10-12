import TrainingCard from '../trainings/TrainingCard'
import classes from './FriendTrainingData.module.css'

const FriendTrainingData = ({ trainings }) => {
  return (
    <div className={classes['friend-trainings-div']}>
      {trainings && trainings.length > 0 ? (
        <div className={classes['trainings']}>
          {trainings.map((training) => (
            <TrainingCard
              key={training.id}
              date={training.dateOfTraining}
              totalPayload={training.totalPayload}
              numOfSeries={training.numberOfSeries}
              listOfExercises={training.exercises}
            />
          ))}
        </div>
      ) : (
        <div className={classes['no-trainings']}>
          This friend has not add any trainings yet.
        </div>
      )}
    </div>
  )
}

export default FriendTrainingData
