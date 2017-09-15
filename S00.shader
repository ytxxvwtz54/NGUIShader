Shader "Unlit/S00"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
		_Offset ("Offset", Vector) = (0.5, 0.5, 0.5, 0.5)
		_AddPercentage ("Add %", Range (0.0,1.0)) = 1.0
	}

	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			AlphaTest Off
			Fog { Mode Off }
			Offset -1, -1
			ColorMask RGB
			Blend One OneMinusSrcAlpha
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Offset;
			float _AddPercentage;

			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord;
				return o;
			}

			half4 frag (v2f IN) : COLOR
			{
				float2 tc = (IN.texcoord + _AddPercentage) % 1.0;
				half4 col = tex2D(_MainTex, tc) * IN.color + 0.2;
				//col.rgb = half3(0.0, 0.0, 0.0); //lerp(half3(0.0, 0.0, 0.0), col.rgb, col.a);

				return col;
			}
			ENDCG
		}
	}
}
