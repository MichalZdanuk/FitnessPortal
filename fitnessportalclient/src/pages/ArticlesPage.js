import ArticleThumbnail from "../components/articlePage/ArticleThumbnail";
import classes from "./ArticlesPage.module.css"
import { useLocation } from "react-router-dom";

const ArticlesPage = () => {
  const location = useLocation();
  const data = location.state;
  console.log(data);

  let articleThumbnailList = (data === null) ? "" : data.map((article, idx) => (
    <ArticleThumbnail
      key={article.id}
      id={article.id}
      author={article.author}
      title={article.title}
      shortDescription={article.shortDescription}
      content={article.content}
      date={article.dateOfPublication}
      category={article.category}
    />
  ));

  return (
    <>
      <div className={classes["articles-container"]}>{articleThumbnailList}</div>
    </>
  );
};

export default ArticlesPage;
