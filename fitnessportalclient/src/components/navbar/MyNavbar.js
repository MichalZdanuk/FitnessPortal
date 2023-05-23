import React from 'react';
import axios from "axios";
import { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { Container, Nav, Navbar } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import classes from "./Navbar.module.css"
import logoIcon from "../../assets/images/letterHicon.png"

function MyNavbar() {
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
    <Navbar collapseOnSelect expand="lg" bg="light" variant="light" sticky="top">
      <Container>
        <Navbar.Brand href="/" className={classes["main-text"]}><img src={logoIcon} className={classes["logo-img"]} alt="logo"/><emph className={classes["margin-main-text"]}>HealthyHabitHub</emph></Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link onClick={handleClick} className={classes["custom-link"]}>Articles</Nav.Link>
            <Nav.Link href="/exercises" className={classes["custom-link"]}>Exercises</Nav.Link>
            <Nav.Link href="/calculators" className={classes["custom-link"]}>Calculators</Nav.Link>
            <Nav.Link href="/trainings" className={classes["custom-link"]}>MyTrainings</Nav.Link>
            <Nav.Link href="/friends" className={classes["custom-link"]}>Friends</Nav.Link>
          </Nav>
          <Nav>
            <Nav.Link href="/register" className={classes["custom-link"]}>Register</Nav.Link>
            <Nav.Link href="/login" className={classes["custom-link"]}>Login</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default MyNavbar;