using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nc1Ex1Server
{
	class Nc1Ex1ServerMainAm2
	{
		//dll import: NclibCpc4Dll.dll, Jc1Cs.dll, Jc1Dn2_0.dll, Jg1Cs.dll, Jg1CsDn2_0.dll

		public class Sv : NccpcDll.NccpcNw1Sv
		{
			public List<int> mCs = new List<int>();
			public NccpcDll.NccpcMemmgr2Mgr mMm;

			public int mObjX = 0, mObjY = 0;

			public Sv()
				: base()
			{
				mMm = new NccpcDll.NccpcMemmgr2Mgr();
			}

			public bool create()
			{
				if (!mMm.create()) { return false; }

				var co = new NccpcDll.NccpcNw1Sv.CreateOptions(mMm, "7777");

				if (!base.create(co)) { return false; }

				return true;
			}

			new public void release()
			{
				base.release();
				mMm.release();
			}

			public void qv(string s1) { System.Console.WriteLine(s1); }
			public override void onNccpcNwLog(string s1) { qv(s1); }
			//public override void onNccpcNwErr(string s1) { qv("Err " + s1); }
			public override void onNccpcNwEnter(int cti, string peer) {
				qv("Dbg NwEnter ct:" + cti + " Peer:" + peer);
				mCs.Add(cti);

				using (var pkw = mMm.allocNw1pk(0xff)) {
					pkw.setType(100);
					pkw.wStrToNclib1FromClr("Name"+cti);
					pkw.wInt32s(mObjX);
					pkw.wInt32s(mObjY);
					//send(mCs, pkw);
					send(cti, pkw);
				}
			}
			//public override NccpcMemmgr2Obj1 onNccpcNwEncode(int cti, int out desclen, NccpcMemmgr2Obj1 srcobj, int srclen, unsigned char cft) { return null; }
			//public override NccpcMemmgr2Obj1 onNccpcNwDecode(int cti, int out desclen, NccpcNw1StreamWar1 srcsw, unsigned char cft) { return null; }
			public override void onNccpcNwRecv(int cti, NccpcDll.NccpcNw1Pk2 ncpk) {
				qv("Dbg NwRecv Type:" + ncpk.getType() + " Len:" + ncpk.getDataLen());
				using (var pkw = ncpk.copyDeep()) {
					send(mCs, pkw);
				}
			}
			public override void onNccpcNwLeave(int cti) { qv("Dbg NwLeave ct:" + cti + " remain:" + (mCs.Count - 1)); mCs.Remove(cti); }

		}

		static public void qv(string s1) { System.Console.WriteLine(s1); }

	static void Main(string[] args)
	{
			NccpcDll.NccpcNw1Cmn.stWsaStartup();

			qv("Dbg mongodb rst");
			//Mdb1.DbEx_Insert1();
			int objx = 10; // Mdb1.DbEx_FindObjX("Obj1");
			qv("Dbg mongodb objx:" + objx);

			var sv = new Sv();

			qv("Dbg server starting");

			if (!sv.create()) { return; }
			sv.mObjX = objx;

			qv("Dbg server started");

			bool bWhile = true;
			while (bWhile)
			{
				sv.framemove();

				System.Threading.Thread.Sleep(100);
			}

			sv.release();

			NccpcDll.NccpcNw1Cmn.stWsaCleanup();
		}
	}
}
