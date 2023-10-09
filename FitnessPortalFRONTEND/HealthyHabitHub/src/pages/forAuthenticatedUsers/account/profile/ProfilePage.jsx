import classes from './ProfilePage.module.css'
import ProfileInfo from '../../../../components/account/profile/ProfileInfo'
import TrainingStats from '../../../../components/account/profile/TrainingStats'
import { useViewport } from '../../../../contexts/viewportContext'
import LoudButton from '../../../../components/buttons/LoudButton'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'
import { useNavigate } from 'react-router-dom'

const ProfilePage = () => {
  useCheckTokenExpiration()
  const { isMobile } = useViewport()
  const buttonWidth = isMobile ? 70 : 40
  const navigate = useNavigate()
  const handleUpdateClick = () => {
    navigate('/account/updateProfile')
  }

  return (
    <div className={classes['profile-page-div']}>
      <div>
        <h2>Account Panel</h2>
        <hr />
      </div>
      <ProfileInfo />
      <TrainingStats />
      <LoudButton
        text="UPDATE PROFILE"
        widthPercentage={buttonWidth}
        onClick={handleUpdateClick}
      />
    </div>
  )
}

export default ProfilePage
