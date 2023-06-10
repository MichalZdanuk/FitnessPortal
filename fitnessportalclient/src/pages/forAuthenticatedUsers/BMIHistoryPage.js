import classes from "./BMIHistoryPage.module.css";
import { useContext, useEffect, useState } from "react";
import axios from "axios";
import { InfinitySpin } from "react-loader-spinner";
import AuthContext from "../../store/authContext";
import SentimentSatisfiedAltIcon from "@mui/icons-material/SentimentSatisfiedAlt";
import SentimentVeryDissatisfiedIcon from "@mui/icons-material/SentimentVeryDissatisfied";
import SentimentNeutralIcon from "@mui/icons-material/SentimentNeutral";

const BMIHistoryPage = () => {
  const [bmiData, setBmiData] = useState(null);
  const [loading, setLoading] = useState(true);
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;

  useEffect(() => {
    const fetchBmiData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7087/api/calculator/bmi",
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );
        setBmiData(response.data);
        setLoading(false);
      } catch (error) {
        console.error(error);
      }
    };

    fetchBmiData();
  }, []);

  const getCategoryIcon = (categoryName) => {
    let icon;

    switch (categoryName) {
      case "obesity":
        icon = (
          <span className={classes["obesity-icon"]}>
            <SentimentVeryDissatisfiedIcon />
          </span>
        );
        break;
      case "normalweight":
        icon = (
          <span className={classes["noramlweight-icon"]}>
            <SentimentSatisfiedAltIcon />
          </span>
        );
        break;
      case "overweight":
        icon = (
          <span className={classes["overweight-icon"]}>
            <SentimentVeryDissatisfiedIcon />
          </span>
        );
        break;
      case "underweight":
        icon = (
          <span className={classes["underwieght"]}>
            <SentimentVeryDissatisfiedIcon />
          </span>
        );
        break;
      default:
        icon = <SentimentNeutralIcon />;
        break;
    }

    return icon;
  };

  return (
    <div className={classes["bmihistory-main-div"]}>
      <p className={classes["bmi-header"]}>Your BMI Historic Results</p>
      <div className={classes["container"]}>
        {loading ? (
          // Render the spinner while loading
          <div className={classes["spinner"]}>
            <InfinitySpin width="200" color="#02C39A" />
          </div>
        ) : // Render the BMI data if loaded
        bmiData && bmiData.length > 0 ? (
          bmiData.map((bmiEntry) => (
            <div className={classes["bmi-row"]} key={bmiEntry.id}>
              <p>{bmiEntry.bmiScore.toString().substring(0, 5)}</p>
              <p>{bmiEntry.bmiCategory}</p>
              <p>{bmiEntry.date.substring(0, 10)}</p>
              <p>{getCategoryIcon(bmiEntry.bmiCategory)}</p>
            </div>
          ))
        ) : (
          // Render a message if no BMI data is available
          <p className={classes["no-data"]}>No BMI data available</p>
        )}
      </div>
    </div>
  );
};

export default BMIHistoryPage;
