import {useState, useEffect} from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import {setHours, setMinutes} from "date-fns";
import {Stack, Col} from "react-bootstrap";

function Home() {
    const [time, setTime] = useState(new Date());
    const [startDate, setStartDate] = useState(
        setHours(setMinutes(new Date(), 30), 18)
    );
    useEffect(() => {
        const id = setInterval(() => {
            setTime(new Date());
        }, 1000);
        return (() => clearInterval(id))
    }, []);
    const gap = startDate.getTime() - time.getTime();
    const days = Math.floor(gap / (1000 * 60 * 60 * 24)); // 일
    const hour = String(Math.floor((gap / (1000 * 60 * 60)) % 24)).padStart(2, "0"); // 시
    const minutes = String(Math.floor((gap / (1000 * 60)) % 60)).padStart(2, "0"); // 분
    const second = String(Math.floor((gap / 1000) % 60)).padStart(2, "0"); // 초
    const d_day = `${days}일 ${hour}: ${minutes}: ${second}`
    // @ts-ignore
    return (
        <>
            <Stack direction="horizontal" gap={3} className="text-center">
                    <Col className="bg-light border">
                        <h1>현재시간</h1>
                        <h1>{time.toLocaleTimeString()}</h1>
                    </Col>
                    <Col className="bg-light border">
                        <h1>대회까지 남은시간</h1>
                        {/* <h1>{startDate.getTime() - time.getTime()}</h1>*/}
                        <h1>{gap >= 0 ? d_day : '게임시작'}</h1>
                    </Col>
                    <Col className="bg-light border">
                        <h1>대회 시작 시간</h1>
                        <h2 className="bg-light border">
                            <DatePicker
                                selected={startDate}
                                onChange={(date:Date) => setStartDate(date)}
                                showTimeSelect
                                excludeTimes={[
                                    setHours(setMinutes(new Date(), 0), 17),
                                    setHours(setMinutes(new Date(), 30), 18),
                                    setHours(setMinutes(new Date(), 30), 19),
                                    setHours(setMinutes(new Date(), 30), 17),
                                ]}
                                dateFormat="MMMM d, yyyy h:mm aa"
                            />
                        </h2>
                    </Col>
            </Stack>
        </>
    );
}

export default Home;