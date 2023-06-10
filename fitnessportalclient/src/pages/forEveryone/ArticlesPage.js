import { useEffect, useState } from "react";
import ArticleThumbnail from "../../components/articlePage/ArticleThumbnail";
import classes from "./ArticlesPage.module.css";
import { InfinitySpin } from "react-loader-spinner";
import axios from "axios";

const ArticlesPage = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get("https://localhost:7087/api/article");
        setData(response.data);
        setLoading(false);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();
  }, []);

  let articleThumbnailList = null;

  if (loading) {
    articleThumbnailList = (
      <div className={classes["spinner-div"]}>
        <InfinitySpin width="200" color="#02C39A" />
      </div>
    );
  } else if (data && data.length > 0) {
    articleThumbnailList = data.map((article) => (
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
  } else {
    articleThumbnailList = <p>No articles available</p>;
  }

  return (
    <>
      <div className={classes["articles-container"]}>
        {articleThumbnailList}
      </div>
    </>
  );
};

export default ArticlesPage;
