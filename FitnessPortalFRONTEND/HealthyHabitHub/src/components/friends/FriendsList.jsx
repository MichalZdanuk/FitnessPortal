import classes from './FriendsList.module.css'
import AccountBoxIcon from '@mui/icons-material/AccountBox'
import PersonRemoveIcon from '@mui/icons-material/PersonRemove'
import { useNavigate } from 'react-router-dom'
import { useEffect, useState } from 'react'
import fetchData from '../../utils/fetchData'
import MySpinner from '../mySpinner/MySpinner'
import ErrorMessage from '../messages/ErrorMessage'

const FriendsList = ({ token }) => {
  const navigate = useNavigate()
  const [friendList, setFriendList] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  useEffect(() => {
    const fetchFriendsList = async () => {
      try {
        const headers = {
          Authorization: `Bearer ${token}`,
        }
        const data = await fetchData('/api/friendship/friends', {}, headers)
        setFriendList(data)
      } catch (error) {
        setError('Error while fetching friends list.')
      } finally {
        setLoading(false)
      }
    }

    fetchFriendsList()
  }, [token, friendList])

  const removeFriend = async (friendId) => {
    try {
      setLoading(true)
      setError(null)
      const headers = {
        Authorization: `Bearer ${token}`,
      }
      await fetchData(
        `/api/friendship/remove/${friendId}`,
        {},
        headers,
        'delete',
      )
      const updatedList = friendList.filter((friend) => friend.id !== friend)
      setFriendList(updatedList)
    } catch (error) {
      setError('Error while removing friend.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className={classes['friends-list-div']}>
      <div>
        <h3 className={classes['title']}>Friends List</h3>
        <hr />
      </div>
      <p className={classes['friend-list-motto']}>
        Cherish your friendships, explore the bonds!
      </p>
      {loading ? (
        <div className={classes['center-spinner']}>
          <MySpinner />
        </div>
      ) : error ? (
        <ErrorMessage message={error} widthPercentage={80} />
      ) : friendList.length > 0 ? (
        <div className={classes['friends-list']}>
          {friendList.map((friend) => (
            <FriendItem
              key={friend.id}
              friend={friend}
              removeFriend={removeFriend}
              navigate={navigate}
            />
          ))}
        </div>
      ) : (
        <p className={classes['no-friends']}>You dont have friends yet.</p>
      )}
    </div>
  )
}

const FriendItem = ({ friend, removeFriend, navigate }) => {
  return (
    <div className={classes['friend-div']}>
      <h5 className={classes['contact-data']}>
        {friend.username} {friend.email}
      </h5>
      <div className={classes['manage-div']}>
        <AccountBoxIcon
          className={classes['profile-icon']}
          onClick={() => navigate(`/account/friendlist/${friend.id}`)}
        />
        <PersonRemoveIcon
          className={classes['remove-icon']}
          onClick={() => removeFriend(friend.id)}
        />
      </div>
    </div>
  )
}

export default FriendsList
