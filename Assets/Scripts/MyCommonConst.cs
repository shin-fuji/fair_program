using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCommonConst {

    public static class MyConst
    {
        /// <summary>
        /// 客が現在どこにいるかなどを示す定数
        /// 0 : わたあめ屋(WAT)
        /// 1 : たこ焼き屋(TA)
        /// 2 : 射的(SHA)
        /// 3 : 焼きそば屋(YA)
        /// 4 : 海鮮焼き屋(KAI)
        /// 5 : 輪投げ(WAN)
        /// 6 : ヨーヨー釣り(YO)
        /// 7 : 金魚すくい(KI)
        /// 8 : かき氷屋(KAK)
        /// 9 : リンゴ飴屋(RI)
        /// 10: ファンタジー屋台1(FAN_1)
        /// 11: ファンタジー屋台2(FAN_2)
        /// 12: 曲がり角(COR)
        /// 13: 舞台前(STA_F)
        /// 14: 舞台横(STA_S)
        /// 15: 右側出口(RI_EXIT)
        /// 16: 下側出口(LO_EXIT)
        /// </summary>
        public const int
            WAT = 0, TA = 1, SHA = 2, YA = 3, KAI = 4,
            WAN = 5, YO = 6, KI = 7, KAK = 8, RI = 9,
            FAN_1 = 10, FAN_2 = 11,
            COR = 12, STA_F = 13, STA_S = 14,
            RI_EXIT = 15, LO_EXIT = 16;
    }


}
