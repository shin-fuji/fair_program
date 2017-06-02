using UnityEngine;
using System.Collections;

public class InterSecCollidor : MonoBehaviour
{

    // アニメーション
    Animator animeData;

    [SerializeField] GameObject customer;

    /// <summary>
    /// 客が現在どこにいるかを示す変数
    /// 0 : dl (下・左に通路があるセクション)
    /// 1 : dlr(下・左・右に　　　〃　　　)
    /// 2 : udl(上・下・左に　　　〃　　　)
    /// 3 : udr(上・下・右に　　　〃　　　)
    /// 4 : ul (上・左に　　　　　〃　　　)
    /// 5 : ur (上・右に　　　　　〃　　　)
    /// 6 : ulr(上・左・右に　　　〃　　　)
    /// 7 : CashRegister(レジ前のセクション)
    /// 8 : Return(レジを出た後のセクション)
    /// 9 : Exit(出口セクション)
    /// 10: Entrance(入口セクション)
    /// 上以外: 商品棚通路など、その他のセクション
    /// </summary>
    int mode = 10;

    const int DL = 0, DLR = 1, UDL = 2, UDR = 3, UL = 4, UR = 5, ULR = 6,
        CASHREGISTER = 7, RETURN = 8, EXIT = 9, ENTRANCE = 10;

    // Use this for initialization
    void Start()
    {
        animeData = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        /*
        switch (mode)
        {
            case DL:
                dlMode();
                break;
            case DLR:
                dlrMode();
                break;
            case UDL:
                udlMode();
                break;
            case UDR:
                udrMode();
                break;
            case UL:
                ulMode();
                break;
            case UR:
                urMode();
                break;
            case ULR:
                ulrMode();
                break;
            case CASHREGISTER:
                CashRegisterMode();
                break;
            case RETURN:
                ReturnMode();
                break;
            case EXIT:
                ExitMode();
                break;
            case ENTRANCE:
                EntranceMode();
                break;
            default:
                OthersMode();
                break;
        }
        */


    }

    /// <summary>
    /// 各セクションにおける動作
    /// </summary>
    void dlMode()
    {
        animeData.SetBool("dl", true);
    }

    void dlrMode()
    {
        animeData.SetBool("dlr", true);
    }

    void udlMode()
    {
        animeData.SetBool("udl", true);
    }

    void udrMode()
    {
        animeData.SetBool("udr", true);
    }

    void ulMode()
    {
        animeData.SetBool("ul", true);
    }

    void urMode()
    {
        animeData.SetBool("ur", true);
    }

    void ulrMode()
    {
        animeData.SetBool("ulr", true);
    }

    void CashRegisterMode()
    {
        animeData.SetBool("CashRegister", true);
    }

    void ReturnMode()
    {
        animeData.SetBool("Return", true);
    }

    void ExitMode()
    {
        animeData.SetBool("Exit", true);
    }

    void EntranceMode()
    {
        animeData.SetBool("Entrance", true);
    }

    void OthersMode()
    {
        animeData.SetBool("Others", true);
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "InterSec_dl")
        {
            dlMode();
        }
        else if (collider.gameObject.tag == "InterSec_dlr")
        {
            dlrMode();
        }
        else if (collider.gameObject.tag == "InterSec_udl")
        {
            udlMode();
        }
        else if (collider.gameObject.tag == "InterSec_udr")
        {
            udrMode();
        }
        else if (collider.gameObject.tag == "InterSec_ul")
        {
            ulMode();
        }
        else if (collider.gameObject.tag == "InterSec_ur")
        {
            urMode();
        }
        else if (collider.gameObject.tag == "InterSec_ulr")
        {
            ulrMode();
        }
        else if (collider.gameObject.tag == "InterSec_CashRegister")
        {
            CashRegisterMode();
        }
        else if (collider.gameObject.tag == "InterSec_Return")
        {
            ReturnMode();
        }
        else if (collider.gameObject.tag == "InterSec_Exit")
        {
            ExitMode();
        }
        else if (collider.gameObject.tag == "InterSec_Entrance")
        {
            EntranceMode();
        }
    }
}
