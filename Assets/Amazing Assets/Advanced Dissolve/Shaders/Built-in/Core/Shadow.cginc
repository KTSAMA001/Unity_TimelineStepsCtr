#ifndef ADVANCED_DISSOLVE_SHADOW_CGINC
#define ADVANCED_DISSOLVE_SHADOW_CGINC



//Variables/////////////////////////////////////////////////////////////
float4 _MainTex_ST;
sampler2D _MainTex;
float _Cutoff;
fixed4 _Color;

//Structs///////////////////////////////////////////////////////////////
struct v2f 
{
	float2 uv0 : TEXCOORD2;
	V2F_SHADOW_CASTER;
	UNITY_VERTEX_OUTPUT_STEREO


	//Advanced Dissolve
	float3 positionWS  : TEXCOORD3;
	float3 normalWS    : TEXCOORD4;
	ADVANCED_DISSOLVE_UV(5)
};

//Vertex////////////////////////////////////////////////////////////////
v2f vert( appdata_base v )
{
	v2f o = (v2f)0;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	#if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
		CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
	#endif


	o.uv0 = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;


	TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)


	//Advanced Dissolve 
	#if defined(_AD_STATE_ENABLED)
		o.positionWS =  mul(unity_ObjectToWorld, v.vertex).xyz;
		o.normalWS = UnityObjectToWorldNormal(v.normal);
		ADVANCED_DISSOLVE_INIT_UV(o, v.texcoord.xy, o.pos)
	#endif

	return o;
}

//Fragment//////////////////////////////////////////////////////////////
float4 frag( v2f i ) : SV_Target
{

//Advanced Dissolve////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#if defined(_AD_STATE_ENABLED)

    float4 dissolveBase = 0;
    #if defined(_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA) || defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
        dissolveBase = tex2D(_MainTex, i.uv0.xy);
        dissolveBase.rgb *= _Color.rgb;
    #endif

	ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE_USING_WS(i, dissolveBase, i.positionWS.xyz, i.normalWS.xyz)

    #if !defined(_ALPHATEST_ON)
        AdvancedDissolveClip(cutoutSource);
    #endif   	

#endif
//Advanced Dissolve/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	#if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
		clip(-1);
	#elif defined(_ALPHATEST_ON)
		
		float4 alpha = tex2D (_MainTex, i.uv0.xy) * _Color;
		float cutout = _Cutoff * 1.001;

		//Advanced Dissolve
		#if defined(_AD_STATE_ENABLED)
			AdvancedDissolveCalculateAlphaAndClip(cutoutSource, alpha.a, cutout);
		#endif

		clip (alpha.a - cutout);            
	#endif

	SHADOW_CASTER_FRAGMENT(i)
}


#endif