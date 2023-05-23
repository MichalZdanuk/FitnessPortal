import { useNavigate, Outlet } from "react-router-dom";
import axios from "axios";
import { useState } from "react";

const MainPageLayout = () =>{
    const navigate = useNavigate();
    const [articlesList, setArticlesList] = useState(null);

    const handleClick = async (e) => {
        e.preventDefault();
        await axios
        .get("https://localhost:7087/api/article", {})
        .then((response) => {
            setArticlesList(response.data);
            console.log(response.data);
            navigate("/articles", {state:articlesList})
        });
    };
    
    return (
        <>
        <h1>HealthyHabitHub</h1>
        <button onClick={(e) => {navigate("/")}}>Home</button>
        <button onClick={handleClick}>Articles</button>
        <button onClick={(e) => {navigate("/exercises")}}>Exercises</button>
        <button onClick={(e) => {navigate("/calculators")}}>Calculators</button>
        <button onClick={(e) => {navigate("/trainings")}}>MyTrainings</button>
        <button onClick={(e) => {navigate("/friends")}}>Friends</button>
        <button onClick={(e) => {navigate("/register")}}>REGISTER</button>
        <button onClick={(e) => {navigate("/login")}}>LOGIN</button>
        <button onClick={(e) => {navigate("/account")}}>Account</button>

        <Outlet />
        </>
    )
};


export default MainPageLayout;