import classes from './Calculator.module.css'
import { useContext, useState } from 'react'
import { CalculatorInput } from './CalculatorInput'
import fetchData from '../../utils/fetchData'
import { Alert } from 'react-bootstrap'
import LoudButton from '../buttons/LoudButton'
import AuthContext from '../../contexts/authContext'

const BMICalculator = () => {
  const [result, setResult] = useState({
    bmiScore: 0,
    bmiCategory: '',
  })
  const authCtx = useContext(AuthContext)
  const [error, setError] = useState('')

  const fetchBMI = async (height, weight) => {
    setError('')
    if (authCtx.isUserLogged) {
      const token = authCtx.tokenJWT
      try {
        const response = await fetchData(
          '/api/calculator/bmi',
          {
            height,
            weight,
          },
          {
            Authorization: `Bearer ${token}`,
          },
          'post',
        )

        setResult({
          bmiScore: response.bmiScore,
          bmiCategory: response.bmiCategory,
        })
      } catch (error) {
        setError('Inavlid data')
      }
    } else {
      try {
        const response = await fetchData(`/api/calculator/bmi/anonymous`, {
          height,
          weight,
        })

        setResult({
          bmiScore: response.bmiScore,
          bmiCategory: response.bmiCategory,
        })
      } catch (error) {
        setError('Inavlid data')
      }
    }
  }

  const handleCaluclate = async (e, calculatorInput) => {
    e.preventDefault()
    const { height, weight } = calculatorInput
    fetchBMI(height, weight)
  }

  return (
    <div className={classes['calculator-container']}>
      <div className={classes['alert-div']}>
        {error && <Alert variant="danger">{error}</Alert>}
      </div>
      <BMIResult bmiScore={result.bmiScore} bmiCategory={result.bmiCategory} />
      <BMICalculatorForm handleCaluclate={handleCaluclate} />
    </div>
  )
}

const BMICalculatorForm = ({ handleCaluclate }) => {
  const [calculatorInput, setCalculatorInput] = useState({
    height: '',
    weight: '',
  })

  const onCalculatorInputChange = (e) => {
    e.persist()
    const { name, value } = e.target
    setCalculatorInput((prevState) => ({
      ...prevState,
      [name]: value,
    }))
  }

  return (
    <form
      onSubmit={(e) => handleCaluclate(e, calculatorInput)}
      className={classes['form-container']}
    >
      <CalculatorInput
        name="height"
        inputName="Height"
        value={calculatorInput.height}
        handleOnChange={onCalculatorInputChange}
        placeholder="height"
        type="number"
      />
      <CalculatorInput
        name="weight"
        inputName="Weight"
        value={calculatorInput.weight}
        handleOnChange={onCalculatorInputChange}
        placeholder="weight"
        type="number"
      />

      <div className={classes['calculate-button-div']}>
        <LoudButton text="CALCULATE" widthPercentage={90} type="submit" />
      </div>
    </form>
  )
}

const BMIResult = ({ bmiScore, bmiCategory }) => {
  const formattedBMI = bmiScore.toFixed(2)

  return (
    <div className={classes['result-div']}>
      <h2>Your BMI: {formattedBMI}</h2>
      <h3>BMI Category: {bmiCategory}</h3>
    </div>
  )
}

export default BMICalculator
