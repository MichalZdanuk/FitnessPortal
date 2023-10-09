import classes from './ExerciseThumbnail.module.css'

const ExerciseThumbnail = ({ photo, name, description }) => {
  return (
    <div className={classes['exercise-thumbnail-container']}>
      <img src={photo} alt="exercise" className={classes['thumbnail-img']} />
      <h3 className={classes['thumnail-title']}>{name}</h3>
      <p className={classes['thumbnail-description']}>{description}</p>
    </div>
  )
}

export default ExerciseThumbnail
