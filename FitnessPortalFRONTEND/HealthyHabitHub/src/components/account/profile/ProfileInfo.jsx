import { useContext, useEffect, useState } from 'react'
import classes from './ProfileInfo.module.css'
import AuthContext from '../../../contexts/authContext'
import fetchData from '../../../utils/fetchData'
import MySpinner from '../../mySpinner/MySpinner'
import ChevronRightIcon from '@mui/icons-material/ChevronRight'
import FaceIcon from '@mui/icons-material/Face'
import { useViewport } from '../../../contexts/viewportContext'
import { Alert } from 'react-bootstrap'

const ProfileInfo = () => {
  const url = '/api/account/profile-info'
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const [profileData, setProfileData] = useState()
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState(null)
  const { isMobile } = useViewport()

  useEffect(() => {
    const fetchProfileData = async () => {
      setIsLoading(true)
      setError(null)
      try {
        const data = await fetchData(url, null, {
          Authorization: `Bearer ${token}`,
        })
        setProfileData(data)
        setIsLoading(false)
      } catch (error) {
        setError('Error while fetching profile info')
        setIsLoading(false)
      }
    }

    fetchProfileData()
  }, [url, token])

  return (
    <div className={classes['profile-info-div']}>
      {isLoading ? (
        <MySpinner />
      ) : error ? (
        <Alert variant={'danger'}>{error}</Alert>
      ) : isMobile ? (
        <MobileProfileContent profileData={profileData} />
      ) : (
        <DesktopProfileContent profileData={profileData} />
      )}
    </div>
  )
}

const MobileProfileContent = ({ profileData }) => {
  const { dateOfBirth, numberOfFriends, height, weight } = profileData
  return (
    <div className={classes['mobile-div']}>
      <h3 className={classes['heading']}>
        Personal Info <FaceIcon />
      </h3>
      <div className={classes['info-container']}>
        <p>
          <ChevronRightIcon />
          Date of birth: {dateOfBirth.substring(0, 10)}
        </p>
        <p>
          <ChevronRightIcon />
          Height: {height} cm
        </p>
        <p>
          <ChevronRightIcon />
          Weight: {weight} kg
        </p>
        <p>
          <ChevronRightIcon />
          Number of friends: {numberOfFriends}
        </p>
      </div>
    </div>
  )
}

const DesktopProfileContent = ({ profileData }) => {
  const { dateOfBirth, numberOfFriends, height, weight } = profileData
  return (
    <div className={classes['desktop-div']}>
      <h3 className={classes['desktop-heading']}>
        Personal Info <FaceIcon />
      </h3>
      <hr className={classes['my-hr']} />
      <div className={classes['container']}>
        <div className={classes['column']}>
          <ul className={classes['no-bullet-points']}>
            <li>
              <ChevronRightIcon />
              Date of birth: {dateOfBirth.substring(0, 10)}
            </li>
            <li>
              <ChevronRightIcon />
              Number of friends: {numberOfFriends}
            </li>
          </ul>
        </div>
        <div className={classes['column']}>
          <ul className={classes['no-bullet-points']}>
            <li>
              <ChevronRightIcon />
              Height: {height} cm
            </li>
            <li>
              <ChevronRightIcon />
              Weight: {weight} kg
            </li>
          </ul>
        </div>
      </div>
    </div>
  )
}

export default ProfileInfo
