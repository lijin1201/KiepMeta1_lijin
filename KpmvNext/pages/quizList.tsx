import useSWR from 'swr'
import Axios from "axios";
import {useState} from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import {Button, Table, Modal, Form, Stack} from 'react-bootstrap';
import Quiz from './quiz';


const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

const QuizList = () => {
    const {data, error, isLoading} = useSWR("http://localhost:3000/api/metaquiz", axios1);
    const [smShow, setSmShow] = useState(false);
    const [ipt1, setipt1] = useState("");
    const [ipt2, setipt2] = useState("");
    const handleClose = () => setSmShow(false);

    if (error) {
        return <>error!</>;
    }
    if (!data) {
        return <>loading</>;
    }

    return (
        <>
            <Quiz/>
            <Table striped bordered hover>
                {data?.map((e: { content: string, answer: String }) => {
                    return <>
                        <tr>
                            <th>퀴즈 내용:</th>
                            <th><a href={'/user/' + e.content}>{e.content}</a></th>
                            <th> 정답!! :</th>
                            <th>{e.answer}</th>
                            <tr>
                                <Stack direction="horizontal" gap={2}>
                                    <Button onClick={() => setSmShow(true)}>수정</Button>
                                    <Button onClick={() => {
                                        Axios.get("http://localhost:3000/api/metaquiz?del=" + e.content);
                                        alert("삭제!!");
                                    }}>삭제</Button>
                                </Stack>
                            </tr>
                        </tr>
                    </>
                })
                }
            </Table>

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
                        <Form.Label>퀴즈</Form.Label>
                        <Form.Control as="textarea" rows={3} value={ipt1} onChange={(e) => setipt1(e.target.value)} placeholder="퀴즈 추가"/>
                    </Form.Group>
                    <Form.Group className='mb-3'>
                        <Form.Label>퀴즈내용</Form.Label>
                        <Form.Control value={ipt2} onChange={(e) => setipt2(e.target.value)}
                                      placeholder="퀴즈 정답 O/X"/>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={handleClose} variant="secondary">Close</Button>
                    <Button variant="primary" onClick={() => {
                        Axios.get("http://localhost:3000/api/metaquiz?update=" + ipt1 + "&answer=" + ipt2).then(() => {
                            setSmShow(false);
                        });
                    }}>Save changes</Button>
                </Modal.Footer>
            </Modal>
        </>
    )
}

export default QuizList;