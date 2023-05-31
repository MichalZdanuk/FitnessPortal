import { useLocation } from "react-router-dom"
import classes from "./CalculatorPage.module.css"
import BMICalculator from "../components/calculator/BMICalculator"
import BMRCalculator from "../components/calculator/BMRCalculator"
import BodyFatCalculator from "../components/calculator/BodyFatCalculator"

const CalculatorPage = () => {
    const location = useLocation();
    // console.log(location.pathname);
    const calcType = location.pathname.substring(13);
    // console.log(calcType);
    return (
        <div className={classes["container"]}>
            {calcType === "bmi" && <BMICalculator />}
            {calcType === "bmr" && <BMRCalculator />}
            {calcType === "bodyFat" && <BodyFatCalculator />}
        </div>
    )
}

export default CalculatorPage