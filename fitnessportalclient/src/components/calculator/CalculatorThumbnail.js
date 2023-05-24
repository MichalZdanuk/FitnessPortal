import classes from "./CalculatorThumbnail.module.css";
import calculatorIcon from "../../assets/images/calculator.png";

const CalculatorThumbnail = (props) => {
  return (
    <div className={classes["calculator-thumbnail-div"]}>
      <img
        src={calculatorIcon}
        alt="calculatorIcon"
        className={classes["img"]}
      />
      <p className={classes["calculator-name"]}>{props.name}</p>
      <button className={classes["calculate-button"]}>CALCULATE</button>
    </div>
  );
};

export default CalculatorThumbnail;
