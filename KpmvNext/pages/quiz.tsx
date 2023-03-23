import useSWR from 'swr'
import Axios from "axios";
import { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import {Button, Modal, Dropdown, Form} from 'react-bootstrap';

export default function Quiz(){
    const[ipt1, setipt1] = useState("");
    const[ipt2, setipt2] = useState("");
    const [smShow3, setSmShow3] = useState(false);
    const handleClose = () => setSmShow3(false);

    return(
        <>
            <div>
                {/* 퀴즈 드롭다운 */}
                <Dropdown>
                    <Dropdown.Toggle variant="success" id="dropdown-basic">
                        퀴즈 추가
                    </Dropdown.Toggle>
                    <Dropdown.Menu>
                        <Dropdown.Item  onClick={() => setSmShow3(true)}> 퀴즈추가 </Dropdown.Item>
                    </Dropdown.Menu>
                </Dropdown>
            </div>
            <div>
                <Modal
                    size="sm"
                    show={smShow3}
                    onHide={() => setSmShow3(false)}
                    aria-labelledby="example-modal-sizes-title-sm"
                >
                    <Modal.Header closeButton>
                        <Modal.Title id="example-modal-sizes-title-sm">
                            퀴즈 추가
                        </Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form.Group className='mb-3'>
                            <Form.Label>퀴즈!</Form.Label>
                            <Form.Control as="textarea" rows={3} value={ipt1} onChange={(e)=> setipt1(e.target.value)} placeholder="퀴즈 추가"/>
                        </Form.Group>
                        <Form.Group className='mb-3'>
                            <Form.Label>퀴즈 정답!</Form.Label>
                            <Form.Control value={ipt2} onChange={(e)=> setipt2(e.target.value)} placeholder="퀴즈 정답(O/X)"/>
                        </Form.Group>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button type="button" className='btn btn-secondary' data-bs-dismiss="modal" onClick={handleClose}>Close</Button>
                        <Button className='btn btn-primary' onClick={(e)=>{
                            Axios.get("http://localhost:3000/api/metaquiz?add="+ipt1+"&answer="+ipt2).then(()=>{setSmShow3(false);});}}>Save</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        </>
    );
}