import { useEffect, useState } from 'react'
import classes from './FriendRequests.module.css'
import PersonAddIcon from '@mui/icons-material/PersonAdd'
import PersonRemoveIcon from '@mui/icons-material/PersonRemove'
import fetchData from '../../utils/fetchData'
import MySpinner from '../mySpinner/MySpinner'
import ErrorMessage from '../messages/ErrorMessage'

const FriendRequests = ({ token }) => {
  const [friendRequestsList, setFriendRequestsList] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)
  const url = '/api/friendship/friendship-requests'

  useEffect(() => {
    const fetchRequests = async () => {
      try {
        const data = await fetchData(
          url,
          {},
          {
            Authorization: `Bearer ${token}`,
          },
        )

        setFriendRequestsList(data)
      } catch (error) {
        setError('Error while fetching friend requests.')
      } finally {
        setLoading(false)
      }
    }

    fetchRequests()
  }, [token])

  const acceptRequest = async (requestId) => {
    try {
      setLoading(true)
      setError(null)
      const headers = {
        Authorization: `Bearer ${token}`,
      }
      await fetchData(
        `/api/friendship/accept/${requestId}`,
        {},
        headers,
        'post',
      )
      const updatedList = friendRequestsList.filter(
        (request) => request.id !== requestId,
      )
      setFriendRequestsList(updatedList)
    } catch (error) {
      setError('Error on accepting friend request.')
    } finally {
      setLoading(false)
    }
  }

  const rejectRequest = async (requestId) => {
    try {
      setLoading(true)
      setError(null)
      const headers = {
        Authorization: `Bearer ${token}`,
      }
      await fetchData(
        `/api/friendship/reject/${requestId}`,
        {},
        headers,
        'delete',
      )
      const updatedList = friendRequestsList.filter(
        (request) => request.id !== requestId,
      )
      setFriendRequestsList(updatedList)
    } catch (error) {
      setError('Error on rejecting friend request.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className={classes['friend-requests-div']}>
      <div>
        <h3 className={classes['title']}>Friends Requests</h3>
        <hr />
      </div>
      <p className={classes['friend-request-motto']}>
        Expand your social circle, accept new connections!
      </p>
      {loading ? (
        <div className={classes['center-spinner']}>
          <MySpinner />
        </div>
      ) : error ? (
        <ErrorMessage message={error} widthPercentage={80} />
      ) : friendRequestsList.length > 0 ? (
        <div className={classes['requests-list']}>
          {friendRequestsList.map((request) => (
            <FriendRequestItem
              key={request.id}
              request={request}
              acceptRequest={acceptRequest}
              rejectRequest={rejectRequest}
            />
          ))}
        </div>
      ) : (
        <p className={classes['no-friend-requests']}>
          You dont have any friend requests at the moment
        </p>
      )}
    </div>
  )
}

const FriendRequestItem = ({ request, acceptRequest, rejectRequest }) => {
  return (
    <div className={classes['request-div']}>
      <h5>{request.senderName}</h5>
      <div className={classes['icons-container']}>
        <h5>{request.sendDate.toString().substring(0, 10)}</h5>
        <PersonAddIcon
          onClick={() => acceptRequest(request.id)}
          className={`${classes['icon']} ${classes['accept-request-icon']}`}
        />
        <PersonRemoveIcon
          onClick={() => rejectRequest(request.id)}
          className={`${classes['icon']} ${classes['reject-request-icon']}`}
        />
      </div>
    </div>
  )
}

export default FriendRequests
