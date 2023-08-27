import { useEffect, useState } from "react";
import ArticleThumbnail from "../../components/articlePage/ArticleThumbnail";
import classes from "./ArticlesPage.module.css";
import axios from "axios";

import PaginationPanel from "../../components/pagination/PaginationPanel";
import MySpinner from "../../components/spinner/MySpinner";

const ArticlesPage = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(3);
  const [itemFrom, setItemFrom] = useState(0);
  const [itemTo, setItemTo] = useState(0);
  const [totalItemsCount, setTotalItemsCount] = useState(0);
  const [totalPages, setTotalPages] = useState(0);


  useEffect(() => {
    fetchData();
  }, [pageNumber, pageSize]);

  const fetchData = async () => {
    try {
      const response = await axios.get("https://localhost:7087/api/article", {
        params: {
          pageNumber: pageNumber,
          pageSize: pageSize,
        }
      });
      setData(response.data.items);
      console.log("data: ", response.data);
      setItemFrom(response.data.itemFrom);
      setItemTo(response.data.itemTo);
      setTotalItemsCount(response.data.totalItemsCount);
      setTotalPages(response.data.totalPages);
      setLoading(false);
    } catch (error) {
      console.error(error);
    }
  };

  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value));
    setPageNumber(1);
    setLoading(true);
  };

  const handlePageChange = (newPageNumber) => {
    setPageNumber(newPageNumber);
    setLoading(true);
  };

  let articleThumbnailList = null;

  if (loading) {
    articleThumbnailList = (
      <MySpinner />
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
        <br/>
        {articleThumbnailList}
        <PaginationPanel 
          pageSize={pageSize} 
          itemName="Articles" 
          handlePageSizeChange={handlePageSizeChange} 
          handlePageChange={handlePageChange} 
          itemFrom={itemFrom} 
          itemTo={itemTo}
          pageNumber={pageNumber}
          totalItemsCount={totalItemsCount}
          totalPages={totalPages}
          value1={3}
          value2={5}
          value3={10}
        />
      </div>
    </>
  );
};


export default ArticlesPage;
