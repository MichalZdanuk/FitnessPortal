import classes from "./Info.module.css"
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import FitnessCenterIcon from '@mui/icons-material/FitnessCenter';

const TrainingInfo = (props) => {
    return (
      <div>
        <p className={classes["main-section-label"]}>Training Stats <FitnessCenterIcon/></p>
        <hr className={classes["hr-minimal-margin"]}/>
        <p>Total number of trainings: 56</p>
        <div className={classes["container"]}>
          <div className={classes["column"]}>
            <p className={classes["section-label"]}>Recent Training</p>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Performed date: DD-MM-YYYY</li>
              <li><ChevronRightIcon/>Payload: XXX kg</li>
            </ul>
          </div>
          <div className={classes["column"]}>
            <p className={classes["section-label"]}>Best Training</p>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Performed date: DD-MM-YYYY</li>
              <li><ChevronRightIcon/>Payload: XXX kg</li>
            </ul>
          </div>
        </div>
      </div>
    );
  };

export default TrainingInfo;