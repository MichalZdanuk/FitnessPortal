import classes from './AdvantagesList.module.css'

const AdvantagesList = ({ advantageList }) => {
  return (
    <div className={classes['advantages-container']}>
      {advantageList.map((a) => (
        <AdvantageItem key={a.id} image={a.img} content={a.content} />
      ))}
    </div>
  )
}

const AdvantageItem = ({ content, image }) => {
  return (
    <div className={classes['advantage-div']}>
      <img
        className={classes['advantage-img']}
        src={image}
        alt="advantage-icon"
      />
      <p className={classes['advantage-text']}>{content}</p>
    </div>
  )
}

export default AdvantagesList
