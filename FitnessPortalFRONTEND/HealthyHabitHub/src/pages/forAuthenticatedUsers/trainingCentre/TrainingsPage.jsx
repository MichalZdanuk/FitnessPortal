import { RequiredAuth } from '../../../contexts/authContext'
import { useViewport } from '../../../contexts/viewportContext'
import TrainingPageDesktop from './TrainingPageDesktop'
import TrainingPageMobile from './TrainingPageMobile'

const TrainingPage = () => {
  const { isMobile } = useViewport()

  return (
    <RequiredAuth>
      {isMobile ? <TrainingPageMobile /> : <TrainingPageDesktop />}
    </RequiredAuth>
  )
}

export default TrainingPage
