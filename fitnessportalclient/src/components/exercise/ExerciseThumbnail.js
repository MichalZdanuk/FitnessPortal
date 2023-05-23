import classes from "./ExerciseThumbnail.module.css";

const ExerciseThumbnail = (props) => {
    return (
        <div className={classes["exercise-div"]}>
            <p className={classes["exercise-title"]}>{props.name}</p>
            <p className={classes["description-label"]}>Description how to perform:</p>
            <p className={classes["text"]}>{props.description}</p>
        </div>
    )
};

export default ExerciseThumbnail