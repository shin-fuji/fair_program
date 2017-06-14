using UnityEngine;
using System.Collections;
using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text; //Encoding
using System.Collections.Generic;

public class BehaviorScriptReader_jikkenn2 : MonoBehaviour {

    private string behavLine = "";
    public List<string> behavLineList = new List<string>();
    char[] SPLIT = { '\n' };

    // 行動記号列を読み込むか
    bool readFileOrNot = true;

    // 一番最初に呼び出される
    void Awake()
    {
        Debug.Log("readFileOrNot(jikkenn2) = " + readFileOrNot);
        ReadFile();
    }

    // 読み込み関数
    void ReadFile()
    {

        // FileReadTest.txtファイルを読み込む
        FileInfo fi = new FileInfo(Application.dataPath + "/behavior_array.txt");
        try
        {
            // 一行毎読み込み
            using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
            {
                behavLine = sr.ReadToEnd();
                behavLineList.AddRange(behavLine.Split(SPLIT, System.StringSplitOptions.RemoveEmptyEntries));

                //Debug.Log("行動記号列ファイル・行数：" + behavLineList.Count);
            }
        }
        catch (Exception e)
        {
            // 改行コード
            //behavLine += SetDefaultText();
        }


    }

    // 改行コード処理
    string SetDefaultText()
    {
        return "C#あ\n";
    }
}
