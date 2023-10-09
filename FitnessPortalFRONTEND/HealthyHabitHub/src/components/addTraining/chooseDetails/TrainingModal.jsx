import classes from './TrainingModal.module.css'
import { Button, Modal } from 'react-bootstrap'
import { getCurrentDate } from '../../../utils/getDateAsString'
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter'
import SentimentVeryDissatisfiedIcon from '@mui/icons-material/SentimentVeryDissatisfied'
import { useNavigate } from 'react-router-dom'

const TrainingModal = ({
  show,
  handleClose,
  numberOfSeries,
  numberOfExercises,
  isError,
}) => {
  const navigate = useNavigate()
  const handleHomeClick = () => {
    handleClose()
    navigate('/')
  }

  const handleTrainingsClick = () => {
    handleClose()
    navigate('/account/trainingHistory')
  }

  return (
    <Modal
      show={show}
      onHide={handleClose}
      backdrop="static"
      keyboard={false}
      centered
    >
      <Modal.Header closeButton>
        <Modal.Title>
          {isError ? (
            <span className={classes['red-color']}>
              Error on sending training <SentimentVeryDissatisfiedIcon />
            </span>
          ) : (
            <span className={classes['green-color']}>
              Successfully added training <FitnessCenterIcon />
            </span>
          )}{' '}
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {isError ? (
          <div>
            Something went wrong
            <br />
            Please wait and try again
          </div>
        ) : (
          <div>
            <span className={classes['motto']}>
              One step closer to your dream body!
            </span>
            <br />
            Date: {getCurrentDate()}
            <br />
            Number of series: {numberOfSeries}
            <br />
            Number of exercises: {numberOfExercises}
          </div>
        )}
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleHomeClick}>
          Home
        </Button>
        {isError ? (
          ''
        ) : (
          <Button variant="success" onClick={handleTrainingsClick}>
            Trainings
          </Button>
        )}
      </Modal.Footer>
    </Modal>
  )
}

export default TrainingModal
