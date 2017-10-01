Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_UVCenter ("UVCenter", Vector) = (0.0, 0.0, 0.0, 0.0)
		_UVRoll ("UVRoll", Int) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _UVCenter;
			int _UVRoll;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float2 uv0 = TRANSFORM_TEX(v.uv, _MainTex);
				if (_UVRoll == 0)
				{
					o.uv = uv0;
				}
				else
				{
					uv0 -=  _UVCenter;
					float2 uv1 = float2(uv0.y, -uv0.x);
					uv1 +=  _UVCenter;
					o.uv = uv1;
				}
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
