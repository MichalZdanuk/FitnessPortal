import classes from "./CalculatorThumbnail.module.css";
import calculatorIcon from "../../assets/images/calculator.png";
import { useNavigate } from "react-router-dom";

const CalculatorThumbnail = (props) => {
  const navigate = useNavigate();
  return (
    <div className={classes["calculator-thumbnail-div"]}>
      <img
        src={calculatorIcon}
        alt="calculatorIcon"
        className={classes["img"]}
        onClick={(e) => {
          navigate(props.path);
        }}
      />
      <p className={classes["calculator-name"]}>{props.name}</p>
      <button className={classes["calculate-button"]} onClick={(e) => {
        navigate(props.path);
      }}>CALCULATE</button>
    </div>
  );
};

export default CalculatorThumbnail;
