using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    public QuizManager mQuizManager;

    public List<Obj1> mlist()
    {
        return dbList;
    }

    static public void qv(string s1) { Debug.Log(s1); }
    public class Obj1
    {
        public string mContent, mAnswer;
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
                            mObj1.mContent = s1;
                            mObj1.mAnswer = s2;
                            dbList.Add(mObj1);
                            qv("dbList : " + dbList.Count);
                        }
                    }
                    break;
            }
            return true;
        }
    }

    public Client mCt;
    
    static public List<Obj1> dbList;

    void Awake()
    {
        mQuizManager.terrain = GameObject.Find("Terrain");
        mQuizManager.quizStart = GameObject.Find("QuizStart");
        mQuizManager.winner = GameObject.Find("Trophy");
        mQuizManager.player = GameObject.Find("Player");
    }

    void Start()
    {
        mCt = new Client();
        mCt.connect("127.0.0.3", 7777);
        Debug.Log("Start 1111");
    }
    

    void Update()
    {
        mCt.framemove();

        //게임 시작(게임 유)
        if (mQuizManager.isCompetitionState_Starting())
        {
            mQuizManager.quizText.text = "10초 후 퀴즈가 시작됩니다" + System.Environment.NewLine +
              "문제 종료시 중앙에 있어도 탈락 처리 됩니다" + System.Environment.NewLine + Mathf.Round(mQuizManager.mRemainCompetitionTime);
        }
        //게임 Play
        if (mQuizManager.isCompetitionState_QuizPlay())
        {
            mQuizManager.cleanFloor = true;
            mQuizManager.quizText.text = dbList[mQuizManager.curQuiz].mContent + System.Environment.NewLine + Mathf.Round(mQuizManager.getAnswerTimeOut()); // 문제
            mQuizManager.mAnswerTimeOut -= Time.deltaTime;
        }
        //답 비교 / 결과 출력
        if (mQuizManager.isCompetitionState_QuizAnswer())
        {
            string result = (dbList[mQuizManager.curQuiz].mAnswer == "O" ? "O" : "X");
            mQuizManager.NextAnswerDelayTimeOut -= Time.deltaTime;
            //Debug.Log("NextAnswerDelayTimeOut " + NextAnswerDelayTimeOut); //남은시간
            mQuizManager.quizText.text = "결과는 : " + result + "입니다" + System.Environment.NewLine + Mathf.Round(mQuizManager.NextAnswerDelayTimeOut);
            CheckFloor();
        }
        //다음 퀴즈 넘어감
        if(mQuizManager.isCompetitionState_QuizNext())
        {
            if (mQuizManager.nextAnswer()) //퀴즈가 남아있을때
            {
                mQuizManager.NextAnswerDelayTimeOut = 5.0f;
            }

            if(mQuizManager.getRemainQuizCount() == 0) //퀴즈가 남아있지 않을때 종료
            {
                mQuizManager.quizText.text = "퀴즈가 종료되었습니다";

                if (mQuizManager.canvas.gameObject.activeSelf)
                {
                    StartCoroutine(EndQuizAfterDelay(5f));
                }
                IEnumerator EndQuizAfterDelay(float delay)
                {
                    yield return new WaitForSeconds(delay);
                    mQuizManager.quizStart.GetComponent<QuizStart>().EndQuiz();
                }

                mQuizManager.terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Quiz";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Quiz";
                mQuizManager.mOobj.gameObject.tag = "Quiz";
                mQuizManager.mXobj.gameObject.tag = "Quiz";

                mQuizManager.player.GetComponent<PlayerMotion>().save = false;
                mQuizManager.winner.GetComponent<Winner>().isFollowing = false;
                mQuizManager.winner.GetComponent<Winner>().winner.transform.position = new Vector3(8, 36f, 9);
                Invoke("ResetQuiz", 5);
            }
        }
    }
    //발판(O/X)과 정답 확인
    public void CheckFloor()
    {
        //cleanFloor가 true이고 게임이 시작되면
        if (mQuizManager.cleanFloor && !mQuizManager.isCompetitionPlay())
        {
            //문제 정답이 O일 때 X 발판을 Die로 변경하고, X일 때 O 발판을 Die로 변경
            if (dbList[mQuizManager.curQuiz].mAnswer == "O")
            {
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Die";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Die";
                mQuizManager.mOobj.gameObject.tag = "Quiz";
                mQuizManager.mXobj.gameObject.tag = "Die";
            }
            else if (dbList[mQuizManager.curQuiz].mAnswer == "X")
            {
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Die";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Die";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Quiz";
                mQuizManager.mOobj.gameObject.tag = "Die";
                mQuizManager.mXobj.gameObject.tag = "Quiz";
            }
            Invoke("ClearFloor", 1f);
            mQuizManager.cleanFloor = false;
        }
    }

    public void ClearFloor()
    {
        mQuizManager.terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
        mQuizManager.terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Quiz";
        mQuizManager.terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Quiz";
        mQuizManager.mOobj.gameObject.tag = "Quiz";
        mQuizManager.mXobj.gameObject.tag = "Quiz";
    }

    public void CloseWall()
    {
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallz0.GetComponent<BoxCollider>().enabled = true;
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallz1.GetComponent<BoxCollider>().enabled = true;
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallx0.GetComponent<BoxCollider>().enabled = true;
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallx1.GetComponent<BoxCollider>().enabled = true;

    }

    public void OpenWall()
    {
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallz0.GetComponent<BoxCollider>().enabled = false;
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallz1.GetComponent<BoxCollider>().enabled = false;
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallx0.GetComponent<BoxCollider>().enabled = false;
        mQuizManager.terrain.GetComponent<TerrainGenerator>().cWallx1.GetComponent<BoxCollider>().enabled = false;
    }
    //초기화
    private void ResetQuiz() 
    {
        mQuizManager.userQuiz.Clear(); // 사용된 문제 인덱스 리스트 초기화
        mQuizManager.mCurrentQuizIndex = 0; // 다음 문제 선택을 위한 인덱스 초기화
        mQuizManager.curQuiz = 0; // 현재 문제 초기화
        mQuizManager.ox = false; // 정답 여부 초기화
        mQuizManager.mRemainCompetitionTime = 3.0f; // 퀴즈 시작까지 시간 초기화
        mQuizManager.mAnswerTimeOut = 5.0f; // 문제 시간 초기화
        mQuizManager.NextAnswerDelayTimeOut = 5.0f; // 다음 문제 시간 초기화
        mQuizManager.gameStarted = false; // 게임 시작 여부 초기화

        OpenWall();
    }
}