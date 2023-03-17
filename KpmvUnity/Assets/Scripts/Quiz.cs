using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
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

                        for (int i = 0; i < 3; i++)
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

    Client mCt;

    public TextMeshProUGUI quizText;

    private GameObject quizStart;
    private GameObject terrain; // Generator;
    private GameObject winner;

    public GameObject mOobj;
    public GameObject mXobj;
    public GameObject win;

    static public List<Obj1> dbList;
    // 나온 문제를 저장하는 리스트
    private List<int> userQuiz = new List<int>();

    private int curQuiz; // 현재 질문 추적(초기화)
    private bool ox; // 문제 정답이 맞았는지 여부

    private float nextTime = 3.0f; // 퀴즈 시작까지 시간
    private float countdown = 5.0f; // 문제 시간
    private float nextQuiz = 5.0f; // 다음 문제 시간

    private bool gameStarted = false; // 게임 시작 여부
    public int quizCount = 3; // 나올 문제 수
    private int quizIndex = 0; // 랜덤으로 선택한 문제 인덱스

    bool cleanFloor = true;

    void Awake()
    {
        terrain = GameObject.Find("Terrain");
        quizStart = GameObject.Find("Quiz Start");
        winner = GameObject.Find("Trophy");
    }

    void Start()
    {
        mCt = new Client();
        mCt.connect("127.0.0.3", 7777);
        Debug.Log("Start 1111");

        //ShuffleQuiz();
        CloseWall();
    }

    /*/// 문제를 무작위로 섞는 함수
    private void ShuffleQuiz()
    {
        for (int i = 0; i < dbList.Count; i++)
        {
            Debug.Log("Before shuffling: " + i + " " + dbList[i].mContent);
            int randomIndex = Random.Range(i, dbList.Count);
            Obj1 tempQuiz = dbList[i];
            dbList[i] = dbList[randomIndex];
            dbList[randomIndex] = tempQuiz;
            Debug.Log("After shuffling: " + i + " " + dbList[i].mContent);
        }
    }*/

    void Update()
    {
        mCt.framemove();

        if (userQuiz.Count < quizCount)
        {
            if (nextTime > 0 && !gameStarted)
            {
                nextTime -= Time.deltaTime;
                quizText.text = "10초 후 퀴즈가 시작됩니다" + System.Environment.NewLine + 
                    "문제 종료시 중앙에 있어도 탈락 처리 됩니다" + System.Environment.NewLine + Mathf.Round(nextTime); // 시작
            }
            else if (countdown > 0)
            {
                cleanFloor = true;
                quizText.text = dbList[curQuiz].mContent + System.Environment.NewLine + Mathf.Round(countdown); // 문제
                countdown -= Time.deltaTime;
            }
            else if (nextQuiz > 0)
            {
                string result = (dbList[curQuiz].mAnswer=="O" ? "O" : "X");
                nextQuiz -= Time.deltaTime;
                quizText.text = "결과는 : " + result + "입니다" + System.Environment.NewLine + Mathf.Round(nextQuiz);
                CheckFloor();
                if (userQuiz.Count == quizCount)
                {
                    CheckAnswer((dbList[curQuiz].mAnswer=="X") == ox);
                }
            }
            else
            {
                if (userQuiz.Count < quizCount)
                {
                    do
                    {
                        quizIndex = Random.Range(0, dbList.Count);
                    } while (userQuiz.Contains(quizIndex));
                    userQuiz.Add(quizIndex);

                    curQuiz = quizIndex;
                    nextQuiz = 5.0f;
                    countdown = 5.0f;
                }
                ox = false;
            }
        }
        else
        {
            quizText.text = "퀴즈가 종료되었습니다";

            StartCoroutine(EndQuizAfterDelay(3f));
            IEnumerator EndQuizAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay);
                quizStart.GetComponent<QuizStart>().EndQuiz();
            }

            terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
            terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Quiz";
            terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Quiz";
            mOobj.gameObject.tag = "Quiz";
            mXobj.gameObject.tag = "Quiz";

            winner.GetComponent<Winner>().winner.transform.position = new Vector3(8, 33.5f, 9);

            gameStarted = false; // 게임 종료
            Invoke("ResetQuiz", 5);
        }
    }

    public void CheckFloor()
    {
        if (cleanFloor)
        {
            //문제 정답이 O일 때 X 발판을 Die로 변경하고, X일 때 O 발판을 Die로 변경
            if (dbList[curQuiz].mAnswer == "O")
            {
                terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
                terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Die";
                terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Die";
                mOobj.gameObject.tag = "Quiz";
                mXobj.gameObject.tag = "Die";
            }
            else if (dbList[curQuiz].mAnswer == "X")
            {
                terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Die";
                terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Die";
                terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Quiz";
                mOobj.gameObject.tag = "Die";
                mXobj.gameObject.tag = "Quiz";
            }
            Invoke("ClearFloor", 1f);
            cleanFloor = false;
        }
  
    } 

    public void ClearFloor()
    {
        terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
        terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Quiz";
        terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Quiz";
        mOobj.gameObject.tag = "Quiz";
        mXobj.gameObject.tag = "Quiz";
    }

    public void CheckAnswer(bool isCorrect)
    {
        if (isCorrect)
        {
            // 정답 처리 후 문제와 정답 리스트에서 제거
            dbList.RemoveAt(curQuiz);

            // 사용된 문제 인덱스 리스트에 추가
            userQuiz.Add(quizIndex);
        }
        else
        {
            // 문제 오답 처리 후 다음 문제로 넘어감
            curQuiz++;
            nextQuiz = 5.0f;
            countdown = 5.0f;

            // 사용된 문제 인덱스 리스트에 추가
            userQuiz.Add(quizIndex);
        }
    }

    void CloseWall()
    {
        terrain.GetComponent<TerrainGenerator>().cWallz0.GetComponent<BoxCollider>().enabled = true;
        terrain.GetComponent<TerrainGenerator>().cWallz1.GetComponent<BoxCollider>().enabled = true;
        terrain.GetComponent<TerrainGenerator>().cWallx0.GetComponent<BoxCollider>().enabled = true;
        terrain.GetComponent<TerrainGenerator>().cWallx1.GetComponent<BoxCollider>().enabled = true;

    }

    void OpenWall()
    {
        terrain.GetComponent<TerrainGenerator>().cWallz0.GetComponent<BoxCollider>().enabled = false;
        terrain.GetComponent<TerrainGenerator>().cWallz1.GetComponent<BoxCollider>().enabled = false;
        terrain.GetComponent<TerrainGenerator>().cWallx0.GetComponent<BoxCollider>().enabled = false;
        terrain.GetComponent<TerrainGenerator>().cWallx1.GetComponent<BoxCollider>().enabled = false;
    }

    private void ResetQuiz()
    {
        userQuiz.Clear(); // 사용된 문제 인덱스 리스트 초기화
        quizIndex = 0; // 다음 문제 선택을 위한 인덱스 초기화
        curQuiz = 0; // 현재 문제 초기화
        ox = false; // 정답 여부 초기화
        nextTime = 3.0f; // 퀴즈 시작까지 시간 초기화
        countdown = 5.0f; // 문제 시간 초기화
        nextQuiz = 5.0f; // 다음 문제 시간 초기화
        gameStarted = false; // 게임 시작 여부 초기화

        winner.GetComponent<Winner>().winner.transform.position = new Vector3(8, 0f, 9);
        OpenWall();
    }
}
