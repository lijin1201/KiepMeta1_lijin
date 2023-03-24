using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    public QuizManager mQuizManager;
    public MainClient mClient;

    public static List<MainClient> dbList;

    void Awake()
    {
        mQuizManager.terrain = GameObject.Find("Terrain");
        mQuizManager.quizStart = GameObject.Find("QuizStart");
        mQuizManager.winner = GameObject.Find("Trophy");
        mQuizManager.player = GameObject.Find("Player");
        mClient = GameObject.FindObjectOfType<MainClient>();
    }


    void Update()
    {

        //���� ����(���� ��)
        if (mQuizManager.isCompetitionState_Starting())
        {
            mQuizManager.quizText.text = "10�� �� ��� ���۵˴ϴ�" + System.Environment.NewLine +
              "���� ����� �߾ӿ� �־ Ż�� ó�� �˴ϴ�" + System.Environment.NewLine + Mathf.Round(mQuizManager.mRemainCompetitionTime);
        }
        //���� Play
        if (mQuizManager.isCompetitionState_QuizPlay())
        {
            mQuizManager.cleanFloor = true;
            mQuizManager.quizText.text = MainClient.dbList[mQuizManager.curQuiz].mContent + System.Environment.NewLine + Mathf.Round(mQuizManager.getAnswerTimeOut()); // ����
            mQuizManager.mAnswerTimeOut -= Time.deltaTime;
        }
        //�� �� / ��� ���
        if (mQuizManager.isCompetitionState_QuizAnswer())
        {
            string result = (MainClient.dbList[mQuizManager.curQuiz].mAnswer == "O" ? "O" : "X");
            mQuizManager.NextAnswerDelayTimeOut -= Time.deltaTime;
            //Debug.Log("NextAnswerDelayTimeOut " + NextAnswerDelayTimeOut); //�����ð�
            mQuizManager.quizText.text = "����� : " + result + "�Դϴ�" + System.Environment.NewLine + Mathf.Round(mQuizManager.NextAnswerDelayTimeOut);
            CheckFloor();
        }
        //���� ���� �Ѿ
        if (mQuizManager.isCompetitionState_QuizNext())
        {
            if (mQuizManager.nextAnswer()) //��� ����������
            {
                mQuizManager.NextAnswerDelayTimeOut = 5.0f;
            }

            if (mQuizManager.getRemainQuizCount() == 0) //��� �������� ������ ����
            {
                mQuizManager.quizText.text = "��� ����Ǿ����ϴ�";

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
    //����(O/X)�� ���� Ȯ��
    public void CheckFloor()
    {
        //cleanFloor�� true�̰� ������ ���۵Ǹ�
        if (mQuizManager.cleanFloor && !mQuizManager.isCompetitionPlay())
        {
            //���� ������ O�� �� X ������ Die�� �����ϰ�, X�� �� O ������ Die�� ����
            if (MainClient.dbList[mQuizManager.curQuiz].mAnswer == "O")
            {
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mOFloor.gameObject.tag = "Quiz";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mCenterFloor.gameObject.tag = "Die";
                mQuizManager.terrain.GetComponent<TerrainGenerator>().mXFloor.gameObject.tag = "Die";
                mQuizManager.mOobj.gameObject.tag = "Quiz";
                mQuizManager.mXobj.gameObject.tag = "Die";
            }
            else if (MainClient.dbList[mQuizManager.curQuiz].mAnswer == "X")
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
    //�ʱ�ȭ
    private void ResetQuiz()
    {
        mQuizManager.userQuiz.Clear(); // ���� ���� �ε��� ����Ʈ �ʱ�ȭ
        mQuizManager.mCurrentQuizIndex = 0; // ���� ���� ������ ���� �ε��� �ʱ�ȭ
        mQuizManager.curQuiz = 0; // ���� ���� �ʱ�ȭ
        mQuizManager.ox = false; // ���� ���� �ʱ�ȭ
        mQuizManager.mRemainCompetitionTime = 3.0f; // ���� ���۱��� �ð� �ʱ�ȭ
        mQuizManager.mAnswerTimeOut = 5.0f; // ���� �ð� �ʱ�ȭ
        mQuizManager.NextAnswerDelayTimeOut = 5.0f; // ���� ���� �ð� �ʱ�ȭ
        mQuizManager.gameStarted = false; // ���� ���� ���� �ʱ�ȭ

        OpenWall();
    }
}