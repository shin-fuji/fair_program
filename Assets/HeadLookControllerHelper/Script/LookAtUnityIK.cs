using UnityEngine;
using System.Collections;

namespace Mebiustos.HeadLookControllerHelper {
    public class LookAtUnityIK : MonoBehaviour {
        public Transform target;

        Animator animator;

        public float totalWeight;
        public float eyeWeight;
        public float clampWeight;

        void Start() {
            this.animator = this.GetComponent<Animator>();
        }

        void OnAnimatorIK(int layerIndex) {
            if (target != null) {
                animator.SetLookAtPosition(target.position);
                animator.SetLookAtWeight(this.totalWeight, 0f, 0f, this.eyeWeight, this.clampWeight);
            }
        }
    }
}