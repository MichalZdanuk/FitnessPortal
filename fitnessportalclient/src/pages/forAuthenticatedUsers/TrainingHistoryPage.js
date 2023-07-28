import { useContext, useEffect } from "react";
import classes from "./TrainingHistory.module.css";
import axios from "axios";
import AuthContext from "../../store/authContext";
import { useState } from "react";

import PaginationPanel from "../../components/pagination/PaginationPanel";
import TrainingCard from "../../components/training/TrainingCard";
import MySpinner from "../../components/spinner/MySpinner";

const TrainingHistoryPage = () => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [itemFrom, setItemFrom] = useState(0);
  const [itemTo, setItemTo] = useState(0);
  const [totalItemsCount, setTotalItemsCount] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  const authCtx = useContext(AuthContext);
  const token = authCtx.tokenJWT;

  useEffect(()=>{
    fetchData();
  },[pageNumber,pageSize]);

  const fetchData = async () => {
    try {
      const response = await axios.get("https://localhost:7087/api/training", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
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

  let trainingList = null;

  if (loading) {
    trainingList = (
      <MySpinner />
    );
  } else if (data && data.length > 0) {
    
    trainingList = data.map((training) => (
      <TrainingCard
          key={training.id}
          id={training.id}
          date={training.dateOfTraining}
          totalPayload={training.totalPayload}
          numOfSeries={training.numberOfSeries}
          listOfExercises={training.exercises}
          />
    ));
  } else {
    trainingList = <p>No articles available</p>;
  }

  return (
    <div className={classes["trainingHistory-main-div"]}>
      <p className={classes["training-header"]}>Your Training History</p>
      <hr />
      {trainingList}
      <div className={classes["pagination-panel-div"]}>
        <PaginationPanel 
          pageSize={pageSize} 
          itemName="Trainings" 
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
    </div>
  );
};

export default TrainingHistoryPage;
