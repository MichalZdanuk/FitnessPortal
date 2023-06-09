import React, { useContext } from 'react';
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import { Container, Nav, Navbar } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import classes from "./Navbar.module.css"
import logoIcon from "../../assets/images/letterHicon.png"
import AuthContext from '../../store/authContext';
import LogoutIcon from '@mui/icons-material/Logout';

function MyNavbar() {
  const authCtx = useContext(AuthContext);
  const isUserLogged = authCtx.isUserLogged;
  const username = authCtx.username;
  // console.log("isUserLogged: ",isUserLogged);
  
  //console.log("username: ",username);
  // const decodedToken = jwtDecode(authCtx.tokenJWT);
  // console.log("tokenDate: ", decodedToken.exp);
  // console.log("dateNow: ", Date.now()/1000)

  const navigate = useNavigate();

  const handleLogout = (e) => {
    e.preventDefault();
    navigate("/");
    authCtx.logout();
  };

  return (
    <Navbar collapseOnSelect expand="lg" bg="light" variant="light" sticky="top" className={classes["navbar-shadow"]}>
      <Container>
        <Navbar.Brand href="/" className={classes["main-text"]}><img src={logoIcon} className={classes["logo-img"]} alt="logo"/><span className={classes["margin-main-text"]}>HealthyHabitHub</span></Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="/articles" className={classes["custom-link"]}>Articles</Nav.Link>
            <Nav.Link href="/exercises" className={classes["custom-link"]}>Exercises</Nav.Link>
            <Nav.Link href="/calculators" className={classes["custom-link"]}>Calculators</Nav.Link>
            {isUserLogged && (
              <>
                <Nav.Link href="/trainings" className={classes["custom-link"]}>Training Centre</Nav.Link>     
              </>
            )

            }
          </Nav>
          <Nav>
            {!isUserLogged && (<>
            <Nav.Link href="/register" className={classes["custom-link"]}>Register</Nav.Link>
            <Nav.Link href="/login" className={classes["custom-link"]}>Login</Nav.Link>
            </>
            )}
            {isUserLogged && (<>
              <Nav.Link href="/account" className={classes["custom-link"]}>Account</Nav.Link>
            <Navbar.Brand onClick={handleLogout} className={classes["logged-panel"]}>Logged as: {username} <LogoutIcon /></Navbar.Brand>
            </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default MyNavbar;