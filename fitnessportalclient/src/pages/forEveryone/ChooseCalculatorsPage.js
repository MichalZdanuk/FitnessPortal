import classes from "./ChooseCalculatorsPage.module.css";
import CalculatorThumbnail from "../../components/calculator/CalculatorThumbnail";

const CalculatorsPage = () => {
  const calculatorsNames = [
    { id: 1, name: "BMI Calculator", path: "/calculators/bmi" },
    { id: 2, name: "BMR Calculator", path: "/calculators/bmr" },
    { id: 3, name: "Body Fat Calculator", path: "/calculators/bodyFat" },
  ];

  const calculators = calculatorsNames.map((cal) => {
    return <CalculatorThumbnail key={cal.id} name={cal.name} path={cal.path} />;
  });
  return <div className={classes["calculators-div"]}>{calculators}</div>;
};

export default CalculatorsPage;
