import { useContext, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import AuthContext from '../../contexts/authContext'
import axios from 'axios'
import { apiUrl } from '../../global/api_url'
import { InputField } from './InputField'
import classes from './LoginForm.module.css'
import { Alert } from 'react-bootstrap'
import LoudButton from '../buttons/LoudButton'
import { useViewport } from '../../contexts/viewportContext'
import RemoveRedEyeIcon from '@mui/icons-material/RemoveRedEye'
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff'

export const LoginForm = () => {
  const navigate = useNavigate()
  const authCtx = useContext(AuthContext)
  const { isMobile } = useViewport()

  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [loginError, setLoginError] = useState('')
  const [showPassword, setShowPassword] = useState(false)

  const handleLogin = async (e) => {
    e.preventDefault()
    try {
      const url = apiUrl + '/api/account/login'

      const response = await axios.post(url, {
        email: email,
        password: password,
      })

      authCtx.login(response.data)
      navigate('/')
    } catch (error) {
      setLoginError({ title: 'Login credentials invalid' })
    }
  }

  const buttonWidth = isMobile ? 50 : 90

  return (
    <div className={classes['container']}>
      <form onSubmit={handleLogin} className={classes['form-container']}>
        <InputField
          inputName="Email"
          value={email}
          handleOnChange={(e) => setEmail(e.target.value)}
          placeholder="enter email"
          type="email"
        />
        <InputField
          inputName="Password"
          value={password}
          handleOnChange={(e) => setPassword(e.target.value)}
          placeholder="enter password"
          type={showPassword ? 'text' : 'password'}
          endIcon={
            showPassword ? (
              <VisibilityOffIcon
                onClick={() => setShowPassword(false)}
                style={{ cursor: 'pointer' }}
              />
            ) : (
              <RemoveRedEyeIcon
                onClick={() => setShowPassword(true)}
                style={{ cursor: 'pointer' }}
              />
            )
          }
        />
        <LoudButton text="LOG IN" widthPercentage={buttonWidth} type="submit" />
      </form>
      <h5
        className={classes['bottom-text']}
        onClick={() => {
          navigate('/register')
        }}
      >
        Do not have an account?{' '}
        <span className={classes['emph-text']}>Create NOW</span>
      </h5>
      {loginError && (
        <div className={classes['alert-div']}>
          <Alert variant={'danger'}>{loginError.title}</Alert>
        </div>
      )}
    </div>
  )
}
