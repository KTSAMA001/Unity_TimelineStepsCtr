// Made with Amplify Shader Editor v1.9.0.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/PostEffect_KT"
{
	Properties
	{
		_TriPlaneMap("TriPlaneMap", 2D) = "white" {}
		_tilling("tilling", Vector) = (0,0,0,0)
		_normalScale("normalScale", Vector) = (0,0,0,0)
		_falloff("falloff", Float) = 0
		_LerpMin("LerpMin", Float) = 0
		_LerpMax("LerpMax", Float) = 0
		_EnvBridPostAmount("EnvBridPostAmount", Range( 0 , 1)) = 0
		_FadeWidth("FadeWidth", Range( 0 , 0.99)) = 0
		_BackGroundColor("BackGroundColor", Color) = (0,0,0,0)
		[HDR]_LineColor("LineColor", Color) = (0,0,0,0)
		_FadeHardness("FadeHardness", Range( 0.01 , 1)) = 0.01
		_Interval("Interval", Float) = 0
		_Vector0("Vector 0", Vector) = (0,0,0,0)

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		GrabPass{ }

		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
			#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
			#else
			#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
			#endif


			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#include "Assets/VFXAssets/SceneShowsPostProcessing/Shaders/EdgeByNormalDepth.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float4 _BackGroundColor;
			uniform float _FadeWidth;
			uniform float _FadeHardness;
			uniform float _LerpMin;
			uniform float _LerpMax;
			uniform float _EnvBridPostAmount;
			uniform float _Interval;
			uniform float4 _LineColor;
			uniform sampler2D _TriPlaneMap;
			uniform float _falloff;
			uniform float2 _tilling;
			uniform float3 _normalScale;
			uniform float2 _Vector0;
			ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
			float ScreenDepth288( float2 uv )
			{
				return LinearEyeDepth(tex2D(_CameraDepthTexture,uv).r);
			}
			
			float2 UnStereo( float2 UV )
			{
				#if UNITY_SINGLE_PASS_STEREO
				float4 scaleOffset = unity_StereoScaleOffset[ unity_StereoEyeIndex ];
				UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
				#endif
				return UV;
			}
			
			float ScreenDepth76_g12( float2 uv )
			{
				return tex2D(_CameraDepthTexture,uv).r;
			}
			
			float3 InvertDepthDir72_g12( float3 In )
			{
				float3 result = In;
				#if !defined(ASE_SRP_VERSION) || ASE_SRP_VERSION <= 70301
				result *= float3(1,1,-1);
				#endif
				return result;
			}
			
			float3 GetNormal283( float2 screenPos )
			{
				return  UnpackNormal( tex2D(_CameraGBufferTexture2,screenPos));
			}
			
			float4 TriPlaneTexMap( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
			{
				worldNormal*=normalScale;
				float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
				projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
				float3 nsign = sign( worldNormal );
				half4 xNorm; half4 yNorm; half4 zNorm;
				xNorm = tex2D( topTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) );
				yNorm = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) );
				zNorm = tex2D( topTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) );
				return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
			}
			
			float Edge( float2 uv, float _Sensitivity_Normal, float _Sensitivity_Depth, float4 ScreenParams )
			{
				return edgeByNormalDepth(uv,ScreenParams,_Sensitivity_Normal,_Sensitivity_Depth);
			}
			
			inline float4 ASE_ComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord1 = screenPos;
				
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float SmoothnessMin229 = saturate( _FadeWidth );
				float SmoothnessMax230 = saturate( ( _FadeWidth + _FadeHardness ) );
				float4 screenPos = i.ase_texcoord1;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 uv288 = ase_screenPosNorm.xy;
				float localScreenDepth288 = ScreenDepth288( uv288 );
				float lerpResult147 = lerp( _LerpMin , _LerpMax , _EnvBridPostAmount);
				float Depth_Offset_Eye237 = ( localScreenDepth288 - lerpResult147 );
				float temp_output_211_0 = distance( min( Depth_Offset_Eye237 , 0.0 ) , 0.0 );
				float smoothstepResult226 = smoothstep( SmoothnessMin229 , SmoothnessMax230 , saturate( ( temp_output_211_0 - _Interval ) ));
				float ScreenAreaMask277 = smoothstepResult226;
				sampler2D topTexMap129 = _TriPlaneMap;
				float2 UV22_g13 = ase_screenPosNorm.xy;
				float2 localUnStereo22_g13 = UnStereo( UV22_g13 );
				float2 break64_g12 = localUnStereo22_g13;
				float2 uv76_g12 = ase_screenPosNorm.xy;
				float localScreenDepth76_g12 = ScreenDepth76_g12( uv76_g12 );
				#ifdef UNITY_REVERSED_Z
				float staticSwitch38_g12 = ( 1.0 - localScreenDepth76_g12 );
				#else
				float staticSwitch38_g12 = localScreenDepth76_g12;
				#endif
				float3 appendResult39_g12 = (float3(break64_g12.x , break64_g12.y , staticSwitch38_g12));
				float4 appendResult42_g12 = (float4((appendResult39_g12*2.0 + -1.0) , 1.0));
				float4 temp_output_43_0_g12 = mul( unity_CameraInvProjection, appendResult42_g12 );
				float3 temp_output_46_0_g12 = ( (temp_output_43_0_g12).xyz / (temp_output_43_0_g12).w );
				float3 In72_g12 = temp_output_46_0_g12;
				float3 localInvertDepthDir72_g12 = InvertDepthDir72_g12( In72_g12 );
				float4 appendResult49_g12 = (float4(localInvertDepthDir72_g12 , 1.0));
				float3 worldPos129 = mul( unity_CameraToWorld, appendResult49_g12 ).xyz;
				float2 screenPos283 = ase_screenPosNorm.xy;
				float3 localGetNormal283 = GetNormal283( screenPos283 );
				float3 worldNormal129 = localGetNormal283;
				float falloff129 = _falloff;
				float2 tiling129 = _tilling;
				float3 normalScale129 = _normalScale;
				float3 index129 = float3( 1,1,1 );
				float4 localTriPlaneTexMap129 = TriPlaneTexMap( topTexMap129 , worldPos129 , worldNormal129 , falloff129 , tiling129 , normalScale129 , index129 );
				float TriPlaneColor215 = localTriPlaneTexMap129.x;
				float smoothstepResult152 = smoothstep( SmoothnessMin229 , SmoothnessMax230 , ( 1.0 - saturate( ( ( distance( max( Depth_Offset_Eye237 , 0.0 ) , 0.0 ) - _Interval ) + temp_output_211_0 ) ) ));
				float2 uv247 = ase_screenPosNorm.xy;
				float _Sensitivity_Normal247 = _Vector0.x;
				float _Sensitivity_Depth247 = _Vector0.y;
				float4 ScreenParams247 = ( 1.0 / _ScreenParams );
				float localEdge247 = Edge( uv247 , _Sensitivity_Normal247 , _Sensitivity_Depth247 , ScreenParams247 );
				float Edge275 = localEdge247;
				float4 lerpResult243 = lerp( ( _BackGroundColor * ( 1.0 - ScreenAreaMask277 ) ) , _LineColor , saturate( ( ( TriPlaneColor215 * smoothstepResult152 ) + ( smoothstepResult152 * Edge275 ) ) ));
				float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
				float4 screenColor227 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_grabScreenPos.xy/ase_grabScreenPos.w);
				
				
				finalColor = ( lerpResult243 + ( smoothstepResult226 * screenColor227 ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19001
2039;481;1054;794;-971.123;-2212.544;1.793063;True;False
Node;AmplifyShaderEditor.CommentaryNode;236;-1796.74,2040.424;Inherit;False;1037.363;929.5732;利用eye深度的偏移达成以自身为中心的范围变化;8;141;146;142;148;147;237;288;74;深度偏移组_Depth_Offset_Eye;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;148;-1728.081,2800.929;Inherit;False;Property;_EnvBridPostAmount;EnvBridPostAmount;6;0;Create;True;0;0;0;False;0;False;0;0.351;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-1723.776,2637.203;Inherit;False;Property;_LerpMax;LerpMax;5;0;Create;True;0;0;0;False;0;False;0;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;74;-1731.227,2089.406;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;141;-1746.74,2337.394;Inherit;True;Property;_LerpMin;LerpMin;4;0;Create;True;0;0;0;False;0;False;0;-12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;288;-1415.425,2109.481;Inherit;False;return LinearEyeDepth(tex2D(_CameraDepthTexture,uv).r)@;1;Create;1;True;uv;FLOAT2;0,0;In;;Inherit;False;ScreenDepth;True;False;0;;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;147;-1368.855,2290.256;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;142;-1170.05,2100.736;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;228;-177.5828,1606.683;Inherit;False;2164.598;832.1251;Comment;19;244;243;246;268;218;149;267;242;152;219;231;223;232;213;212;188;205;276;278;三平面映射的网格部分_Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;237;-994.1511,2275.05;Inherit;False;Depth_Offset_Eye;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;205;-147.9571,1930.833;Inherit;False;787.3201;296.8328;Comment;4;208;189;199;238;前面白色区域;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;206;-298.6457,2626.211;Inherit;False;777.9685;358.2688;Comment;4;224;211;210;239;后面白色区域;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;235;-1775.265,1410.95;Inherit;False;909.6319;418.3411;Smoothstep_Min_Max;7;154;153;155;157;158;229;230;平滑参数组_Smoothstep;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;238;-122.7921,1997.416;Inherit;False;237;Depth_Offset_Eye;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;153;-1725.265,1547.161;Inherit;False;Property;_FadeWidth;FadeWidth;7;0;Create;True;0;0;0;False;0;False;0;0;0;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;154;-1683.902,1713.291;Inherit;False;Property;_FadeHardness;FadeHardness;11;0;Create;True;0;0;0;False;0;False;0.01;1;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;214;-1783.364,379.8048;Inherit;False;1871.476;824.9829;三平面映射纹理颜色_Color;13;215;161;129;134;162;136;48;133;245;283;284;294;298;三平面映射纹理颜色_Color;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMaxOpNode;208;97.68365,1997.801;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;239;-267.5002,2686.949;Inherit;False;237;Depth_Offset_Eye;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;199;242.8325,1991.832;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;274;-1774.356,3165.424;Inherit;False;1369.485;837.1184;通过深度信息以及法线信息的变化程度判断边缘所在;8;247;254;257;264;256;258;249;275;场景内物体描边;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;188;8.661751,2372.131;Inherit;False;Property;_Interval;Interval;12;0;Create;True;0;0;0;False;0;False;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;284;-1709.038,922.8538;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMinOpNode;210;-54.42031,2694.606;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;155;-1379.489,1584.026;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;283;-1434.812,731.52;Inherit;False;return  UnpackNormal( tex2D(_CameraGBufferTexture2,screenPos))@;3;Create;1;True;screenPos;FLOAT2;0,0;In;;Inherit;False;GetNormal;True;False;0;;False;1;0;FLOAT2;0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;258;-1545.784,3644.171;Inherit;False;Constant;_Float0;Float 0;15;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;294;-1701.565,615.4881;Inherit;False;Reconstruct World Position From Depth_Fixed;-1;;12;76da3c854a6607044a74c08123099417;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;158;-1238.745,1591.624;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;189;478.4774,1995.609;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;134;-1156.565,978.4924;Inherit;False;Property;_normalScale;normalScale;2;0;Create;True;0;0;0;False;0;False;0,0,0;1,1,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector2Node;133;-1320.771,857.6954;Inherit;False;Property;_tilling;tilling;1;0;Create;True;0;0;0;False;0;False;0,0;2,2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TexturePropertyNode;48;-1398.405,429.8048;Inherit;True;Property;_TriPlaneMap;TriPlaneMap;0;0;Create;True;0;0;0;False;0;False;None;3c3b341fe87698f4198a4c9d90df17e6;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SaturateNode;157;-1386.393,1482.864;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;136;-1188.62,776.0643;Inherit;False;Property;_falloff;falloff;3;0;Create;True;0;0;0;False;0;False;0;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenParams;249;-1724.356,3694.326;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;211;92.71229,2693.982;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;129;-810.5811,662.4184;Inherit;False;worldNormal*=normalScale@$float3 projNormal = ( pow( abs( worldNormal ), falloff ) )@$projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001@$float3 nsign = sign( worldNormal )@$half4 xNorm@ half4 yNorm@ half4 zNorm@$xNorm = tex2D( topTexMap, tiling * worldPos.zy * float2(  nsign.x, 1.0 ) )@$yNorm = tex2D( topTexMap, tiling * worldPos.xz * float2(  nsign.y, 1.0 ) )@$zNorm = tex2D( topTexMap, tiling * worldPos.xy * float2( -nsign.z, 1.0 ) )@$return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z@;4;Create;7;True;topTexMap;SAMPLER2D;;In;;Inherit;False;True;worldPos;FLOAT3;0,0,0;In;;Inherit;False;True;worldNormal;FLOAT3;0,0,0;In;;Inherit;False;True;falloff;FLOAT;1;In;;Inherit;False;True;tiling;FLOAT2;1,1;In;;Inherit;False;True;normalScale;FLOAT3;1,1,1;In;;Inherit;False;True;index;FLOAT3;1,1,1;In;;Inherit;False;TriPlaneTexMap;False;False;0;;False;7;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;1;False;4;FLOAT2;1,1;False;5;FLOAT3;1,1,1;False;6;FLOAT3;1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;264;-1379.751,3215.424;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;257;-1349.384,3649.964;Inherit;False;2;0;FLOAT;1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;230;-1099.633,1676.778;Inherit;False;SmoothnessMax;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;224;326.208,2698.253;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;240;590.3241,2649.887;Inherit;False;694.8105;570.4497;这里拿到的是网格区域过后恢复的屏幕色彩区域;6;216;227;226;233;222;234;屏幕颜色组_ScreenColor;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;229;-1152.924,1460.95;Inherit;False;SmoothnessMin;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;256;-1542.594,3521.486;Inherit;False;Property;_Vector0;Vector 0;13;0;Create;True;0;0;0;False;0;False;0,0;0.1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;212;662.1229,1992.791;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;245;-411.7367,759.4903;Inherit;False;FLOAT4;1;0;FLOAT4;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.CustomExpressionNode;247;-1044.72,3348.723;Float;False;return edgeByNormalDepth(uv,ScreenParams,_Sensitivity_Normal,_Sensitivity_Depth)@;1;Create;4;True;uv;FLOAT2;0,0;In;;Inherit;False;True;_Sensitivity_Normal;FLOAT;0;In;;Inherit;False;True;_Sensitivity_Depth;FLOAT;0;In;;Inherit;False;True;ScreenParams;FLOAT4;0,0,0,0;In;;Inherit;False;Edge;False;False;0;;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;233;657.6437,2772.25;Inherit;False;229;SmoothnessMin;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;213;792.0396,1986.609;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;222;699.8126,2699.887;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;234;640.3241,2948.111;Inherit;False;230;SmoothnessMax;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;231;791.7364,2061.092;Inherit;False;229;SmoothnessMin;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;215;-207.792,691.4001;Inherit;False;TriPlaneColor;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;223;920.08,1986.344;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;232;789.5314,2165.132;Inherit;False;230;SmoothnessMax;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;275;-706.9831,3382.55;Inherit;False;Edge;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;226;892.0853,2725.77;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;152;1060.515,1985.956;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;277;1069.191,2523.742;Inherit;False;ScreenAreaMask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;276;1100.457,2355.688;Inherit;False;275;Edge;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;219;820.8311,1879.064;Inherit;False;215;TriPlaneColor;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;278;515.7295,1836.684;Inherit;False;277;ScreenAreaMask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;267;1309.285,2124.195;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;218;1270.153,1906.878;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;268;1517.901,1937.524;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;242;983.9315,1733.926;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;149;514.1155,1655.53;Inherit;False;Property;_BackGroundColor;BackGroundColor;8;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;244;1076.764,2127.152;Inherit;False;Property;_LineColor;LineColor;9;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;0,0.1754184,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;270;1684.367,2018.921;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;227;885.1786,2982.854;Inherit;False;Global;_GrabScreen0;Grab Screen 0;16;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;246;1171.467,1664.851;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;243;1703.433,1673.808;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;216;1144.708,2724.931;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;298;-1521.924,712.1248;Inherit;False;Reconstruct World Position From Depth;-1;;14;e7094bcbcc80eb140b2a3dbe6a861de8;0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;279;2104.631,3238.693;Inherit;False;215;TriPlaneColor;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;254;-1431.133,3446.148;Inherit;False;-1;;1;0;OBJECT;;False;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;220;2142.889,2701.248;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;162;-794.9302,480.4371;Inherit;False;Property;_GridTexColor;GridTexColor;10;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;161;-473.3665,591.6888;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;297;2395.245,2678.381;Float;False;True;-1;2;ASEMaterialInspector;100;3;Hidden/PostEffect_KT;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;;0;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;255;False;;255;False;;255;False;;7;False;;1;False;;1;False;;1;False;;7;False;;1;False;;1;False;;1;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;RenderType=Opaque=RenderType;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;False;2;Include;;False;;Native;Include;;True;3649bed50de4f934b813c438e68781a0;Custom;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;0;1;True;False;;False;0
WireConnection;288;0;74;0
WireConnection;147;0;141;0
WireConnection;147;1;146;0
WireConnection;147;2;148;0
WireConnection;142;0;288;0
WireConnection;142;1;147;0
WireConnection;237;0;142;0
WireConnection;208;0;238;0
WireConnection;199;0;208;0
WireConnection;210;0;239;0
WireConnection;155;0;153;0
WireConnection;155;1;154;0
WireConnection;283;0;284;0
WireConnection;158;0;155;0
WireConnection;189;0;199;0
WireConnection;189;1;188;0
WireConnection;157;0;153;0
WireConnection;211;0;210;0
WireConnection;129;0;48;0
WireConnection;129;1;294;0
WireConnection;129;2;283;0
WireConnection;129;3;136;0
WireConnection;129;4;133;0
WireConnection;129;5;134;0
WireConnection;257;0;258;0
WireConnection;257;1;249;0
WireConnection;230;0;158;0
WireConnection;224;0;211;0
WireConnection;224;1;188;0
WireConnection;229;0;157;0
WireConnection;212;0;189;0
WireConnection;212;1;211;0
WireConnection;245;0;129;0
WireConnection;247;0;264;0
WireConnection;247;1;256;1
WireConnection;247;2;256;2
WireConnection;247;3;257;0
WireConnection;213;0;212;0
WireConnection;222;0;224;0
WireConnection;215;0;245;0
WireConnection;223;0;213;0
WireConnection;275;0;247;0
WireConnection;226;0;222;0
WireConnection;226;1;233;0
WireConnection;226;2;234;0
WireConnection;152;0;223;0
WireConnection;152;1;231;0
WireConnection;152;2;232;0
WireConnection;277;0;226;0
WireConnection;267;0;152;0
WireConnection;267;1;276;0
WireConnection;218;0;219;0
WireConnection;218;1;152;0
WireConnection;268;0;218;0
WireConnection;268;1;267;0
WireConnection;242;0;278;0
WireConnection;270;0;268;0
WireConnection;246;0;149;0
WireConnection;246;1;242;0
WireConnection;243;0;246;0
WireConnection;243;1;244;0
WireConnection;243;2;270;0
WireConnection;216;0;226;0
WireConnection;216;1;227;0
WireConnection;220;0;243;0
WireConnection;220;1;216;0
WireConnection;161;0;162;0
WireConnection;161;1;129;0
WireConnection;297;0;220;0
ASEEND*/
//CHKSM=E9D67850BADD23E51A24E8D190698B757D53D758