import classes from "./BMRCalculator.module.css";
import axios from "axios";
import { useState } from "react";

const BMRCalculator = () => {
  const [weight, setWeight] = useState(0);
  const [height, setHeight] = useState(0);
  const [age, setAge] = useState(0);
  const [sex, setSex] = useState("");
  const [bmrScore, setbmrScore] = useState("XXX");

  const handleCalculate = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(
        "https://localhost:7087/api/calculator/bmr/anonymous",
        {
          params: {
            height: height,
            weight: weight,
            age: age,
            sex: sex
          },
        }
      );
      const bmrResult = response.data;
      console.log(bmrResult);
      setbmrScore(bmrResult.bmrScore);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      {
        <BMRResult
          bmrScore={bmrScore}
        />
      }
      <div className={classes["body-fat-calc-div"]}>
        <h1 className={classes["calculator-title"]}>BMR Calculator</h1>
        <form onSubmit={handleCalculate}>
          <div className={classes["radio-container"]}>
            <div className={classes["sex-container"]}>
              <label className={classes["radio-label"]}>Male</label>
              <input
                name="gender"
                value={sex}
                onChange={() => setSex("male")}
                type="radio"
              ></input>
            </div>
            <div className={classes["sex-container"]}>
              <label className={classes["radio-label"]}>Female</label>
              <input
                name="gender"
                value={sex}
                onChange={() => setSex("female")}
                type="radio"
              ></input>
            </div>
          </div>
          <div className={classes["input-container"]}>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Height (cm)</label>
            <input
              value={height}
              onChange={(e) => setHeight(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
            <label className={classes["form-label"]}>Weight (kg)</label>
            <input
              value={weight}
              onChange={(e) => setWeight(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Age (yr)</label>
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

const BMRResult = (props) => {
  return (
    <div>
      <h1 className={classes["bmr-score"]}>
        Your BMR is: {props.bmrScore} Kcal/Day
      </h1>
    </div>
  );
};

export default BMRCalculator;
