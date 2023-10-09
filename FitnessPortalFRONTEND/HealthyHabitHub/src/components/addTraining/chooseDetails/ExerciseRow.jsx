import classes from './ExerciseRow.module.css'
import AddIcon from '@mui/icons-material/Add'
import RemoveIcon from '@mui/icons-material/Remove'
import { useViewport } from '../../../contexts/viewportContext'

const ExerciseRow = ({ name, numberOfReps = 0, payload = 0, onChange }) => {
  const handleChange = (type, value, minValue, maxValue) => {
    const newValue =
      type === 'payload' ? parseFloat(value) : parseInt(value, 10)
    if (newValue >= minValue && newValue <= maxValue) {
      if (type === 'count') {
        onChange('numberOfReps', newValue)
      } else if (type === 'payload') {
        onChange('payload', newValue)
      }
    }
  }

  const { isMobile } = useViewport()

  return (
    <div
      className={`${classes['exercise-row']} ${
        isMobile ? classes['exercise-row-mobile'] : ''
      }`}
    >
      <h4 className={classes['exercise-name']}>{name}</h4>
      <div
        className={`${classes['set-numbers-div']} ${
          isMobile ? classes['set-numbers-div-mobile'] : ''
        }`}
      >
        <div className={classes['input-with-icons']}>
          reps:
          <input
            type="number"
            value={numberOfReps}
            min={0}
            max={20}
            step={1}
            onChange={(e) => handleChange('count', e.target.value, 0, 20)}
            className={`${classes['count-input']} ${classes['input']}`}
          />
          <AddIcon
            className={`${classes['icon']} ${classes['add']}`}
            onClick={() => handleChange('count', numberOfReps + 1, 0, 20)}
          />
          <RemoveIcon
            className={`${classes['icon']} ${classes['remove']}`}
            onClick={() => handleChange('count', numberOfReps - 1, 0, 20)}
          />
        </div>
        <div className={classes['input-with-icons']}>
          payload:
          <input
            type="number"
            step="0.5"
            value={payload}
            min={0}
            max={200.5}
            onChange={(e) => handleChange('payload', e.target.value, 0, 200.5)}
            className={`${classes['payload-input']} ${classes['input']}`}
          />
          <AddIcon
            className={`${classes['icon']} ${classes['add']}`}
            onClick={() => handleChange('payload', payload + 0.5, 0, 200.5)}
          />
          <RemoveIcon
            className={`${classes['icon']} ${classes['remove']}`}
            onClick={() => handleChange('payload', payload - 0.5, 0, 200.5)}
          />
        </div>
      </div>
    </div>
  )
}

export default ExerciseRow
