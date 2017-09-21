<<<<<<< HEAD
<<<<<<< HEAD
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
// UNITY_SHADER_NO_UPGRADE
Shader "Custom/SteamVR_Fade"
{
	SubShader
	{
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Always
			Cull Off
			ZWrite Off

			CGPROGRAM
				#pragma vertex MainVS
				#pragma fragment MainPS

				float4 fadeColor;

				float4 MainVS( float4 vertex : POSITION ) : SV_POSITION
				{
					return vertex.xyzw;
				}

				float4 MainPS() : SV_Target
				{
					return fadeColor.rgba;
				}
			ENDCG
		}
	}
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
// UNITY_SHADER_NO_UPGRADE
Shader "Custom/SteamVR_Fade"
{
	SubShader
	{
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Always
			Cull Off
			ZWrite Off

			CGPROGRAM
				#pragma vertex MainVS
				#pragma fragment MainPS

				float4 fadeColor;

				float4 MainVS( float4 vertex : POSITION ) : SV_POSITION
				{
					return vertex.xyzw;
				}

				float4 MainPS() : SV_Target
				{
					return fadeColor.rgba;
				}
			ENDCG
		}
	}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
=======
﻿//======= Copyright (c) Valve Corporation, All rights reserved. ===============
// UNITY_SHADER_NO_UPGRADE
Shader "Custom/SteamVR_Fade"
{
	SubShader
	{
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Always
			Cull Off
			ZWrite Off

			CGPROGRAM
				#pragma vertex MainVS
				#pragma fragment MainPS

				float4 fadeColor;

				float4 MainVS( float4 vertex : POSITION ) : SV_POSITION
				{
					return vertex.xyzw;
				}

				float4 MainPS() : SV_Target
				{
					return fadeColor.rgba;
				}
			ENDCG
		}
	}
>>>>>>> 684eebeece1ce14769f563c1c5c9ea0928383a38
}