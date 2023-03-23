using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    enum CompetitionState
    {
        None,
        QuizLoad,
        Starting,
        QuizPlay, //Qestion,
        QuizAnswer, //Answer,
        QuizWait,
        Result,
        End,
    }
    
    CompetitionState mState;
    
    public TextMeshProUGUI quizText;
    public Canvas canvas;

    public GameObject quizStart;
    public GameObject terrain; // Generator;
    public GameObject winner;
    public GameObject player;

    public GameObject mOobj;
    public GameObject mXobj;
    public GameObject win;

    public List<int> userQuiz = new List<int>();
    public int curQuiz; // 현재 질문 추적(초기화)
    public bool ox; // 문제 정답이 맞았는지 여부

    public float mRemainCompetitionTime = 600.0f; // 퀴즈 시작까지 시간
    public float mAnswerTimeOut = 5.0f; // 문제 시간
    public float NextAnswerDelayTimeOut = 5.0f; // 다음 문제 시간
    public bool gameStarted = false; // 게임 시작 여부
   
    public int quizCount = 3; // 나올 문제 수
    public int mCurrentQuizIndex = 0; // 지금 퀴즈 인덱스
    public bool cleanFloor = true;
    public static List<Quiz.Obj1> dbList = new List<Quiz.Obj1>();
    

    public string getAnswer()
    {
        return dbList[curQuiz].mAnswer;
    }
    public float getAnswerTimeOut()
    {
        return mAnswerTimeOut;
    }

    public bool checkAnswer(string answer)
    {
        return getAnswer() == answer;
    }

    public string getContent()
    {
        return dbList[curQuiz].mContent;
    }

    public bool isCompetitionPlay() //대회여부(유/무)
    {
        return gameStarted;
    }
    public void setCompetitionPlay() //대회여부(유/무)
    {
        gameStarted = true;
        nextAnswer();
    }

    public int getQuizMax()
    {
        return quizCount;
    }

    public int getUsedQuizCount()
    {
        return userQuiz.Count;
    }

    public int getRemainQuizCount()
    {
        return getQuizMax() - getUsedQuizCount();
    }

    public void setCompetitionResult(string ss)
    {

    }

    public float getCompetitionStartTime()
    {
        return mRemainCompetitionTime;
    }

    public bool nextAnswer() 
    {
        
        /*if (userQuiz.Contains(quizIndex)){
            quizIndex = Random.Range(0, dbList.Count);
        }*/
        
        userQuiz.Add(mCurrentQuizIndex);
        curQuiz = mCurrentQuizIndex;
        mAnswerTimeOut = 5.0f;

        return true;
    }

    public void Update()
    {
        //시간은 항상 흐른다.
        mRemainCompetitionTime -= Time.deltaTime;
    }

    //게임시작
    public bool isCompetitionState_Starting()
    {
        //게임 시작조건 :
        //남은 퀴즈 개수 > 0 
        if (getRemainQuizCount() > 0)
        {   
            //게임유무가 true이고 퀴즈시작시간 > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {
                return true;
            }
        }
        //게임 남은 퀴즈 개수 0 이하 이거나 게임유무가 false 이고 퀴즈시작시간이 0 이하 일때 false로
        return false;
    }

    //게임 Play
    public bool isCompetitionState_QuizPlay()
    {
        //게임 Play조건 :
        //남은 퀴즈 개수 > 0 
        if (getRemainQuizCount() > 0)
        {
            //게임유무가 true이고 퀴즈시작시간 > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {

            }
            //문제 시간(지금설정5초) > 0
            else if (getAnswerTimeOut() > 0)
            {
                return true;
            }
        }
        //게임 남은퀴즈 개수가 0 이하 이거나 게임 유무가 false이고 퀴즈시작시간이 0 이하 이거나 문제에 대한 대답시간이 0보다 작으면 false
        return false;
    }

    //퀴즈 풀기
    public bool isCompetitionState_QuizAnswer()
    {
        //퀴즈 푸는 조건 :
        //남은 퀴즈 개수 > 0 
        if (getRemainQuizCount() > 0)
        {
            //게임유무가 true이고 퀴즈시작시간 > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {

            }
            //문제 푸는시간(지금설정 5초) > 0
            else if (getAnswerTimeOut() > 0)
            {

            }
            //다음 문제시간(지금설정 5초) > 0
            else if (NextAnswerDelayTimeOut > 0)
            {
                return true;
            }
        }


        return false;
    }

    //퀴즈 다음으로 넘어가기
    public bool isCompetitionState_QuizNext()
    {
        //퀴즈 다음으로 넘어가는 조건 :
        //남은 퀴즈 개수 > 0 
        if (getRemainQuizCount() > 0)
        {
            //게임유무가 true이고 퀴즈시작시간 > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {

            }
            //문제 푸는시간(지금설정 5초) > 0
            else if (getAnswerTimeOut() > 0)
            {

            }
            //다음 문제시간(지금설정 5초) > 0
            else if (NextAnswerDelayTimeOut > 0)
            { 

            }
            //남은 퀴즈 개수가 0보다 이하일때 다음 게임으로 
            else
            {
                return true;
            }
        }
        return false;
    }
}
