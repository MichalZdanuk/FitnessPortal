import classes from "./MySpinner.module.css";
import { InfinitySpin } from "react-loader-spinner";

const MySpinner = () => {
  return (
    <div className={classes["spinner"]}>
      <InfinitySpin width="200" color="#02C39A" />
    </div>
  );
};

export default MySpinner;
