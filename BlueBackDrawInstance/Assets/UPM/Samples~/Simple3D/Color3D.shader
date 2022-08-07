

/** @brief 色。
*/


Shader "BlueBack/DrawInstance/Samples/Simple3D/Color3D"
{
	Properties
	{
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Queue"
			"Queue" = "Geometry"
		}
		Pass
		{
			Cull Off
			ZTest LEqual
			ZWrite On

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma instancing_options procedural:setup

			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				/** position
				*/
				float4 position		: POSITION;

				/** uv
				*/
				float2 uv			: TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			/** v2f
			*/
			struct v2f
			{
				/** position
				*/
				float4 position		: SV_POSITION;

				/** uv
				*/
				float2 uv			: TEXCOORD0;

				/** color
				*/
				float4 color		: COLOR0;
			};

			#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)

			/** Status
			*/
			struct Status
			{
				/** localmatrix
				*/
				float4x4 localmatrix;
				
				/** color
				*/
				float4 color;
			};

			/** vertexbuffer
			*/
            StructuredBuffer<Status> status;

			#endif

			/** setup
			*/
			void setup()
			{
			}

			/** vert
			*/
			v2f vert(appdata a_appdata)
			{
				UNITY_SETUP_INSTANCE_ID(a_appdata);

				v2f t_ret;

				#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
				{
					float4 t_position = mul(status[a_appdata.instanceID].localmatrix,float4(a_appdata.position.xyz,1));
					t_ret.position = UnityObjectToClipPos(t_position);
					t_ret.uv = a_appdata.uv;
					t_ret.color = status[a_appdata.instanceID].color;
				}
				#else
				{
					float4 t_position = a_appdata.position;
					t_ret.position = UnityObjectToClipPos(t_position);
					t_ret.uv = a_appdata.uv;
					t_ret.color = fixed4(0.0f,0.0f,0.0f,1.0f);
				}
				#endif

				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f a_v2f) : SV_Target
			{
				#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
				return a_v2f.color;
				#else
				return fixed4(0.0f,0.0f,0.0f,1.0f);
				#endif
			}

			ENDCG
		}
	}
}

