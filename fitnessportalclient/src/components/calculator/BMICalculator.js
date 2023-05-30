import { useState } from "react";
import classes from "./BMICalculator.module.css";
import axios from "axios";

const BMICalculator = () => {
  const [weight, setWeight] = useState(0);
  const [height, setHeight] = useState(0);
  const [result, setResult] = useState({
    bmiScore: 0,
    bmiCategory: "",
  });

  const handleCalculate = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(
        "https://localhost:7087/api/calculator/anonymous",
        {
          params: {
            height: height,
            weight: weight,
          },
        }
      );
      const bmiResult = response.data;
      console.log(bmiResult);
      setResult({
        bmiScore: bmiResult.bmiScore,
        bmiCategory: bmiResult.bmiCategory,
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className={classes["main-container"]}>
      {result.bmiScore !== 0 && (
        <BMIResult
          bmiScore={result.bmiScore}
          bmiCategory={result.bmiCategory}
        />
      )}
      <div className={classes["form-container"]}>
        <h1 className={classes["calculator-title"]}>BMI Calculator</h1>
        <form onSubmit={handleCalculate}>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Weight</label>
            <input
              value={weight}
              onChange={(e) => setWeight(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Height</label>
            <input
              value={height}
              onChange={(e) => setHeight(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
          <input
            className={classes["submit-button"]}
            type="submit"
            value="CALCULATE"
          />
        </form>
      </div>
    </div>
  );
};

const BMIResult = (props) => {
  return (
    <div>
      <h1 className={classes["bmi-score"]}>Your BMI: {props.bmiScore}</h1>
      <h5 className={classes["bmi-category"]}>BMI Category: {props.bmiCategory}</h5>
    </div>
  );
};

export default BMICalculator;
