import useSWR from 'swr'
import Axios from "axios";
import 'bootstrap/dist/css/bootstrap.css';
import {Accordion} from 'react-bootstrap';
import {useState} from 'react';
import Header from './header';
import QuizList from "./quizList";
import Footer from "./footer";

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

const App1 = () => {
    const {data, error, isLoading} = useSWR("http://localhost:3000/api/nftList", axios1);

    if (error) {
        return <>error!</>;
    }
    if (!data) {
        return <>loading</>;
    }

    return (
        <>
            <hr/>
            {/*{data.map((e: { game: string, nftId: string, winner: string, nftName: string }) => {
                return <>
                    <p><h1>{e.game}</h1></p>
                    <p><h1>nft id : {e.nftId}</h1></p>
                    <p><h1>우승자 : {e.winner}</h1></p>
                    <p><h1>nft이름 : {e.nftName}</h1></p>
                </>
            })
            }*/}
            <Accordion className ="text-center">
                {data.map((e: { game: string, nftId: string, winner: string, nftName: string }) => {
                    return <>
                        <Accordion.Item eventKey={e.game}>
                            <Accordion.Header>{e.game}</Accordion.Header>
                            <Accordion.Body>
                                <h2> 대회 리스트</h2>
                                <br/>
                                <p><h1>{e.game}</h1></p>
                                <p><h1>nft id : {e.nftId}</h1></p>
                                <p><h1>우승자 : {e.winner}</h1></p>
                                <p><h1>nft이름 : {e.nftName}</h1></p>
                            </Accordion.Body>
                        </Accordion.Item>
                    </>
                })}
            </Accordion>
        </>
    )
}
export default App1;