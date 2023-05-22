import { useNavigate } from "react-router-dom";
import classes from "./ArticleThumbnail.module.css";
import clock from "../../assets/images/clock.png"

const ArticleThumbnail = (props) => {
  const navigate = useNavigate();

  return (
    <div
      className={classes["article-thumbnail-div"]}
      onClick={(e) => {
        navigate("/articles/x");
      }}
    >
      <div className={classes["article-top-info-container"]}>
        <h1 className={classes["article-title"]}>{props.title}</h1>
        <h1 className={classes["article-category"]}>{props.category}</h1>
      </div>
      <p className={classes["article-date"]}><img src={clock} alt="clock" className={classes["img"]}></img> {props.date.substring(0,10)}</p>
      <p>{props.shortDescription}</p>
    </div>
  );
};

export default ArticleThumbnail;
