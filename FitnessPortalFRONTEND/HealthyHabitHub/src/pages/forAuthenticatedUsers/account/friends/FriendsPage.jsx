import classes from './FriendsPage.module.css'
import FriendRequests from '../../../../components/friends/FriendRequests'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'
import FriendsList from '../../../../components/friends/FriendsList'
import AddFriend from '../../../../components/friends/AddFriend'
import { useContext } from 'react'
import AuthContext from '../../../../contexts/authContext'
import { useViewport } from '../../../../contexts/viewportContext'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'

const FriendsPage = () => {
  useCheckTokenExpiration()
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const { isMobile } = useViewport()
  const { contentHeight } = useContentHeightDesktop()

  const friendsDivHeight = isMobile ? contentHeight : '100%'

  return (
    <div
      className={classes['friends-page-div']}
      style={{ minHeight: friendsDivHeight }}
    >
      <div>
        <h2>Friends Panel</h2>
        <hr />
      </div>
      <FriendRequests token={token} />
      <AddFriend token={token} />
      <FriendsList token={token} />
    </div>
  )
}

export default FriendsPage
