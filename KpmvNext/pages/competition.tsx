import {useState} from 'react';
import Axios from "axios";
import 'bootstrap/dist/css/bootstrap.css';
import {Button, Modal, Form} from 'react-bootstrap';
import {setHours, setMinutes} from "date-fns";
import DatePicker from "react-datepicker";


export default function Competition() {
    const [startDate, setStartDate] = useState(
        setHours(setMinutes(new Date(), 30), 18)
    );
    const [ipt1, setipt1] = useState("");
    const [ipt2, setipt2] = useState("");
    const [smShow1, setSmShow1] = useState(false);
    const handleClose = () => setSmShow1(false);
    const startday = startDate.toLocaleString('ja-JP').slice(0, 20);
    fetch('/api/nftapi4?test').then((res) => res.json()).then((res) => {
        setipt2(res.name);
    });
    return (
        <>
            <Button variant="primary" onClick={() => setSmShow1(true)}>
                대회 추가
            </Button>
            <div>
                <Modal
                    size="lg"
                    show={smShow1}
                    onHide={() => setSmShow1(false)}
                    aria-labelledby="example-modal-sizes-title-sm"
                >
                    <Modal.Header closeButton>
                        <Modal.Title id="example-modal-sizes-title-sm">
                            대회 개최
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form.Group className='mb-3'>
                            <Form.Label>대회 이름</Form.Label>
                            <Form.Control value={ipt1} onChange={(e) => setipt1(e.target.value)}
                                          placeholder="대회 이름 추가"/>
                        </Form.Group>
                        <Form.Group className='mb-3'>
                            <Form.Label>대회 시작 시간</Form.Label>
                            <DatePicker
                                selected={startDate}
                                onChange={(date: Date) => setStartDate(date)}
                                showTimeSelect
                                excludeTimes={[
                                    setHours(setMinutes(new Date(), 0), 17),
                                    setHours(setMinutes(new Date(), 30), 18),
                                    setHours(setMinutes(new Date(), 30), 19),
                                    setHours(setMinutes(new Date(), 30), 17),
                                ]}
                                dateFormat="MMMM d, yyyy h:mm aa"
                            />
                        </Form.Group>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button type="button" className='btn btn-secondary' data-bs-dismiss="modal"
                                onClick={handleClose}>Close</Button>
                        <Button className='btn btn-primary' onClick={(e) => {
                            Axios.get("http://localhost:3000/api/nftlist?add=" + ipt1 + "&nftId=" + "&winner=" + "&EOA="+ "&CA="+ ipt2 + "&startTime=" + startday + "&count=").then(() => {
                                setSmShow1(false);
                            });
                            alert("대회 추가");
                        }}>Save</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        </>
    );
}