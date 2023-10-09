import { useEffect, useState } from 'react'
import classes from './AddFriend.module.css'
import fetchData from '../../utils/fetchData'
import { debounce } from 'lodash'
import { Alert } from 'react-bootstrap'
import MySpinner from '../mySpinner/MySpinner'
import ErrorMessage from '../messages/ErrorMessage'

const AddFriend = ({ token }) => {
  const [pattern, setPattern] = useState('')
  const [matchingUsers, setMatchingUsers] = useState([])
  const [successfullySentInvitation, setSuccessfullySentInvitation] =
    useState(false)
  const [error, setError] = useState(null)
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    const debouncedFetchMatchingUsers = debounce(async () => {
      if (pattern.length > 2) {
        setSuccessfullySentInvitation(false)
        setLoading(true)
        setError(null)
        try {
          const data = await fetchData(
            `/api/friendship/matching-users?pattern=${pattern}`,
            {},
            {
              Authorization: `Bearer ${token}`,
            },
          )
          setMatchingUsers(data)
        } catch (error) {
          console.error('Error fetching matching users:', error)
          setMatchingUsers([])
        } finally {
          setLoading(false)
        }
      } else {
        setMatchingUsers([])
      }
    }, 500)

    debouncedFetchMatchingUsers()
  }, [pattern, token])

  const handleInvite = async (userId) => {
    try {
      setSuccessfullySentInvitation(false)
      setError(null)
      setLoading(true)
      await fetchData(
        `/api/friendship/request/${userId}`,
        null,
        {
          Authorization: `Bearer ${token}`,
        },
        'post',
      )
      console.log('Invitation sent successfully!')
      setSuccessfullySentInvitation(true)
    } catch (error) {
      setError('Error while sending invitation')
      console.error('Error sending invitation:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className={classes['add-friend-div']}>
      <div>
        <h3 className={classes['title']}>Invite a Friend</h3>
        <hr />
      </div>
      <p className={classes['friend-add-motto']}>
        Enter your friends email to send an invitation.
      </p>
      <div className={classes['search-section-div']}>
        <input
          type="text"
          value={pattern}
          onChange={(e) => setPattern(e.target.value)}
          placeholder="Enter an email"
          className={classes['input-box']}
        />
        <ul className={classes['matching-users-list']}>
          {loading ? (
            <MySpinner />
          ) : error ? (
            <ErrorMessage message={error} widthPercentage={80} />
          ) : matchingUsers.length > 0 ? (
            matchingUsers.map((user) => (
              <li
                key={user.id}
                onClick={() => setPattern(user.email)}
                className={classes['matching-user']}
              >
                {user.email} {user.username}
              </li>
            ))
          ) : (
            <li>No matching users found.</li>
          )}
        </ul>
        <button
          className={classes['invite-button']}
          onClick={() =>
            matchingUsers.length > 0 && handleInvite(matchingUsers[0].id)
          }
          disabled={loading || matchingUsers.length !== 1}
        >
          Invite
        </button>
        {successfullySentInvitation && (
          <div className={classes['alert-div']}>
            <Alert variant={'success'}>Invitation sent successfully!</Alert>
          </div>
        )}
      </div>
    </div>
  )
}

export default AddFriend
