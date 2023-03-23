import {useState, useEffect} from 'react';
import "react-datepicker/dist/react-datepicker.css";
import {Stack, Col} from "react-bootstrap";
import Axios from "axios";
import useSWR from "swr";

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

function Time() {
    const {data, error} = useSWR("http://localhost:3000/api/nftlist", axios1);
    const [time, setTime] = useState(new Date());
    useEffect(() => {
        const id = setInterval(() => {
            setTime(new Date());
        }, 1000);
        return (() => clearInterval(id))
    }, []);

    // 현재 진행예정과 진행중인 대회 출력
    let starttime: string = data?.map((e: { game: string, winner: string, startTime: string }) => {
        if (!e.winner) {
            return e.startTime.slice(0, 4) + '/' + e.startTime.slice(5, 6) + '/' + e.startTime.slice(7, 9) + '/' + e.startTime.slice(10, 15);
        }
    });
    let gap = Date.parse(starttime) - time.getTime();

    // @ts-ignore
    return (
        <>
            <Stack direction="horizontal" gap={3} className="text-center">
                <Col className="bg-light border">
                    <h1>현재시간</h1>
                    <h1>{time.toLocaleTimeString()}</h1>
                </Col>

                <Col className="bg-light border">
                    {/*게임 남은 시간, 시작, 끝*/}
                    <h1 style={{whiteSpace: "pre-wrap"}}>{gap>=0 ? gap >= 0 ? `대회까지 남은시간 \n ${printDate(gap)}` : '게임시작' : '현재 진행중인 대회가 없습니다.' }</h1>

                </Col>
                <Col className="bg-light border">

                    {data?.map((e: { game: string, winner: string, startTime: string }) => {
                        if (!e.winner) {
                            return <><h1>대회 시작 시간</h1><h1>{e.startTime}</h1></>
                        }
                    })}
                </Col>
            </Stack>
            <div className="d-flex justify-content-center mt-5">
                {data?.map((e: { game: string, winner: string, startTime: string }) => {
                    if (!e.winner) {
                        return <><h1>{gap >= 0 ? `${e.game} (시작전)` : '진행중'}</h1></>
                    }
                })}
            </div>
        </>
    );
}

function printDate(time: number) {
    const days = Math.floor(time / (1000 * 60 * 60 * 24)); // 일
    const hour = String(Math.floor((time / (1000 * 60 * 60)) % 24)).padStart(2, "0"); // 시
    const minutes = String(Math.floor((time / (1000 * 60)) % 60)).padStart(2, "0"); // 분
    const second = String(Math.floor((time / 1000) % 60)).padStart(2, "0"); // 초
    return `${days}일 ${hour}: ${minutes}: ${second}`;
}

export default Time;