import useSWR from 'swr';
import Axios from 'axios';
import { useRouter } from 'next/router';
import { useState } from 'react';

const axios1 = (url: string) => Axios.get(url).then((res) => res.data);

const App1 = () => {
    const {query: qr1} = useRouter();
    const {data, error} = useSWR(
        `http://localhost:3000/api/nftlist?read1=${qr1.nft}`,
        axios1
    );

    if (error) {
        return <>error!</>;
    }
    if (!data) {
        return <>
            <h2>nftId : {qr1.nft}</h2>
            loading...</>;
    }

    return (
        <div>
            <h2>우승자 : {data?.winner}</h2><br/>
            <p>대회이름 : {data?.game}</p><br/>
            <p>nftId : {data?.nftId}</p><br/>
            <p>우승횟수 : {data?.count}</p><br/>
        </div>
    );
}

export default App1;