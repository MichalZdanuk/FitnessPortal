import { useParams } from 'react-router-dom'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'
import classes from './FriendProfilePage.module.css'
import { useContext, useEffect, useState } from 'react'
import fetchData from '../../../../utils/fetchData'
import AuthContext from '../../../../contexts/authContext'
import MySpinner from '../../../../components/mySpinner/MySpinner'
import ErrorMessage from '../../../../components/messages/ErrorMessage'
import { useViewport } from '../../../../contexts/viewportContext'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'
import FriendTrainingData from '../../../../components/friendProfile/FriendTrainingData'
import FriendPersonalData from '../../../../components/friendProfile/FriendPersonalData'

const FriendProfilePage = () => {
  useCheckTokenExpiration()
  const { friendId } = useParams()
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const { isMobile } = useViewport()
  const { contentHeight } = useContentHeightDesktop()

  const url = `/api/friendship/friend/${friendId}`
  const [friendData, setFriendData] = useState(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    const fetchFriendData = async () => {
      setIsLoading(true)
      setError(null)
      try {
        const data = await fetchData(url, null, {
          Authorization: `Bearer ${token}`,
        })
        setFriendData(data)
      } catch (error) {
        if (error.response && error.response.status === 401) {
          setError('You are not allowed to see this user profile!')
        } else if (error.response && error.response.status === 404) {
          setError('You are not allowed to see this user profile!')
        } else {
          setError('Something went wrong.')
        }
      } finally {
        setIsLoading(false)
      }
    }

    fetchFriendData()
  }, [url, token])

  const friendProfileDivHeight = isMobile ? contentHeight : '100%'

  return (
    <div
      className={classes['friend-profile-div']}
      style={{ minHeight: friendProfileDivHeight }}
    >
      <div>
        <h2>Friend Profile</h2>
        <hr />
      </div>
      {isLoading ? (
        <MySpinner />
      ) : error ? (
        <ErrorMessage message={error} widthPercentage={70} />
      ) : (
        <div className={classes['data']}>
          <h3>Personal Info</h3>
          <FriendPersonalData
            username={friendData.username}
            email={friendData.email}
            weight={friendData.weight}
            height={friendData.height}
            dateOfBirth={friendData.dateOfBirth}
            numberOfFriends={friendData.numberOfFriends}
          />
          <h3>Last Trainings</h3>
          <FriendTrainingData trainings={friendData.lastThreeTrainings} />
        </div>
      )}
    </div>
  )
}

export default FriendProfilePage
