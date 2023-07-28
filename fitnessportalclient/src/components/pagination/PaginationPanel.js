import classes from "./PaginationPanel.module.css"
import KeyboardDoubleArrowLeftIcon from '@mui/icons-material/KeyboardDoubleArrowLeft';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRightIcon from '@mui/icons-material/KeyboardArrowRight';

const PaginationPanel = (props) => {
    const handlePreviousPage = () => {
        if(props.pageNumber > 1){
            props.handlePageChange(props.pageNumber - 1);
        }
    };

    const handleNextPage = () => {
        if(props.pageNumber < props.totalPages){
            props.handlePageChange(props.pageNumber + 1);
            console.log("next page");
        }
    };

    const handleFirstPage = () => {
        props.handlePageChange(1);
    };

    const handleLastPage = () => {
        props.handlePageChange(props.totalPages);
    };


    return (
      <div className={classes["pagination-div"]}>
        {props.itemName} per page: 
        <form>
          <select value={props.pageSize} onChange={props.handlePageSizeChange}>
            <option value={props.value1}>{props.value1}</option>
            <option value={props.value2}>{props.value2}</option>  
            <option value={props.value3}>{props.value3}</option>  
          </select> 
        </form>
        {props.itemFrom} - {props.itemTo} of {props.totalItemsCount}
        <KeyboardDoubleArrowLeftIcon className={classes["arrow-icon"]} onClick={handleFirstPage}/>
        <KeyboardArrowLeftIcon className={classes["arrow-icon"]} onClick={handlePreviousPage}/>
        <KeyboardArrowRightIcon className={classes["arrow-icon"]} onClick={handleNextPage}/>
        <KeyboardDoubleArrowRightIcon className={classes["arrow-icon"]} onClick={handleLastPage}/>
      </div>
    )
  };

export default PaginationPanel