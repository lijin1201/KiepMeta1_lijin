import "bootstrap/dist/css/bootstrap.min.css"; // Import bootstrap CSS
//import '@/styles/globals.css'
import type { AppProps } from 'next/app'
import Header from "./header";
import Footer from "./footer";

export default function App({ Component, pageProps }: AppProps) {

	// useEffect(() => {
	// 	require("bootstrap/dist/js/bootstrap.bundle.min.js");
	// }, []);

	return(
		<div className="d-flex flex-column min-vh-100">
			<Header></Header>
			<Component {...pageProps} />
			<Footer></Footer>
		</div>
	)
}
