using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    public float timeLimit;

    [SerializeField] GameObject startTxt;
    [SerializeField] GameObject FinishTxt;
    [SerializeField] AudioSource Submit;
    [SerializeField] AudioSource Submit2;
    [SerializeField] AudioSource se_Start;
    [SerializeField] AudioSource End;
    [SerializeField] AudioSource Attack;

    GameObject touchEnemy;
    GameObject Seconds_100;//�c�莞�Ԃ̕b�̂P�O�O�̈�
    GameObject Seconds_10;//�c�莞�Ԃ̕b�̂P�O�̈�
    GameObject Seconds_1;//�c�莞�Ԃ̕b�̂P�̈�
    GameObject result;
    GameObject defeatWindow;
    GameObject ClearWindow;
    GameObject DefeatNum_10;
    GameObject DefeatNum_1;
    GameObject Clear;
    GameObject NotClear;
    GameObject shade;
    GameObject againOrEnd;
    Transform tr_enemies;
    Transform tr_timeWindow;
    Transform tr_ClearWindow;
    Animator anime_atk;
    Sprite numSprite;//�������ԁA�|�������\���p
    Coroutine coolTime;
    int enemyType = -1;//�G���Ă���G�@0:�ԁ@1:�@2:�΁@-1:�����Ȃ�
    int killNum = 0;
    float move_distance = 0.75f;//��t���[���œ�������
    bool activeInput = false;

    void Awake()
    {
        var enemies = GameObject.Find("Enemies");
        tr_enemies = enemies.transform;

        var charactor = GameObject.Find("Nedayasimaru");
        anime_atk = charactor.GetComponent<Animator>();

        var timeWindow = GameObject.Find("TimeWindow");
        tr_timeWindow = timeWindow.GetComponent<Transform>();
        Seconds_100 = tr_timeWindow.GetChild(0).gameObject;
        Seconds_10 = tr_timeWindow.GetChild(1).gameObject;
        Seconds_1 = tr_timeWindow.GetChild(2).gameObject;

        var clearWindow = GameObject.Find("ClearWindows");
        tr_ClearWindow = clearWindow.GetComponent<Transform>();
        result = tr_ClearWindow.GetChild(0).gameObject;
        defeatWindow = tr_ClearWindow.GetChild(1).gameObject;
        ClearWindow = tr_ClearWindow.GetChild(2).gameObject;
        DefeatNum_10 = tr_ClearWindow.GetChild(3).gameObject;
        DefeatNum_1 = tr_ClearWindow.GetChild(4).gameObject; ;
        Clear = tr_ClearWindow.GetChild(5).gameObject;
        NotClear = tr_ClearWindow.GetChild(6).gameObject;
        shade = tr_ClearWindow.GetChild(7).gameObject;
        againOrEnd = tr_ClearWindow.GetChild(8).gameObject;
    }

    IEnumerator Start()
    {
        TimeDraw();
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 3; i++)
        {
            StartCoroutine(EnemeisMove(move_distance));
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(StartCount());
        StartCoroutine(TimeCount());
    }

    void Update()
    {
        if(activeInput == true)
        {
            if (Input.GetAxisRaw("Submit") == 1)
            {
                Debug.Log("a");
                switch (enemyType)
                {
                    case -1:
                        Debug.Log("��U��");
                        Coroutine attak = StartCoroutine(Attak());
                        break;
                    case 0:
                        Debug.Log("��������");
                        attak = StartCoroutine(Attak());
                        Defeat();
                        break;
                    default:
                        Coroutine damege = StartCoroutine(Damege());
                        break;
                }
            }
            if (Input.GetAxisRaw("Jump") == 1)
            {
                Debug.Log("b");
                switch (enemyType)
                {
                    case -1:
                        Debug.Log("��U��");
                        Coroutine attak = StartCoroutine(Attak());
                        break;
                    case 1:
                        Debug.Log("��������");
                        attak = StartCoroutine(Attak());
                        Defeat();
                        break;
                    default:
                        Coroutine damege = StartCoroutine(Damege());
                        break;
                }
            }
            if (Input.GetAxisRaw("Cancel") == 1)
            {
                Debug.Log("x");
                switch (enemyType)
                {
                    case -1:
                        Debug.Log("��U��");
                        Coroutine attak = StartCoroutine(Attak());
                        break;
                    case 2:
                        Debug.Log("��������");
                        attak = StartCoroutine(Attak());
                        Defeat();
                        break;
                    default:
                        Coroutine damege = StartCoroutine(Damege());
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        touchEnemy = other.gameObject;
        if (other.gameObject.tag == "RedEnemy")
        {
            Debug.Log("��");
            enemyType = 0;
        }
        if (other.gameObject.tag == "BrueEnemy")
        {
            Debug.Log("��");
            enemyType = 1;
        }
        if (other.gameObject.tag == "GreenEnemy")
        {
            Debug.Log("��");
            enemyType = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyType = -1;
        touchEnemy = null;
    }

    void Defeat()
    {
        Destroy(touchEnemy.gameObject);
        Coroutine move = StartCoroutine(EnemeisMove(move_distance));
        killNum++;
    }

    IEnumerator Attak()
    {
        Debug.Log("�U��");
        anime_atk.SetBool("blAtk", true);//�U���A�j���J�n
        Attack.Play();
        yield return coolTime = StartCoroutine(CoolTime(0.4f));
    }

    IEnumerator Damege()
    {
        Debug.Log("�_���[�W");
        anime_atk.SetBool("blMiss", true);//���s�A�j���J�n
        yield return coolTime = StartCoroutine(CoolTime(1f));
    }

    //�󂯎�����ϐ��b���͂��~�߂�
    IEnumerator CoolTime(float time)
    {
        activeInput = false;
        Debug.Log("�N�[���^�C����");
        float elapsedTime = 0;
        var wait = new WaitForFixedUpdate();
        while (time > elapsedTime)
        {
            yield return wait;
            elapsedTime += 0.02f;
        }
        anime_atk.SetBool("blAtk", false);//�U���A�j���I��
        anime_atk.SetBool("blMiss", false);//���s�A�j���I��
        yield return new WaitForEndOfFrame();
        activeInput = true;
        Debug.Log("�N�[���^�C���I��");
    }

    //�G�������R���[�`��
    IEnumerator EnemeisMove(float x)
    {
        Debug.Log("�G����");

        Vector3 distance = new Vector3(-x, 0, 0);

        float position = 0;
        var wait = new WaitForFixedUpdate();
        while(position < 3)
        {
            yield return wait;
            tr_enemies.Translate(distance);
            position += x;
        }
    }

    IEnumerator StartCount()
    {
        Debug.Log("�͂���");
        startTxt.SetActive(true);
        se_Start.Play();
        yield return new WaitForSeconds(0.6f);
        startTxt.SetActive(false);
        activeInput = true;
    }

    IEnumerator TimeCount()
    {
        var wait = new WaitForFixedUpdate();//FixedUpdate�̓f�t�H���g��0.02�b���ƂɌĂ΂��
        float last_time = timeLimit;
        TimeDraw();
        while (-1 < timeLimit)
        {
            yield return wait;
            timeLimit -= 0.02f;
            //��b���ƂɎ��Ԃ̕\�����]�P
            if (last_time - timeLimit >= 1)
            {
                //�������ԕ\���̕ύX
                TimeDraw();
                last_time = timeLimit;
                if (timeLimit <= 5)
                {
                    //AudioSource.pitch = 1.2f;
                }
            }
        }
        StartCoroutine(GameEnd());
    }

    private void TimeDraw()
    {
        double game_time = Math.Ceiling(timeLimit);
        int seconds = (int)game_time % 60;
        Debug.Log(seconds / 100 + seconds % 100 / 10 + seconds % 10);
        //���b�i�P�O�O�̈ʁj��\��
        numSprite = Resources.Load<Sprite>($"Numbers/Number_{seconds / 100}");
        Seconds_100.GetComponent<SpriteRenderer>().sprite = numSprite;
        //���b�i�P�O�̈ʁj��\��
        numSprite = Resources.Load<Sprite>($"Numbers/Number_{seconds % 100 / 10}");
        Seconds_10.GetComponent<SpriteRenderer>().sprite = numSprite;
        //���b�i�P�̈ʁj��\��
        numSprite = Resources.Load<Sprite>($"Numbers/Number_{seconds % 10}");
        Seconds_1.GetComponent<SpriteRenderer>().sprite = numSprite;
    }

    IEnumerator GameEnd()
    {
        var wait = new WaitForSeconds(0.8f);
        if(coolTime != null) StopCoroutine(coolTime);//�N�[���^�C���R���[�`���I��
        anime_atk.SetBool("blMiss", false);//�v���C���[�A�j���I��
        activeInput = false;//���͂𖳌���
        FinishTxt.SetActive(true);
        End.Play();
        yield return new WaitForSeconds(2.5f);

        shade.SetActive(true);
        yield return wait;

        result.SetActive(true);
        Submit.Play();
        yield return wait;

        //�|�������i�P�O�̈ʁj��ύX
        numSprite = Resources.Load<Sprite>($"Numbers/Number_{killNum / 10}");
        DefeatNum_10.GetComponent<SpriteRenderer>().sprite = numSprite;
        //�|�������i�P�̈ʁj��ύX
        numSprite = Resources.Load<Sprite>($"Numbers/Number_{killNum % 10}");
        DefeatNum_1.GetComponent<SpriteRenderer>().sprite = numSprite;
        defeatWindow.SetActive(true);
        DefeatNum_10.SetActive(true);
        DefeatNum_1.SetActive(true);
        Submit.Play();

        yield return wait;

        ClearWindow.SetActive(true);
        if (killNum >= 40) Clear.SetActive(true);
        else NotClear.SetActive(true);
        Submit2.Play();

        Debug.Log($"�|�������F{killNum}");

        yield return new WaitForSeconds(1f);
        againOrEnd.SetActive(true);

        var wait2 = new WaitForEndOfFrame();
        while(Input.GetAxis("Submit") == 0 || Input.GetAxis("Jump") == 0)
        {
            yield return wait2;
            if (Input.GetAxis("Submit") == 1) SceneManager.LoadScene("Title");
            if (Input.GetAxis("Jump") == 1) SceneManager.LoadScene("Main");
        }
    }
}
