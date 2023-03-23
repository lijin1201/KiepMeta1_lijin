import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import {useState} from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {faMeta }from "@fortawesome/free-brands-svg-icons";
import {faMagnifyingGlass} from "@fortawesome/free-solid-svg-icons"

function Header() {

    const[ipt1, setipt1] = useState("");

    return (
        <Navbar bg="light" expand="lg">
            <Container fluid>

                <Navbar.Brand href="/"><FontAwesomeIcon icon={faMeta}/> 메타버스 퀴즈 대회</Navbar.Brand>
                <Navbar.Toggle aria-controls="navbarScroll" />
                <Navbar.Collapse id="navbarScroll">
                    <Nav
                        className="me-auto my-2 my-lg-0"
                        style={{ maxHeight: '200px' }}
                        navbarScroll
                    >
                        <Nav.Link href="/">Home</Nav.Link>
                        <Nav.Link href="http://localhost:3000/quizList">퀴즈 목록</Nav.Link>
                        <NavDropdown title="Object" id="navbarScrollingDropdown">
                            <NavDropdown.Item href="#action3">Action</NavDropdown.Item>
                            <NavDropdown.Item href="#action4">
                                Another action
                            </NavDropdown.Item>
                            <NavDropdown.Divider />
                            <NavDropdown.Item href="#action5">
                                Something else here
                            </NavDropdown.Item>
                        </NavDropdown>
                        <Nav.Link href="http://localhost:3000/competitionList">
                            역대 수상 목록
                        </Nav.Link>
                        <Nav.Link href="http://localhost:3000/nftusage_test">
                            nft 발행
                        </Nav.Link>
                        <Nav.Link href="http://localhost:3000/gamewinner">
                            우승자 nftTEST
                        </Nav.Link>
                        <Nav.Link href="#" disabled>
                            nft 조회
                        </Nav.Link>
                    </Nav>
                    <Form className="d-flex">
                        <Form.Control
                            type="search"
                            placeholder="Search"
                            value={ipt1}
                            onChange={(e) => setipt1(e.target.value)}
                            className="me-2"
                            aria-label="Search"
                        />
                        <Button variant="outline-success" href={"http://localhost:3000/user/"+ipt1}><FontAwesomeIcon icon={faMagnifyingGlass}/></Button>
                    </Form>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default Header;