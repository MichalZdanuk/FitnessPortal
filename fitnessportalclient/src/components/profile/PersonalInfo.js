import classes from "./Info.module.css"
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import FaceIcon from '@mui/icons-material/Face';

const PersonalInfo = (props) => {
    return (
      <div>
        <p className={classes["main-section-label"]}>Personal Info <FaceIcon/></p>
        <hr className={classes["hr-minimal-margin"]}/>
        <div className={classes["container"]}>
          <div className={classes["column"]}>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Date of birth: {props.data.dateOfBirth.substring(0,10)}</li>
              <li><ChevronRightIcon/>Number of friends: YY</li>
            </ul>
          </div>
          <div className={classes["column"]}>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Height: {props.data.height} cm</li>
              <li><ChevronRightIcon/>Weight: {props.data.weight} kg</li>
            </ul>
          </div>
        </div>
      </div>
    );
  };

export default PersonalInfo