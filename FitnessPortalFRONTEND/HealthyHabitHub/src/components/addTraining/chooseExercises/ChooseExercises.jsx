import { useViewport } from '../../../contexts/viewportContext'
import StickyButton from '../../buttons/StickyButton'
import LoudButton from '../../buttons/LoudButton'
import BodyPartLabel from './BodyPartLabel'
import classes from './ChooseExercises.module.css'
import MuscleGroupCard from './MuscleGroupCard'

const ChooseExercises = ({
  exercisesList,
  selectedExercises,
  onExerciseSelection,
  onProceedToDetails,
}) => {
  const desktopMargins = '10%'
  const { isMobile } = useViewport()
  const handleExerciseClick = (exercise) => {
    if (selectedExercises.includes(exercise)) {
      onExerciseSelection(selectedExercises.filter((item) => item !== exercise))
    } else {
      onExerciseSelection([...selectedExercises, exercise])
    }
  }

  const handleProceedClick = () => {
    onProceedToDetails()
  }

  const containerStyle = isMobile
    ? null
    : {
        marginLeft: desktopMargins,
        marginRight: desktopMargins,
      }

  return (
    <div className={classes['choose-exercises-div']} style={containerStyle}>
      <h2 className={classes['choose-label']}>Choose Exercises</h2>
      <h5 className={classes['sub-text']}>(Choose at least 6 exercises)</h5>
      {exercisesList.map((exerciseSet) => (
        <div className={classes['body-part-div']} key={exerciseSet.category}>
          <BodyPartLabel bodyPartName={exerciseSet.category} />
          <div className={classes['muscle-groups-container']}>
            {exerciseSet.exercises.map((bodyPart) => (
              <MuscleGroupCard
                key={`${exerciseSet.category}-${bodyPart.subcategory}`}
                muscleGroupName={bodyPart.subcategory}
                bodyPartExercises={bodyPart.subExercises}
                selectedExercises={selectedExercises}
                toggleExercise={handleExerciseClick}
              />
            ))}
          </div>
        </div>
      ))}
      {isMobile ? (
        <div className={classes['bottom-margin']}>
          <StickyButton
            text="CHOOSE PAYLOAD"
            onClick={handleProceedClick}
            disabled={selectedExercises.length < 6}
          />
        </div>
      ) : (
        <div className={classes['center-button']}>
          <LoudButton
            text="CHOOSE PAYLOAD"
            onClick={handleProceedClick}
            disabled={selectedExercises.length < 6}
          />
        </div>
      )}
    </div>
  )
}

export default ChooseExercises
