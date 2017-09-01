using UnityEngine;
using System.Collections;
using System.IO; //System.IO.FileInfo, System.IO.StreamReader, System.IO.StreamWriter
using System; //Exception
using System.Text; //Encoding
using System.Collections.Generic;

// ファイル読み込み
public class BehaviourScriptReader : MonoBehaviour
{
    private string behavLine = "";
    public List<string> behavLineList = new List<string>();
    char[] SPLIT = { '\n' };

    // 行動記号列を読み込むか
    public static bool readFileOrNot = false;

    // 一番最初に呼び出される
    void Awake()
    {
        Debug.Log("readFileOrNot = " + readFileOrNot);
        if(readFileOrNot == true) ReadFile();
    }


    /// <summary>
    /// 読み込み関数
    /// 
    /// <<実験2>>
    /// 以下の4つのファイルを読み込んで動画を作る
    /// /NoHerdBehav_1.txt
    /// /NoHerdBehav_2.txt
    /// /HerdBehav_1.txt
    /// /HerdBehav_2.txt
    /// </summary>
    void ReadFile()
    {

        // FileReadTest.txtファイルを読み込む
        FileInfo fi = new FileInfo(Application.dataPath + "/HerdBehav_test.txt");
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
