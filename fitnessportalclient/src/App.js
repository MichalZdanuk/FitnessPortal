import "./App.css";
import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
  RouterProvider,
} from "react-router-dom";

import MainPageLayout from "./pages/MainPageLayout";
import LoginPage from "./pages/LoginPage"
import RegisterPage from "./pages/RegisterPage"
import ArticlesPage from "./pages/ArticlesPage";
import ExercisesPage from "./pages/ExercisesPage";
import CalculatorsPage from "./pages/CalculatorsPage";
import TrainingsPage from "./pages/TrainingsPage";
import FriendsPage from "./pages/FriendsPage";
import AccountPage from "./pages/AccountPage";
import Article from "./components/articlePage/Article"

const router = createBrowserRouter(
  createRoutesFromElements([
    <Route path="/" element={<MainPageLayout />}>
      <Route path="articles" element={<ArticlesPage/>}/>,
      <Route path="articles/:articleId" element={<Article/>} />,
      <Route path="exercises" element={<ExercisesPage/>}/>,
      <Route path="calculators" element={<CalculatorsPage/>}/>,
      <Route path="calculators/:calculatorType" element={<CalculatorsPage/>}/>,
      <Route path="trainings" element={<TrainingsPage/>}/>,
      <Route path="friends" element={<FriendsPage/>}/>,
      <Route path="account" element={<AccountPage/>}/>
    </Route>,
    <Route path="register" element={<RegisterPage/>}></Route>,
    <Route path="login" element={<LoginPage/>}></Route>
  ])
);

function App() {
  return (
    <RouterProvider router={router}>
      <div className="App"></div>
    </RouterProvider>
  );
}

export default App;
