using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc1Ex1Client
{
	class Nc1Ex1ClientMain
	{
		//dll import: Jc1Dn2_0.dll, JcCtUnity1Dll.dll
		class Obj1
		{
			//Obj1[] mObjs;
			public string mName;
			public int mX, mY;

			public void moveSend(Client ct, int plusx, int plusy)
			{
				using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(50))
				{
					pkw.wStr1(ct.mObj1.mName);
					pkw.wInt32s(mX + plusx); //x
					pkw.wInt32s(mY + plusy); //y
					ct.send(pkw);
				}
			}
		}

		class Client : JcCtUnity1.JcCtUnity1
		{
			public Obj1 mObj1 = new Obj1();

			public Client() : base(System.Text.Encoding.Unicode) { }
			public void qv(string s1) { innLogOutput(s1); }

			// JcCtUnity1.JcCtUnity1
			protected override void innLogOutput(string s1) { Console.WriteLine(s1); }
			protected override void onConnect(JcCtUnity1.NwRst1 rst1, System.Exception ex = null)
			{
				qv("Dbg on connect: " + rst1);
				if (!true)
				{
					int pkt = 1111;
					using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(pkt))
					{
						pkw.wInt32u(2222);
						this.send(pkw);
					}
					qv("Dbg send packet Type:" + pkt);
				}
			}
			protected override void onDisconnect()
			{ qv("Dbg on disconnect"); }
			//protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
			//{ qv("Dbg on recv: " + pkrd.getPkt()/* + pkrd.ReadString()*/ ); return true; }
			protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
			{
				switch (pkrd.getPkt())
				{
					case 11:
						{
							//qv("Dbg on recv: " + pkrd.getPkt());
							var s1 = pkrd.rStr1def();
							int v1 = pkrd.rInt32s();
							Console.WriteLine("수신 s: " + s1 + " pos x:" + v1); //x
						}
						break;
					case 20:
						{
							var s1 = pkrd.rStr1def();
							Console.WriteLine("수신 대화: " + s1);
						}
						break;
					case 30:
						{
							int v1 = pkrd.rInt32s();
							Console.WriteLine("수신 위치 x: " + v1);
						}
						break;
					case 50:
						{
							var s1 = pkrd.rStr1def();
							var x1 = pkrd.rInt32s();
							var y1 = pkrd.rInt32s();
							if (mObj1.mName == s1)
							{
								//mObj1.mName = s1;
								mObj1.mX = x1; mObj1.mY = y1;
							}
							Console.WriteLine("이동 s1: " + s1 + " x:" + x1 + " y:" + y1);
						}
						break;
					case 100:
						{
							var s1 = pkrd.rStr1def();
							var x1 = pkrd.rInt32s();
							var y1 = pkrd.rInt32s();
							mObj1.mName = s1;
							mObj1.mX = x1;
							mObj1.mY = y1;
							Console.WriteLine("ServerEnter 수신 s1: " + s1 + " x:" + x1 + " y:" + y1);
						}
						break;
				}
				return true;
			}
		}

		static public void qv(string s1) { Console.WriteLine(s1); }

		static void Main(string[] args)
		{
			Client ct = new Client();

			qv("Dbg client start");

			if (!ct.connect("127.0.0.1", 7777)) { qv("Dbg connect fail"); return; }

			int x = 0;
			bool bWhile = true;
			while (bWhile)
			{
				//if(ct.isConnected()) { string s1 = "send"+System.DateTime.Now.Ticks; JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(11); pkw.wStr1(s1); ct.send(pkw); Console.WriteLine(s1); }

				for (; Console.KeyAvailable;)
				{
					ConsoleKeyInfo k = Console.ReadKey(false);
					//if (k.Key == ConsoleKey.D1)
					//{
					//	string s1 = "기본 테스트";

					//	using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(11))
					//	{
					//		pkw.wStr1(s1);
					//		pkw.wInt32s(x++); //x
					//		ct.send(pkw);
					//	}
					//}
					//if (k.Key == ConsoleKey.D2)
					//{
					//	string s1 = "대화 테스트";

					//	using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(20))
					//	{
					//		pkw.wStr1(s1);
					//		ct.send(pkw);
					//	}
					//}
					//if (k.Key == ConsoleKey.D3)
					//{
					//	//이동
					//	using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(30))
					//	{
					//		pkw.wInt32s(ct.mObj1.mX++); //x
					//		ct.send(pkw);
					//	}
					//}
					switch (k.Key)
					{
						case ConsoleKey.W: { ct.mObj1.moveSend(ct, +0, +1); } break;
						case ConsoleKey.A: { ct.mObj1.moveSend(ct, -1, +0); } break;
						case ConsoleKey.S: { ct.mObj1.moveSend(ct, +0, -1); } break;
						case ConsoleKey.D: { ct.mObj1.moveSend(ct, +1, +0); } break;
					}
				}

				ct.framemove();
				System.Threading.Thread.Sleep(100);
			}

			ct.disconnect();
		}
	}
}
