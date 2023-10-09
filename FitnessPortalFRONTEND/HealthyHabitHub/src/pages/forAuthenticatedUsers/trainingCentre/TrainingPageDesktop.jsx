import useContentHeightDesktop from '../../../hooks/useContentHeightDesktop'
import classes from './TrainingPageDesktop.module.css'
import RecentTrainings from '../../../components/trainings/RecentTrainings'
import FavouriteExercises from '../../../components/trainings/FavouriteExercises'
import workout from '../../../assets/images/workout.png'
import LoudButton from '../../../components/buttons/LoudButton'
import { useNavigate } from 'react-router-dom'

const TrainingPageDesktop = () => {
  const { contentHeight } = useContentHeightDesktop()

  return (
    <div
      className={classes['training-page-container']}
      style={{ minHeight: contentHeight }}
    >
      <div className={classes['left-side']}>
        <AddTrainingSection />
        <FavouriteExercises />
      </div>
      <div className={classes['right-side']}>
        <h1 className={classes['heading']}>Recent Trainings</h1>
        <hr className={classes['my-hr']} />
        <RecentTrainings />
      </div>
    </div>
  )
}

const AddTrainingSection = () => {
  const navigate = useNavigate()
  const handleAddTrainingClick = () => {
    navigate('/addTraining')
  }

  return (
    <div className={classes['add-training-section']}>
      <img src={workout} className={classes['workout-img']} alt="workout" />
      <LoudButton
        text="ADD TRAINING"
        widthPercentage={50}
        onClick={handleAddTrainingClick}
      />
    </div>
  )
}

export default TrainingPageDesktop
