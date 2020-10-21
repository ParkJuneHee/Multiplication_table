using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{
    #region 메인오브젝트
    public GameObject mainPanel_Obj;
    public GameObject gamePanel_Obj;
    public GameObject popupPanel_Obj;        
    #endregion

    #region 게임오브젝트
    GameObject time_obj;
    GameObject answerYes_obj;
    GameObject answerNo_obj;
    GameObject questNow_obj;
    GameObject questNext_obj;
    #endregion

    #region 팝업텍스트
    Text levelselection_Level;

    #endregion

    #region 게임텍스트
    Text time_text;

    Text quest_text_0;
    Text answer_text_0;
    Text quest_text_1;
    Text quest_text_2;

    Text score_text;
    Text combo_text;
    #endregion

    int gameLv = 0;
    int answer = 0;
    int time = 0;
    int combo = 0;
    int score = 0;

    Vector3 comboScale = new Vector3(2f, 2f, 2f);
    float comboEffextNum = 0.2f;

    Image questionImg;
    Color corrext_Color_Normal = new Color(168, 168, 168, 32);
    Color corrext_Color_Answer = Color.blue;
    Color corrext_Color_WrongAnswer = Color.red;

    bool sound_State = true;

    public Sprite sound_Img_On;
    public Sprite sound_Img_Off;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        Set_Obj();
    }

    #region 오브젝트 연결
    
    void Set_Obj()
    {
        // 오브젝트 및 텍스트 연결
        mainPanel_Obj = GameObject.Find("Canvas").transform.Find("Main_Panel").gameObject;
        gamePanel_Obj = GameObject.Find("Canvas").transform.Find("Game_Panel").gameObject;
        popupPanel_Obj = GameObject.Find("Canvas").transform.Find("Popup_Panel").gameObject;

        quest_text_0 = gamePanel_Obj.transform.Find("Questions").Find("Question_0").transform.Find("Quest_Text").GetComponent<Text>();
        answer_text_0 = gamePanel_Obj.transform.Find("Questions").Find("Question_0").transform.Find("Answer_Text").GetComponent<Text>();
        quest_text_1 = gamePanel_Obj.transform.Find("Questions").Find("Question_1").transform.Find("Quest_Text").GetComponent<Text>();
        quest_text_2 = gamePanel_Obj.transform.Find("Questions").Find("Question_2").transform.Find("Quest_Text").GetComponent<Text>();

        time_text = gamePanel_Obj.transform.Find("Time").GetComponent<Text>(); 
        score_text = gamePanel_Obj.transform.Find("Score").GetComponent<Text>();
        combo_text = gamePanel_Obj.transform.Find("Combo").GetComponent<Text>();
        combo_text.gameObject.SetActive(false);

        levelselection_Level = popupPanel_Obj.transform.Find("Levelselection_Popup").Find("Level").GetComponent<Text>();

        // 시작시 메인 오브젝트를 킨다. 나머지는 끈다.
        mainPanel_Obj.SetActive(true);
        gamePanel_Obj.SetActive(false);

        //버튼 연결
        Set_ButtonMain();
        Set_ButtonGame();

        questionImg = gamePanel_Obj.transform.Find("Questions").transform.Find("Question_0").GetComponent<Image>();
    }
    #endregion


    #region 버튼이벤트

    // 메인화면 버튼 이벤트 연결
    void Set_ButtonMain()
    {
        // 게임스타트 버튼연결
        Button gameStr_but;
        gameStr_but = mainPanel_Obj.transform.Find("GameStart_Button").transform.GetComponent<Button>();
        gameStr_but.onClick.AddListener(Button_StartingGame);

        //사운드조절
        Button sound_but;
        sound_but = mainPanel_Obj.transform.Find("Shop_Panel").Find("Sound_Button").transform.GetComponent<Button>();
        sound_but.onClick.AddListener(Button_Sound);

        //랭킹
        Button ranking_but;
        ranking_but = mainPanel_Obj.transform.Find("Shop_Panel").Find("Ranking_Button").transform.GetComponent<Button>();
        ranking_but.onClick.AddListener(Button_Ranking);

        //캐쉬충전
        Button cash_but;
        cash_but = mainPanel_Obj.transform.Find("Shop_Panel").Find("Cash_Button").transform.GetComponent<Button>();
        cash_but.onClick.AddListener(Button_Cash);

        //아이템샵
        Button itemShop_but;
        itemShop_but = mainPanel_Obj.transform.Find("Shop_Panel").Find("ItemShop_Button").transform.GetComponent<Button>();
        itemShop_but.onClick.AddListener(Button_ItemShop);

        // 팝업
        // 게임난이도 조절
        Button LevelPlus_but;
        LevelPlus_but = popupPanel_Obj.transform.Find("Levelselection_Popup").Find("Plus_Button").transform.GetComponent<Button>();
        LevelPlus_but.onClick.AddListener(Button_Levelselection_Plus);

        Button LevelMinus_but;
        LevelMinus_but = popupPanel_Obj.transform.Find("Levelselection_Popup").Find("Minus_Button").transform.GetComponent<Button>();
        LevelMinus_but.onClick.AddListener(Button_Levelselection_Minus);    
    }

    // 게임화면 버튼 이벤트 연결
    void Set_ButtonGame()
    {
        // 키패드 버튼
        Button num_but;
     
        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_1").transform.GetComponent<Button>();    
        num_but.onClick.AddListener(delegate { Button_Number(1); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_2").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(2); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_3").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(3); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_4").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(4); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_5").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(5); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_6").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(6); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_7").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(7); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_8").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(8); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_9").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(9); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_0").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(0); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_back").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(-1); });

        num_but = gamePanel_Obj.transform.Find("NumberPad").Find("Number_ok").transform.GetComponent<Button>();
        num_but.onClick.AddListener(delegate { Button_Number(99); });
    }
    #endregion


    #region 메인메뉴

    // 게임 시작
    public void Button_StartingGame()
    {
        Debug.Log("게임시작 테스트");
        mainPanel_Obj.SetActive(false);
        gamePanel_Obj.SetActive(true);

        StartGame();
    }
    //사운드 조절
    public void Button_Sound()
    {
       

        if (sound_State)
        {
            Debug.Log("사운드 설정 테스트 True");

            sound_State = false;
            mainPanel_Obj.transform.Find("Shop_Panel").Find("Sound_Button").GetComponent<Image>().sprite = sound_Img_Off;
            AudioListener.pause = false;
        }
        else
        {
            Debug.Log("사운드 설정 테스트 False");

            sound_State = true;
            mainPanel_Obj.transform.Find("Shop_Panel").Find("Sound_Button").GetComponent<Image>().sprite = sound_Img_On;
            AudioListener.pause = true;
        }

    }

    // 랭킹 열기
    public void Button_Ranking()
    {
        Debug.Log("랭킹보기 테스트");
    }

    // 캐쉬충전
    public void Button_Cash()
    {
        OpenPopup("Cash");
           
        Debug.Log("캐쉬샵 테스트");
    }

    // 아이템샵
    public void Button_ItemShop()
    {
        OpenPopup("Item");

        Debug.Log("아이템샵 테스트");
    }

    // 안드로이드 회사 어플 목록 열기
    public void Button_MoreGames()
    {
        Application.OpenURL("market:/search?q=블리자드&c=apps");
    }

    // 게임종료
    public void Button_ExitGame()
    {
        // 광고후 게임종료를 하자
        Application.Quit();
    }
    #endregion

    #region 팝업메뉴

    // 게임난이도 조절
    public void Button_Levelselection_Plus() 
    {
        if(gameLv == 0)
        {
            gameLv++;
            levelselection_Level.text = "?? X ??";
        }
        else if(gameLv == 1)
        {
            gameLv++;
            levelselection_Level.text = "??? X ???";
        }
        else if (gameLv == 2)
        {
            gameLv++;
            levelselection_Level.text = "???? X ????";
        }

        // else if (gameLv == 3)
        // {
        //    gameLv++;
        //    levelselection_Level.text = "????? X ?????";
        // }
    }

    public void Button_Levelselection_Minus()
    {
        if (gameLv == 0)
        {   
            levelselection_Level.text = "? X ?";
        }
        else if (gameLv == 1)
        {
            gameLv--;
            levelselection_Level.text = "? X ?";
        }
        else if (gameLv == 2)
        {
            gameLv--;
            levelselection_Level.text = "?? X ??";
        }
        else if (gameLv == 3)
        {
            gameLv--;
            levelselection_Level.text = "??? X ???";
        }
    }

    #endregion

    void OpenPopup(string Type)
    {
        //메인판넬을 활성화 시킨다.
        popupPanel_Obj.SetActive(true);

        //하위 팝업  오브젝트들을 끈다.
        popupPanel_Obj.transform.Find("Levelselection_Popup").gameObject.SetActive(false);
        popupPanel_Obj.transform.Find("GameOver_Popup").gameObject.SetActive(false);
        popupPanel_Obj.transform.Find("Cash_Popup").gameObject.SetActive(false);
        popupPanel_Obj.transform.Find("ItemShop_Popup").gameObject.SetActive(false);

        if (Type == "Cash")
        {

        }
        else if(Type == "Item")
        {

        }
        else if(Type == "GameSelection")
        {
            popupPanel_Obj.transform.Find("Levelselection_Popup").gameObject.SetActive(true);
        }
    }


    #region 게임화면
    
    // 게임시작 카운트다운
    void StartGame()
    {
        Set_Questions();
        
        time = 60;
        time_text.text = "Time : " + time;

        StartCoroutine(testCoroutine());
    }

    // 문제 셋팅
    void Set_Questions()
    {
        answer_text_0.text = "???";

        Questions(gameLv);
    }

    // 초 카운트 시작
    IEnumerator testCoroutine() 
    {
        while (true) {
            Debug.Log("Do something!");

            yield return new WaitForSeconds(1f);

            time--;
            time_text.text = "Time : " + time;
        
            if(time <= 0)
            {
                // 게임오버
                GameOver();
                break;
            }
        }

        yield return null;
    }
    
    //게임오버
    void GameOver()
    {

    }

    // 정답 체크
    void CorrectAnswer(bool b_answer = true)
    {
        if (b_answer) // 정답
        {
            StartCoroutine(Combo_Effext());
            StartCoroutine(Corrext_Effext(true));

            score += 10 * combo;
            time += 1 * combo;

            score_text.text = "Score : " + score.ToString();
            time_text.text = "Time : " + time;
        }

        else // 오답
        {
            StartCoroutine(Corrext_Effext(false));
        }
    }

    IEnumerator Corrext_Effext(bool answer)
    {
        if(answer)
            questionImg.color = corrext_Color_Answer;
        else
            questionImg.color = corrext_Color_WrongAnswer;

        yield return new WaitForSeconds(1f);

        questionImg.color = corrext_Color_Normal;
    }


    IEnumerator Combo_Effext()
    {
        combo_text.text = "COMBO " + combo;
        combo_text.gameObject.SetActive(true);        
        //combo_text.gameObject.transform.localScale = comboScale;

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            //combo_text.gameObject.transform.localScale = new Vector3(combo_text.gameObject.transform.localScale.x - comboEffextNum, combo_text.gameObject.transform.localScale.y - comboEffextNum, combo_text.gameObject.transform.localScale.z - comboEffextNum);            
            if(combo_text.gameObject.transform.localScale.x <= 1)
            {
                //combo_text.gameObject.transform.localScale = Vector3.one;
                //combo_text.gameObject.SetActive(false);
                break;
            }
        }

        yield return null;
    }

    // 숫자 패드 입력
    public void Button_Number(int Num = 0)
    {
        if (answer_text_0.text == "???" && Num != -1)
        {
            answer_text_0.text = "";
        }
        switch (Num)
        {
            case 1: // 1번키                              
                answer_text_0.text = answer_text_0.text + "1";
                break;
            case 2: // 2번키
                answer_text_0.text = answer_text_0.text + "2";
                break;
            case 3: // 3번키
                answer_text_0.text = answer_text_0.text + "3";
                break;
            case 4: // 4번키
                answer_text_0.text = answer_text_0.text + "4";
                break;
            case 5: // 5번키
                answer_text_0.text = answer_text_0.text + "5";
                break;
            case 6: // 6번키
                answer_text_0.text = answer_text_0.text + "6";
                break;
            case 7: // 7번키
                answer_text_0.text = answer_text_0.text + "7";
                break;
            case 8: // 8번키
                answer_text_0.text = answer_text_0.text + "8";
                break;
            case 9: // 9번키
                answer_text_0.text = answer_text_0.text + "9";
                break;
            case 0: // 0번키
                answer_text_0.text = answer_text_0.text + "0";
                break;
            case -1: // 삭제
                int textLength = answer_text_0.text.Length;
                if (answer_text_0.text == "???")
                {
                    break;
                }
                else
                {               
                answer_text_0.text = answer_text_0.text.Substring(0, textLength - 1);

                Debug.Log("글자수 : " + textLength);
                }

                if (textLength <= 1)
                    answer_text_0.text = "???";
                break;

            case 99: // 입력
                if(answer_text_0.text == answer.ToString())
                {
                    Debug.Log("정답 : " + answer.ToString());
                    
                    combo++;
                    Questions(gameLv);
                    CorrectAnswer();
                }
                else
                {
                    Debug.Log("오답 : " + answer.ToString());
                    combo = 0;
                    Questions(gameLv);
                    CorrectAnswer(false);
                }
                break;
        }
    }
    // 문제 출제

    void Questions(int Lv = 0)
    {
        answer_text_0.text = "???";

        int Quest1 = 0;
        int Quest2 = 0;

        if (Lv == 0)
        {
            Quest1 = Random.Range(1, 9);
            Quest2 = Random.Range(1, 9);

            answer = Quest1 * Quest2;
        }

        else if(Lv == 1)
        {
            Quest1 = Random.Range(1, 99);
            Quest2 = Random.Range(1, 99);

            answer = Quest1 * Quest2;
        }

        else if (Lv == 2)
        {
            Quest1 = Random.Range(1, 999);
            Quest2 = Random.Range(1, 999);

            answer = Quest1 * Quest2;
        }

        else if (Lv == 3)
        {
            Quest1 = Random.Range(1, 9999);
            Quest2 = Random.Range(1, 9999);

            answer = Quest1 * Quest2;
        }

        else if (Lv == 4)
        {
            Quest1 = Random.Range(1, 99999);
            Quest2 = Random.Range(1, 99999);

            answer = Quest1 * Quest2;
        }

        quest_text_0.text = Quest1.ToString() + " x " + Quest2.ToString() + " = ";
    } 

    // 게임종료


    #endregion

}
