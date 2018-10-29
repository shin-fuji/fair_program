using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRTK.Examples;
using VRTK;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class TextController : MonoBehaviour
{
    const float TEXT_SPEED = 0.5F;
    const float TEXT_SPEED_STRING = 0.05F;
    const float COMPLETE_LINE_DELAY = 0.3F;
    const float DIALOG_BUTTON_SET_INACTIVE_DELAY = 0.3F;

    [SerializeField] Text lineText;     // 文字表示Text
    string[] scenarios;    // 会話内容
    [SerializeField] List<int> scenariosDialogOccurNumList; // 会話内容の何番目の文章で分岐するかを示す数字の配列
    [SerializeField] List<int> scenariosKeyboard; // 会話内容の何番目の文章でキーボードを表示するかを示す数字の配列
    [SerializeField] int scenariosLineNum;      // 総シナリオ行数     
    int currentDialogNum = 0;                   // 何回目の分岐か
    int currentKeyboard = 0;                    // 何回目のキーボードか

    float textSpeed = 0;                    // 表示速度
    float completeLineDelay = COMPLETE_LINE_DELAY;  // 表示し終えた後の待ち時間
    int currentLine = 0;                    // 表示している行数
    string currentText = string.Empty;      // 表示している文字
    bool isCompleteLine = false;            // １文が全部表示されたか？
    
    [SerializeField] GameObject dialogButton;   // ダイアローグボタン
    [SerializeField] Text buttonTextYES;
    [SerializeField] Text buttonTextNO;
    bool whichChoisesClicked;               // 「はい」「いいえ」どちらのダイアローグボタンを押したか
    bool isScenariosDialog = false;         // 会話が分岐中か(現在，scenariosYES/NOを表示しているか)

    [SerializeField] GameObject worldKeyboard;  // 名前入力用のキーボード
    UI_Keyboard keyboard;
    string playerName = "";
    bool isDisplayKeyboard = false;         // キーボードを表示するか否か
    bool isPlayerNameInputted = false;
    bool isScenariosSorBChoised = false;

    // コントローラ
    [SerializeField] GameObject leftController;
    [SerializeField] GameObject rightController;
    SteamVR_TrackedObject trackedObj_Left;
    SteamVR_TrackedObject trackedObj_Right;
    SteamVR_Controller.Device deviceL;
    SteamVR_Controller.Device deviceR;

    public bool IsCompleteLine
    {
        get { return isCompleteLine; }
    }
    public string PlayerName
    {
        set { playerName = value; }
    }
    public bool IsDisplayKeyboard
    {
        set { isDisplayKeyboard = value; }
    }

    private void Awake()
    {
        trackedObj_Left = leftController.GetComponent<SteamVR_TrackedObject>();
        trackedObj_Right = rightController.GetComponent<SteamVR_TrackedObject>();

        keyboard = worldKeyboard.GetComponent<UI_Keyboard>();
    }

    void Start()
    {
        dialogButton.SetActive(false);
        worldKeyboard.SetActive(false);

        scenarios = new string[scenariosLineNum];
        ContentOfScenarios(ref scenarios);

        Show();
    }
    

    /// <summary>
    /// 会話シーン表示
    /// </summary>
    void Show()
    {
        Initialize();
        StartCoroutine(ScenarioCoroutine());
    }

    /// <summary>
    /// 初期化
    /// 
    /// ここでダイアローグボタン・キーボード表示の処理も行う
    /// </summary>
    void Initialize()
    {
        isCompleteLine = false;
        lineText.text = "";

        currentText = scenarios[currentLine];

        textSpeed = TEXT_SPEED + (currentText.Length * TEXT_SPEED_STRING);

        // 文字列を少しずつ表示する処理を行う
        LineUpdate();


        // 会話分岐時なら
        if(currentDialogNum < scenariosDialogOccurNumList.Count && currentLine == scenariosDialogOccurNumList[currentDialogNum])
        {
            // ダイアローグボタンを表示する
            dialogButton.SetActive(true);
        }
        // 名前入力時なら
        if (currentKeyboard < scenariosKeyboard.Count && currentLine == scenariosKeyboard[currentKeyboard])
        {
            // キーボードを表示する
            worldKeyboard.SetActive(true);
            currentKeyboard++;
        }
    }

    /// <summary>
    /// 初期化(ダイアローグボタン選択後の文章)
    /// </summary>
    void InitializeDialog()
    {
        isCompleteLine = false;
        lineText.text = "";


        
        currentText = scenarios[currentLine];

        textSpeed = TEXT_SPEED + (currentText.Length * TEXT_SPEED_STRING);

        // 文字列を少しずつ表示する処理を行う
        LineUpdateDialog();
    }

    /// <summary>
    /// 会話シーン更新
    /// </summary>
    /// <returns>The coroutine.</returns>
    IEnumerator ScenarioCoroutine()
    {
        while (true)
        {
            
            yield return null;

            //Debug.Log("次のscenarios：" + scenarios[currentLine]);

            // ダイアローグボタン表示中なら，トリガーのみ入力を受け付けず，
            // ボタンへのポインタ＆トリガー入力のみ受け付ける
            if (dialogButton.activeSelf || worldKeyboard.activeSelf)
            {
                continue;
            }
            if(!isPlayerNameInputted && playerName != "")
            {
                ContentOfScenariosPlayerName(ref scenarios, ref playerName);
                isPlayerNameInputted = true;
            }


            Debug.Log("playerName = " + playerName);

            // コントローラの状態を取得
            deviceL = SteamVR_Controller.Input((int)trackedObj_Left.index);
            deviceR = SteamVR_Controller.Input((int)trackedObj_Right.index);

            // 次の内容へ
            if (isCompleteLine && 
                (deviceL.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) || 
                deviceR.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)))
            {
                yield return new WaitForSeconds(completeLineDelay);

                if (currentLine > scenarios.Length - 1)
                {
                    ScenarioEnd();
                    yield break;
                }

                if (!isScenariosDialog)
                    Initialize();
                else
                    InitializeDialog();
            }

            // 表示中にボタンが押されたら全部表示
            else if (!isCompleteLine &&
                (deviceL.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) ||
                deviceR.GetPressDown(SteamVR_Controller.ButtonMask.Trigger)))
            {
                iTween.Stop();
                TextUpdate(currentText.Length); // 全部表示

                if (!isScenariosDialog)
                    TextEnd();
                else
                    TextDialogEnd();
                yield return new WaitForSeconds(completeLineDelay);
            }
        }
    }

    /// <summary>
    /// 文字を少しずつ表示
    /// </summary>
    void LineUpdate()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 0,
            "to", currentText.Length,
            "time", textSpeed,
            "onupdate", "TextUpdate",
            "oncompletetarget", this.gameObject,
            "oncomplete", "TextEnd"
        ));
    }
    /// <summary>
    /// 文字を少しずつ表示(ダイアローグボタン選択後の文章)
    /// </summary>
    void LineUpdateDialog()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 0,
            "to", currentText.Length,
            "time", textSpeed,
            "onupdate", "TextUpdate",
            "oncompletetarget", this.gameObject,
            "oncomplete", "TextDialogEnd"
        ));
    }

    /// <summary>
    /// 表示文字更新
    /// </summary>
    /// <param name="lineCount">Line count.</param>
    void TextUpdate(int lineCount)
    {
        lineText.text = currentText.Substring(0, lineCount);
    }

    /// <summary>
    /// 表示完了
    /// </summary>
    void TextEnd()
    {
        Debug.Log((currentLine + 1) + "文目：表示完了");
        Debug.Log("scenarios.Length = " + scenarios.Length);
        isCompleteLine = true;
        currentLine++;
    }

    /// <summary>
    /// 表示完了(ダイアローグボタン選択後の文章)
    /// </summary>
    void TextDialogEnd()
    {
        Debug.Log((currentLine + 1) + "文目(分岐)：表示完了(ダイアローグボタン選択後の文章)");
        Debug.Log("scenarios.Length = " + scenarios.Length);
        isCompleteLine = true;
        isScenariosDialog = false;
        currentLine++;
    }



    /// <summary>
    /// 会話終了
    /// </summary>
    void ScenarioEnd()
    {
        Debug.Log("会話終了");
    }

    /// <summary>
    /// YES(NO)ボタンが押された時の処理
    /// </summary>
    public void OnClick_YES()
    {
        whichChoisesClicked = true;
        isScenariosDialog = true;

        if (currentDialogNum == 0)
        {
            ContentOfScenariosShopping(ref scenarios, ref playerName);
            // ダイアローグボタンのメッセージを「買い物シナリオ」に更新する
            buttonTextYES.text = "ご飯系を食べたいなー";
            buttonTextNO.text = "スイーツ系がいいね！";

            isScenariosSorBChoised = true;
        }
        else if (currentDialogNum == 1)
        {
            if(isScenariosSorBChoised)
            {
                ContentOfScenariosShopping_Gohan(ref scenarios, ref playerName);
            }
            else
            {
                ContentOfScenariosShopping_Familiar(ref scenarios, ref playerName);
            }
        }

        currentDialogNum++;
        
        dialogButton.SetActive(false);
    }
    public void OnClick_NO()
    {
        whichChoisesClicked = false;
        isScenariosDialog = true;

        if (currentDialogNum == 0)
        {
            ContentOfScenariosButai(ref scenarios, ref playerName);
            buttonTextYES.text = "何でも聞いて！";
            buttonTextNO.text = "そこまでかなー？";

            isScenariosSorBChoised = false;
        }
        else if (currentDialogNum == 1)
        {
            if (isScenariosSorBChoised)
            {
                ContentOfScenariosShopping_Sweets(ref scenarios, ref playerName);
            }
            else
            {
                ContentOfScenariosShopping_notFamiliar(ref scenarios, ref playerName);
            }
        }

        currentDialogNum++;

        dialogButton.SetActive(false);
    }
    






    /// <summary>
    /// 会話内容をここで登録
    /// </summary>
    /// <param name="scenarios"></param>
    /// <param name="scenariosYES"></param>
    /// <param name="scenariosNO"></param>
    void ContentOfScenarios(ref string[] scenarios)
    {
        scenarios[0] = "おーい！　こっちこっちー．";
        scenarios[1] = "こんばんは！　僕はジョージっていうんだ．よろしくね！";
        scenarios[2] = "あなたのお名前を　教えてもらっていいかな？(左側のキーボードで入力)"; // キーボード表示，名前入力

    }
    void ContentOfScenariosPlayerName(ref string[] scenarios, ref string playerName)
    {
        scenarios[3] = "へぇー，" + playerName + "さんかぁ・・・　いい名前だね！";
        scenarios[4] = playerName + "さんも　お祭りを楽しみに来たんでしょう？　今日は晴れてて，絶好のお祭り日和だねー．";

        // 分岐１
        scenarios[5] = "そういえば，" + playerName + "さんは　今日のお祭りで「特にこれが楽しみ！」ってもの　ある？";

        scenarios[6] = "このメッセージが表示されるのはおかしいよ";
    }

    void ContentOfScenariosShopping(ref string[] scenarios, ref string playerName)
    {
        scenarios[6] = "おー，そうなんだ！ここの縁日は　食べ物もアトラクションも　たくさん屋台があるから，色々見て回るといいよ！";
        scenarios[7] = "特に食べ物を買うんだったら，ここのお祭りは　まさにうってつけの場所だね！";
        scenarios[8] = "ご飯系なら　焼きそばやたこ焼き，スイーツ系なら　かき氷やりんご飴・・・";
        scenarios[9] = "お祭りの定番メニューなら　一通りそろってるみたいだね．";

        // 分岐２
        scenarios[10] = playerName + "さんは，どんな食べ物を買うつもりなの？";

    }
    void ContentOfScenariosButai(ref string[] scenarios, ref string playerName)
    {
        scenarios[6] = "なるほどー．今日は　３人組のロボットが　舞台でダンスを披露してるらしいよ！　どんなダンスなんだろう・・・";
        scenarios[7] = "グループの名前は，えーっと・・・そうそう！　「Verfume」！";
        scenarios[8] = "最近デビューしたばっかりの　３人組アイドルユニットらしいよ．";
        scenarios[9] = "アイドルグループって，50人くらいで　一斉に歌って踊るのもいるし，いろいろあるよね．";

        // 分岐２
        scenarios[10] = "そういえば，" + playerName + "さんは，そういうアイドルとか　詳しい方？";

    }

    void ContentOfScenariosShopping_Gohan(ref string[] scenarios, ref string playerName)
    {
        scenarios[11] = "いいね！　ちょうどご飯時だし，お腹もすいてくる頃だもんね．";
        scenarios[12] = "そんなことを話してたら　僕もお腹がすいてきちゃった・・・何食べようかな？";
        scenarios[13] = "たこ焼きもいいし・・・あ！　海鮮焼きとかも捨てがたいなー！";
        scenarios[14] = "・・・はやくお祭りを回りたい！って顔してるね．じゃあそろそろ行こうか！";
    }
    void ContentOfScenariosShopping_Sweets(ref string[] scenarios, ref string playerName)
    {
        scenarios[11] = "おお！　スイーツかー！　甘いもの，いいよねー．";
        scenarios[12] = "僕も　甘いもの大好きなんだよね．今日は何を買おうかな？";
        scenarios[13] = "僕は　りんご飴が　大好きだから，お祭りに来ると　絶対買ってるんだ！もちろん　今日も買うつもりだよ．";
        scenarios[14] = "ん？　はやくお祭りを回りたくて　うずうずしてる？　じゃあそろそろ行こう！";
    }

    void ContentOfScenariosShopping_Familiar(ref string[] scenarios, ref string playerName)
    {
        scenarios[11] = "おおすごい！　テレビとかも　結構見てるのかな？";
        scenarios[12] = "僕は　あんまり詳しくなくてね・・・好きなやつばっかり観てるって感じかな？";
        scenarios[13] = "もし　オススメのアイドルがいたら　ぜひ教えてよ！　僕も　そういうの　結構興味あるからさ！";
        scenarios[14] = "おっと，もうこんな時間．じゃあそろそろ行こう！";
    }
    void ContentOfScenariosShopping_notFamiliar(ref string[] scenarios, ref string playerName)
    {
        scenarios[11] = "そうなんだね．　たくさんいすぎて　分からないよねー．";
        scenarios[12] = "僕も　好きなアイドルは　結構観るんだけど，あんまり他に手を出さないというか・・・";
        scenarios[13] = "何か　オススメのアイドルがいれば，ぜひ教えてほしいな．僕も　オススメがあれば　教えるよ！";
        scenarios[14] = "・・・ついついしゃべりすぎちゃった．そろそろ行こうか！";
    }


}