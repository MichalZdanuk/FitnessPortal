import classes from './TrainingPageMobile.module.css'
import FavouriteExercises from '../../../components/trainings/FavouriteExercises'
import RecentTrainings from '../../../components/trainings/RecentTrainings'
import StickyButton from '../../../components/buttons/StickyButton'
import { useNavigate } from 'react-router-dom'

const TrainingPageMobile = () => {
  const navigate = useNavigate()
  const handleAddTrainingClick = () => {
    navigate('/addTraining')
  }

  return (
    <div className={classes['training-page-container']}>
      <h1 className={classes['heading']}>Your Favourites</h1>
      <hr className={classes['my-hr']} />
      <FavouriteExercises />
      <h1 className={classes['heading']}>Recent Trainings</h1>
      <hr className={classes['my-hr']} />
      <RecentTrainings />
      <div className={classes['margin-bottom']} />
      <StickyButton text="ADD TRAINING" onClick={handleAddTrainingClick} />
    </div>
  )
}

export default TrainingPageMobile
