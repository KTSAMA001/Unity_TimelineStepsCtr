Shader "Amazing Assets/Advanced Dissolve/Mobile/Particles/VertexLit Blended" {
Properties {

//[HideInInspector][CurvedWorldBendSettings] _CurvedWorldBendSettings("0|1|1", Vector) = (0, 0, 0, 0)


 _EmisColor ("Emissive Color", Color) = (0.200000,0.200000,0.200000,0.000000)
 _MainTex ("Particle Texture", 2D) = "white" { }


//Advanced Dissolve Properties Start////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Cutout
[HideInInspector]                                                   _AdvancedDissolveCutoutStandardClip("", Range(0,1)) = 0.5

[HideInInspector]											        _AdvancedDissolveCutoutStandardMap1("", 2D) = "white" { }
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap1Tiling("", Vector) = (1, 1, 1, 0)
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap1Offset("", Vector) = (0, 0, 0, 0)
[HideInInspector]					                                _AdvancedDissolveCutoutStandardMap1Scroll("", Vector) = (0, 0, 0, 0)
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap1Intensity("", Range(0, 1)) = 1
[HideInInspector][Enum(Red, 0, Green, 1, Blue, 2, Alpha, 3)]        _AdvancedDissolveCutoutStandardMap1Channel("", INT) = 3
[HideInInspector][AdvancedDissolveToggleFloat]				        _AdvancedDissolveCutoutStandardMap1Invert("", INT) = 0
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap2("", 2D) = "white" { }
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap2Tiling("", Vector) = (1, 1, 1, 0)
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap2Offset("", Vector) = (0, 0, 0, 0)
[HideInInspector]					                                _AdvancedDissolveCutoutStandardMap2Scroll("", Vector) = (0, 0, 0, 0)
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap2Intensity("", Range(0, 1)) = 1
[HideInInspector][Enum(Red, 0, Green, 1, Blue, 2, Alpha, 3)]        _AdvancedDissolveCutoutStandardMap2Channel("", INT) = 3
[HideInInspector][AdvancedDissolveToggleFloat]				        _AdvancedDissolveCutoutStandardMap2Invert("", INT) = 0
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap3("", 2D) = "white" { }
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap3Tiling("", Vector) = (1, 1, 1, 0)
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap3Offset("", Vector) = (0, 0, 0, 0)
[HideInInspector]					                                _AdvancedDissolveCutoutStandardMap3Scroll("", Vector) = (0, 0, 0, 0)
[HideInInspector]											        _AdvancedDissolveCutoutStandardMap3Intensity("", Range(0, 1)) = 1
[HideInInspector][Enum(Red, 0, Green, 1, Blue, 2, Alpha, 3)]        _AdvancedDissolveCutoutStandardMap3Channel("", INT) = 3
[HideInInspector][AdvancedDissolveToggleFloat]				        _AdvancedDissolveCutoutStandardMap3Invert("", INT) = 0

[HideInInspector][Enum(Multiply, 0, Add, 1)]				        _AdvancedDissolveCutoutStandardMapsBlendType("", Float) = 0
[HideInInspector][Enum(World, 0, Local, 1)]					        _AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace("", Float) = 0	
[HideInInspector][Enum(Constant, 0, Camera Relative, 1)]            _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale("", Float) = 0
[HideInInspector][AdvancedDissolveToggleFloat]				        _AdvancedDissolveCutoutStandardBaseInvert("", INT) = 0

//Geometric
[HideInInspector][AdvancedDissolveToggleFloat]			    	    _AdvancedDissolveCutoutGeometricInvert("", Float) = 0
[HideInInspector]										    	    _AdvancedDissolveCutoutGeometricNoise("", Float) = 0.1	

[HideInInspector][Enum(X, 0, Y, 1, Z, 2)]                           _AdvancedDissolveCutoutGeometricXYZAxis("", Float) = 0
[HideInInspector][Enum(Linear, 0, Symmetrical, 1)]                  _AdvancedDissolveCutoutGeometricXYZStyle("", Float) = 0 
[HideInInspector][Enum(World, 0, Local, 1)]                         _AdvancedDissolveCutoutGeometricXYZSpace("", Float) = 0	 
[HideInInspector]											        _AdvancedDissolveCutoutGeometricXYZRollout("", Float) = 0
[HideInInspector]											        _AdvancedDissolveCutoutGeometricXYZPosition("", Vector) = (0, 0, 0, 0)

[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric1Position("", Vector) = (0,0,0,0)
[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric1Normal("", Vector) = (1,0,0,0)
[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric1Radius("", Float) = 1
[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric1Height("", Float) = 1

[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric2Position("", Vector) = (0,0,0,0)
[HideInInspector]									    		    _AdvancedDissolveCutoutGeometric2Normal("", Vector) = (1,0,0,0)
[HideInInspector]									    		    _AdvancedDissolveCutoutGeometric2Radius("", Float) = 1
[HideInInspector]									    		    _AdvancedDissolveCutoutGeometric2Height("", Float) = 1
 
[HideInInspector]									    		    _AdvancedDissolveCutoutGeometric3Position("", Vector) = (0,0,0,0)
[HideInInspector]									    		    _AdvancedDissolveCutoutGeometric3Normal("", Vector) = (1,0,0,0)
[HideInInspector]									    		    _AdvancedDissolveCutoutGeometric3Radius("", Float) = 1
[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric3Height("", Float) = 1

[HideInInspector]										    	    _AdvancedDissolveCutoutGeometric4Position("", Vector) = (0,0,0,0)
[HideInInspector]											        _AdvancedDissolveCutoutGeometric4Normal("", Vector) = (1,0,0,0)
[HideInInspector]											        _AdvancedDissolveCutoutGeometric4Radius("", Float) = 1
[HideInInspector]											        _AdvancedDissolveCutoutGeometric4Height("", Float) = 1

//Edge
[HideInInspector]										    	    _AdvancedDissolveEdgeBaseWidthStandard("", Range(0,1)) = 0.1 
[HideInInspector]										    	    _AdvancedDissolveEdgeBaseWidthGeometric("", Range(0,1)) = 0.1 
[HideInInspector][Enum(Solid, 0, Smooth, 1, Smoother, 2)]           _AdvancedDissolveEdgeBaseShape("", INT) = 0
[HideInInspector][AdvancedDissolveColorRGB]  				        _AdvancedDissolveEdgeBaseColor("", Color) = (0,1,0,1)
[HideInInspector]											        _AdvancedDissolveEdgeBaseColorTransparency("", Range(0, 1)) = 1
[HideInInspector][AdvancedDissolveExponental]                       _AdvancedDissolveEdgeBaseColorIntensity("", Vector) = (0, 0, 0, 0)		

[HideInInspector][AdvancedDissolveColorRGB]					        _AdvancedDissolveEdgeAdditionalColor("", color) = (1, 0, 0, 1)
[HideInInspector]											        _AdvancedDissolveEdgeAdditionalColorTransparency("", Range(0, 1)) = 1
[HideInInspector][AdvancedDissolveExponental]			            _AdvancedDissolveEdgeAdditionalColorIntensity("", Vector) = (0, 0, 0, 0)
[HideInInspector]								                    _AdvancedDissolveEdgeAdditionalColorMap("", 2D) = "white" { }
[HideInInspector]					                                _AdvancedDissolveEdgeAdditionalColorMapTiling("", Vector) = (1, 1, 1, 0)
[HideInInspector]					                                _AdvancedDissolveEdgeAdditionalColorMapOffset("", Vector) = (0, 0, 0, 0)
[HideInInspector]					                                _AdvancedDissolveEdgeAdditionalColorMapScroll("", Vector) = (0, 0, 0, 0)
[HideInInspector][AdvancedDissolveToggleFloat]				        _AdvancedDissolveEdgeAdditionalColorMapReverse("", FLOAT) = 0
[HideInInspector]											        _AdvancedDissolveEdgeAdditionalColorMapMipmap("", Range(0, 10)) = 1	
[HideInInspector]											        _AdvancedDissolveEdgeAdditionalColorPhaseOffset("", FLOAT) = 0
[HideInInspector]											        _AdvancedDissolveEdgeAdditionalColorAlphaOffset("", Range(-1, 1)) = 0	
[HideInInspector][AdvancedDissolveToggleFloat]				        _AdvancedDissolveEdgeAdditionalColorClipInterpolation("", Float) = 0


[HideInInspector]								                    _AdvancedDissolveEdgeUVDistortionMap("", 2D) = "black" { }
[HideInInspector]					                                _AdvancedDissolveEdgeUVDistortionMapTiling("", Vector) = (1, 1, 1, 0)
[HideInInspector]					                                _AdvancedDissolveEdgeUVDistortionMapOffset("", Vector) = (0, 0, 0, 0)
[HideInInspector]					                                _AdvancedDissolveEdgeUVDistortionMapScroll("", Vector) = (0, 0, 0, 0)
[HideInInspector]				                                    _AdvancedDissolveEdgeUVDistortionStrength("", Float) = 0

[HideInInspector][AdvancedDissolvePositiveFloat]			        _AdvancedDissolveEdgeGIMetaPassMultiplier("", Float) = 1

//Keywords
[HideInInspector][AdvancedDissolveKeywordState]                     _AdvancedDissolveKeywordState("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordCutoutStandardSource]      _AdvancedDissolveKeywordCutoutStandardSource("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordCutoutStandardMappingType] _AdvancedDissolveKeywordCutoutStandardSourceMapsMappingType("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordCutoutGeometricType]       _AdvancedDissolveKeywordCutoutGeometricType("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordCutoutGeometricCount]      _AdvancedDissolveKeywordCutoutGeometricCount("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordEdgeBaseSource]            _AdvancedDissolveKeywordEdgeBaseSource("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordEdgeAdditionalColorSource] _AdvancedDissolveKeywordEdgeAdditionalColorSource("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordEdgeUVDistortionSource]    _AdvancedDissolveKeywordEdgeUVDistortionSource("", INT) = 0
[HideInInspector][AdvancedDissolveKeywordGlobalControlID]           _AdvancedDissolveKeywordGlobalControlID("", INT) = 0

//BakedKeywords
[HideInInspector]                                                   _AdvancedDissolveBakedKeywords("", Vector) = (0,0,0,0)	

//Advanced Dissolve Properties End////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
 Pass {
  Tags { "LIGHTMODE"="Vertex" "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
  ZWrite Off
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#include "UnityCG.cginc"
#pragma multi_compile_fog
#define USING_FOG (defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2))


//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


//Advanced Dissolve Keywords Start///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#pragma shader_feature_local   _AD_STATE_ENABLED
#pragma shader_feature_local _ _AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA				  _AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP                     _AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS _AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS _AD_CUTOUT_STANDARD_SOURCE_USER_DEFINED
#pragma shader_feature_local _ _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
#pragma shader_feature_local _ _AD_CUTOUT_GEOMETRIC_TYPE_XYZ						  _AD_CUTOUT_GEOMETRIC_TYPE_PLANE                           _AD_CUTOUT_GEOMETRIC_TYPE_SPHERE           _AD_CUTOUT_GEOMETRIC_TYPE_CUBE               _AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE       _AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH
#pragma shader_feature_local _ _AD_CUTOUT_GEOMETRIC_COUNT_TWO					      _AD_CUTOUT_GEOMETRIC_COUNT_THREE                          _AD_CUTOUT_GEOMETRIC_COUNT_FOUR
#pragma shader_feature_local _ _AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD                   _AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC                     _AD_EDGE_BASE_SOURCE_ALL
#pragma shader_feature_local _ _AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR                   _AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP                      _AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP     _AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR     _AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED
#pragma shader_feature_local   _AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP    
#pragma shader_feature_local _ _AD_GLOBAL_CONTROL_ID_ONE                              _AD_GLOBAL_CONTROL_ID_TWO                                 _AD_GLOBAL_CONTROL_ID_THREE                _AD_GLOBAL_CONTROL_ID_FOUR
//Advanced Dissolve Keywords End/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#define ADVANCED_DISSOLVE_BUILTIN_RENDER_PIPELINE
#include "../../cginc/Core.cginc"
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


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

// Compile specialized variants for when positional (point/spot) and spot lights are present
#pragma multi_compile __ POINT SPOT

// Compute illumination from one light, given attenuation
half3 computeLighting (int idx, half3 dirToLight, half3 eyeNormal, half3 viewDir, half4 diffuseColor, half shininess, half atten, inout half3 specColor) {
  half NdotL = max(dot(eyeNormal, dirToLight), 0.0);
  // diffuse
  half3 color = NdotL * diffuseColor.rgb * unity_LightColor[idx].rgb;
  return color * atten;
}

// Compute attenuation & illumination from one light
half3 computeOneLight(int idx, float3 eyePosition, half3 eyeNormal, half3 viewDir, half4 diffuseColor, half shininess, inout half3 specColor) {
  float3 dirToLight = unity_LightPosition[idx].xyz;
  half att = 1.0;
  #if defined(POINT) || defined(SPOT)
    dirToLight -= eyePosition * unity_LightPosition[idx].w;
    // distance attenuation
    float distSqr = dot(dirToLight, dirToLight);
    att /= (1.0 + unity_LightAtten[idx].z * distSqr);
    if (unity_LightPosition[idx].w != 0 && distSqr > unity_LightAtten[idx].w) att = 0.0; // set to 0 if outside of range
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

// uniforms
half4 _EmisColor;
int4 unity_VertexLightParams; // x: light count, y: zero, z: one (y/z needed by d3d9 vs loop instruction)
float4 _MainTex_ST;

// vertex shader input data
struct appdata {
  float3 pos : POSITION;
  float3 normal : NORMAL;
  half4 color : COLOR;
  float3 uv0 : TEXCOORD0;
  float4 tangent : TANGENT;
  UNITY_VERTEX_INPUT_INSTANCE_ID
};

// vertex-to-fragment interpolators
struct v2f {
  fixed4 color : COLOR0;
  float2 uv0 : TEXCOORD0;
  #if USING_FOG
    fixed fog : TEXCOORD1;
  #endif
  float4 pos : SV_POSITION;
  UNITY_VERTEX_OUTPUT_STEREO

  //Advanced Dissolve
  float3 positionWS : TEXCOORD2;
  float3 normalWS   : TEXCOORD3;
  ADVANCED_DISSOLVE_UV(4)
};

// vertex shader
v2f vert (appdata IN) {
  v2f o;
  UNITY_SETUP_INSTANCE_ID(IN);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);


  //Curved World
	#if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
		#ifdef CURVEDWORLD_NORMAL_TRANSFORMATION_ON
			CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(IN.pos, IN.normal, IN.tangent)
		#else
			CURVEDWORLD_TRANSFORM_VERTEX(IN.pos)
		#endif
	#endif


  half4 color = IN.color;
  float3 eyePos = UnityObjectToViewPos(float4(IN.pos,1)).xyz;
  half3 eyeNormal = normalize (mul ((float3x3)UNITY_MATRIX_IT_MV, IN.normal).xyz);
  half3 viewDir = 0.0;
  // lighting
  half3 lcolor = _EmisColor.rgb + color.rgb * glstate_lightmodel_ambient.rgb;
  half3 specColor = 0.0;
  half shininess = 0 * 128.0;
  LIGHT_LOOP_ATTRIBUTE for (int il = 0; il < LIGHT_LOOP_LIMIT; ++il) {
    lcolor += computeOneLight(il, eyePos, eyeNormal, viewDir, color, shininess, specColor);
  }
  color.rgb = lcolor.rgb;
  color.a = color.a;
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
   o.positionWS = mul(unity_ObjectToWorld, float4(IN.pos, 1)).xyz;
  o.normalWS = UnityObjectToWorldNormal(IN.normal);
	ADVANCED_DISSOLVE_INIT_UV(o, IN.uv0.xy, o.pos)

  return o;
}

// textures
sampler2D _MainTex;

// fragment shader
fixed4 frag (v2f IN) : SV_Target {


//Advanced Dissolve////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#if defined(_AD_STATE_ENABLED)

  float4 dissolveBase = 0;
  #if defined(_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA) || defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
        dissolveBase = tex2D(_MainTex, IN.uv0.xy);
        //dissolveBase.rgb *= _Color.rgb;    // no _Color
  #endif


  ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE_USING_WS(IN, dissolveBase, IN.positionWS, IN.normalWS)
  AdvancedDissolveClip(cutoutSource);
    	
  float3 dissolveAlbedo = 0; 
  float3 dissolveEmission = 0;
  float dissolveBlend = AdvancedDissolveAlbedoEmission(cutoutSource, dissolveBase, dissolveAlbedo, dissolveEmission, IN.uv0.xy);

#endif
//Advanced Dissolve/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


  fixed4 col;
  fixed4 tex, tmp0, tmp1, tmp2;
  // SetTexture #0
  tex = tex2D (_MainTex, IN.uv0.xy);
  col = tex * IN.color;


//Advanced Dissolve///////////
#if defined(_AD_STATE_ENABLED)
    col.rgb = lerp(col.rgb, dissolveAlbedo, dissolveBlend);
    col.rgb += dissolveEmission * dissolveBlend;
#endif


  // fog
  #if USING_FOG
    col.rgb = lerp (unity_FogColor.rgb, col.rgb, IN.fog);
  #endif
  return col;
}

// texenvs
//! TexEnv0: 01010103 01010103 [_MainTex]
ENDCG
 }
}

CustomEditor "AmazingAssets.AdvancedDissolveEditor.DefaultUnlitShaderGUI"
}
