using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TextTest
{

	public class NewtworkTextTestExample : MonoBehaviour
	{
		static public void qv(string s1) { Debug.Log(s1); }
		public class Obj1
		{
			public string mName, mContent;
		}

		public class Client : JcCtUnity1.JcCtUnity1
		{

			//public int mX = 0;
			public Client() : base(System.Text.Encoding.Unicode) { }
			//public void qv(string s1) { innLogOutput(s1); }

			// JcCtUnity1.JcCtUnity1
			protected override void innLogOutput(string s1) { Debug.Log(s1); }
			protected override void onConnect(JcCtUnity1.NwRst1 rst1, System.Exception ex = null)
			{
				qv("Dbg on connect: " + rst1);
				int pkt = 1111;
				using (JcCtUnity1.PkWriter1Nm pkw = new JcCtUnity1.PkWriter1Nm(pkt))
				{
					pkw.wInt32u(2222);
					//this.send(pkw);
				}
				qv("Dbg send packet Type:" + pkt);
			}
			protected override void onDisconnect()
			{ qv("Dbg on disconnect"); }
			//protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
			//{ qv("Dbg on recv: " + pkrd.getPkt()/* + pkrd.ReadString()*/ ); return true; }
			protected override bool onRecvTake(Jc1Dn2_0.PkReader1 pkrd)
			{
				switch (pkrd.getPkt())
				{
					case 100:
						{
							dbList = new List<Obj1>();
							var count = pkrd.rInt32s();

							var s1 = "";
							var s2 = "";

							for (int i = 0; i < count; i++)
							{
								Obj1 mObj1 = new Obj1();
								s1 = pkrd.rStr1def();
								s2 = pkrd.rStr1def();

								qv("ServerEnter 수신 s1: " + s1 + " s2 : " + s2);
								mObj1.mName = s1;
								mObj1.mContent = s2;
								dbList.Add(mObj1);
								qv("dbList : " + dbList.Count);
							}
						}
						break;
				}
				return true;
			}
		}

		// Start is called before the first frame update
		Client mCt;
		public List<GameObject> mOs = new List<GameObject>();
		static public List<Obj1> dbList;
		public TextMeshPro tmpUgui;
		int check = 0;
		//public TMP_Text tmpT;
		void Start()
		{
			mCt = new Client();
			mCt.connect("127.0.0.2", 7777);
			Debug.Log("Start 1111");
			tmpUgui.text = "QUIZ를 시작합니다!!!!! 모두 준비해 주세요~~";
		}

		private void Update()
		{
			mCt.framemove();
			if (dbList != null)
			{
				if (Input.GetMouseButtonDown(0))
				{
					tmpUgui.text = dbList[check].mContent;
					check++;
					foreach (var d1 in dbList)
					{
						Debug.Log("dbList 있는거냐고" + d1.mContent);
					}
					if (check > dbList.Count)
					{
						tmpUgui.text = "quiz끝!!!!";
						dbList = null;
					}
				}
			}
		}
	}
}
