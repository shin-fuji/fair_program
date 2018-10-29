using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Customer の髪形をランダムで設定する
/// </summary>
public class CustomerHairs : MonoBehaviour {

    [SerializeField] List<GameObject> hairList; // Liam の髪の毛のリスト

	// Use this for initialization
	void Start () {
        SetHair();
	}
	
	public void SetHair()
    {
        int hairNum = hairList.Count;
        int whichHair = Random.Range(0, hairNum);

        for (int i = 0; i < hairNum; i++)
        {
            if (i == whichHair)
                hairList[i].SetActive(true);
            else
                hairList[i].SetActive(false);
        }
    }
}
