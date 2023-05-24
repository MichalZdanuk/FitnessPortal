import classes from "./Article.module.css";
import { useLocation } from "react-router-dom";
import clock from "../../assets/images/clock.png"

const Article = () => {
  const location = useLocation();
  const data = location.state;
  console.log(data);
  let date = data.date.substring(0,10);

  return (
    <div className={classes["article-container"]}>
      <div className={classes["article-top-info-container"]}>
      <p className={classes["article-title"]}>{data.title}</p>
      <p className={classes["article-category"]}>{data.category}</p>
      </div>
      <p className={classes["article-date-author"]}><img src={clock} alt="clock" className={classes["img"]}/>&nbsp;&nbsp;&nbsp;{date}&nbsp;&nbsp;{data.author}</p>
      <p className={classes["article-text-content"]}>{data.content}</p>
    </div>
  );
};

export default Article;
