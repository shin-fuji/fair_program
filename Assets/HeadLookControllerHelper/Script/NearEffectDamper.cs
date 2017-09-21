using UnityEngine;
using System.Collections;

namespace Mebiustos.HeadLookControllerHelper {
    public class NearEffectDamper : MonoBehaviour {
        public float thresholdMin = 0.2f;
        public float thresholdRange = 0.3f;

        //[Header("--- Debug Info")]
        //[SerializeField]
        float totalRange;
        //[SerializeField]
        float distance;
        //[SerializeField]
        float effect;

        HeadLookController hlc;
        Transform rootNodeHead;

        float totalTAD;

        void Start() {
            hlc = GetComponent<HeadLookController>();
            totalRange = thresholdMin + thresholdRange;
            totalTAD = hlc.segments[0].thresholdAngleDifference;

            rootNodeHead = hlc.rootNode.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).transform;
        }

        void Update() {
            distance = Vector3.Distance(hlc.target, rootNodeHead.position);
            if (distance < totalRange) {
                if (distance <= thresholdMin) {
                    effect = 0;
                } else {
                    effect = (distance - thresholdMin) / thresholdRange;
                }
            } else {
                effect = 1;
            }

            hlc.segments[0].thresholdAngleDifference = totalTAD + 180 - (180 * effect);
        }
    }
}