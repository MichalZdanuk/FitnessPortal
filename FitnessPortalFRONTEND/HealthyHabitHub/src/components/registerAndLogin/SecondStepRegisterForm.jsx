import classes from './RegisterForm.module.css'
import { useNavigate } from 'react-router-dom'
import { InputField } from './InputField'
import { useViewport } from '../../contexts/viewportContext'
import LoudButton from '../buttons/LoudButton'

const SecondStepRegisterForm = ({
  registerInput,
  handleRegister,
  onRegisterInputChange,
  setStep,
}) => {
  const navigate = useNavigate()
  const { isMobile } = useViewport()
  const buttonWidth = isMobile ? 50 : 90

  return (
    <div className={classes['container']}>
      <form onSubmit={handleRegister} className={classes['form-container']}>
        <InputField
          name="dateOfBirth"
          inputName="Date of Birth"
          value={registerInput.dateOfBirth}
          handleOnChange={onRegisterInputChange}
          placeholder="enter date of birth"
          type=""
        />
        <InputField
          name="height"
          inputName="Height"
          value={registerInput.height}
          handleOnChange={onRegisterInputChange}
          placeholder="enter height"
          type="number"
        />
        <InputField
          name="weight"
          inputName="Weight"
          value={registerInput.weight}
          handleOnChange={onRegisterInputChange}
          placeholder="enter weight"
          type="number"
        />
        <div className={classes['buttons-container']}>
          <LoudButton
            text="REGISTER"
            widthPercentage={buttonWidth}
            type="submit"
          />
          <button
            className={classes['back-button']}
            onClick={() => setStep('1')}
          >
            BACK
          </button>
        </div>
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

export default SecondStepRegisterForm
