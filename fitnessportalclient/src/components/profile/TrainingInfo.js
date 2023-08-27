import classes from "./Info.module.css"
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter';
import axios from "axios";
import { useContext } from "react";
import { useState } from "react";
import { useEffect } from "react";
import AuthContext from "../../store/authContext";

const TrainingInfo = (props) => {
  const [trainingStats, setTrainingStats] = useState();
  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;

  useEffect(() => {
    fetchTrainingStats();
  },[]);

  const fetchTrainingStats = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7087/api/training/stats",
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      setTrainingStats(response.data);
      console.log(response.data)
    } catch (error) {
      console.log(error);
    }
  };

    return (
      <div className={classes["training-info-div"]}>
        <p className={classes["main-section-label"]}>Training Stats <FitnessCenterIcon/></p>
        <hr className={classes["hr-minimal-margin"]}/>
        <p className={classes["number-of-trainings"]}>Total number of trainings: {trainingStats && trainingStats.numberOfTrainings}</p>
        <div className={classes["container"]}>
          <div className={classes["column"]}>
            <p className={classes["section-label"]}>Recent Training</p>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Performed date: {trainingStats && trainingStats.mostRecentTraining.dateOfTraining.slice(0,10)}</li>
              <li><ChevronRightIcon/>Payload: {trainingStats && trainingStats.mostRecentTraining.totalPayload} kg</li>
              <li><ChevronRightIcon/>Number of series: {trainingStats && trainingStats.mostRecentTraining.numberOfSeries}</li>
              {trainingStats && trainingStats.mostRecentTraining.exercises.map((ex)=> {return <li className={classes["ex-item"]}>{ex.name} - {ex.numberOfReps} x {ex.payload} kg</li>})}
            </ul>
          </div>
          <div className={classes["column"]}>
            <p className={classes["section-label"]}>Best Training</p>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Performed date: {trainingStats && trainingStats.bestTraining.dateOfTraining.slice(0,10)}</li>
              <li><ChevronRightIcon/>Payload: {trainingStats && trainingStats.bestTraining.totalPayload} kg</li>
              <li><ChevronRightIcon/>Number of series: {trainingStats && trainingStats.bestTraining.numberOfSeries}</li>
              {trainingStats && trainingStats.bestTraining.exercises.map((ex)=> {return <li className={classes["ex-item"]}>{ex.name} - {ex.numberOfReps} x {ex.payload} kg</li>})}
            </ul>
          </div>
        </div>
      </div>
    );
  };

export default TrainingInfo;