import classes from './ExercisesPage.module.css'
import ExerciseThumbnail from '../../../components/exercise/ExerciseThumbnail'
import { exercisesList } from '../../../mocks/mockedExercises'

const ExercisesPage = () => {
  const exercisesThumbnailsList = exercisesList.map((exercise) => (
    <ExerciseThumbnail
      key={exercise.id}
      photo={exercise.img}
      name={exercise.name}
      description={exercise.description}
    />
  ))

  return (
    <div className={classes['exercises-page-container']}>
      {exercisesThumbnailsList}
    </div>
  )
}

export default ExercisesPage
