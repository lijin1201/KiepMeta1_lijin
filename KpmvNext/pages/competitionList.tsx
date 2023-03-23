import useSWR from 'swr'
import Axios from "axios";
import 'bootstrap/dist/css/bootstrap.css';
import {Accordion} from 'react-bootstrap';

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

const App1 = () => {
    const {data, error, isLoading} = useSWR("http://localhost:3000/api/nftlist", axios1);

    if (error) {
        return <>error!</>;
    }
    if (!data) {
        return <>loading</>;
    }

    return (
        <>
            <Accordion className ="text-center">
                {data.map((e: { game: string, nftId: string, winner: string, EOA: string, CA: string, startTime: string }) => {
                        return <>
                            <Accordion.Item eventKey={e.game}>
                                <Accordion.Header>{e.game}</Accordion.Header>
                                <Accordion.Body>
                                    <h2> 대회 리스트</h2>
                                    <br/>
                                    <p><h1>{e.game}</h1></p>
                                    <p><h1>우승자 : <a href={'/user/' + e.winner}>{e.winner}</a></h1></p>
                                    <p><h1>EOA : {e.EOA}</h1></p>
                                    <p><h1>CA : <a href={'/nftusage_test'}>{e.CA}</a></h1></p>
                                    <p><h1>nft id : <a href={'/nft/' + e.nftId}>{e.nftId}</a></h1></p>
                                    <p><h1>대회 시작 시간 : {e.startTime}</h1></p>
                                </Accordion.Body>
                            </Accordion.Item>
                        </>
                })}
            </Accordion>
        </>
    )
}
export default App1;