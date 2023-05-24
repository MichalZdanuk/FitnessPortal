import classes from "./CalculatorsPage.module.css";
import CalculatorThumbnail from "../components/calculator/CalculatorThumbnail";

const CalculatorsPage = () => {
  const calculatorsNames = [
    { name: "BMI Calculator" },
    { name: "BMR Calculator" },
    { name: "Body Fat Calculator" },
  ];

  const calculators = calculatorsNames.map((cal) => {
    return <CalculatorThumbnail name={cal.name} />;
  });
  return (
    <div className={classes["calculators-div"]}>
      {calculators}
    </div>
  );
};

export default CalculatorsPage;
