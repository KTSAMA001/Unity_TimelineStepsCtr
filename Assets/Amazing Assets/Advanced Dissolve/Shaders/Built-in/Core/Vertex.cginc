#ifndef ADVANCED_DISSOLVE_VERTEX_CGINC
#define ADVANCED_DISSOLVE_VERTEX_CGINC

#include "../Core/Limits.cginc"
#include "../../cginc/Core.cginc"



#define USING_FOG (defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2))

 // ES2.0/WebGL can not do loops with non-constant-expression iteration counts :(
 #if defined(SHADER_API_GLES)
    #define LIGHT_LOOP_LIMIT 8
#else
    #define LIGHT_LOOP_LIMIT unity_VertexLightParams.x
#endif

// Some ES3 drivers (e.g. older Adreno) have problems with the light loop
#if defined(SHADER_API_GLES3) && !defined(SHADER_API_DESKTOP) && (defined(SPOT) || defined(POINT))
    #define LIGHT_LOOP_ATTRIBUTE UNITY_UNROLL
#else
    #define LIGHT_LOOP_ATTRIBUTE
#endif

#define ENABLE_SPECULAR 1






 // uniforms
int4 unity_VertexLightParams; // x: light count, y: zero, z: one (y/z needed by d3d9 vs loop instruction)
float4 _MainTex_ST;
sampler2D _MainTex;
float _Cutoff;
fixed4 _Color;



 // Compute illumination from one light, given attenuation
half3 computeLighting (int idx, half3 dirToLight, half3 eyeNormal, half3 viewDir, half4 diffuseColor, half shininess, half atten, inout half3 specColor) 
{
    half NdotL = max(dot(eyeNormal, dirToLight), 0.0);
    // diffuse
    half3 color = NdotL * diffuseColor.rgb * unity_LightColor[idx].rgb;
    return color * atten;
}

// Compute attenuation & illumination from one light
half3 computeOneLight(int idx, float3 eyePosition, half3 eyeNormal, half3 viewDir, half4 diffuseColor, half shininess, inout half3 specColor) 
{
    float3 dirToLight = unity_LightPosition[idx].xyz;
    half att = 1.0;

    #if defined(POINT) || defined(SPOT)
        dirToLight -= eyePosition * unity_LightPosition[idx].w;
        // distance attenuation
        float distSqr = dot(dirToLight, dirToLight);
        att /= (1.0 + unity_LightAtten[idx].z * distSqr);

        if (unity_LightPosition[idx].w != 0 && distSqr > unity_LightAtten[idx].w) 
            att = 0.0; // set to 0 if outside of range

        distSqr = max(distSqr, 0.000001); // don't produce NaNs if some vertex position overlaps with the light
        dirToLight *= rsqrt(distSqr);
    
        #if defined(SPOT)
            // spot angle attenuation
            half rho = max(dot(dirToLight, unity_SpotDirection[idx].xyz), 0.0);
            half spotAtt = (rho - unity_LightAtten[idx].x) * unity_LightAtten[idx].y;
            att *= saturate(spotAtt);
        #endif
    #endif
    
    att *= 0.5; // passed in light colors are 2x brighter than what used to be in FFP
    
    return min (computeLighting (idx, dirToLight, eyeNormal, viewDir, diffuseColor, shininess, att, specColor), 1.0);
}

// vertex shader input data
struct appdata 
{
    float4 pos : POSITION;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
    float3 uv0 : TEXCOORD0;
    float4 color : COLOR;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

// vertex-to-fragment interpolators
struct v2f 
{
    fixed4 color : COLOR0;
    float2 uv0 : TEXCOORD0;
    #if USING_FOG
        fixed fog : TEXCOORD1;
    #endif
    float4 pos : SV_POSITION;
    UNITY_VERTEX_OUTPUT_STEREO

