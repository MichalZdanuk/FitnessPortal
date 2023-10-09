import { useCallback, useContext, useEffect, useState } from 'react'
import classes from './FavouriteExercises.module.css'
import fetchData from '../../utils/fetchData'
import AuthContext from '../../contexts/authContext'
import MySpinner from '../mySpinner/MySpinner'
import NoDataMessage from '../messages/NoDatamessage'
import ErrorMessage from '../messages/ErrorMessage'
import { formatPayload } from '../../utils/formatTrainingPayload'

const FavouriteExercises = () => {
  const [data, setData] = useState(null)
  const [dataFetched, setDataFetched] = useState(false)
  const [error, setError] = useState(null)
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT

  const fetchFavouriteExercises = useCallback(async () => {
    setError(null)
    try {
      const response = await fetchData(
        '/api/training/favourite',
        {},
        {
          Authorization: `Bearer ${token}`,
        },
      )

      setData(response.exercises)
    } catch (error) {
      setError(error.message)
    } finally {
      setDataFetched(true)
    }
  }, [token])

  useEffect(() => {
    if (!dataFetched)
      fetchFavouriteExercises().catch((error) => {
        setError(error.message)
      })
  }, [dataFetched, fetchFavouriteExercises])

  if (!dataFetched) {
    return (
      <div className={classes['center-spinner']}>
        <MySpinner />
      </div>
    )
  }

  if (error) {
    return (
      <ErrorMessage
        message="Error while fetching favourites"
        widthPercentage={80}
      />
    )
  }
  // Dumbbell Flyes , Overhead Press , Bent-Over Rows , Reverse Wrist Curls , Glute Bridges , Standing Calf Raises , Romanian Deadlift
  return (
    <div className={classes.wrapper}>
      <p className={classes['info-paragraph']}>
        Based on your last 3 training sessions
      </p>
      <div className={classes['favourite-exercises-container']}>
        {data &&
          data.map((data) => (
            <div key={data.name} className={classes['exercise-card']}>
              <h4 className={classes['exercise-name']}>{data.name}</h4>
              <div>
                <h5>{data.numberOfReps} Reps</h5>
                <h5>{formatPayload(data.payload)}</h5>
              </div>
            </div>
          ))}
        {data.length === 0 && (
          <NoDataMessage
            mainMessage="No favourite exercises yet"
            linkMessage="Add training"
            link="/addTraining"
          />
        )}
      </div>
    </div>
  )
}

export default FavouriteExercises
