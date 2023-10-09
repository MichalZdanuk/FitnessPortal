import ExerciseItem from './ExerciseItem'
import classes from './MuscleGroupCard.module.css'
import MuscleGroupLabel from './MuscleGroupLabel'

const MuscleGroupCard = ({
  muscleGroupName,
  bodyPartExercises,
  selectedExercises,
  toggleExercise,
}) => {
  return (
    <div className={classes['muscle-group-div']}>
      <MuscleGroupLabel muscleGroupName={muscleGroupName} />
      <div>
        {bodyPartExercises.map((exercise) => (
          <ExerciseItem
            exercise={exercise}
            key={exercise}
            isSelected={selectedExercises.includes(exercise)}
            toggleExercise={toggleExercise}
          />
        ))}
      </div>
    </div>
  )
}

export default MuscleGroupCard