    //Advanced Dissolve
	float3 positionOS  : TEXCOORD2;
    float3 normalOS    : TEXCOORD3;
	ADVANCED_DISSOLVE_UV(4)
};

// vertex shader
v2f vert (appdata IN) 
{
    v2f o = (v2f)0;
    UNITY_SETUP_INSTANCE_ID(IN);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


	#if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
	    CURVEDWORLD_TRANSFORM_VERTEX(IN.pos)
    #endif
            

    half4 color = half4(0,0,0,1.1);
    float3 eyePos = UnityObjectToViewPos(IN.pos).xyz;
    half3 eyeNormal = normalize (mul ((float3x3)UNITY_MATRIX_IT_MV, IN.normal).xyz);
    half3 viewDir = 0.0;
    // lighting
    half3 lcolor = half4(0,0,0,1).rgb + half4(1,1,1,1).rgb * glstate_lightmodel_ambient.rgb;
    half3 specColor = 0.0;
    half shininess = 0 * 128.0;
    LIGHT_LOOP_ATTRIBUTE for (int il = 0; il < LIGHT_LOOP_LIMIT; ++il) 
    {
        lcolor += computeOneLight(il, eyePos, eyeNormal, viewDir, half4(1,1,1,1), shininess, specColor);
    }
    color.rgb = lcolor.rgb;
    color.a = half4(1,1,1,1).a;
    o.color = saturate(color);

    // compute texture coordinates
    o.uv0 = IN.uv0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    // fog
    #if USING_FOG
        float fogCoord = length(eyePos.xyz); // radial fog distance
        UNITY_CALC_FOG_FACTOR_RAW(fogCoord);
        o.fog = saturate(unityFogFactor);
    #endif
    // transform position
    o.pos = UnityObjectToClipPos(IN.pos);


    //Advanced Dissolve 
    #if defined(_AD_STATE_ENABLED)
	    o.positionOS =  IN.pos.xyz;
        o.normalOS = IN.normal;

	    ADVANCED_DISSOLVE_INIT_UV(o, IN.uv0.xy, o.pos)
    #endif

    return o;
}

          
// fragment shader
fixed4 frag (v2f IN) : SV_Target 
{

//Advanced Dissolve////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#if defined(_AD_STATE_ENABLED)

    float4 dissolveBase = 0;
    #if defined(_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA) || defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
        dissolveBase = tex2D(_MainTex, IN.uv0.xy);
        dissolveBase.rgb *= _Color.rgb;
    #endif

	ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE_USING_OS(IN, dissolveBase, IN.positionOS.xyz, IN.normalOS.xyz)

    #if !defined(_ALPHATEST_ON)
        AdvancedDissolveClip(cutoutSource);
    #endif
    	
    float3 dissolveAlbedo = 0; 
    float3 dissolveEmission = 0;
	float dissolveBlend = AdvancedDissolveAlbedoEmission(cutoutSource, dissolveBase, dissolveAlbedo, dissolveEmission, IN.uv0.xy);

#endif
//Advanced Dissolve/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    fixed4 col;
    fixed4 tex, tmp0, tmp1, tmp2;
    // SetTexture #0
    tex = tex2D (_MainTex, IN.uv0.xy) * _Color;

    #if defined(_ALPHATEST_ON)

        float cutout = _Cutoff * 1.01;

		//Advanced Dissolve
		#if defined(_AD_STATE_ENABLED)
		    AdvancedDissolveCalculateAlphaAndClip(cutoutSource, tex.a, cutout);
        #endif

        clip (tex.a - cutout);

    #endif

    col = tex * IN.color;
    col *= 2;


//Advanced Dissolve/////////////////////////////////////////
#if defined(_AD_STATE_ENABLED)
    col.rgb = lerp(col.rgb, dissolveAlbedo, dissolveBlend);
    col.rgb += dissolveEmission * dissolveBlend;
#endif

              
    // fog
    #if USING_FOG
        col.rgb = lerp (unity_FogColor.rgb, col.rgb, IN.fog);
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