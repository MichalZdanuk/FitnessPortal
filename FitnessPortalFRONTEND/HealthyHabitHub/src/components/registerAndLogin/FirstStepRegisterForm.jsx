import classes from './RegisterForm.module.css'
import { useNavigate } from 'react-router-dom'
import { InputField } from './InputField'
import LoudButton from '../buttons/LoudButton'
import { useViewport } from '../../contexts/viewportContext'
import { useState } from 'react'
import RemoveRedEyeIcon from '@mui/icons-material/RemoveRedEye'
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff'

const FirstStepRegisterForm = ({
  registerInput,
  handleRegister,
  onRegisterInputChange,
}) => {
  const navigate = useNavigate()
  const { isMobile } = useViewport()
  const buttonWidth = isMobile ? 50 : 90
  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)

  return (
    <div className={classes['container']}>
      <form onSubmit={handleRegister} className={classes['form-container']}>
        <InputField
          name="username"
          inputName="Username"
          value={registerInput.username}
          handleOnChange={onRegisterInputChange}
          placeholder="username"
          type=""
        />
        <InputField
          name="email"
          inputName="Email"
          value={registerInput.email}
          handleOnChange={onRegisterInputChange}
          placeholder="email"
          type="email"
        />
        <InputField
          name="password"
          inputName="Password"
          value={registerInput.password}
          handleOnChange={onRegisterInputChange}
          placeholder="password"
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
        <InputField
          name="confirmPassword"
          inputName="Confirm Password"
          value={registerInput.confirmPassword}
          handleOnChange={onRegisterInputChange}
          placeholder="password"
          type={showConfirmPassword ? 'text' : 'password'}
          endIcon={
            showConfirmPassword ? (
              <VisibilityOffIcon
                onClick={() => setShowConfirmPassword(false)}
                style={{ cursor: 'pointer' }}
              />
            ) : (
              <RemoveRedEyeIcon
                onClick={() => setShowConfirmPassword(true)}
                style={{ cursor: 'pointer' }}
              />
            )
          }
        />
        <LoudButton text="NEXT" widthPercentage={buttonWidth} type="submit" />
      </form>
      <h5
        className={classes['bottom-text']}
        onClick={() => {
          navigate('/login')
        }}
      >
        Already have an account?{' '}
        <span className={classes['emph-text']}>Log in NOW</span>
      </h5>
    </div>
  )
}

export default FirstStepRegisterForm
