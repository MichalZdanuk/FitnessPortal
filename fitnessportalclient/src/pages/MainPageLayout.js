import { useNavigate, Outlet } from "react-router-dom";

const MainPageLayout = () =>{
    const navigate = useNavigate();
    return (
        <>
        <h1>Navbar</h1>
        <button onClick={(e) => {navigate("/")}}>Home</button>
        <button onClick={(e) => {navigate("/articles")}}>Articles</button>
        <button onClick={(e) => {navigate("/exercises")}}>Exercises</button>
        <button onClick={(e) => {navigate("/calculators")}}>Calculators</button>
        <button onClick={(e) => {navigate("/trainings")}}>MyTrainings</button>
        <button onClick={(e) => {navigate("/friends")}}>Friends</button>
        <button onClick={(e) => {navigate("/account")}}>Account</button>

        <p>Some Content:</p>
        <Outlet />
        <p>Footer</p>
        </>
    )
};


export default MainPageLayout;