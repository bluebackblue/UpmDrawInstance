

/** @brief 色。
*/


Shader "BlueBack/DrawInstance/Samples/Simple2D/Color2D"
{
	Properties
	{
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}
		Pass
		{
			Cull Off
			ZTest Always
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha 

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
				/** position
				*/
				float2 position;
				
				/** color
				*/
				float4 color;
			};

			/** vertexbuffer
			*/
            StructuredBuffer<Status> status;

			#endif

			/** custom_matrix
			*/
			float4x4 custom_matrix;

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
					float2 t_position = a_appdata.position.xy + status[a_appdata.instanceID].position.xy;
					t_ret.position = mul(custom_matrix,float4(t_position.xy,0,1));
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

