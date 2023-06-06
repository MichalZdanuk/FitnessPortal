import classes from "./AddTraining.module.css"
import { RequiredAuth } from "../../store/authContext";

const AddTrainingPage = () => {
    return(
        <RequiredAuth>
            <div className={classes["main-div"]}>
                <h1>Add Training Center</h1>
                <p className={classes["red"]}>to be implemented</p>
            </div>
        </RequiredAuth>
    )

};

export default AddTrainingPage