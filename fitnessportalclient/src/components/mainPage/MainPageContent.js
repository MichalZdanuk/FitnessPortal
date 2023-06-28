import classes from "./MainPageContent.module.css"
import workout from "../../assets/images/workout_main_page.png"
import { benefits } from "../../mocks/mockedData";
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../../store/authContext";
import book from "../../assets/images/book.png";
import two_dumbbells from "../../assets/images/two_dumbbells.png";
import friends from "../../assets/images/two_friends.png";
import progress from "../../assets/images/progress.png";



const MainPageContent = () => {
    const navigate = useNavigate();
    const authCtx = useContext(AuthContext);
    const isUserLogged = authCtx.isUserLogged;

    return (
        <div className={classes["div-container"]}>
            <div className={classes["motto-div"]}>
                <div className={classes["motto-text-div"]}>
                    <h1 className={classes["motto-header"]}><span className={classes["text-with-border"]}>SHAPE</span> YOUR<br/> IDEAL BODY</h1>
                    <p className={classes["motto-text"]}><span className={classes["green-text"]}>HealthyHabitHub</span> will help you to measure your progress and achieve your dream body.</p>
                    {isUserLogged === false && <button className={classes["start-button"]} onClick={() => navigate("/register")}>START NOW</button>}
                </div>
                <div className={classes["motto-img-div"]}>
                    <img src={workout} className={classes["workout-img"]} alt=""/>
                </div>
            </div>
            <div className={classes["advantages-div"]}>
                <h2 className={classes["advantages-heading"]}>WITH US YOU CAN</h2>
                <div className={classes["advantages-container"]}>
                    <BenefitList benefits={benefits} />
                </div>
            </div>
            <div className={classes["statistics-div"]}>
                <div className={classes["statistic"]}>
                    <span>+ 56</span>
                    <p className={classes["statistic-text"]}>ARTICLES</p>
                </div>
                <div className={classes["statistic"]}>
                    <span>+ 25 k</span>
                    <p className={classes["statistic-text"]}>MEMBERS JOINED</p>
                </div>
                <div className={classes["statistic"]}>
                    <span>12</span>
                    <p className={classes["statistic-text"]}>EXERCISES</p>
                </div>
            </div>
            <div className={classes["offer-div"]}>
                <div className={classes["offer"]}>
                    <img src={book} className={classes["offer-img"]} alt=""/>
                    <h3 className={classes["offer-heading"]}>ARTICLES</h3>
                    <p className={classes["offer-text"]}>
                        Gain valuable knowledge about diet, regeneration, exercises, performance, and more through our extensive collection of articles on the fitness portal.
                    </p>
                </div>
                <div className={classes["offer"]}>
                    <img src={two_dumbbells} className={classes["offer-img"]} alt=""/>
                    <h3 className={classes["offer-heading"]}>EXERCISES</h3>
                    <p className={classes["offer-text"]}>
                        Explore our comprehensive exercises lexicon, offering concise instructions and guidance on how to perform various exercises effectively.
                    </p>
                </div>
                <div className={classes["offer"]}>
                    <img src={friends} className={classes["offer-img"]} alt=""/>
                    <h3 className={classes["offer-heading"]}>FRIENDS</h3>
                    <p className={classes["offer-text"]}>
                        Connect with friends, track their results, and compare profiles to stay motivated and inspired on your fitness journey.   
                    </p>
                </div>
                <div className={classes["offer"]}>
                    <img src={progress} className={classes["offer-img"]} alt=""/>
                    <h3 className={classes["offer-heading"]}>PROGRESS</h3>
                    <p className={classes["offer-text"]}>
                        Monitor your progress, analyze workout data, and visualize your history through interactive diagrams, empowering you to achieve continuous improvement and growth.   
                    </p>
                </div>
            </div>
        </div>
    )
};

const Benefit = (props) => {
    return(
        <div className={classes["advantage-div"]}>
            <img src={props.img} className={classes["benefit-icon"]} alt="a"/>
            <p className={classes["benefit-text"]}>{props.content}</p>
        </div>
    );
};

const BenefitList = (props) => {
    const benefits = props.benefits.map((b) => {return <Benefit key={b.id} img={b.img} content={b.content}/>})
    return (
        <>{benefits}</>
    );
};

export default MainPageContent