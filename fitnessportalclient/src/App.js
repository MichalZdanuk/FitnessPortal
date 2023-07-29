import "./App.css";
import {
  Route,
  // createBrowserRouter,
  // createRoutesFromElements,
  // RouterProvider,
  BrowserRouter,
  Routes,
} from "react-router-dom";

import MainPageLayout from "./pages/forEveryone/MainPageLayout";
import LoginPage from "./pages/loginAndRegister/LoginPage"
import RegisterPage from "./pages/loginAndRegister/RegisterPage"
import ArticlesPage from "./pages/forEveryone/ArticlesPage";
import ExercisesPage from "./pages/forEveryone/ExercisesPage";
import ChooseCalculatorsPage from "./pages/forEveryone/ChooseCalculatorsPage";
import TrainingsPage from "./pages/forAuthenticatedUsers/TrainingsPage";
import FriendsPage from "./pages/forAuthenticatedUsers/FriendsPage";
import AccountPage from "./pages/forAuthenticatedUsers/AccountPage";
import Article from "./components/articlePage/Article"
import FriendProfilePage from "./pages/forAuthenticatedUsers/FriendProfilePage";
import AddTrainingPage from "./pages/forAuthenticatedUsers/AddTrainingPage";
import CalculatorPage from "./pages/forEveryone/CalculatorPage";
import NotFoundPage from "./pages/forEveryone/NotFoundPage";
import AccountPageLayout from "./pages/forAuthenticatedUsers/AccountPageLayout";
import TrainingHistoryPage from "./pages/forAuthenticatedUsers/TrainingHistoryPage";
import BMIHistoryPage from "./pages/forAuthenticatedUsers/BMIHistoryPage";
import UpdateProfilePage from "./pages/forAuthenticatedUsers/UpdateProfilePage";

import AuthContext from "./store/authContext";
import { useContext, useEffect } from "react";
import jwtDecode from "jwt-decode";

// const router = createBrowserRouter(
//   createRoutesFromElements([
//     <Route path="/" element={<MainPageLayout />}>
//       <Route path="articles" element={<ArticlesPage/>}/>,
//       <Route path="articles/:articleId" element={<Article/>} />,
//       <Route path="exercises" element={<ExercisesPage/>}/>,
//       <Route path="calculators" element={<ChooseCalculatorsPage/>}/>,
//       <Route path="calculators/:calculatorType" element={<CalculatorPage/>}/>,
//       <Route path="trainings" element={<TrainingsPage/>}/>,
//       <Route path="friends" element={<FriendsPage/>}/>,
//       <Route path="friends/:friendEmail" element={<FriendProfilePage/>}/>,
//       <Route path="account" element={<AccountPage/>}/>
//       <Route path="addTraining" element={<AddTrainingPage/>}/>
//     </Route>,
//     <Route path="test/:calculatorType" element={<CalculatorPage/>}/>,
//     <Route path="register" element={<RegisterPage/>}></Route>,
//     <Route path="login" element={<LoginPage/>}></Route>
//   ])
// );

// function App() {
//   return (
//     <AuthContextProvider>
//       <RouterProvider router={router}>
//         <div className="App"></div>
//       </RouterProvider>
//     </AuthContextProvider>
//   );
// }

function App() {
  const authCtx = useContext(AuthContext);

  const checkTokenExpiration = () => {
    const token = localStorage.getItem('healthyhabithubTOKEN');

    if(token) {
      const decodedToken = jwtDecode(token);

      if(decodedToken.exp < Date.now()/1000 ){
        authCtx.logout();
      }
    }
  };

  useEffect(() => {
    checkTokenExpiration();
  
  }, []);

  

  return (
    
        <div className="App">
          <BrowserRouter>
            <Routes>
              <Route path="/" element={<MainPageLayout />}>
                <Route path="articles" element={<ArticlesPage/>}/>,
                <Route path="articles/:articleId" element={<Article/>} />,
                <Route path="exercises" element={<ExercisesPage/>}/>,
                <Route path="calculators" element={<ChooseCalculatorsPage/>}/>,
                <Route path="calculators/:calculatorType" element={<CalculatorPage/>}/>,
                <Route path="trainings" element={<TrainingsPage/>}/>,
                {/* <Route path="friends" element={<FriendsPage/>}/>,
                <Route path="friends/:friendEmail" element={<FriendProfilePage/>}/>, */}
                <Route path="account" element={<AccountPageLayout/>}>
                  <Route path ="" element={<AccountPage/>}/>
                  <Route path="updateProfile" element={<UpdateProfilePage/>}/>
                  <Route path="friendlist" element={<FriendsPage/>}/>
                  <Route path="friendlist/:friendEmail" element={<FriendProfilePage/>}/>
                  <Route path="traininghistory" element={<TrainingHistoryPage/>}/>
                  <Route path="bmihistory" element={<BMIHistoryPage/>}/>
                </Route>
                <Route path="addTraining" element={<AddTrainingPage/>}/>
              </Route>,
              <Route path="register" element={<RegisterPage/>}></Route>,
              <Route path="login" element={<LoginPage/>}></Route>,
              <Route path="*" element={<NotFoundPage/>}/>
            </Routes>
          </BrowserRouter>
        </div>
  );
}

export default App;
