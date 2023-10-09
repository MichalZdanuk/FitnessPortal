import classes from './ChartButtons.module.css'

const ChartButtons = ({ selectedPeriod, setSelectedPeriod }) => {
  return (
    <div className={classes['buttons-container']}>
      <Button
        name="MONTH"
        isSelected={selectedPeriod === 'month'}
        onClick={() => setSelectedPeriod('month')}
      />
      <Button
        name="QUARTER"
        isSelected={selectedPeriod === 'quarter'}
        onClick={() => setSelectedPeriod('quarter')}
      />
      <Button
        name="HALFYEAR"
        isSelected={selectedPeriod == 'halfyear'}
        onClick={() => setSelectedPeriod('halfyear')}
      />
    </div>
  )
}

const Button = ({ name, isSelected, onClick }) => {
  return (
    <button
      onClick={onClick}
      className={`${classes['button']} ${
        isSelected ? classes['selected'] : ''
      }`}
      disabled={isSelected}
    >
      {name}
    </button>
  )
}

export default ChartButtons
