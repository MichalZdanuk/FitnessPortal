import { useCallback, useContext, useEffect, useState } from 'react'
import AuthContext from '../../contexts/authContext'
import fetchData from '../../utils/fetchData'
import MySpinner from '../mySpinner/MySpinner'
import TrainingCard from './TrainingCard'
import classes from './RecentTrainings.module.css'
import { useViewport } from '../../contexts/viewportContext'
import NoDataMessage from '../messages/NoDatamessage'
import ErrorMessage from '../messages/ErrorMessage'

const RecentTrainings = () => {
  const [data, setData] = useState(null)
  const [dataFetched, setDataFetched] = useState(false)
  const [error, setError] = useState(null)
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const { isMobile } = useViewport()
  const pageSize = isMobile ? 5 : 3

  const fetchRecentTraining = useCallback(async () => {
    setError(null)
    try {
      const response = await fetchData(
        '/api/training',
        { PageNumber: 1, PageSize: pageSize },
        { Authorization: `Bearer ${token}` },
      )
      setData(response)
    } catch (error) {
      setError(error.message)
    } finally {
      setDataFetched(true)
    }
  }, [token, pageSize])

  useEffect(() => {
    if (!dataFetched)
      fetchRecentTraining().catch((error) => {
        setError(error.message)
      })
  }, [dataFetched, fetchRecentTraining])

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
        message="Error while fetching trainings"
        widthPercentage={80}
      />
    )
  }

  return (
    <>
      {data && data.items.length > 0 && (
        <div className={classes['recent-trainings-container']}>
          {data.items.map((training) => (
            <TrainingCard
              key={training.id}
              date={training.dateOfTraining}
              totalPayload={training.totalPayload}
              numOfSeries={training.numberOfSeries}
              listOfExercises={training.exercises}
            />
          ))}
        </div>
      )}
      {data && data.items.length === 0 && (
        <NoDataMessage
          mainMessage="No added trainings yet"
          linkMessage="Add training"
          link="/addTraining"
        />
      )}
    </>
  )
}

export default RecentTrainings
