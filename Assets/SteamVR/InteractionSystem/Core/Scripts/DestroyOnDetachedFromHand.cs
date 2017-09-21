<<<<<<< HEAD
<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Destroys this object when it is detached from the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class DestroyOnDetachedFromHand : MonoBehaviour
	{
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}
	}
}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Destroys this object when it is detached from the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class DestroyOnDetachedFromHand : MonoBehaviour
	{
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Destroys this object when it is detached from the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class DestroyOnDetachedFromHand : MonoBehaviour
	{
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
			Destroy( gameObject );
		}
	}
}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
