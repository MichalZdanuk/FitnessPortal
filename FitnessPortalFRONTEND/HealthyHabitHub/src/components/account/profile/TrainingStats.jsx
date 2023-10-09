import { useContext, useEffect, useState } from 'react'
import classes from './TrainingStats.module.css'
import AuthContext from '../../../contexts/authContext'
import fetchData from '../../../utils/fetchData'
import MySpinner from '../../mySpinner/MySpinner'
import ChevronRightIcon from '@mui/icons-material/ChevronRight'
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter'
import { useViewport } from '../../../contexts/viewportContext'
import { Alert } from 'react-bootstrap'

const TrainingStats = () => {
  const url = '/api/training/stats'
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const [trainingStats, setTrainingStats] = useState()
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState(null)
  const { isMobile } = useViewport()

  useEffect(() => {
    const fetchTrainingStats = async () => {
      setIsLoading(true)
      setError(null)
      try {
        const data = await fetchData(url, null, {
          Authorization: `Bearer ${token}`,
        })
        setTrainingStats(data)
        setIsLoading(false)
      } catch (error) {
        setError('Error while fetching training stats')
        setIsLoading(false)
      }
    }

    fetchTrainingStats()
  }, [url, token])

  return (
    <div className={classes['training-stats-div']}>
      {isLoading ? (
        <MySpinner />
      ) : error ? (
        <Alert variant={'danger'}>{error}</Alert>
      ) : isMobile ? (
        <MobileTrainingStatsContent trainingStats={trainingStats} />
      ) : (
        <DesktopTrainingStatsContent trainingStats={trainingStats} />
      )}
    </div>
  )
}

const MobileTrainingStatsContent = ({ trainingStats }) => {
  return (
    <div className={classes['mobile-div']}>
      <div>
        <h3 className={classes['heading']}>
          Training Stats <FitnessCenterIcon />
        </h3>
        <p>Number of trainings: {trainingStats.numberOfTrainings}</p>
      </div>
      <div className={classes['trainings-container']}>
        <TrainingDiv
          name="Recent Training"
          trainingData={trainingStats.mostRecentTraining}
        />
        <TrainingDiv
          name="Best Training"
          trainingData={trainingStats.bestTraining}
        />
      </div>
    </div>
  )
}

const DesktopTrainingStatsContent = ({ trainingStats }) => {
  return (
    <div className={classes['desktop-div']}>
      <h3 className={classes['desktop-heading']}>
        Training Stats <FitnessCenterIcon />
      </h3>
      <hr className={classes['my-hr']} />
      <div className={classes['container']}>
        <div className={classes['column']}>
          <TrainingDiv
            name="Recent Training"
            trainingData={trainingStats.mostRecentTraining}
          />
        </div>
        <div className={classes['column']}>
          <TrainingDiv
            name="Best Training"
            trainingData={trainingStats.bestTraining}
          />
        </div>
      </div>
    </div>
  )
}

const TrainingDiv = ({ name, trainingData }) => {
  const { dateOfTraining, exercises, numberOfSeries, totalPayload } =
    trainingData ?? {}
  return (
    <div>
      <h4 className={classes['training-type']}>{name}</h4>
      <div>
        <p>
          <ChevronRightIcon />
          Performed date: {dateOfTraining && dateOfTraining.substring(0, 10)}
        </p>
        <p>
          <ChevronRightIcon />
          Payload: {totalPayload && totalPayload} kg
        </p>
        <p>
          <ChevronRightIcon />
          Number of series: {numberOfSeries && numberOfSeries}
        </p>
        <ul>
          {exercises &&
            exercises.map((ex) => (
              <li
                key={`${dateOfTraining}_${ex.name}`}
                className={classes['ex-item']}
              >
                {ex.name} - {ex.numberOfReps} x {ex.payload} kg
              </li>
            ))}
        </ul>
      </div>
    </div>
  )
}

export default TrainingStats
