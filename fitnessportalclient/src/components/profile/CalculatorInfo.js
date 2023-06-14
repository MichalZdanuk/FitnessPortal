import classes from "./Info.module.css"
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import CalculateIcon from '@mui/icons-material/Calculate';

const CalculatorInfo = (props) => {
    return (
      <div>
        <p className={classes["main-section-label"]}>Calculator Stats <CalculateIcon/></p>
        <hr className={classes["hr-minimal-margin"]}/>
        <div className={classes["container"]}>
          <div className={classes["column"]}>
            <p className={classes["section-label"]}>Recent BMI measure</p>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Date: DD-MM-YYYY</li>
              <li><ChevronRightIcon/>Result: XX.YY</li>
              <li><ChevronRightIcon/>Category: aaaaaaaaa</li>
            </ul>
          </div>
          <div className={classes["column"]}>
            <p className={classes["section-label"]}>First BMI measure</p>
            <ul className={classes["no-bullet-points"]}>
              <li><ChevronRightIcon/>Date: DD-MM-YYYY</li>
              <li><ChevronRightIcon/>Result: XX.YY</li>
              <li><ChevronRightIcon/>Category: aaaaaaaaa</li>
            </ul>
          </div>
        </div>
      </div>
    );
  };

export default CalculatorInfo