import { Outlet } from "react-router-dom";
import MyNavbar from "../components/navbar/MyNavbar";

const MainPageLayout = () => {
  return (
    <div>
      <MyNavbar />

      <Outlet />
    </div>
  );
};

export default MainPageLayout;
