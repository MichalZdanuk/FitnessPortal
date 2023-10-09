import classes from './MuscleGroupLabel.module.css'

const MuscleGroupLabel = ({ muscleGroupName }) => {
  return <h4 className={classes['muscle-group-label']}>{muscleGroupName}</h4>
}

export default MuscleGroupLabel
