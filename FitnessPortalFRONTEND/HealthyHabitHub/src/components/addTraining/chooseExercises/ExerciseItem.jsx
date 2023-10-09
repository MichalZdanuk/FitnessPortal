import classes from './ExerciseItem.module.css'

const ExerciseItem = ({ exercise, isSelected, toggleExercise }) => {
  return (
    <div
      className={`${classes['exercise-item']} ${
        isSelected ? classes['selected'] : ''
      }`}
      onClick={() => toggleExercise(exercise)}
    >
      {exercise}
    </div>
  )
}

export default ExerciseItem
