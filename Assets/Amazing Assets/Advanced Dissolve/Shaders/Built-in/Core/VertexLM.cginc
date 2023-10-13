#ifndef ADVANCED_DISSOLVE_VERTEXLM_CGINC
#define ADVANCED_DISSOLVE_VERTEXLM_CGINC

#include "../Core/Limits.cginc"
#include "../../cginc/Core.cginc"

float4 _MainTex_ST;

        struct appdata
        {
            float4 pos : POSITION;
            float3 normal : NORMAL;
            float3 uv1 : TEXCOORD1;
            float3 uv0 : TEXCOORD0;
            float4 color : COLOR;

            UNITY_VERTEX_INPUT_INSTANCE_ID 
        };

        struct v2f
        {
            float2 uv0 : TEXCOORD0;
            float2 uv1 : TEXCOORD1;
            #if USING_FOG
                fixed fog : TEXCOORD2;
            #endif
            float4 pos : SV_POSITION;

            fixed4 color : COLOR0;

            UNITY_VERTEX_OUTPUT_STEREO

             //Advanced Dissolve
	          float3 positionOS  : TEXCOORD3;
              float3 normalOS    : TEXCOORD4;
	          ADVANCED_DISSOLVE_UV(5)
        };

        v2f vert(appdata IN)
        {
            v2f o = (v2f)0;
            UNITY_SETUP_INSTANCE_ID(IN);
            UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


			#if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
				CURVEDWORLD_TRANSFORM_VERTEX(IN.pos)
			#endif
            


            o.uv0 = IN.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
            o.uv1 = IN.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw;

            #if USING_FOG
                float3 eyePos = UnityObjectToViewPos(IN.pos);
                float fogCoord = length(eyePos.xyz);
                UNITY_CALC_FOG_FACTOR_RAW(fogCoord);
                o.fog = saturate(unityFogFactor);
            #endif

            o.pos = UnityObjectToClipPos(IN.pos);
            o.color = IN.color;


            //Advanced Dissolve 
            #if defined(_AD_STATE_ENABLED)
	            o.positionOS =  IN.pos.xyz;
                o.normalOS = IN.normal;

	            ADVANCED_DISSOLVE_INIT_UV(o, IN.uv0.xy, o.pos)
            #endif

            return o;
        }


        sampler2D _MainTex;
        float _Cutoff;
        fixed4 _Color;

        fixed4 frag(v2f IN) : SV_Target
        {

//Advanced Dissolve////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#if defined(_AD_STATE_ENABLED)

    float4 dissolveBase = 0;
    #if defined(_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA) || defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
        dissolveBase = tex2D(_MainTex, IN.uv1.xy);
        dissolveBase.rgb *= _Color.rgb;
    #endif

	ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE_USING_OS(IN, dissolveBase, IN.positionOS.xyz, IN.normalOS.xyz)

    #if !defined(_ALPHATEST_ON)
        AdvancedDissolveClip(cutoutSource);
    #endif
    	
    float3 dissolveAlbedo = 0; 
    float3 dissolveEmission = 0;
	float dissolveBlend = AdvancedDissolveAlbedoEmission(cutoutSource, dissolveBase, dissolveAlbedo, dissolveEmission, IN.uv1.xy);

#endif
//Advanced Dissolve/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            fixed4 col;
            fixed4 tex = UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.uv0.xy);
            half3 bakedColor = DecodeLightmap(tex);

            tex = tex2D(_MainTex, IN.uv1.xy) * _Color;

            #if defined(_ALPHATEST_ON)
                    float cutout = _Cutoff * 1.01;

		            //Advanced Dissolve
		            #if defined(_AD_STATE_ENABLED)
			            AdvancedDissolveCalculateAlphaAndClip(cutoutSource, tex.a, cutout);
		            #endif

                    clip (tex.a - cutout);
            #endif
                       

            col.rgb = tex.rgb * bakedColor;
            col.a = tex.a;


//Advanced Dissolve/////////////////////////////////////////
#if defined(_AD_STATE_ENABLED)
    col.rgb = lerp(col.rgb, dissolveAlbedo, dissolveBlend);
    col.rgb += dissolveEmission * dissolveBlend;
#endif


            #if USING_FOG
            col.rgb = lerp(unity_FogColor.rgb, col.rgb, IN.fog);
            #endif



			#if defined(_ALPHABLEND_ON) 
				col.a = tex.a;
			#elif defined(_ALPHAPREMULTIPLY_ON)
				col.rgb *= tex.a;
				col.a = tex.a;
			#else
				UNITY_OPAQUE_ALPHA(col.a);
			#endif

            return col;
        }

#endif