import ArticleThumbnail from "../components/articlePage/ArticleThumbnail";
import classes from "./ArticlesPage.module.css"
import { useLocation } from "react-router-dom";

const ArticlesPage = () => {
  const location = useLocation();
  const data = location.state;

  let articleThumbnailList = (data === null) ? "" : data.map((article, idx) => (
    <ArticleThumbnail
      key={idx}
      title={article.title}
      shortDescription={article.shortDescription}
      date={article.dateOfPublication}
      category={article.category}
    />
  ));

  return (
    <>
      <h2>Articles page</h2>
      <div className={classes["articles-container"]}>{articleThumbnailList}</div>
    </>
  );
};

export default ArticlesPage;
