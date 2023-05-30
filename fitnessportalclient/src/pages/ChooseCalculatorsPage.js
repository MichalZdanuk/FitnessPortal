import classes from "./ChooseCalculatorsPage.module.css";
import CalculatorThumbnail from "../components/calculator/CalculatorThumbnail";

const CalculatorsPage = () => {
  const calculatorsNames = [
    { name: "BMI Calculator", path: "/calculators/bmi" },
    { name: "BMR Calculator", path: "/calculators/bmr" },
    { name: "Body Fat Calculator", path: "/calculators/bodyFat" },
  ];

  const calculators = calculatorsNames.map((cal) => {
    return <CalculatorThumbnail name={cal.name} path={cal.path}/>;
  });
  return (
    <div className={classes["calculators-div"]}>
      {calculators}
    </div>
  );
};

export default CalculatorsPage;
