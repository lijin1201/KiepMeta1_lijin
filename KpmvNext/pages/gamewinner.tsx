import {useState} from "react";
import useSWR from 'swr'
import Axios from "axios";
import {Button, Dropdown} from "react-bootstrap";

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

export default function App1() {
    const {data, error, isLoading} = useSWR("http://localhost:3000/api/nftlist", axios1);

    const [ipt1, setipt1] = useState("");
    const [ipt2, setipt2] = useState("");
    const [ipt3, setipt3] = useState("");
    const [ipt4, setipt4] = useState("");

    const cbInit = () => {
        fetch('/api/nftapi4?EOA=' + ipt2).then((rst) => rst.json()).then(rst => {
            //setState1("b1: " + rst.balanceof1 + "id: " + rst.nftid + "b2: " + rst.balanceof2);
            Axios.get("http://localhost:3000/api/nftlist?update3=" + ipt4 + "&nftId=" + rst.nftid);
        });
    };

    return (
        <>
            <div>
                <Dropdown>
                    <Dropdown.Toggle variant="success" id="dropdown-basic">
                        대회 리스트
                    </Dropdown.Toggle>
                    <Dropdown.Menu>
                        {data?.map((e: { game: string, winner: string }) => {
                            return <><Dropdown.Item onClick={() => {
                                setipt4(e.game);
                            }}>{e.game}</Dropdown.Item>
                            </>
                        })}
                    </Dropdown.Menu>
                </Dropdown>
            </div>
            <h1>{ipt4}</h1>
            <h1>우승자 입력 테스트</h1>
            <input type="text" value={ipt1} onChange={(e) => setipt1(e.target.value)} placeholder="이름"/>
            <input type="text" value={ipt2} onChange={(e) => setipt2(e.target.value)} placeholder="EOA"/>
            <Button onClick={() => {
                Axios.get("http://localhost:3000/api/nftlist?update2="+ipt4+"&winner="+ipt1+"&EOA="+ipt2+"&count="+1);
                cbInit();
            }
            }> 우승자 입력 </Button>
        </>
    )
}