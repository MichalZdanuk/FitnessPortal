import { useViewport } from '../../contexts/viewportContext'
import classes from './WelcomeBanner.module.css'
import bodyImage from '../../assets/images/workout_main_page.png'
import LoudButton from '../buttons/LoudButton'
import { useNavigate } from 'react-router-dom'

const WelcomeBanner = () => {
  const { isMobile } = useViewport()

  return isMobile ? <MobileBanner /> : <DesktopBanner />
}

const MobileBanner = () => {
  const navigate = useNavigate()
  const handleStartClick = () => {
    navigate('/register')
  }

  return (
    <div className={classes['mobile-container']}>
      <WelcomeTextMobile />
      <BodyImage />
      <MotivationText />
      <LoudButton
        text="START NOW"
        widthPercentage={70}
        onClick={handleStartClick}
      />
    </div>
  )
}

const DesktopBanner = () => {
  const navigate = useNavigate()
  const handleStartClick = () => {
    navigate('/register')
  }

  return (
    <div className={classes['desktop-container']}>
      <div className={classes['text-container']}>
        <WelcomeText />
        <MotivationText />
        <LoudButton
          text="START NOW"
          widthPercentage={80}
          onClick={handleStartClick}
        />
      </div>
      <div className={classes['image-container']}>
        <BodyImage />
      </div>
    </div>
  )
}

const WelcomeTextMobile = () => {
  return (
    <h1 className={classes['mobile-text']}>
      SHAPE YOUR
      <br />
      IDEAL BODY
    </h1>
  )
}

const WelcomeText = () => {
  return (
    <h1 className={classes['welcome-text-desktop']}>
      <span className={classes['text-with-border']}>SHAPE</span> YOUR
      <br />
      IDEAL BODY
    </h1>
  )
}

const BodyImage = () => {
  return (
    <img className={classes['body-img']} src={bodyImage} alt="body image" />
  )
}

const MotivationText = () => {
  return (
    <h4>
      <span className={classes['green-text']}>HealthyHabitHub</span> will help
      you to measure your progress and achieve your dream body
    </h4>
  )
}

export default WelcomeBanner
