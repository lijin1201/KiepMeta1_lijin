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
    public int curQuiz; // ���� ���� ����(�ʱ�ȭ)
    public bool ox; // ���� ������ �¾Ҵ��� ����

    public float mRemainCompetitionTime = 600.0f; // ���� ���۱��� �ð�
    public float mAnswerTimeOut = 5.0f; // ���� �ð�
    public float NextAnswerDelayTimeOut = 5.0f; // ���� ���� �ð�
    public bool gameStarted = false; // ���� ���� ����

    public int quizCount = 3; // ���� ���� ��
    public int mCurrentQuizIndex = 0; // ���� ���� �ε���
    public bool cleanFloor = true;
    public static List<MainClient.Obj1> dbList = new List<MainClient.Obj1>();


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

    public bool isCompetitionPlay() //��ȸ����(��/��)
    {
        return gameStarted;
    }
    public void setCompetitionPlay() //��ȸ����(��/��)
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
        //�ð��� �׻� �帥��.
        mRemainCompetitionTime -= Time.deltaTime;
    }

    //���ӽ���
    public bool isCompetitionState_Starting()
    {
        //���� �������� :
        //���� ���� ���� > 0 
        if (getRemainQuizCount() > 0)
        {
            //���������� true�̰� ������۽ð� > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {
                return true;
            }
        }
        //���� ���� ���� ���� 0 ���� �̰ų� ���������� false �̰� ������۽ð��� 0 ���� �϶� false��
        return false;
    }

    //���� Play
    public bool isCompetitionState_QuizPlay()
    {
        //���� Play���� :
        //���� ���� ���� > 0 
        if (getRemainQuizCount() > 0)
        {
            //���������� true�̰� ������۽ð� > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {

            }
            //���� �ð�(���ݼ���5��) > 0
            else if (getAnswerTimeOut() > 0)
            {
                return true;
            }
        }
        //���� �������� ������ 0 ���� �̰ų� ���� ������ false�̰� ������۽ð��� 0 ���� �̰ų� ������ ���� ���ð��� 0���� ������ false
        return false;
    }

    //���� Ǯ��
    public bool isCompetitionState_QuizAnswer()
    {
        //���� Ǫ�� ���� :
        //���� ���� ���� > 0 
        if (getRemainQuizCount() > 0)
        {
            //���������� true�̰� ������۽ð� > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {

            }
            //���� Ǫ�½ð�(���ݼ��� 5��) > 0
            else if (getAnswerTimeOut() > 0)
            {

            }
            //���� �����ð�(���ݼ��� 5��) > 0
            else if (NextAnswerDelayTimeOut > 0)
            {
                return true;
            }
        }


        return false;
    }

    //���� �������� �Ѿ��
    public bool isCompetitionState_QuizNext()
    {
        //���� �������� �Ѿ�� ���� :
        //���� ���� ���� > 0 
        if (getRemainQuizCount() > 0)
        {
            //���������� true�̰� ������۽ð� > 0
            if (!isCompetitionPlay() && mRemainCompetitionTime > 0)
            {

            }
            //���� Ǫ�½ð�(���ݼ��� 5��) > 0
            else if (getAnswerTimeOut() > 0)
            {

            }
            //���� �����ð�(���ݼ��� 5��) > 0
            else if (NextAnswerDelayTimeOut > 0)
            {

            }
            //���� ���� ������ 0���� �����϶� ���� �������� 
            else
            {
                return true;
            }
        }
        return false;
    }
}
