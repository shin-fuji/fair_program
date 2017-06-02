using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Neuron;

/// <summary>
/// Config画面でアドレスを入力・変更するためのスクリプト
/// 参考：http://tech.pjin.jp/blog/2016/09/27/unity_skill_4/
/// </summary>
public class InputAdrManager : MonoBehaviour
{

    InputField inputField;


    /// <summary>
    /// Startメソッド
    /// InputFieldコンポーネントの取得および初期化メソッドの実行
    /// </summary>
    void Start()
    {

        inputField = GetComponent<InputField>();

        InitInputField();
    }
    
    /// <summary>
    /// Log出力用メソッド
    /// 入力値を取得してLogに出力し、初期化
    /// </summary>
    public void InputAdr()
    {

        string inputValue = inputField.text;

        //Debug.Log(inputValue);

        if (inputValue == "") return;

        NeuronConnection.changedAdr = true;
        NeuronConnection.newAddress = inputValue;

        //InitInputField();
    }

    
    /// <summary>
    /// InputFieldの初期化用メソッド
    /// 入力値をリセットして、フィールドにフォーカスする
    /// </summary>
    void InitInputField()
    {

        // 値をリセット
        inputField.text = "";

        // フォーカス
        inputField.ActivateInputField();
    }

}
