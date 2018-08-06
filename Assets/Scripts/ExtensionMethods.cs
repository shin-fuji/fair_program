using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods{


    /// <summary>
    /// リストの中身を表示する関数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">中身を表示したいリスト</param>
    public static void ShowListContentsInTheDebugLog<T>(this List<T> self)

    {
        string log = "";

        foreach (var content in self.Select((val, idx) => new { val, idx }))
        {
            if (content.idx == self.Count - 1)
                log += content.val.ToString();
            else
                log += content.val.ToString() + " ";
        }

        Debug.Log(log);
    }
}
