using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using MyCommonConst;

public static class ExtensionMethods
{


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

    /// <summary>
    /// 客が読み込んだ行動記号列を読み，その客がどのグループに属しているかを判定する関数
    /// グループは「0：屋台で買い物」「1：舞台でダンス鑑賞」「2：通りがかり」の3つ
    /// </summary>
    /// <param name="behavLine">客が読み込んだ行動記号列</param>
    /// <returns>客が所属するグループ番号</returns>
    public static int WhichCustomerGroup(this string behavLine)
    {
        List<string> behavList = new List<string>();

        behavList.AddRange(behavLine.Split(','));

        int shopping = behavList.Count(x => x == "4" || x == "5" || x == "6");
        int butai = behavList.Count(x => x == "7" || x == "8" || x == "9");


        if (shopping == 0 && butai == 0) return MyConst.GROUP_PASSERBY;
        else if (shopping >= butai) return MyConst.GROUP_SHOPPING;
        else return MyConst.GROUP_BUTAI;
    }
}
