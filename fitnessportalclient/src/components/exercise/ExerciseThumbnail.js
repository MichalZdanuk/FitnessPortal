import classes from "./ExerciseThumbnail.module.css";

const ExerciseThumbnail = (props) => {
    return (
        <div className={classes["exercise-div"]}>
            <img className={classes["exercise-img"]} src={props.photo} alt="exercise" />
            <p className={classes["exercise-title"]}>{props.name}</p>
            <p className={classes["text"]}>{props.description}</p>
        </div>
    )
};

export default ExerciseThumbnail