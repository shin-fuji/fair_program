using UnityEngine;
using System.Collections;

namespace UMA
{
	/// <summary>
	/// Auxillary slot which adds a TwistBone component for the forearms of a newly created character.
	/// </summary>
	public class O3nArmLowerLegTwistSlotScript : MonoBehaviour 
	{
		static int leftHandHash;
		static int rightHandHash;
		static int leftTwistHash;
		static int rightTwistHash;

        static int leftFootHash;
        static int rightFootHash;
        static int leftFootTwistHash;
        static int rightFootTwistHash;
        static bool hashesFound = false;

		public void OnDnaApplied(UMAData umaData)
		{
			if (!hashesFound)
			{
				leftHandHash = UMAUtils.StringToHash("hand_L");
				rightHandHash = UMAUtils.StringToHash("hand_R");
				leftTwistHash = UMAUtils.StringToHash("LowerarmAdjustTwist_L");
				rightTwistHash = UMAUtils.StringToHash("LowerarmAdjustTwist_R");
                leftFootHash = UMAUtils.StringToHash("Foot_L");
                rightFootHash = UMAUtils.StringToHash("Foot_R");
                leftFootTwistHash = UMAUtils.StringToHash("CalfAdjustTwist_L");
                rightFootTwistHash = UMAUtils.StringToHash("CalfAdjustTwist_R");
                hashesFound = true;
			}

			GameObject leftHand = umaData.GetBoneGameObject(leftHandHash);
			GameObject rightHand = umaData.GetBoneGameObject(rightHandHash);
			GameObject leftTwist = umaData.GetBoneGameObject(leftTwistHash);
			GameObject rightTwist = umaData.GetBoneGameObject(rightTwistHash);

            GameObject leftFoot = umaData.GetBoneGameObject(leftFootHash);
            GameObject rightFoot = umaData.GetBoneGameObject(rightFootHash);
            GameObject leftFootTwist = umaData.GetBoneGameObject(leftFootTwistHash);
            GameObject rightFootTwist = umaData.GetBoneGameObject(rightFootTwistHash);

            if ((leftHand == null) || (rightHand == null) || (leftTwist == null) || (rightTwist == null)
                || (leftFoot == null) || (rightFoot == null) || (leftFootTwist == null) || (rightFootTwist == null))
			{
				Debug.LogError("Failed to add o3n Forearm Twist to: " + umaData.name);
				return;
			}

			var twist = umaData.umaRoot.AddComponent<TwistBones>();
			twist.twistValue = 0.5f;
			twist.twistBone = new Transform[] {leftTwist.transform, rightTwist.transform, leftFootTwist.transform, rightFootTwist.transform};
			twist.refBone = new Transform[] {leftHand.transform, rightHand.transform, leftFoot.transform, rightFoot.transform};



		}
	}
}