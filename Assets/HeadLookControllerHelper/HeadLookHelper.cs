<<<<<<< HEAD
<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mebiustos.HeadLookControllerHelper {
    public class HeadLookHelper : MonoBehaviour {
        public bool delaySetup = true;

        [Header("--- Target")]
        public Transform lookAtTargetObject;
        public float targetVelocityAdjustment = -0.045f;

        [Header("--- LookAtController初期設定")]
        public float torsoResponsive = 2.5f;
        public float headResponsive = 4f;
        public bool overrideAnimation = false;
        bool torsoEffective = true;
        bool headEffective = true;

        [Header("--- 近距離におけるLookAt効果の減衰")]
        public bool nearEffectDamping = false;
        [Tooltip("LookAt効果が無効になる距離")]
        public float nearDampingMin = 0.17f;
        [Tooltip("減衰を開始するMinからの距離")]
        public float nearDampingRange = 0.3f;

        [Header("--- IKでの視線制御 [必須:Animator IKEnable]")]
        public bool lookAtUnityIK = true;
        public float lookAtTotalWeight = 1f;
        public float lookAtEyeWeight = 0.13f;
        public float lookAtClampWeight = 1f;

        //[Header("--- Option")]
        //public Object modelFBX;
        //void Awake() {
        //    // sync fbx bone rotation
        //    if (this.modelFBX) {
        //        GameObject obj = (GameObject)Instantiate(this.modelFBX, Vector3.zero, Quaternion.identity);
        //        var a2 = obj.GetComponent<Animator>();
        //        var a1 = this.GetComponent<Animator>();
        //        foreach (HumanBodyBones bone in System.Enum.GetValues(typeof(HumanBodyBones))) {
        //            var t2 = a2.GetBoneTransform(bone);
        //            if (t2) {
        //                var t1 = a1.GetBoneTransform(bone);
        //                if (t1) {
        //                    t1.localRotation = t2.localRotation;
        //                }
        //            }
        //        }
        //        Destroy(obj);
        //    }
        //}

        void Start() {
            lookAtTargetObject = GameObject.FindGameObjectWithTag("PlayerHead").transform;
            if (!this.delaySetup)
                this.Setup();
        }

        int updatecount = 0;
        void Update() {
            if (Vector3.Distance(this.transform.position, lookAtTargetObject.transform.position) > 10)
            {
                return;
            }

            if (this.delaySetup) {
                this.updatecount++;
                if (this.updatecount >= 3) {
                    this.Setup();
                }
            }
        }

        void Setup() {
            var hlc = this.InitLookAtController();

            var updater = gameObject.AddComponent<TargetUpdater>();
            updater.lookAtTargetObject = this.lookAtTargetObject;
            updater.targetVelocityAdjustment = this.targetVelocityAdjustment;
            updater.hlc = hlc;

            if (this.nearEffectDamping) {
                var damper = gameObject.AddComponent<NearEffectDamper>();
                damper.thresholdMin = nearDampingMin;
                damper.thresholdRange = nearDampingRange;
            }

            if (lookAtUnityIK) {
                var lookat = gameObject.AddComponent<LookAtUnityIK>();
                lookat.target = this.lookAtTargetObject;
                lookat.totalWeight = this.lookAtTotalWeight;
                lookat.eyeWeight = this.lookAtEyeWeight;
                lookat.clampWeight = this.lookAtClampWeight;
            }

            Destroy(this);
        }

        HeadLookController InitLookAtController() {
            var controller = gameObject.AddComponent<HeadLookController>();

            var bsList = new List<BendingSegment>();
            BendingSegment bs;
            var anim = GetComponent<Animator>();

            if (torsoEffective) {
                bs = new BendingSegment();
                bs.thresholdAngleDifference = 0f;
                bs.thresholdAngleDifference2 = 40f;
                bs.bendingMultiplier = 0.6f;
                bs.maxAngleDifference = 50f;
                bs.maxBendingAngle = 40f;
                bs.responsiveness = torsoResponsive;
                bs.firstTransform = anim.GetBoneTransform(HumanBodyBones.Spine);
                bs.lastTransform = anim.GetBoneTransform(HumanBodyBones.Chest);
                bsList.Add(bs);
            }
            if (headEffective) {
                bs = new BendingSegment();
                bs.thresholdAngleDifference = 0f;
                bs.thresholdAngleDifference2 = 40f;
                bs.bendingMultiplier = 0.7f;
                bs.maxAngleDifference = 30f;
                bs.maxBendingAngle = 40f;
                bs.responsiveness = headResponsive;
                bs.firstTransform = anim.GetBoneTransform(HumanBodyBones.Neck);
                bs.lastTransform = anim.GetBoneTransform(HumanBodyBones.Head);
                bsList.Add(bs);
            }
            controller.segments = bsList.ToArray();
            controller.nonAffectedJoints = new NonAffectedJoints[2];
            controller.nonAffectedJoints[0] = new NonAffectedJoints();
            controller.nonAffectedJoints[0].effect = 0.3f;
            controller.nonAffectedJoints[0].joint = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            controller.nonAffectedJoints[1] = new NonAffectedJoints();
            controller.nonAffectedJoints[1].effect = 0.3f;
            controller.nonAffectedJoints[1].joint = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
            controller.overrideAnimation = overrideAnimation;

            if (this.lookAtTargetObject != null)
                controller.target = this.lookAtTargetObject.position;

            return controller;
        }
    }
=======
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mebiustos.HeadLookControllerHelper {
    public class HeadLookHelper : MonoBehaviour {
        public bool delaySetup = true;

        [Header("--- Target")]
        public Transform lookAtTargetObject;
        public float targetVelocityAdjustment = -0.045f;

        [Header("--- LookAtController初期設定")]
        public float torsoResponsive = 2.5f;
        public float headResponsive = 4f;
        public bool overrideAnimation = false;
        bool torsoEffective = true;
        bool headEffective = true;

        [Header("--- 近距離におけるLookAt効果の減衰")]
        public bool nearEffectDamping = false;
        [Tooltip("LookAt効果が無効になる距離")]
        public float nearDampingMin = 0.17f;
        [Tooltip("減衰を開始するMinからの距離")]
        public float nearDampingRange = 0.3f;

        [Header("--- IKでの視線制御 [必須:Animator IKEnable]")]
        public bool lookAtUnityIK = true;
        public float lookAtTotalWeight = 1f;
        public float lookAtEyeWeight = 0.13f;
        public float lookAtClampWeight = 1f;

        //[Header("--- Option")]
        //public Object modelFBX;
        //void Awake() {
        //    // sync fbx bone rotation
        //    if (this.modelFBX) {
        //        GameObject obj = (GameObject)Instantiate(this.modelFBX, Vector3.zero, Quaternion.identity);
        //        var a2 = obj.GetComponent<Animator>();
        //        var a1 = this.GetComponent<Animator>();
        //        foreach (HumanBodyBones bone in System.Enum.GetValues(typeof(HumanBodyBones))) {
        //            var t2 = a2.GetBoneTransform(bone);
        //            if (t2) {
        //                var t1 = a1.GetBoneTransform(bone);
        //                if (t1) {
        //                    t1.localRotation = t2.localRotation;
        //                }
        //            }
        //        }
        //        Destroy(obj);
        //    }
        //}

        void Start() {
            lookAtTargetObject = GameObject.FindGameObjectWithTag("PlayerHead").transform;
            if (!this.delaySetup)
                this.Setup();
        }

        int updatecount = 0;
        void Update() {
            if (Vector3.Distance(this.transform.position, lookAtTargetObject.transform.position) > 10)
            {
                return;
            }

            if (this.delaySetup) {
                this.updatecount++;
                if (this.updatecount >= 3) {
                    this.Setup();
                }
            }
        }

        void Setup() {
            var hlc = this.InitLookAtController();

            var updater = gameObject.AddComponent<TargetUpdater>();
            updater.lookAtTargetObject = this.lookAtTargetObject;
            updater.targetVelocityAdjustment = this.targetVelocityAdjustment;
            updater.hlc = hlc;

            if (this.nearEffectDamping) {
                var damper = gameObject.AddComponent<NearEffectDamper>();
                damper.thresholdMin = nearDampingMin;
                damper.thresholdRange = nearDampingRange;
            }

            if (lookAtUnityIK) {
                var lookat = gameObject.AddComponent<LookAtUnityIK>();
                lookat.target = this.lookAtTargetObject;
                lookat.totalWeight = this.lookAtTotalWeight;
                lookat.eyeWeight = this.lookAtEyeWeight;
                lookat.clampWeight = this.lookAtClampWeight;
            }

            Destroy(this);
        }

        HeadLookController InitLookAtController() {
            var controller = gameObject.AddComponent<HeadLookController>();

            var bsList = new List<BendingSegment>();
            BendingSegment bs;
            var anim = GetComponent<Animator>();

            if (torsoEffective) {
                bs = new BendingSegment();
                bs.thresholdAngleDifference = 0f;
                bs.thresholdAngleDifference2 = 40f;
                bs.bendingMultiplier = 0.6f;
                bs.maxAngleDifference = 50f;
                bs.maxBendingAngle = 40f;
                bs.responsiveness = torsoResponsive;
                bs.firstTransform = anim.GetBoneTransform(HumanBodyBones.Spine);
                bs.lastTransform = anim.GetBoneTransform(HumanBodyBones.Chest);
                bsList.Add(bs);
            }
            if (headEffective) {
                bs = new BendingSegment();
                bs.thresholdAngleDifference = 0f;
                bs.thresholdAngleDifference2 = 40f;
                bs.bendingMultiplier = 0.7f;
                bs.maxAngleDifference = 30f;
                bs.maxBendingAngle = 40f;
                bs.responsiveness = headResponsive;
                bs.firstTransform = anim.GetBoneTransform(HumanBodyBones.Neck);
                bs.lastTransform = anim.GetBoneTransform(HumanBodyBones.Head);
                bsList.Add(bs);
            }
            controller.segments = bsList.ToArray();
            controller.nonAffectedJoints = new NonAffectedJoints[2];
            controller.nonAffectedJoints[0] = new NonAffectedJoints();
            controller.nonAffectedJoints[0].effect = 0.3f;
            controller.nonAffectedJoints[0].joint = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            controller.nonAffectedJoints[1] = new NonAffectedJoints();
            controller.nonAffectedJoints[1].effect = 0.3f;
            controller.nonAffectedJoints[1].joint = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
            controller.overrideAnimation = overrideAnimation;

            if (this.lookAtTargetObject != null)
                controller.target = this.lookAtTargetObject.position;

            return controller;
        }
    }
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Mebiustos.HeadLookControllerHelper {
    public class HeadLookHelper : MonoBehaviour {
        public bool delaySetup = true;

        [Header("--- Target")]
        public Transform lookAtTargetObject;
        public float targetVelocityAdjustment = -0.045f;

        [Header("--- LookAtController初期設定")]
        public float torsoResponsive = 2.5f;
        public float headResponsive = 4f;
        public bool overrideAnimation = false;
        bool torsoEffective = true;
        bool headEffective = true;

        [Header("--- 近距離におけるLookAt効果の減衰")]
        public bool nearEffectDamping = false;
        [Tooltip("LookAt効果が無効になる距離")]
        public float nearDampingMin = 0.17f;
        [Tooltip("減衰を開始するMinからの距離")]
        public float nearDampingRange = 0.3f;

        [Header("--- IKでの視線制御 [必須:Animator IKEnable]")]
        public bool lookAtUnityIK = true;
        public float lookAtTotalWeight = 1f;
        public float lookAtEyeWeight = 0.13f;
        public float lookAtClampWeight = 1f;

        //[Header("--- Option")]
        //public Object modelFBX;
        //void Awake() {
        //    // sync fbx bone rotation
        //    if (this.modelFBX) {
        //        GameObject obj = (GameObject)Instantiate(this.modelFBX, Vector3.zero, Quaternion.identity);
        //        var a2 = obj.GetComponent<Animator>();
        //        var a1 = this.GetComponent<Animator>();
        //        foreach (HumanBodyBones bone in System.Enum.GetValues(typeof(HumanBodyBones))) {
        //            var t2 = a2.GetBoneTransform(bone);
        //            if (t2) {
        //                var t1 = a1.GetBoneTransform(bone);
        //                if (t1) {
        //                    t1.localRotation = t2.localRotation;
        //                }
        //            }
        //        }
        //        Destroy(obj);
        //    }
        //}

        void Start() {
            lookAtTargetObject = GameObject.FindGameObjectWithTag("PlayerHead").transform;
            if (!this.delaySetup)
                this.Setup();
        }

        int updatecount = 0;
        void Update() {
            if (Vector3.Distance(this.transform.position, lookAtTargetObject.transform.position) > 10)
            {
                return;
            }

            if (this.delaySetup) {
                this.updatecount++;
                if (this.updatecount >= 3) {
                    this.Setup();
                }
            }
        }

        void Setup() {
            var hlc = this.InitLookAtController();

            var updater = gameObject.AddComponent<TargetUpdater>();
            updater.lookAtTargetObject = this.lookAtTargetObject;
            updater.targetVelocityAdjustment = this.targetVelocityAdjustment;
            updater.hlc = hlc;

            if (this.nearEffectDamping) {
                var damper = gameObject.AddComponent<NearEffectDamper>();
                damper.thresholdMin = nearDampingMin;
                damper.thresholdRange = nearDampingRange;
            }

            if (lookAtUnityIK) {
                var lookat = gameObject.AddComponent<LookAtUnityIK>();
                lookat.target = this.lookAtTargetObject;
                lookat.totalWeight = this.lookAtTotalWeight;
                lookat.eyeWeight = this.lookAtEyeWeight;
                lookat.clampWeight = this.lookAtClampWeight;
            }

            Destroy(this);
        }

        HeadLookController InitLookAtController() {
            var controller = gameObject.AddComponent<HeadLookController>();

            var bsList = new List<BendingSegment>();
            BendingSegment bs;
            var anim = GetComponent<Animator>();

            if (torsoEffective) {
                bs = new BendingSegment();
                bs.thresholdAngleDifference = 0f;
                bs.thresholdAngleDifference2 = 40f;
                bs.bendingMultiplier = 0.6f;
                bs.maxAngleDifference = 50f;
                bs.maxBendingAngle = 40f;
                bs.responsiveness = torsoResponsive;
                bs.firstTransform = anim.GetBoneTransform(HumanBodyBones.Spine);
                bs.lastTransform = anim.GetBoneTransform(HumanBodyBones.Chest);
                bsList.Add(bs);
            }
            if (headEffective) {
                bs = new BendingSegment();
                bs.thresholdAngleDifference = 0f;
                bs.thresholdAngleDifference2 = 40f;
                bs.bendingMultiplier = 0.7f;
                bs.maxAngleDifference = 30f;
                bs.maxBendingAngle = 40f;
                bs.responsiveness = headResponsive;
                bs.firstTransform = anim.GetBoneTransform(HumanBodyBones.Neck);
                bs.lastTransform = anim.GetBoneTransform(HumanBodyBones.Head);
                bsList.Add(bs);
            }
            controller.segments = bsList.ToArray();
            controller.nonAffectedJoints = new NonAffectedJoints[2];
            controller.nonAffectedJoints[0] = new NonAffectedJoints();
            controller.nonAffectedJoints[0].effect = 0.3f;
            controller.nonAffectedJoints[0].joint = anim.GetBoneTransform(HumanBodyBones.LeftUpperArm);
            controller.nonAffectedJoints[1] = new NonAffectedJoints();
            controller.nonAffectedJoints[1].effect = 0.3f;
            controller.nonAffectedJoints[1].joint = anim.GetBoneTransform(HumanBodyBones.RightUpperArm);
            controller.overrideAnimation = overrideAnimation;

            if (this.lookAtTargetObject != null)
                controller.target = this.lookAtTargetObject.position;

            return controller;
        }
    }
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
}