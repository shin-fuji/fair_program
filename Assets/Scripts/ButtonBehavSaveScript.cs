﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Text; //Encoding

/// <summary>
/// ポーズ画面の「保存して終了」ボタンを押すと
/// それまで行動記号列を書き込んでいたファイルを閉じて
/// ゲームを終了する
/// </summary>
public class ButtonBehavSaveScript : MonoBehaviour
{
    [SerializeField]
    private PlayerBehaviorText_VIVE pbt_VIVE;


    void Start()
    {
        //playerBehavTextObj = GameObject.Find("Player");
    }


    public void ButtonPush()
    {
        // 別のオブジェクト(Player)のスクリプトを参照
        //PlayerBehaviorText_Fair2Ver playerBehavText = playerBehavTextObj.GetComponent<PlayerBehaviorText_Fair2Ver>();
        //PlayerBehaviorText_VIVE playerBehavText = playerBehavTextObj.GetComponent<PlayerBehaviorText_VIVE>();
        
        // すべての行動を行ったら退場させる
        // 
        if (Random.Range(0f, 1f) >= 0.5f)
            pbt_VIVE.playerBehavList.Add("P");
        else
            pbt_VIVE.playerBehavList.Add("Q");

        SaveBehav(pbt_VIVE.playerBehavList);

        WalkerCreator.walkerList.Clear();
        Pausable.pauseGame = false;


        // タイトル画面へ
        SceneManager.LoadScene("title");
    }

    public void NotSaveButtonSave()
    {

        WalkerCreator.walkerList.Clear();
        Pausable.pauseGame = false;

        // タイトル画面へ
        SceneManager.LoadScene("title");
    }

    
    // テキストファイルとして行動記号列をセーブ
    public void SaveBehav(List<string> playerBehavList)
    {
        // ファイル保存用
        //FileInfo fi = new FileInfo(Application.dataPath + @"/arrayTxtFile/behavior_array.txt");
        FileInfo fi = new FileInfo(Application.dataPath + "/behavior_array.txt");
        StreamWriter sw = fi.AppendText();

        //for(int i = 0; i < playerBehavList.Count; i++)
        //{
        //    Debug.Log("Loop1");
        //    sw.Write(playerBehavList[i]);
        //}
        

        foreach (string behav in playerBehavList)
        {
            sw.Write(behav);
        }
        sw.WriteLine("");


        // ファイルを閉じる
        sw.Flush();
        sw.Close();
    }
}
