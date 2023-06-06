import { Outlet, useLocation } from "react-router-dom";
import MyNavbar from "../components/navbar/MyNavbar";
import MainPageContent from "../components/mainPage/MainPageContent"
import { useState } from "react";
import { Alert } from "react-bootstrap";

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
