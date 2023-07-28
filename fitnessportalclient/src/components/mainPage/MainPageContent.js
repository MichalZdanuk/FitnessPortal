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
import AnimatedElement from "../animations/AnimatedElement";
import NumberCounter from 'number-counter';

const MainPageContent = () => {
    const navigate = useNavigate();
    const authCtx = useContext(AuthContext);
    const isUserLogged = authCtx.isUserLogged;

    return (
        <div className={classes["div-container"]}>
            <div className={classes["motto-div"]}>
                <div className={classes["motto-text-div"]}>
                <AnimatedElement animationType="slide-in" base="animated-text">
                    <h1 className={classes["motto-header"]}><span className={classes["text-with-border"]}>SHAPE</span> YOUR<br/> IDEAL BODY</h1>
                    <p className={classes["motto-text"]}><span className={classes["green-text"]}>HealthyHabitHub</span> will help you to measure your progress and achieve your dream body.</p>
                    {isUserLogged === false && <button className={classes["start-button"]} onClick={() => navigate("/register")}>START NOW</button>}
                </AnimatedElement>
                </div>
                <div className={classes["motto-img-div"]}>
                    <img src={workout} className={classes["workout-img"]} alt=""/>
                </div>
            </div>
            <div className={classes["statistics-div"]}>
                <div className={classes["statistic"]}>
                    <span className={classes["numbers"]}><NumberCounter end={56} start={30} delay='4' preFix="+"/></span>
                    <p className={classes["statistic-text"]}>ARTICLES</p>
                </div>
                <div className={classes["statistic"]}>
                <span className={classes["numbers"]}><NumberCounter end={978} start={800} delay='4' preFix="+"/></span>
                    <p className={classes["statistic-text"]}>MEMBERS JOINED</p>
                </div>
                <div className={classes["statistic"]}>
                <span className={classes["numbers"]}><NumberCounter end={12} start={1}/></span>
                    <p className={classes["statistic-text"]}>EXERCISES</p>
                </div>
            </div>
            <div className={classes["advantages-div"]}>
                <h2 className={classes["advantages-heading"]}>WITH US YOU CAN</h2>
                <div className={classes["advantages-container"]}>
                    <BenefitList benefits={benefits} />
                </div>
            </div>
            <div className={classes["offer-div"]}>
                <Offer img={book} name="ARTICLES" content="Gain valuable knowledge about diet, regeneration, exercises, performance, and more through our extensive collection of articles on the fitness portal." />
                <Offer img={two_dumbbells} name="EXERCISES" content="Explore our comprehensive exercises lexicon, offering concise instructions and guidance on how to perform various exercises effectively." />
                <Offer img={friends} name="FRIENDS" content="Connect with friends, track their results, and compare profiles to stay motivated and inspired on your fitness journey." />
                <Offer img={progress} name="PROGRESS" content="Monitor your progress, analyze workout data, and visualize your history through interactive diagrams, empowering you to achieve improvement." />
            </div>
        </div>
    )
};

const Benefit = (props) => {
    return(
        <AnimatedElement animationType="grow" base="animated-v2">
            <div className={classes["advantage-div"]}>
                <img src={props.img} className={classes["benefit-icon"]} alt="a"/>
                <p className={classes["benefit-text"]}>{props.content}</p>
            </div>
        </AnimatedElement>
    );
};

const BenefitList = (props) => {
    const benefits = props.benefits.map((b) => {return <Benefit key={b.id} img={b.img} content={b.content}/>})
    return (
        <>{benefits}</>
    );
};

const Offer = (props) => {
    return (
        <AnimatedElement animationType="grow-tiny" base="animated-v2">
            <div className={classes["offer"]}>
                <img src={props.img} className={classes["offer-img"]} alt=""/>
                <h3 className={classes["offer-heading"]}>{props.name}</h3>
                <p className={classes["offer-text"]}>{props.content}</p>
            </div>
        </AnimatedElement>
    );
};

export default MainPageContent