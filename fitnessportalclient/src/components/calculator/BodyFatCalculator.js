import classes from "./BodyFatCalculator.module.css";
import axios from "axios";
import { useState } from "react";

const BodyFatCalculator = () => {
  const [weight, setWeight] = useState(0);
  const [height, setHeight] = useState(0);
  const [age, setAge] = useState(0);
  const [sex, setSex] = useState("");
  const [result, setResult] = useState({
    bmiScore: 10,
    bmiCategory: "",
  });

  const handleCalculate = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(
        "https://localhost:7087/api/calculator/bmr/anonymous",
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
    <>
      {result.bmiScore !== 0 && (
        <BodyFatResult
          bmiScore={result.bmiScore}
          bmiCategory={result.bmiCategory}
        />
      )}
      <div className={classes["body-fat-calc-div"]}>
        <h1 className={classes["calculator-title"]}>BodyFat Calculator</h1>
        <form onSubmit={handleCalculate}>
          <div className={classes["radio-container"]}>
            <div className={classes["sex-container"]}>
              <label className={classes["radio-label"]}>Male</label>
              <input
                name="gender"
                value={sex}
                onChange={() => setSex("male")}
                className={classes["radio-input"]}
                type="radio"
              ></input>
            </div>
            <div className={classes["sex-container"]}>
              <label className={classes["radio-label"]}>Female</label>
              <input
                name="gender"
                value={sex}
                onChange={() => setSex("female")}
                className={classes["radio-input"]}
                type="radio"
              ></input>
            </div>
          </div>
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
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Age</label>
            <input
              value={age}
              onChange={(e) => setAge(e.target.value)}
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
    </>
  );
};

const BodyFatResult = (props) => {
  return (
    <div>
      <h1 className={classes["bmr-score"]}>
        Your BodyFat is: {props.bmiScore} %
      </h1>
    </div>
  );
};

export default BodyFatCalculator;
