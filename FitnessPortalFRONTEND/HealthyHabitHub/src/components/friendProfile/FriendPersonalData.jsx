import { useViewport } from '../../contexts/viewportContext'
import classes from './FriendPersonalData.module.css'

const FriendPersonalData = ({
  username,
  email,
  weight,
  height,
  dateOfBirth,
  numberOfFriends,
}) => {
  const { isMobile } = useViewport()

  return isMobile ? (
    <MobilePersonalInfo
      username={username}
      email={email}
      weight={weight}
      height={height}
      dateOfBirth={dateOfBirth}
      numberOfFriends={numberOfFriends}
    />
  ) : (
    <DesktopPersonalInfo
      username={username}
      email={email}
      weight={weight}
      height={height}
      dateOfBirth={dateOfBirth}
      numberOfFriends={numberOfFriends}
    />
  )
}

const MobilePersonalInfo = ({
  username,
  email,
  weight,
  height,
  dateOfBirth,
  numberOfFriends,
}) => {
  return (
    <div className={classes['personal-div']}>
      <p>
        <span className={classes['thick-text']}>username:</span> {username}
      </p>
      <p>
        <span className={classes['thick-text']}>email:</span> {email}
      </p>
      <p>
        <span className={classes['thick-text']}>weight:</span> {weight}
      </p>
      <p>
        <span className={classes['thick-text']}>height:</span> {height}
      </p>
      <p>
        <span className={classes['thick-text']}>date of birth:</span>{' '}
        {dateOfBirth.substring(0, 10)}
      </p>
      <p>
        <span className={classes['thick-text']}>number of friends:</span>{' '}
        {numberOfFriends}
      </p>
    </div>
  )
}

const DesktopPersonalInfo = ({
  username,
  email,
  weight,
  height,
  dateOfBirth,
  numberOfFriends,
}) => {
  return (
    <div className={`${classes['personal-div']} ${classes['desktop']}`}>
      <div className={classes['row']}>
        <p className={classes['column']}>
          <span className={classes['thick-text']}>username:</span> {username}
        </p>
        <p className={classes['column']}>
          <span className={classes['thick-text']}>email:</span> {email}
        </p>
      </div>
      <div className={classes['row']}>
        <p className={classes['column']}>
          <span className={classes['thick-text']}>weight:</span> {weight}
        </p>
        <p className={classes['column']}>
          <span className={classes['thick-text']}>height:</span> {height}
        </p>
      </div>
      <div className={classes['row']}>
        <p className={classes['column']}>
          <span className={classes['thick-text']}>date of birth:</span>{' '}
          {dateOfBirth.substring(0, 10)}
        </p>
        <p className={classes['column']}>
          <span className={classes['thick-text']}>number of friends:</span>{' '}
          {numberOfFriends}
        </p>
      </div>
    </div>
  )
}

export default FriendPersonalData
