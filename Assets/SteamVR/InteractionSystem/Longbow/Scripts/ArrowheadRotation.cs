<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sets a random rotation for the arrow head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class ArrowheadRotation : MonoBehaviour
	{
		//-------------------------------------------------
		void Start()
		{
			float randX = Random.Range( 0f, 180f );
			transform.localEulerAngles = new Vector3( randX, -90f, 90f );
		}
	}
}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sets a random rotation for the arrow head
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class ArrowheadRotation : MonoBehaviour
	{
		//-------------------------------------------------
		void Start()
		{
			float randX = Random.Range( 0f, 180f );
			transform.localEulerAngles = new Vector3( randX, -90f, 90f );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
