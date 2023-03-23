//import { count } from "console";
import {useEffect, useState} from "react";
import Axios from "axios";
import useSWR from "swr";
import {Dropdown} from "react-bootstrap";
import {tr} from "date-fns/locale";

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

export default function App1() {
    const {data, error, isLoading} = useSWR("http://localhost:3000/api/nftlist", axios1);
    const [state1, setState1] = useState("");
    const [receiver, setReceiver] = useState(0);
    const [ipt1, setipt1] = useState("");
    const [ipt2, setipt2] = useState("");
    const [ipt3, setipt3] = useState("");
    const [nfts, setNfts] = useState(new Map());
    const [supply, setSupply] = useState(0);
    const [users, setUsers] = useState(new Map());

    fetch('/api/nftapi4?test').then((res) => res.json()).then((res) => {
        setipt3(res.name);
    });
    const cbList = () => {
        console.log("nfts len1: " + nfts);
        fetch('/api/nftapi4?supply').then((rst) => rst.json()).then(rst => {
            //if(nfts.length > 0) {return }
            console.log("nfts len2: " + nfts.size);
            setSupply(rst.supply);
            const count = rst.supply;
            console.log("count First: " + count);

            for (let i = 0; i < count; i++) {
                console.log("i: " + i);
                //const contact = await contactList.methods.contacts(i).call();
                fetch('/api/nftapi4?list=' + i).then((rst) => rst.json()).then(rst => {
                    console.log("id: " + rst.id + " " + rst.data);
                    //if(nfts 중복검사)
                    setNfts(map => new Map(map.set(rst.id, rst.data)));

                });
                //setNfts({...nfts, ["key"+rst.id]:rst.data } );

            }

            for (let i = 0; i < 4; i++) {
                fetch('/api/nftapi4?balance=' + i).then((rst) => rst.json()).then(rst => {
                    console.log("user id: " + rst.id + " " + rst.balance + " ");

                    //setUsers(map => new Map(map.set(rst.id, JSON.stringify({addr: rst.addr, balance: rst.balance} ) ) ));
                    setUsers(map => new Map(map.set(rst.id, {addr: rst.addr, balance: rst.balance})));

                });
            }

        });
    }

    const cbInit = () => {
        setState1("try mint");
        fetch('/api/nftapi4?mint=' + receiver).then((rst) => rst.json()).then(rst => {
            setState1("b1: " + rst.balanceof1 + "id: " + rst.nftid + "b2: " + rst.balanceof2);
            Axios.get("http://localhost:3000/api/nftlist?update1=" + ipt1 + "&winner=" + ipt2 + "&nftId=" + rst.nftid+" name"+rst.nftid+" URI"+rst.nftid + "&EOA=" + rst.addr);
        });
    };

    useEffect(() => {
            cbList();
        }
        , []);

    const myKeys: any[] = [];
    nfts.forEach((value, key) => myKeys.push(key));
    const uIDs: any[] = [];
    users.forEach((value, key) => uIDs.push(key));

    return (
        <div>

            <h1>contract 주소 : {ipt3}</h1>
            <h1> NFT 발행 </h1>
            <div> Receiver:
                <input value={receiver} onChange={(e) => setReceiver(Number(e.target.value))}/>
                <button onClick={cbInit}>발행</button>
                State: {state1}
            </div>
            <br/>
            <div>
                <h1>대회 이름: {ipt1}</h1>
                <h1>우승자: {ipt2}</h1>
            </div>
            <div>
                <Dropdown>
                    <Dropdown.Toggle variant="success" id="dropdown-basic">
                        대회 리스트
                    </Dropdown.Toggle>
                    <Dropdown.Menu>
                        {data?.map((e: { game: string, winner: string }) => {
                            return <><Dropdown.Item onClick={() => {
                                setipt1(e.game);
                                setipt2(e.winner);
                            }}>{e.game}</Dropdown.Item>
                            </>
                        })}
                    </Dropdown.Menu>
                </Dropdown>
            </div>

            List: {supply}
            {/* Length: {nfts.length} */}
            <br/>
            <ul>
                {
                    // nfts.forEach( (value,key) =>  (<li>"value: " {value} </li>))

                    myKeys.map((key) => (

                        <li>
                            index: {key} nftId: {nfts.get(key).slice(0, 12)} addr: {nfts.get(key).slice(12)}
                        </li>
                    ))
                }
            </ul>

            Balance:
            <table>
                {
                    // nfts.forEach( (value,key) =>  (<li>"value: " {value} </li>))
                    uIDs.map((uid) => (
                        // eslint-disable-next-line react/jsx-key
                        <tr>
                            <td>User ID: {uid}</td>
                            <td>
                                {data.map((e: { winner: string, EOA: string }) => {
                                    if (e.EOA == users.get(uid)['addr']) {
                                        return <>{e.winner}</>
                                    }
                                })}
                            </td>
                            <td>Addr: {users.get(uid)['addr']}</td>
                            <td> Balance: {users.get(uid)['balance']}</td>
                        </tr>
                    ))
                }
            </table>
        </div>
    );
}