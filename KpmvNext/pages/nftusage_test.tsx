//import { count } from "console";
import {useEffect, useState} from "react";

export default function App1() {
    const [state1, setState1] = useState("");
    const [receiver, setReceiver] = useState(0);

    const [nfts, setNfts] = useState(new Map());
    const [supply, setSupply] = useState(0);
    const [users, setUsers] = useState(new Map());

  
    const cbList =  () => {
        console.log("nfts len1: "+ nfts);
        
        fetch('/api/nftapi4?supply').then((rst) => rst.json()).then(rst=>{
            //if(nfts.length > 0) {return }
            console.log("nfts len2: "+ nfts.length);
            setSupply(rst.supply);
            const count = rst.supply;
            console.log("count First: "+ count);
       
            for (let i = 0; i < count ; i++) {
                console.log("i: "+i );
                //const contact = await contactList.methods.contacts(i).call();
                fetch('/api/nftapi4?list='+i).then((rst) => rst.json()).then(rst=>{
                    console.log("id: "+rst.id +" " + rst.data);
                    //if(nfts 중복검사)
                    setNfts(map => new Map(map.set(rst.id, rst.data ) ) );
   
                });
                //setNfts({...nfts, ["key"+rst.id]:rst.data } );
                
            }

            for (let i=0; i<4; i++) {
                fetch('/api/nftapi4?balance='+i).then((rst) => rst.json()).then(rst=>{
                    console.log("user id: "+rst.id +" " + rst.balance + " ");
                  
                    //setUsers(map => new Map(map.set(rst.id, JSON.stringify({addr: rst.addr, balance: rst.balance} ) ) ));
                    setUsers(map => new Map(map.set(rst.id, {addr: rst.addr, balance: rst.balance}  ) ));
   
                });
            }
            
        });
    }

    const cbInit = () => {
        setState1("try mint");       
        fetch('/api/nftapi4?mint='+receiver).then((rst) => rst.json()).then(rst=>{
            setState1("b1: " + rst.balanceof1 + "id: " + rst.nftid + "b2: " + rst.balanceof2);


        });
    };


    useEffect(() => {      
        cbList();
        } 
    ,[]);

    var myKeys = [];
    nfts.forEach((value, key) => myKeys.push(key));
    var uIDs = [];
    users.forEach((value, key) => uIDs.push(key));

    return(
        <div>
            <h1> Mint</h1>
            <div> Receiver:
                <input value={receiver} onChange= {(e)=>setReceiver(e.target.value)} />
                <button onClick={cbInit}>Mint</button>
                State: {state1}
            </div>
            <br/>



            List: {supply}
            {/* Length: {nfts.length} */}
            <br/>
            <ul>
            {   
                // nfts.forEach( (value,key) =>  (<li>"value: " {value} </li>)) 

                myKeys.map((key) => (
                            <li>index: {key} String: {nfts.get(key)}
                            </li>
                        ) ) 
            }
            </ul>

            Balance:
            <table>
            {   
                // nfts.forEach( (value,key) =>  (<li>"value: " {value} </li>)) 
                uIDs.map( (uid) => (          
                    <tr>
                        <td>User ID: {uid}</td>
                        <td>Addr: {users.get(uid)['addr'] }</td>
                        <td>  Balance: {users.get(uid)['balance'] }</td>
                    </tr>
                ) ) 
            }
               
            </table>
        </div>
    );
}