import { useLocation } from "react-router-dom"
import classes from "./CalculatorPage.module.css"
import BMICalculator from "../components/calculator/BMICalculator"

const CalculatorPage = () => {
    const location = useLocation();
    // console.log(location.pathname);
    const calcType = location.pathname.substring(13);
    console.log(calcType);
    return (
        <div>
            {calcType === "bmi" && <BMICalculator />}
        </div>
    )
}

export default CalculatorPage