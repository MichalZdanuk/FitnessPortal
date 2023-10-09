import classes from './Calculator.module.css'
import { useState } from 'react'
import { CalculatorInput } from './CalculatorInput'
import fetchData from '../../utils/fetchData'
import { Alert } from 'react-bootstrap'
import LoudButton from '../buttons/LoudButton'

const BMRCalculator = () => {
  const [result, setResult] = useState({
    bmrScore: 0,
  })
  const [error, setError] = useState('')

  const fetchBMI = async (height, weight, age, sex) => {
    setError('')
    try {
      const response = await fetchData(`/api/calculator/bmr/anonymous`, {
        height,
        weight,
        age,
        sex,
      })

      setResult({
        bmrScore: response.bmrScore,
      })
    } catch (error) {
      setError('Invalid data')
    }
  }

  const handleCaluclate = async (e, calculatorInput) => {
    e.preventDefault()
    const { height, weight, age, sex } = calculatorInput
    fetchBMI(height, weight, age, sex)
  }

  return (
    <div className={classes['calculator-container']}>
      <div className={classes['alert-div']}>
        {error && <Alert variant="danger">{error}</Alert>}
      </div>
      <BMRResult bmrScore={result.bmrScore} />
      <BMRCalculatorForm handleCaluclate={handleCaluclate} />
    </div>
  )
}

const BMRCalculatorForm = ({ handleCaluclate }) => {
  const [calculatorInput, setCalculatorInput] = useState({
    height: '',
    weight: '',
    age: '',
    sex: '',
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
      <CalculatorInput
        name="age"
        inputName="Age"
        value={calculatorInput.age}
        handleOnChange={onCalculatorInputChange}
        placeholder="age"
        type="number"
      />
      <CalculatorInput
        name="sex"
        inputName="Sex"
        value={calculatorInput.sex} // Change 'gender' to 'sex'
        handleOnChange={onCalculatorInputChange}
        type="radio"
        options={['Male', 'Female']}
      />
      <div className={classes['calculate-button-div']}>
        <LoudButton text="CALCULATE" widthPercentage={90} type="submit" />
      </div>
    </form>
  )
}

const BMRResult = ({ bmrScore }) => {
  const roundedBMR = Math.floor(bmrScore)

  return (
    <div className={classes['result-div']}>
      <h2>Your BMR: {roundedBMR} Kcal/Day</h2>
    </div>
  )
}

export default BMRCalculator
