<<<<<<< HEAD
<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sets this GameObject as inactive when it loses focus from the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class HideOnHandFocusLost : MonoBehaviour
	{
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}
	}
}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sets this GameObject as inactive when it loses focus from the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class HideOnHandFocusLost : MonoBehaviour
	{
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Sets this GameObject as inactive when it loses focus from the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class HideOnHandFocusLost : MonoBehaviour
	{
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
			gameObject.SetActive( false );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
