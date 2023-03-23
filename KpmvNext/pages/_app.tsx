import "bootstrap/dist/css/bootstrap.min.css"; // Import bootstrap CSS
//import '@/styles/globals.css'
import type {AppProps} from 'next/app'
import Head from 'next/head';
import { config } from '@fortawesome/fontawesome-svg-core'
import '@fortawesome/fontawesome-svg-core/styles.css'
config.autoAddCss = false
import Header from "./header";
import Footer from "./footer";

export default function App({Component, pageProps}: AppProps) {

    // useEffect(() => {
    // 	require("bootstrap/dist/js/bootstrap.bundle.min.js");
    // }, []);

    return (
        <>
            <Head>
                <title>METAVERSE WITH UNITY</title>
                <link rel="icon" href="/meta.svg" />
            </Head>
            <div className="d-flex flex-column min-vh-100">
                <Header></Header>
                <hr/>
                <Component {...pageProps} />
                <Footer></Footer>
            </div>
        </>
    )
}
