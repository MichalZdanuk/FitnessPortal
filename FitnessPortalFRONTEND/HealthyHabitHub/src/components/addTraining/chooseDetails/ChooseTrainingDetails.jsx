import { useContext, useEffect, useState } from 'react'
import { useViewport } from '../../../contexts/viewportContext'
import classes from './ChooseTrainingDetails.module.css'
import ExerciseRow from './ExerciseRow'
import AddIcon from '@mui/icons-material/Add'
import RemoveIcon from '@mui/icons-material/Remove'
import StickyButton from '../../buttons/StickyButton'
import LoudButton from '../../buttons/LoudButton'
import TrainingModal from './TrainingModal'
import AuthContext from '../../../contexts/authContext'
import axios from 'axios'
import { apiUrl } from '../../../global/api_url'

const ChooseTrainingDetails = ({ selectedExercises, onGoBack }) => {
  const [trainingObject, setTrainingObject] = useState({
    numberOfSeries: 0,
    exercises: [],
  })
  const authCtx = useContext(AuthContext)

  const [numberOfSeries, setNumberOfSeries] = useState(
    trainingObject.numberOfSeries,
  )

  const desktopMargins = '10%'
  const { isMobile } = useViewport()
  const containerStyle = isMobile
    ? null
    : {
        marginLeft: desktopMargins,
        marginRight: desktopMargins,
      }

  const [isError, setIsError] = useState(false)
  const [show, setShow] = useState(false)
  const handleClose = () => setShow(false)
  const handleShow = () => setShow(true)

  const sendTraining = async () => {
    const token = authCtx.tokenJWT
    const url = apiUrl + '/api/training'

    try {
      const response = await axios.post(url, trainingObject, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })

      if (response.status === 201) {
        setIsError(false)
      } else {
        setIsError(true)
      }
    } catch (error) {
      setIsError(true)
    }
    handleShow()
  }

  useEffect(() => {
    const updatedExercises = selectedExercises.map((exercise) => {
      const existingExercise = trainingObject.exercises.find(
        (ex) => ex.name === exercise,
      )

      const newExercise = {
        name: exercise,
        numberOfReps: existingExercise ? existingExercise.numberOfReps : 0,
        payload: existingExercise ? existingExercise.payload : 0,
      }

      return newExercise
    })

    setTrainingObject((prevState) => ({
      ...prevState,
      exercises: updatedExercises,
    }))
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedExercises])

  const handleNumberOfSeriesChange = (value) => {
    if (value >= 0 && value <= 7) {
      setNumberOfSeries(value)
      setTrainingObject((prevState) => ({
        ...prevState,
        numberOfSeries: value,
      }))
    }
  }

  const handleIncrement = () => {
    handleNumberOfSeriesChange(numberOfSeries + 1)
  }

  const handleDecrement = () => {
    handleNumberOfSeriesChange(numberOfSeries - 1)
  }

  const handleTrainingObjectChange = (exerciseName, key, value) => {
    setTrainingObject((prevState) => ({
      ...prevState,
      exercises: prevState.exercises.map((exercise) => {
        if (exercise.name === exerciseName) {
          return {
            ...exercise,
            [key]: value,
          }
        }
        return exercise
      }),
    }))
  }

  const isTrainingReady = () => {
    // Check if numberOfSeries is not 0
    if (numberOfSeries === 0) {
      return false
    }

    // Check if any exercise has non-zero numberOfReps or payload
    for (const exercise of trainingObject.exercises) {
      if (exercise.numberOfReps === 0 || exercise.payload === 0) {
        return false
      }
    }

    return true
  }

  return (
    <div className={classes['choose-details-div']} style={containerStyle}>
      <TrainingModal
        show={show}
        handleClose={handleClose}
        numberOfSeries={trainingObject.numberOfSeries}
        numberOfExercises={trainingObject.exercises.length}
        isError={isError}
      />
      <h2 className={classes['main-label']}>Set Training Details</h2>
      <h5 className={classes['sub-text']}>
        (Set number of series, reps, and payload)
      </h5>
      <div className={classes['number-of-series-div']}>
        <label htmlFor="numberOfSeries">Number of Series:</label>
        <input
          type="number"
          id="numberOfSeries"
          value={numberOfSeries}
          onChange={(e) => handleNumberOfSeriesChange(parseInt(e.target.value))}
          className={classes['series-input']}
        />
        <AddIcon
          className={`${classes['icon']} ${classes['add']}`}
          onClick={handleIncrement}
        />
        <RemoveIcon
          className={`${classes['icon']} ${classes['remove']}`}
          onClick={handleDecrement}
        />
      </div>
      <div>
        {selectedExercises.map((exercise) => (
          <ExerciseRow
            key={exercise}
            name={exercise}
            numberOfReps={
              trainingObject.exercises.find((ex) => ex.name === exercise)
                ?.numberOfReps || 0
            }
            payload={
              trainingObject.exercises.find((ex) => ex.name === exercise)
                ?.payload || 0
            }
            onChange={(key, value) =>
              handleTrainingObjectChange(exercise, key, value)
            }
          />
        ))}
      </div>
      <div className={classes['center-button']}>
        <button onClick={onGoBack} className={classes['back-button']}>
          GO BACK
        </button>
      </div>
      {isMobile ? (
        <div className={classes['bottom-margin']}>
          <StickyButton
            text="ADD TRAINING"
            onClick={() => {
              sendTraining()
            }}
            disabled={!isTrainingReady()}
          />
        </div>
      ) : (
        <div className={classes['center-button']}>
          <LoudButton
            text="ADD TRAINING"
            onClick={() => {
              sendTraining()
            }}
            disabled={!isTrainingReady()}
          />
        </div>
      )}
    </div>
  )
}

export default ChooseTrainingDetails
