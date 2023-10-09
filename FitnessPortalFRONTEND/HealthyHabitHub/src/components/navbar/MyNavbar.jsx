import { useContext } from 'react'
import AuthContext from '../../contexts/authContext'
import { Navbar, Nav, Container } from 'react-bootstrap'
import classes from './MyNavbar.module.css'
import logoIcon from '../../assets/images/letterHicon.png'
import LogoutIcon from '@mui/icons-material/Logout'
import 'bootstrap/dist/css/bootstrap.css'
import { useNavigate } from 'react-router-dom'
import jwtDecode from 'jwt-decode'
import { useViewport } from '../../contexts/viewportContext'

const MyNavbar = () => {
  const authCtx = useContext(AuthContext)
  const isUserLogged = authCtx.isUserLogged

  const getUsername = () => {
    try {
      const decodedToken = jwtDecode(authCtx.tokenJWT)
      const usernameFromToken =
        decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
        ]
      return usernameFromToken
    } catch (error) {
      console.log('Error decoding token:', error)
    }
  }

  const navigate = useNavigate()

  const handleLogout = (e) => {
    e.preventDefault()
    navigate('/')
    authCtx.logout()
  }

  const { isMobile } = useViewport()
  const accountUrl = isMobile ? '/account' : '/account/profile'

  return (
    <Navbar
      collapseOnSelect
      expand="lg"
      bg="light"
      variant="light"
      sticky="top"
      className={classes['navbar-shadow']}
    >
      <Container>
        <Navbar.Brand href="/" className={classes['main-text']}>
          <img src={logoIcon} className={classes['logo-img']} alt="logo" />
          <span className={classes['margin-main-text']}>HealthyHabitHub</span>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="/articles">
              <span className={classes['custom-link']}>Articles</span>
            </Nav.Link>
            <Nav.Link href="/exercises">
              <span className={classes['custom-link']}>Exercises</span>
            </Nav.Link>
            <Nav.Link href="/calculators">
              <span className={classes['custom-link']}>Calculators</span>
            </Nav.Link>
            {isUserLogged && (
              <>
                <Nav.Link href="/trainings">
                  <span className={classes['custom-link']}>Trainings</span>
                </Nav.Link>
              </>
            )}
          </Nav>
          <Nav>
            {!isUserLogged && (
              <>
                <Nav.Link href="/register">
                  <span className={classes['custom-link']}>Register</span>
                </Nav.Link>
                <Nav.Link href="/login">
                  <span className={classes['custom-link']}>Login</span>
                </Nav.Link>
              </>
            )}
            {isUserLogged && (
              <div className={classes['flex']}>
                <Nav.Link href={accountUrl}>
                  <span className={classes['custom-link']}>Account</span>
                </Nav.Link>
                <Navbar.Brand onClick={handleLogout}>
                  <div className={classes['logged-panel']}>
                    Logged as: {getUsername()} <LogoutIcon />
                  </div>
                </Navbar.Brand>
              </div>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  )
}

export default MyNavbar
