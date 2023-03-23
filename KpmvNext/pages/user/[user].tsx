import useSWR from 'swr';
import Axios from 'axios';
import { useRouter } from 'next/router';
import { useState } from 'react';

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

const App1 = () => {
    const {query: qr1} = useRouter();
    const {data, error} = useSWR(
        `http://localhost:3000/api/nftlist?read3=${qr1.user}`,
        axios1
    );

    if (error) {
        return <>error!</>;
    }
    if (!data) {
        return <>
            <h2>우승자 : {qr1.user}</h2>
            loading...</>;
    }

    return (
        <div>
            <h2>우승자 : {qr1.user}</h2><br/>
            {data?.map((e:{game: string, nftId: string, winner:string}) => {
                return<>
                    <p>대회이름 : {e.game}</p><br/>
                    <p>nftId : {e.nftId}</p><br/>
                </>})}
        </div>
    );
}

export default App1;