<<<<<<< HEAD
<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Changes the pitch of this audio source based on a linear mapping
//			and a curve
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearAudioPitch : MonoBehaviour
	{
		public LinearMapping linearMapping;
		public AnimationCurve pitchCurve;
		public float minPitch;
		public float maxPitch;
		public bool applyContinuously = true;

		private AudioSource audioSource;

	
		//-------------------------------------------------
		void Awake()
		{
			if ( audioSource == null )
			{
				audioSource = GetComponent<AudioSource>();
			}

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( applyContinuously )
			{
				Apply();
			}
		}


		//-------------------------------------------------
		private void Apply()
		{
			float y = pitchCurve.Evaluate( linearMapping.value );

			audioSource.pitch = Mathf.Lerp( minPitch, maxPitch, y );
		}
	}
}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Changes the pitch of this audio source based on a linear mapping
//			and a curve
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearAudioPitch : MonoBehaviour
	{
		public LinearMapping linearMapping;
		public AnimationCurve pitchCurve;
		public float minPitch;
		public float maxPitch;
		public bool applyContinuously = true;

		private AudioSource audioSource;

	
		//-------------------------------------------------
		void Awake()
		{
			if ( audioSource == null )
			{
				audioSource = GetComponent<AudioSource>();
			}

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( applyContinuously )
			{
				Apply();
			}
		}


		//-------------------------------------------------
		private void Apply()
		{
			float y = pitchCurve.Evaluate( linearMapping.value );

			audioSource.pitch = Mathf.Lerp( minPitch, maxPitch, y );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Changes the pitch of this audio source based on a linear mapping
//			and a curve
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class LinearAudioPitch : MonoBehaviour
	{
		public LinearMapping linearMapping;
		public AnimationCurve pitchCurve;
		public float minPitch;
		public float maxPitch;
		public bool applyContinuously = true;

		private AudioSource audioSource;

	
		//-------------------------------------------------
		void Awake()
		{
			if ( audioSource == null )
			{
				audioSource = GetComponent<AudioSource>();
			}

			if ( linearMapping == null )
			{
				linearMapping = GetComponent<LinearMapping>();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( applyContinuously )
			{
				Apply();
			}
		}


		//-------------------------------------------------
		private void Apply()
		{
			float y = pitchCurve.Evaluate( linearMapping.value );

			audioSource.pitch = Mathf.Lerp( minPitch, maxPitch, y );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
