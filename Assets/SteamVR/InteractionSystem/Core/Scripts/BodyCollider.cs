<<<<<<< HEAD
<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( CapsuleCollider ) )]
	public class BodyCollider : MonoBehaviour
	{
		public Transform head;

		private CapsuleCollider capsuleCollider;

		//-------------------------------------------------
		void Awake()
		{
			capsuleCollider = GetComponent<CapsuleCollider>();
		}


		//-------------------------------------------------
		void FixedUpdate()
		{
			float distanceFromFloor = Vector3.Dot( head.localPosition, Vector3.up );
			capsuleCollider.height = Mathf.Max( capsuleCollider.radius, distanceFromFloor );
			transform.localPosition = head.localPosition - 0.5f * distanceFromFloor * Vector3.up;
		}
	}
}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( CapsuleCollider ) )]
	public class BodyCollider : MonoBehaviour
	{
		public Transform head;

		private CapsuleCollider capsuleCollider;

		//-------------------------------------------------
		void Awake()
		{
			capsuleCollider = GetComponent<CapsuleCollider>();
		}


		//-------------------------------------------------
		void FixedUpdate()
		{
			float distanceFromFloor = Vector3.Dot( head.localPosition, Vector3.up );
			capsuleCollider.height = Mathf.Max( capsuleCollider.radius, distanceFromFloor );
			transform.localPosition = head.localPosition - 0.5f * distanceFromFloor * Vector3.up;
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Collider dangling from the player's head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( CapsuleCollider ) )]
	public class BodyCollider : MonoBehaviour
	{
		public Transform head;

		private CapsuleCollider capsuleCollider;

		//-------------------------------------------------
		void Awake()
		{
			capsuleCollider = GetComponent<CapsuleCollider>();
		}


		//-------------------------------------------------
		void FixedUpdate()
		{
			float distanceFromFloor = Vector3.Dot( head.localPosition, Vector3.up );
			capsuleCollider.height = Mathf.Max( capsuleCollider.radius, distanceFromFloor );
			transform.localPosition = head.localPosition - 0.5f * distanceFromFloor * Vector3.up;
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
