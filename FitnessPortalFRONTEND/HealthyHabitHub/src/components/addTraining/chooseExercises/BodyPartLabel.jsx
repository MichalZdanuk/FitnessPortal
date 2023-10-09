import classes from './BodyPartLabel.module.css'

const BodyPartLabel = ({ bodyPartName }) => {
  return <h3 className={classes['bodyPartLabel']}>{bodyPartName}</h3>
}

export default BodyPartLabel
