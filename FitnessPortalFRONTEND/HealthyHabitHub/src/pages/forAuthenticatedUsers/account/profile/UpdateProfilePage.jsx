import { useContext, useEffect, useState } from 'react'
import classes from './UpdateProfilePage.module.css'
import AuthContext from '../../../../contexts/authContext'
import fetchData from '../../../../utils/fetchData'
import ErrorMessage from '../../../../components/messages/ErrorMessage'
import MySpinner from '../../../../components/mySpinner/MySpinner'
import UpdateProfileForm from '../../../../components/account/profile/UpdateProfileForm'
import { useViewport } from '../../../../contexts/viewportContext'
import useContentHeightDesktop from '../../../../hooks/useContentHeightDesktop'
import { useCheckTokenExpiration } from '../../../../hooks/useCheckTokenExpiration'

const UpdateProfilePage = () => {
  useCheckTokenExpiration()
  const authCtx = useContext(AuthContext)
  const token = authCtx.tokenJWT
  const [userData, setUserData] = useState(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)
  const { isMobile } = useViewport()
  const { contentHeight } = useContentHeightDesktop()

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const headers = {
          Authorization: `Bearer ${token}`,
        }

        const userData = await fetchData(
          '/api/account/profile-info',
          {},
          headers,
        )

        setUserData(userData)
      } catch (error) {
        setError('An error occured while fetching user data.')
      } finally {
        setLoading(false)
      }
    }

    fetchUserData()
  }, [token])

  const updateprofileDivHeight = isMobile ? contentHeight : '100%'

  return (
    <div
      className={classes['update-profile-page-div']}
      style={{ minHeight: updateprofileDivHeight }}
    >
      <div>
        <h2>Update Profile</h2>
        <hr />
      </div>
      {loading && (
        <div className={classes['center-spinner']}>
          <MySpinner />
        </div>
      )}
      {error && (
        <ErrorMessage
          message="Error occured while fetching user data."
          widthPercentage={80}
        />
      )}
      {userData && <UpdateProfileForm userData={userData} />}
      <div></div>
    </div>
  )
}

export default UpdateProfilePage
