import { BrowserRouter, Route, Routes } from 'react-router-dom'
import './App.module.css'
import MainPageLayout from './pages/forEveryone/mainPage/MainPageLayout'
import NotFoundPage from './pages/forEveryone/notFound/NotFoundPage'
import RegisterPage from './pages/register/RegisterPage'
import LoginPage from './pages/login/LoginPage'
import ExercisesPage from './pages/forEveryone/exercises/ExercisesPage'
import ArticlesPage from './pages/forEveryone/articles/ArticlesPage'
import CalculatorsPage from './pages/forEveryone/calculators/CalculatorsPage'
import TrainingPage from './pages/forAuthenticatedUsers/trainingCentre/TrainingsPage'
import Article from './components/articlePage/Article'
import AccountPageLayout from './pages/forAuthenticatedUsers/account/AccountPageLayout'
import TrainingsHistoryPage from './pages/forAuthenticatedUsers/account/trainingsHistory/TrainingsHistoryPage'
import BMIHistory from './pages/forAuthenticatedUsers/account/bmiHistory/BMIHistory'
import ProfilePage from './pages/forAuthenticatedUsers/account/profile/ProfilePage'
import UpdateProfilePage from './pages/forAuthenticatedUsers/account/profile/UpdateProfilePage'
import { useCheckTokenExpiration } from './hooks/useCheckTokenExpiration'
import NotAllowedPage from './pages/forEveryone/notAllowed/NotAllowedPage'
import AddTrainingPage from './pages/forAuthenticatedUsers/addTraining/AddTrainingPage'
import TrainingProgressPage from './pages/forAuthenticatedUsers/account/trainingProgress/TrainingProgressPage'
import FriendsPage from './pages/forAuthenticatedUsers/account/friends/FriendsPage'
import FriendProfilePage from './pages/forAuthenticatedUsers/account/friends/FriendProfilePage'

function App() {
  useCheckTokenExpiration()

  return (
    <div>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<MainPageLayout />}>
            <Route path="articles" element={<ArticlesPage />} />
            <Route path="articles/:articleId" element={<Article />} />
            <Route path="exercises" element={<ExercisesPage />} />
            <Route path="calculators" element={<CalculatorsPage />} />
            <Route path="trainings" element={<TrainingPage />} />
            <Route path="account" element={<AccountPageLayout />}>
              <Route path="profile" element={<ProfilePage />} />
              <Route path="updateProfile" element={<UpdateProfilePage />} />
              <Route path="friendList" element={<FriendsPage />} />
              <Route
                path="friendList/:friendId"
                element={<FriendProfilePage />}
              />
              <Route
                path="trainingProgress"
                element={<TrainingProgressPage />}
              />
              <Route
                path="trainingHistory"
                element={<TrainingsHistoryPage />}
              />
              <Route path="bmiHistory" element={<BMIHistory />} />
            </Route>
            <Route path="addTraining" element={<AddTrainingPage />} />
          </Route>
          <Route path="*" element={<NotFoundPage />} />
          <Route path="/notAllowed" element={<NotAllowedPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/login" element={<LoginPage />} />
        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App
