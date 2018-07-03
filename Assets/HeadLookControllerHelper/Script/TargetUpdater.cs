using UnityEngine;
using System.Collections;

namespace Mebiustos.HeadLookControllerHelper {
    public class TargetUpdater : MonoBehaviour {
        public Transform lookAtTargetObject;
        public float targetVelocityAdjustment;

        [System.NonSerialized]
        public HeadLookController hlc;

        void FixedUpdate() {
            if (lookAtTargetObject != null) {
                hlc.target = lookAtTargetObject.position + new Vector3(0f, targetVelocityAdjustment, 0f);
            }
        }
    }
}
