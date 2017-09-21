<<<<<<< HEAD
<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Move the position of this object based on a linear mapping
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearDisplacement : MonoBehaviour
	{
		public Vector3 displacement;
		public LinearMapping linearMapping;

		private Vector3 initialPosition;

		//-------------------------------------------------
		void Start()
		{
			initialPosition = transform.localPosition;

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( linearMapping )
			{
				transform.localPosition = initialPosition + linearMapping.value * displacement;
			}
		}
	}
}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Move the position of this object based on a linear mapping
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearDisplacement : MonoBehaviour
	{
		public Vector3 displacement;
		public LinearMapping linearMapping;

		private Vector3 initialPosition;

		//-------------------------------------------------
		void Start()
		{
			initialPosition = transform.localPosition;

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( linearMapping )
			{
				transform.localPosition = initialPosition + linearMapping.value * displacement;
			}
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Move the position of this object based on a linear mapping
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearDisplacement : MonoBehaviour
	{
		public Vector3 displacement;
		public LinearMapping linearMapping;

		private Vector3 initialPosition;

		//-------------------------------------------------
		void Start()
		{
			initialPosition = transform.localPosition;

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( linearMapping )
			{
				transform.localPosition = initialPosition + linearMapping.value * displacement;
			}
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
