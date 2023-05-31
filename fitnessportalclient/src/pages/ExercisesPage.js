import ExerciseThumbnail from "../components/exercise/ExerciseThumbnail"
import {exercisesList} from "../mocks/mockedData"
import classes from "./ExercisePage.module.css"
import {images} from "../mocks/images"

const ExercisesPage = () => {
    const exercisesThumbnailList = exercisesList.map((exercise) => (
        <ExerciseThumbnail
        key={exercise.id}
        photo={images[exercise.id-1]}
        name={exercise.name}
        description={exercise.description} />
    ));

    return (
        <div className={classes["container"]}>{exercisesThumbnailList}</div>
    );
};

export default ExercisesPage;