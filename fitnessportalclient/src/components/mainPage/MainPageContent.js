import classes from "./MainPageContent.module.css"
import mainIcon from "../../assets/images/dumbbell.png"

const MainPageContent = () => {
    return (
        <div className={classes["div-container"]}>
            <img src={mainIcon} className={classes["img"]} alt="logo"/>
            <p className={classes["motto-text"]}>"Fitness is not a destination.</p>
            <p className={classes["motto-text"]}>It is a way of life."</p>
        </div>
    )
};

export default MainPageContent