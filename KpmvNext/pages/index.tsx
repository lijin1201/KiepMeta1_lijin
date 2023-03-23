import useSWR from 'swr'
import Axios from "axios";
import {useState} from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import {Button, Table, Modal, Dropdown, Form, Accordion, Image, Stack} from 'react-bootstrap';
import Quiz from './quiz';
import Time from './time';
import Competition from "@/pages/competition";

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

const App1 = () => {
    const {data, error, isLoading} = useSWR("http://localhost:3000/api/metabus", axios1);
    const [ipt1, setipt1] = useState("");
    const [ipt2, setipt2] = useState("");
    const [ipt3, setipt3] = useState("");
    const [smShow, setSmShow] = useState(false);
    const [smShow2, setSmShow2] = useState(false);
    const handleClose = () => setSmShow(false);
    const handleClose2 = () => setSmShow2(false);

    if (error) {
        return <>error!</>;
    }
    if (!data) {
        return <>loading</>;
    }

    return (
        <>
            <div className="d-flex justify-content-center ">
                <Image
                    src="https://user-images.githubusercontent.com/104874755/224620237-a16af7ef-30ec-4f96-91fe-f48fc31661a3.png"
                    roundedCircle alt="metabusimg"/>
            </div>
            {/* 현재시간 */}
            <div className="m-5">
                <Time/>
            </div>
            <Stack className="d-flex justify-content-center" direction="horizontal" gap={3}>
                {/* 구성요소 드롭다운 */}
                <div>
                    <Dropdown>
                        <Dropdown.Toggle variant="success" id="dropdown-basic">
                            Object 추가
                        </Dropdown.Toggle>
                        <Dropdown.Menu>
                            <Dropdown.Item onClick={() => setSmShow2(true)}> 구성요소1 </Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>
                </div>
                {/* 퀴즈 드롭다운 */}
                <Quiz/>
                <Competition/>
            </Stack>
            <br/>
            {/* 목록 아코디언 */}
            <Accordion>{/*defaultActiveKey="0"*/}
                <Accordion.Item eventKey="0">
                    <Accordion.Header>구성요소1 list</Accordion.Header>
                    <Accordion.Body>
                        <div className='reservation_list'>
                            <h2> 구성요소 목록</h2>
                            <br/>
                            <Table striped bordered hover>
                                {data.map((e: { name: string, x: Int32Array, y: Int32Array }) => {
                                    return <>
                                        <tr>
                                            <th>큐브 :</th>
                                            <td style={{width: "100px"}}><a href={'/user/' + e.name}>{e.name}</a></td>
                                            <th> X좌표 :</th>
                                            <td>{e.x}</td>
                                            <th> Y좌표 :</th>
                                            <td>{e.y}</td>
                                            <td>
                                                <Stack direction="horizontal" gap={2}>
                                                    <Button onClick={() => {
                                                        setSmShow(true);
                                                        setipt1(e.name);
                                                    }}
                                                            className='btn btn-danger'>수정</Button>
                                                    <Button onClick={() => {
                                                        Axios.get("http://localhost:3000/api/metabus?del=" + e.name);
                                                        alert("삭제!!");
                                                    }} className='btn btn-danger'>삭제</Button>
                                                </Stack></td>
                                        </tr>
                                        <br/></>
                                })
                                }
                            </Table>
                        </div>
                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
            {/*footer*/}

            {/* 수정 모달 */}
            <Modal
                size="sm"
                show={smShow}
                onHide={() => setSmShow(false)}
                aria-labelledby="example-modal-sizes-title-sm"
            >
                <Modal.Header closeButton>
                    <Modal.Title id="example-modal-sizes-title-sm">
                        수정
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className='mb-3'>
                        <Form.Label>박스</Form.Label>
                        <Form.Control value={ipt1} onChange={(e) => setipt1(e.target.value)} placeholder="박스 이름"/>
                    </Form.Group>
                    <Form.Group className='mb-3'>
                        <Form.Label>x좌표</Form.Label>
                        <Form.Control value={ipt2} onChange={(e) => setipt2(e.target.value)} placeholder="x 좌표"/>
                    </Form.Group>
                    <Form.Group className='mb-3'>
                        <Form.Label>y좌표</Form.Label>
                        <Form.Control value={ipt3} onChange={(e) => setipt3(e.target.value)} placeholder="y 좌표"/>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={handleClose} variant="secondary">Close</Button>
                    <Button variant="primary" onClick={() => {
                        Axios.get("http://localhost:3000/api/metabus?update=" + ipt1 + "&PosX=" + ipt2 + "&PosY=" + ipt3).then(() => {
                            setSmShow(false);
                        });
                    }}>Save changes</Button>
                </Modal.Footer>
            </Modal>
            {/* 삭제 모달 */}

            {/* 구성요소1 추가 모달 */}
            <Modal
                size="sm"
                show={smShow2}
                onHide={() => setSmShow2(false)}
                aria-labelledby="example-modal-sizes-title-sm"
            >
                <Modal.Header closeButton>
                    <Modal.Title id="example-modal-sizes-title-sm">
                        구성요소1
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className='mb-3'>
                        <Form.Label>박스</Form.Label>
                        <Form.Control value={ipt1} onChange={(e) => setipt1(e.target.value)} placeholder="박스 이름"/>
                    </Form.Group>
                    <Form.Group className='mb-3'>
                        <Form.Label>x좌표</Form.Label>
                        <Form.Control value={ipt2} onChange={(e) => setipt2(e.target.value)} placeholder="x 좌표"/>
                    </Form.Group>
                    <Form.Group className='mb-3'>
                        <Form.Label>y좌표</Form.Label>
                        <Form.Control value={ipt3} onChange={(e) => setipt3(e.target.value)} placeholder="y 좌표"/>
                    </Form.Group>
                </Modal.Body>
                <div className='modal-footer'>
                    <Button type="button" className='btn btn-secondary' data-bs-dismiss="modal" onClick={handleClose2}>Close</Button>
                    <Button className='btn btn-primary' onClick={(e) => {
                        Axios.get("http://localhost:3000/api/metabus?add=" + ipt1 + "&PosX=" + ipt2 + "&PosY=" + ipt3);
                        history.back();
                    }}>Save</Button>
                </div>
            </Modal>
        </>
    );
}
    ;

    export default App1;