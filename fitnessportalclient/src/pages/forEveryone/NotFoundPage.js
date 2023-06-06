import classes from "./NotFoundPage.module.css";
import pageNotFoundIcon from "../../assets/images/pageNotFound.png";
import { useNavigate } from "react-router-dom";
import { Button } from "react-bootstrap";

const NotFoundPage = () => {
  const navigate = useNavigate();
  return (
    <div className={classes["container"]}>
      <div className={classes["notFound"]}>
        <img
          className={classes["img"]}
          src={pageNotFoundIcon}
          alt="not found"
        />
        <div className={classes["textBlock"]}>
          <p className={classes["pageNotFound"]}>PAGE NOT FOUND</p>
          <p className={classes["textRow"]}>
            We looked everywhere for this page.
            <br />
            Are you sure the website URL is correct?
            <br />
            Get in touch with the site owener.
          </p>
          <Button
            onClick={() => {
              navigate("/");
            }}
            variant="outline-info"
            size="lg"
          >
            Go Back Home
          </Button>
        </div>
      </div>
    </div>
  );
};

export default NotFoundPage;
