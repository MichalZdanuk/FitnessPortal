import ChooseExercises from '../../../components/addTraining/chooseExercises/ChooseExercises'
import { RequiredAuth } from '../../../contexts/authContext'
import { exercisesForTraining } from '../../../mocks/exercisesInTrainingOffer'
import { useCheckTokenExpiration } from '../../../hooks/useCheckTokenExpiration'
import { useState } from 'react'
import ChooseTrainingDetails from '../../../components/addTraining/chooseDetails/ChooseTrainingDetails'

const AddTrainingPage = () => {
  useCheckTokenExpiration()
  const [showTrainingDetails, setShowTrainingDetails] = useState(false)
  const [selectedExercises, setSelectedExercises] = useState([])

  const handleProceedToDetails = () => {
    if (selectedExercises.length >= 6) {
      setShowTrainingDetails(true)
    }
  }

  const handleGoBack = () => {
    setShowTrainingDetails(false)
  }

  return (
    <RequiredAuth>
      {showTrainingDetails ? (
        <ChooseTrainingDetails
          selectedExercises={selectedExercises}
          onGoBack={handleGoBack}
        />
      ) : (
        <ChooseExercises
          exercisesList={exercisesForTraining}
          selectedExercises={selectedExercises}
          onExerciseSelection={(selected) => setSelectedExercises(selected)}
          onProceedToDetails={handleProceedToDetails}
        />
      )}
    </RequiredAuth>
  )
}

export default AddTrainingPage
