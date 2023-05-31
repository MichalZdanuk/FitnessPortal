import classes from "./BodyFatCalculator.module.css";
import axios from "axios";
import { useState } from "react";

const BodyFatCalculator = () => {
  const [height, setHeight] = useState(0);
  const [waist, setWaist] = useState(0);
  const [hip, setHip] = useState(0);
  const [neck, setNeck] = useState(0);
  const [sex, setSex] = useState("");
  const [bodyFatLevel, setBodyFatLevel] = useState("__");

  const handleCalculate = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.get(
        "https://localhost:7087/api/calculator/bodyFat/anonymous",
        {
          params: {
            height: height,
            waist: waist,
            hip: hip,
            neck: neck,
            sex: sex
          },
        }
      );
      const bodyFatResult = response.data;
      console.log(bodyFatResult);
      setBodyFatLevel(bodyFatResult.bodyFatLevel);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      {
        <BodyFatResult
          bodyFatLevel={bodyFatLevel}
        />
      }
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
            <label className={classes["form-label"]}>Height</label>
            <input
              value={height}
              onChange={(e) => setHeight(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Waist</label>
            <input
              value={waist}
              onChange={(e) => setWaist(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Hip</label>
            <input
              value={hip}
              onChange={(e) => setHip(e.target.value)}
              className={classes["input-box"]}
              type="number"
            ></input>
          </div>
          <div className={classes["input-container"]}>
            <label className={classes["form-label"]}>Neck</label>
            <input
              value={neck}
              onChange={(e) => setNeck(e.target.value)}
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
      <h1 className={classes["bodyFat-level"]}>
        Your BodyFat is: {props.bodyFatLevel} %
      </h1>
    </div>
  );
};

export default BodyFatCalculator;
