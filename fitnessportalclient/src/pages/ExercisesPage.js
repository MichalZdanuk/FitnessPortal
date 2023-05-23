import ExerciseThumbnail from "../components/exercise/ExerciseThumbnail"
import {exercisesList} from "../components/exercise/exercise"
import classes from "./ExercisePage.module.css"

const ExercisesPage = () => {
    const exercisesThumbnailList = exercisesList.map((exercise) => (
        <ExerciseThumbnail
        key={exercise.id}
        name={exercise.name}
        description={exercise.description} />
    ));

    return (
        <div className={classes["container"]}>{exercisesThumbnailList}</div>
    );
};

export default ExercisesPage;