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
import ChooseCalculatorsPage from "./pages/ChooseCalculatorsPage";
import TrainingsPage from "./pages/TrainingsPage";
import FriendsPage from "./pages/FriendsPage";
import AccountPage from "./pages/AccountPage";
import Article from "./components/articlePage/Article"
import FriendProfilePage from "./pages/FriendProfilePage";
import AddTrainingPage from "./pages/AddTrainingPage";
import CalculatorPage from "./pages/CalculatorPage";

const router = createBrowserRouter(
  createRoutesFromElements([
    <Route path="/" element={<MainPageLayout />}>
      <Route path="articles" element={<ArticlesPage/>}/>,
      <Route path="articles/:articleId" element={<Article/>} />,
      <Route path="exercises" element={<ExercisesPage/>}/>,
      <Route path="calculators" element={<ChooseCalculatorsPage/>}/>,
      <Route path="calculators/:calculatorType" element={<CalculatorPage/>}/>,
      <Route path="trainings" element={<TrainingsPage/>}/>,
      <Route path="friends" element={<FriendsPage/>}/>,
      <Route path="friends/:friendEmail" element={<FriendProfilePage/>}/>,
      <Route path="account" element={<AccountPage/>}/>
      <Route path="addTraining" element={<AddTrainingPage/>}/>
    </Route>,
    <Route path="test/:calculatorType" element={<CalculatorPage/>}/>,
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
