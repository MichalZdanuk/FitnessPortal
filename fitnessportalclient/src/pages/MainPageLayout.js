import { Outlet, useLocation } from "react-router-dom";
import MyNavbar from "../components/navbar/MyNavbar";
import MainPageContent from "../components/mainPage/MainPageContent"

const MainPageLayout = () => {
  const location = useLocation();
  return (
    <div>
      <MyNavbar />
      {location.pathname === "/" ? <MainPageContent /> : ""}
      <Outlet />
    </div>
  );
};

export default MainPageLayout;
