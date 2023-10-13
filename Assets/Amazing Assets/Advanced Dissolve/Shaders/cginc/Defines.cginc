#ifndef ADVANCED_DISSOLVE_DEFINES_CGINC
#define ADVANCED_DISSOLVE_DEFINES_CGINC



#define CUTOUT_CLIP_COEF 1.001


//Limitations//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#if SHADER_TARGET < 30

	//Only one texture for triplanar mapping
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
		#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS)
		#undef      _AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS
		#define     _AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP
		#endif

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
		#undef      _AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS
		#define     _AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP
		#endif
	#endif

#endif


#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)		//No distortion for ShaderGraph
	#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
	#undef      _AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP
	#endif
#else
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_USER_DEFINED)
	#undef      _AD_CUTOUT_STANDARD_SOURCE_USER_DEFINED
	#endif  

	#if defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
	#undef      _AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED
	#endif
#endif


#if defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)	//No distortion for TextMeshPro
	#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
	#undef      _AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP
	#endif
#endif



#if !defined(_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA) && !defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) && !defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) && !defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS) && !defined(_AD_CUTOUT_STANDARD_SOURCE_USER_DEFINED)
	#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
	#define      _AD_CUTOUT_STANDARD_SOURCE_NONE
	#endif
#endif

#if !defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) && !defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) && !defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
	#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR
	#undef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR
	#endif

	#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
	#undef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
	#endif
#endif


#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ) || defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) || defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE) || defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE) || defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE) || defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)
	#ifndef ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED
	#define ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED
	#endif
#endif



#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD)

	//No Cutout edge without Cutout
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
	#undef      _AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD
	#endif

#endif

#if defined(_AD_EDGE_BASE_SOURCE_ALL)

	#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
		
		#undef _AD_EDGE_BASE_SOURCE_ALL

		#if !defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)		
			#define _AD_EDGE_BASE_SOURCE_NONE		        //No edge
		#else
			#define _AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC			//Only Cutout edge
		#endif
	#endif

#endif


#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
//good
#else
	#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
	#undef      _AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR
	#endif

	#if defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)
	#undef      _AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP
	#endif

	#if defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)
	#undef      _AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP
	#endif

	#if defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)
	#undef      _AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR
	#endif

	#if defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
	#undef      _AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED
	#endif
#endif



#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC)

	//No Geometric edge width without Geometric cutout
	#if !defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)
	#undef       _AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC
	#endif

#endif

#if defined(_AD_EDGE_BASE_SOURCE_ALL)

	#if !defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)
		
		#undef _AD_EDGE_BASE_SOURCE_ALL

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)		
			#define _AD_EDGE_BASE_SOURCE_NONE		//No edge
		#else
			#define _AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD			//Only Cutout edge
		#endif
	#endif

#endif
//Limitations//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





#if defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO) || defined(ADVANCED_DISSOLVE_BUILTIN_RENDER_PIPELINE)
	#define ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE
#endif


#ifdef ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE
	#define READ_TEXTURE_2D(t,s,uv,i)        saturate(tex2D(t, (uv).xy) + (1 - i).xxxx)
	#define READ_TEXTURE_2D_LOD(t,s,uv,m)    tex2Dlod(t, float4((uv).xy, 0, m))
#else
	#define READ_TEXTURE_2D(t,s,uv,i)        saturate(SAMPLE_TEXTURE2D(t, s, (uv).xy) + (1 - i).xxxx)
	#define READ_TEXTURE_2D_LOD(t,s,uv,m)    SAMPLE_TEXTURE2D_LOD(t, s, (uv).xy, m)
#endif



#if defined(_AD_GLOBAL_CONTROL_ID_ONE)

	#define DISSOLVE_PROP_INT(v)	  uniform int v##_ID1;
	#define DISSOLVE_PROP_FLOAT(v)    uniform float v##_ID1; 
	#define DISSOLVE_PROP_FLOAT2(v)   uniform float2 v##_ID1;
	#define DISSOLVE_PROP_FLOAT3(v)   uniform float3 v##_ID1;
	#define DISSOLVE_PROP_FLOAT4(v)   uniform float4 v##_ID1;
	#define DISSOLVE_PROP_FLOAT4X4(v) uniform float4x4 v##_ID1;

	#if defined(ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE)
		#define DECLARE_TEXTURE_2D(t) uniform sampler2D t##_ID1;  
	#else
		#define DECLARE_TEXTURE_2D(t) uniform TEXTURE2D(t##_ID1); SAMPLER(sampler##t##_ID1);
	#endif

#elif defined(_AD_GLOBAL_CONTROL_ID_TWO)

	#define DISSOLVE_PROP_INT(v)	  uniform int v##_ID2;
	#define DISSOLVE_PROP_FLOAT(v)    uniform float v##_ID2; 
	#define DISSOLVE_PROP_FLOAT2(v)   uniform float2 v##_ID2;
	#define DISSOLVE_PROP_FLOAT3(v)   uniform float3 v##_ID2;
	#define DISSOLVE_PROP_FLOAT4(v)   uniform float4 v##_ID2;
	#define DISSOLVE_PROP_FLOAT4X4(v) uniform float4x4 v##_ID2;

	#if defined(ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE)
		#define DECLARE_TEXTURE_2D(t) uniform sampler2D t##_ID2;  
	#else
		#define DECLARE_TEXTURE_2D(t) uniform TEXTURE2D(t##_ID2); SAMPLER(sampler##t##_ID2);
	#endif

#elif defined(_AD_GLOBAL_CONTROL_ID_THREE)

	#define DISSOLVE_PROP_INT(v)	  uniform int v##_ID3;
	#define DISSOLVE_PROP_FLOAT(v)    uniform float v##_ID3; 
	#define DISSOLVE_PROP_FLOAT2(v)   uniform float2 v##_ID3;
	#define DISSOLVE_PROP_FLOAT3(v)   uniform float3 v##_ID3;
	#define DISSOLVE_PROP_FLOAT4(v)   uniform float4 v##_ID3;
	#define DISSOLVE_PROP_FLOAT4X4(v) uniform float4x4 v##_ID3;

	#if defined(ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE)
		#define DECLARE_TEXTURE_2D(t) uniform sampler2D t##_ID3;  
	#else
		#define DECLARE_TEXTURE_2D(t) uniform TEXTURE2D(t##_ID3); SAMPLER(sampler##t##_ID3);
	#endif

#elif defined(_AD_GLOBAL_CONTROL_ID_FOUR)

	#define DISSOLVE_PROP_INT(v)	 uniform int v##_ID4;
	#define DISSOLVE_PROP_FLOAT(v)    uniform float v##_ID4; 
	#define DISSOLVE_PROP_FLOAT2(v)   uniform float2 v##_ID4;
	#define DISSOLVE_PROP_FLOAT3(v)   uniform float3 v##_ID4;
	#define DISSOLVE_PROP_FLOAT4(v)   uniform float4 v##_ID4;
	#define DISSOLVE_PROP_FLOAT4X4(v) uniform float4x4 v##_ID4;

	#if defined(ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE)
		#define DECLARE_TEXTURE_2D(t) uniform sampler2D t##_ID4;  
	#else
		#define DECLARE_TEXTURE_2D(t) uniform TEXTURE2D(t##_ID4); SAMPLER(sampler##t##_ID4);
	#endif

#else

	#define DISSOLVE_PROP_INT(v)      int v;
	#define DISSOLVE_PROP_FLOAT(v)    float v;	
	#define DISSOLVE_PROP_FLOAT2(v)   float2 v;	
	#define DISSOLVE_PROP_FLOAT3(v)   float3 v;
	#define DISSOLVE_PROP_FLOAT4(v)   float4 v;
	#define DISSOLVE_PROP_FLOAT4X4(v) float4x4 v;

	#if defined(ADVANCED_DISSOLVE_LEGACY_TEXTURE_SAMPLE)
		#define DECLARE_TEXTURE_2D(t) sampler2D t;  
	#else
		#define DECLARE_TEXTURE_2D(t) TEXTURE2D(t); SAMPLER(sampler##t); 
	#endif

#endif



#if defined(_AD_GLOBAL_CONTROL_ID_ONE)

	//Cutout/////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
		
		#define VALUE_CUTOUT_STANDARD_CLIP										_AdvancedDissolveCutoutStandardClip_ID1	

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			#define VALUE_CUTOUT_STANDARD_MAP1									_AdvancedDissolveCutoutStandardMap1_ID1
			#define VALUE_CUTOUT_STANDARD_MAP1_SAMPLER							sampler_AdvancedDissolveCutoutStandardMap1_ID1
			#define VALUE_CUTOUT_STANDARD_MAP1_INVERT							_AdvancedDissolveCutoutStandardMap1Invert_ID1
		

			#define VALUE_CUTOUT_STANDARD_MAP1_TILING							_AdvancedDissolveCutoutStandardMap1Tiling_ID1
			#define VALUE_CUTOUT_STANDARD_MAP1_OFFSET							_AdvancedDissolveCutoutStandardMap1Offset_ID1
			#define VALUE_CUTOUT_STANDARD_MAP1_SCROLL							_AdvancedDissolveCutoutStandardMap1Scroll_ID1
			#define VALUE_CUTOUT_STANDARD_MAP1_INTENSITY						_AdvancedDissolveCutoutStandardMap1Intensity_ID1
			#define VALUE_CUTOUT_STANDARD_MAP1_CHANNEL							_AdvancedDissolveCutoutStandardMap1Channel_ID1


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
				#define VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE		_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace_ID1
			#elif defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)
				#define VALUE_CUTOUT_STANDARD_MAPS_SCREEN_SPACE_UV_SCALE        _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale_ID1
			#endif


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP2								_AdvancedDissolveCutoutStandardMap2_ID1
				#define VALUE_CUTOUT_STANDARD_MAP2_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap2_ID1
				#define VALUE_CUTOUT_STANDARD_MAP2_INVERT						_AdvancedDissolveCutoutStandardMap2Invert_ID1
						
				#define VALUE_CUTOUT_STANDARD_MAP2_TILING						_AdvancedDissolveCutoutStandardMap2Tiling_ID1
				#define VALUE_CUTOUT_STANDARD_MAP2_OFFSET						_AdvancedDissolveCutoutStandardMap2Offset_ID1
				#define VALUE_CUTOUT_STANDARD_MAP2_SCROLL						_AdvancedDissolveCutoutStandardMap2Scroll_ID1
				#define VALUE_CUTOUT_STANDARD_MAP2_INTENSITY					_AdvancedDissolveCutoutStandardMap2Intensity_ID1
				#define VALUE_CUTOUT_STANDARD_MAP2_CHANNEL						_AdvancedDissolveCutoutStandardMap2Channel_ID1


				#define VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE					_AdvancedDissolveCutoutStandardMapsBlendType_ID1
			#endif

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP3								_AdvancedDissolveCutoutStandardMap3_ID1
				#define VALUE_CUTOUT_STANDARD_MAP3_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap3_ID1
				#define VALUE_CUTOUT_STANDARD_MAP3_INVERT						_AdvancedDissolveCutoutStandardMap3Invert_ID1

		
				#define VALUE_CUTOUT_STANDARD_MAP3_TILING						_AdvancedDissolveCutoutStandardMap3Tiling_ID1
				#define VALUE_CUTOUT_STANDARD_MAP3_OFFSET						_AdvancedDissolveCutoutStandardMap3Offset_ID1
				#define VALUE_CUTOUT_STANDARD_MAP3_SCROLL						_AdvancedDissolveCutoutStandardMap3Scroll_ID1
				#define VALUE_CUTOUT_STANDARD_MAP3_INTENSITY					_AdvancedDissolveCutoutStandardMap3Intensity_ID1
				#define VALUE_CUTOUT_STANDARD_MAP3_CHANNEL						_AdvancedDissolveCutoutStandardMap3Channel_ID1
			#endif

		#else
			#define VALUE_CUTOUT_STANDARD_BASE_INVERT							_AdvancedDissolveCutoutStandardBaseInvert_ID1
		#endif

	#endif


	//Geometric/////////////////////////////////////////////////////////////////////////////////
	#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

		#define VALUE_GEOMETRIC_NOISE											_AdvancedDissolveCutoutGeometricNoise_ID1
		#define VALUE_GEOMETRIC_INVERT											_AdvancedDissolveCutoutGeometricInvert_ID1


		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
				
			#define VALUE_GEOMETRIC_XYZ_AXIS									_AdvancedDissolveCutoutGeometricXYZAxis_ID1
			#define VALUE_GEOMETRIC_XYZ_STYLE									_AdvancedDissolveCutoutGeometricXYZStyle_ID1
			#define VALUE_GEOMETRIC_XYZ_SPACE									_AdvancedDissolveCutoutGeometricXYZSpace_ID1
			#define VALUE_GEOMETRIC_XYZ_ROLLOUT									_AdvancedDissolveCutoutGeometricXYZRollout_ID1
			#define VALUE_GEOMETRIC_XYZ_POSITION								_AdvancedDissolveCutoutGeometricXYZPosition_ID1

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) 
			
			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID1
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID1

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID1
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID1

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID1
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID1
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID1
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID1

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID1
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID1
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID1
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID1

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
			
			#define VALUE_GEOMETRIC_SIZE		   								_AdvancedDissolveCutoutGeometric1Size_ID1
			#define VALUE_GEOMETRIC_MATRIX_TRS									_AdvancedDissolveCutoutGeometric1MatrixTRS_ID1

		#endif


		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO) || defined (_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID1
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID1
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID1
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID1
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID1
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)
				
				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID1
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID1
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID1
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_2_SIZE 									_AdvancedDissolveCutoutGeometric2Size_ID1
				#define VALUE_GEOMETRIC_2_TRS									_AdvancedDissolveCutoutGeometric2MatrixTRS_ID1

			#endif

		#endif //Two

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)	|| defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID1
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID1
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID1
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID1
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID1
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID1
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID1
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID1
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
				  
				#define VALUE_GEOMETRIC_3_SIZE									_AdvancedDissolveCutoutGeometric3Size_ID1
				#define VALUE_GEOMETRIC_3_TRS									_AdvancedDissolveCutoutGeometric3MatrixTRS_ID1

			#endif

		#endif //Three

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID1
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID1
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID1
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID1
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID1
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID1
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID1
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID1
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID1

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_4_SIZE 									_AdvancedDissolveCutoutGeometric4Size_ID1
				#define VALUE_GEOMETRIC_4_TRS									_AdvancedDissolveCutoutGeometric4MatrixTRS_ID1

			#endif

		#endif //Four

	#endif
		

	//Edge///////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_EDGE_BASE_SOURCE_NONE)

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_STANDARD								_AdvancedDissolveEdgeBaseWidthStandard_ID1
		#endif

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_GEOMETRIC   							_AdvancedDissolveEdgeBaseWidthGeometric_ID1
		#endif


		#define VALUE_EDGE_BASE_SHAPE											_AdvancedDissolveEdgeBaseShape_ID1
		#define VALUE_EDGE_BASE_COLOR											_AdvancedDissolveEdgeBaseColor_ID1
		#define VALUE_EDGE_BASE_COLOR_INTENSITY									_AdvancedDissolveEdgeBaseColorIntensity_ID1
		#define VALUE_EDGE_BASE_COLOR_TRANSPARENCY								_AdvancedDissolveEdgeBaseColorTransparency_ID1


		//Additional Color///////////////////////////////////////////////////////////////////////////
		#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID1

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID1

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_MIPMAP						_AdvancedDissolveEdgeAdditionalColorMapMipmap_ID1

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID1

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID1

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE						_AdvancedDissolveEdgeAdditionalColorMapReverse_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID1

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR									_AdvancedDissolveEdgeAdditionalColor_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_INTENSITY 						_AdvancedDissolveEdgeAdditionalColorIntensity_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY					_AdvancedDissolveEdgeAdditionalColorTransparency_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET					_AdvancedDissolveEdgeAdditionalColorPhaseOffset_ID1
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID1

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID1

		#endif


		//UV Distortion//////////////////////////////////////////////////////////////////////////////
		#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			#define VALUE_EDGE_UV_DISTORTION_STRENGTH							_AdvancedDissolveEdgeUVDistortionStrength_ID1

			#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
				#define VALUE_EDGE_UV_DISTORTION_MAP							_AdvancedDissolveEdgeUVDistortionMap_ID1
				#define VALUE_EDGE_UV_DISTORTION_MAP_SAMPLER 					sampler_AdvancedDissolveEdgeUVDistortionMap_ID1

				#define VALUE_EDGE_UV_DISTORTION_MAP_TILING						_AdvancedDissolveEdgeUVDistortionMapTiling_ID1
				#define VALUE_EDGE_UV_DISTORTION_MAP_OFFSET						_AdvancedDissolveEdgeUVDistortionMapOffset_ID1
				#define VALUE_EDGE_UV_DISTORTION_MAP_SCROLL						_AdvancedDissolveEdgeUVDistortionMapScroll_ID1
			#endif
		#endif	


		//GI/////////////////////////////////////////////////////////////////////////////////////////
		#if defined(ADVANCED_DISSOLVE_META_PASS)
			#define VALUE_EDGE_GI_META_PASS_MULTIPLIER							_AdvancedDissolveEdgeGIMetaPassMultiplier_ID1
		#endif

	#endif

#elif defined(_AD_GLOBAL_CONTROL_ID_TWO)

	//Cutout/////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
		
		#define VALUE_CUTOUT_STANDARD_CLIP										_AdvancedDissolveCutoutStandardClip_ID2	

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			#define VALUE_CUTOUT_STANDARD_MAP1									_AdvancedDissolveCutoutStandardMap1_ID2
			#define VALUE_CUTOUT_STANDARD_MAP1_SAMPLER							sampler_AdvancedDissolveCutoutStandardMap1_ID2
			#define VALUE_CUTOUT_STANDARD_MAP1_INVERT							_AdvancedDissolveCutoutStandardMap1Invert_ID2
		

			#define VALUE_CUTOUT_STANDARD_MAP1_TILING							_AdvancedDissolveCutoutStandardMap1Tiling_ID2
			#define VALUE_CUTOUT_STANDARD_MAP1_OFFSET							_AdvancedDissolveCutoutStandardMap1Offset_ID2
			#define VALUE_CUTOUT_STANDARD_MAP1_SCROLL							_AdvancedDissolveCutoutStandardMap1Scroll_ID2
			#define VALUE_CUTOUT_STANDARD_MAP1_INTENSITY						_AdvancedDissolveCutoutStandardMap1Intensity_ID2
			#define VALUE_CUTOUT_STANDARD_MAP1_CHANNEL							_AdvancedDissolveCutoutStandardMap1Channel_ID2


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
				#define VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE		_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace_ID2
			#elif defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)
				#define VALUE_CUTOUT_STANDARD_MAPS_SCREEN_SPACE_UV_SCALE        _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale_ID2
			#endif


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP2								_AdvancedDissolveCutoutStandardMap2_ID2
				#define VALUE_CUTOUT_STANDARD_MAP2_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap2_ID2
				#define VALUE_CUTOUT_STANDARD_MAP2_INVERT						_AdvancedDissolveCutoutStandardMap2Invert_ID2
						
				#define VALUE_CUTOUT_STANDARD_MAP2_TILING						_AdvancedDissolveCutoutStandardMap2Tiling_ID2
				#define VALUE_CUTOUT_STANDARD_MAP2_OFFSET						_AdvancedDissolveCutoutStandardMap2Offset_ID2
				#define VALUE_CUTOUT_STANDARD_MAP2_SCROLL						_AdvancedDissolveCutoutStandardMap2Scroll_ID2
				#define VALUE_CUTOUT_STANDARD_MAP2_INTENSITY					_AdvancedDissolveCutoutStandardMap2Intensity_ID2
				#define VALUE_CUTOUT_STANDARD_MAP2_CHANNEL						_AdvancedDissolveCutoutStandardMap2Channel_ID2


				#define VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE					_AdvancedDissolveCutoutStandardMapsBlendType_ID2
			#endif

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP3								_AdvancedDissolveCutoutStandardMap3_ID2
				#define VALUE_CUTOUT_STANDARD_MAP3_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap3_ID2
				#define VALUE_CUTOUT_STANDARD_MAP3_INVERT						_AdvancedDissolveCutoutStandardMap3Invert_ID2

		
				#define VALUE_CUTOUT_STANDARD_MAP3_TILING						_AdvancedDissolveCutoutStandardMap3Tiling_ID2
				#define VALUE_CUTOUT_STANDARD_MAP3_OFFSET						_AdvancedDissolveCutoutStandardMap3Offset_ID2
				#define VALUE_CUTOUT_STANDARD_MAP3_SCROLL						_AdvancedDissolveCutoutStandardMap3Scroll_ID2
				#define VALUE_CUTOUT_STANDARD_MAP3_INTENSITY					_AdvancedDissolveCutoutStandardMap3Intensity_ID2
				#define VALUE_CUTOUT_STANDARD_MAP3_CHANNEL						_AdvancedDissolveCutoutStandardMap3Channel_ID2
			#endif

		#else
			#define VALUE_CUTOUT_STANDARD_BASE_INVERT							_AdvancedDissolveCutoutStandardBaseInvert_ID2
		#endif

	#endif


	//Geometric/////////////////////////////////////////////////////////////////////////////////
	#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

		#define VALUE_GEOMETRIC_NOISE											_AdvancedDissolveCutoutGeometricNoise_ID2
		#define VALUE_GEOMETRIC_INVERT											_AdvancedDissolveCutoutGeometricInvert_ID2


		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
				
			#define VALUE_GEOMETRIC_XYZ_AXIS									_AdvancedDissolveCutoutGeometricXYZAxis_ID2
			#define VALUE_GEOMETRIC_XYZ_STYLE									_AdvancedDissolveCutoutGeometricXYZStyle_ID2
			#define VALUE_GEOMETRIC_XYZ_SPACE									_AdvancedDissolveCutoutGeometricXYZSpace_ID2
			#define VALUE_GEOMETRIC_XYZ_ROLLOUT									_AdvancedDissolveCutoutGeometricXYZRollout_ID2
			#define VALUE_GEOMETRIC_XYZ_POSITION								_AdvancedDissolveCutoutGeometricXYZPosition_ID2

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) 
			
			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID2
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID2

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID2
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID2

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID2
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID2
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID2
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID2

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID2
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID2
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID2
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID2

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
			
			#define VALUE_GEOMETRIC_SIZE		   								_AdvancedDissolveCutoutGeometric1Size_ID2
			#define VALUE_GEOMETRIC_MATRIX_TRS									_AdvancedDissolveCutoutGeometric1MatrixTRS_ID2

		#endif


		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO) || defined (_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID2
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID2
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID2
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID2
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID2
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)
				
				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID2
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID2
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID2
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_2_SIZE 									_AdvancedDissolveCutoutGeometric2Size_ID2
				#define VALUE_GEOMETRIC_2_TRS									_AdvancedDissolveCutoutGeometric2MatrixTRS_ID2

			#endif

		#endif //Two

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)	|| defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID2
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID2
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID2
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID2
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID2
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID2
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID2
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID2
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
				  
				#define VALUE_GEOMETRIC_3_SIZE									_AdvancedDissolveCutoutGeometric3Size_ID2
				#define VALUE_GEOMETRIC_3_TRS									_AdvancedDissolveCutoutGeometric3MatrixTRS_ID2

			#endif

		#endif //Three

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID2
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID2
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID2
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID2
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID2
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID2
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID2
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID2
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID2

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_4_SIZE 									_AdvancedDissolveCutoutGeometric4Size_ID2
				#define VALUE_GEOMETRIC_4_TRS									_AdvancedDissolveCutoutGeometric4MatrixTRS_ID2

			#endif

		#endif //Four

	#endif
		

	//Edge///////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_EDGE_BASE_SOURCE_NONE)

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_STANDARD								_AdvancedDissolveEdgeBaseWidthStandard_ID2
		#endif

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_GEOMETRIC   							_AdvancedDissolveEdgeBaseWidthGeometric_ID2
		#endif


		#define VALUE_EDGE_BASE_SHAPE											_AdvancedDissolveEdgeBaseShape_ID2
		#define VALUE_EDGE_BASE_COLOR											_AdvancedDissolveEdgeBaseColor_ID2
		#define VALUE_EDGE_BASE_COLOR_INTENSITY									_AdvancedDissolveEdgeBaseColorIntensity_ID2
		#define VALUE_EDGE_BASE_COLOR_TRANSPARENCY								_AdvancedDissolveEdgeBaseColorTransparency_ID2


		//Additional Color///////////////////////////////////////////////////////////////////////////
		#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID2

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID2

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_MIPMAP						_AdvancedDissolveEdgeAdditionalColorMapMipmap_ID2

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID2

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID2

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE						_AdvancedDissolveEdgeAdditionalColorMapReverse_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID2

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR									_AdvancedDissolveEdgeAdditionalColor_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_INTENSITY 						_AdvancedDissolveEdgeAdditionalColorIntensity_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY					_AdvancedDissolveEdgeAdditionalColorTransparency_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET					_AdvancedDissolveEdgeAdditionalColorPhaseOffset_ID2
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID2

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID2

		#endif


		//UV Distortion//////////////////////////////////////////////////////////////////////////////
		#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			#define VALUE_EDGE_UV_DISTORTION_STRENGTH							_AdvancedDissolveEdgeUVDistortionStrength_ID2

			#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
				#define VALUE_EDGE_UV_DISTORTION_MAP							_AdvancedDissolveEdgeUVDistortionMap_ID2
				#define VALUE_EDGE_UV_DISTORTION_MAP_SAMPLER 					sampler_AdvancedDissolveEdgeUVDistortionMap_ID2

				#define VALUE_EDGE_UV_DISTORTION_MAP_TILING						_AdvancedDissolveEdgeUVDistortionMapTiling_ID2
				#define VALUE_EDGE_UV_DISTORTION_MAP_OFFSET						_AdvancedDissolveEdgeUVDistortionMapOffset_ID2
				#define VALUE_EDGE_UV_DISTORTION_MAP_SCROLL						_AdvancedDissolveEdgeUVDistortionMapScroll_ID2
			#endif
		#endif	


		//GI/////////////////////////////////////////////////////////////////////////////////////////
		#if defined(ADVANCED_DISSOLVE_META_PASS)
			#define VALUE_EDGE_GI_META_PASS_MULTIPLIER							_AdvancedDissolveEdgeGIMetaPassMultiplier_ID2
		#endif

	#endif

#elif defined(_AD_GLOBAL_CONTROL_ID_THREE)

	//Cutout/////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
		
		#define VALUE_CUTOUT_STANDARD_CLIP										_AdvancedDissolveCutoutStandardClip_ID3	

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			#define VALUE_CUTOUT_STANDARD_MAP1									_AdvancedDissolveCutoutStandardMap1_ID3
			#define VALUE_CUTOUT_STANDARD_MAP1_SAMPLER							sampler_AdvancedDissolveCutoutStandardMap1_ID3
			#define VALUE_CUTOUT_STANDARD_MAP1_INVERT							_AdvancedDissolveCutoutStandardMap1Invert_ID3
		

			#define VALUE_CUTOUT_STANDARD_MAP1_TILING							_AdvancedDissolveCutoutStandardMap1Tiling_ID3
			#define VALUE_CUTOUT_STANDARD_MAP1_OFFSET							_AdvancedDissolveCutoutStandardMap1Offset_ID3
			#define VALUE_CUTOUT_STANDARD_MAP1_SCROLL							_AdvancedDissolveCutoutStandardMap1Scroll_ID3
			#define VALUE_CUTOUT_STANDARD_MAP1_INTENSITY						_AdvancedDissolveCutoutStandardMap1Intensity_ID3
			#define VALUE_CUTOUT_STANDARD_MAP1_CHANNEL							_AdvancedDissolveCutoutStandardMap1Channel_ID3


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
				#define VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE		_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace_ID3
			#elif defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)
				#define VALUE_CUTOUT_STANDARD_MAPS_SCREEN_SPACE_UV_SCALE        _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale_ID3
			#endif


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP2								_AdvancedDissolveCutoutStandardMap2_ID3
				#define VALUE_CUTOUT_STANDARD_MAP2_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap2_ID3
				#define VALUE_CUTOUT_STANDARD_MAP2_INVERT						_AdvancedDissolveCutoutStandardMap2Invert_ID3
						
				#define VALUE_CUTOUT_STANDARD_MAP2_TILING						_AdvancedDissolveCutoutStandardMap2Tiling_ID3
				#define VALUE_CUTOUT_STANDARD_MAP2_OFFSET						_AdvancedDissolveCutoutStandardMap2Offset_ID3
				#define VALUE_CUTOUT_STANDARD_MAP2_SCROLL						_AdvancedDissolveCutoutStandardMap2Scroll_ID3
				#define VALUE_CUTOUT_STANDARD_MAP2_INTENSITY					_AdvancedDissolveCutoutStandardMap2Intensity_ID3
				#define VALUE_CUTOUT_STANDARD_MAP2_CHANNEL						_AdvancedDissolveCutoutStandardMap2Channel_ID3


				#define VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE					_AdvancedDissolveCutoutStandardMapsBlendType_ID3
			#endif

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP3								_AdvancedDissolveCutoutStandardMap3_ID3
				#define VALUE_CUTOUT_STANDARD_MAP3_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap3_ID3
				#define VALUE_CUTOUT_STANDARD_MAP3_INVERT						_AdvancedDissolveCutoutStandardMap3Invert_ID3

		
				#define VALUE_CUTOUT_STANDARD_MAP3_TILING						_AdvancedDissolveCutoutStandardMap3Tiling_ID3
				#define VALUE_CUTOUT_STANDARD_MAP3_OFFSET						_AdvancedDissolveCutoutStandardMap3Offset_ID3
				#define VALUE_CUTOUT_STANDARD_MAP3_SCROLL						_AdvancedDissolveCutoutStandardMap3Scroll_ID3
				#define VALUE_CUTOUT_STANDARD_MAP3_INTENSITY					_AdvancedDissolveCutoutStandardMap3Intensity_ID3
				#define VALUE_CUTOUT_STANDARD_MAP3_CHANNEL						_AdvancedDissolveCutoutStandardMap3Channel_ID3
			#endif

		#else
			#define VALUE_CUTOUT_STANDARD_BASE_INVERT							_AdvancedDissolveCutoutStandardBaseInvert_ID3
		#endif

	#endif


	//Geometric/////////////////////////////////////////////////////////////////////////////////
	#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

		#define VALUE_GEOMETRIC_NOISE											_AdvancedDissolveCutoutGeometricNoise_ID3
		#define VALUE_GEOMETRIC_INVERT											_AdvancedDissolveCutoutGeometricInvert_ID3


		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
				
			#define VALUE_GEOMETRIC_XYZ_AXIS									_AdvancedDissolveCutoutGeometricXYZAxis_ID3
			#define VALUE_GEOMETRIC_XYZ_STYLE									_AdvancedDissolveCutoutGeometricXYZStyle_ID3
			#define VALUE_GEOMETRIC_XYZ_SPACE									_AdvancedDissolveCutoutGeometricXYZSpace_ID3
			#define VALUE_GEOMETRIC_XYZ_ROLLOUT									_AdvancedDissolveCutoutGeometricXYZRollout_ID3
			#define VALUE_GEOMETRIC_XYZ_POSITION								_AdvancedDissolveCutoutGeometricXYZPosition_ID3

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) 
			
			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID3
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID3

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID3
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID3

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID3
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID3
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID3
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID3

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID3
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID3
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID3
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID3

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
			
			#define VALUE_GEOMETRIC_SIZE		   								_AdvancedDissolveCutoutGeometric1Size_ID3
			#define VALUE_GEOMETRIC_MATRIX_TRS									_AdvancedDissolveCutoutGeometric1MatrixTRS_ID3

		#endif


		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO) || defined (_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID3
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID3
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID3
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID3
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID3
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)
				
				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID3
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID3
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID3
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_2_SIZE 									_AdvancedDissolveCutoutGeometric2Size_ID3
				#define VALUE_GEOMETRIC_2_TRS									_AdvancedDissolveCutoutGeometric2MatrixTRS_ID3

			#endif

		#endif //Two

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)	|| defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID3
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID3
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID3
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID3
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID3
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID3
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID3
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID3
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
				  
				#define VALUE_GEOMETRIC_3_SIZE									_AdvancedDissolveCutoutGeometric3Size_ID3
				#define VALUE_GEOMETRIC_3_TRS									_AdvancedDissolveCutoutGeometric3MatrixTRS_ID3

			#endif

		#endif //Three

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID3
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID3
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID3
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID3
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID3
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID3
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID3
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID3
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID3

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_4_SIZE 									_AdvancedDissolveCutoutGeometric4Size_ID3
				#define VALUE_GEOMETRIC_4_TRS									_AdvancedDissolveCutoutGeometric4MatrixTRS_ID3

			#endif

		#endif //Four

	#endif
		

	//Edge///////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_EDGE_BASE_SOURCE_NONE)

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_STANDARD								_AdvancedDissolveEdgeBaseWidthStandard_ID3
		#endif

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_GEOMETRIC   							_AdvancedDissolveEdgeBaseWidthGeometric_ID3
		#endif


		#define VALUE_EDGE_BASE_SHAPE											_AdvancedDissolveEdgeBaseShape_ID3
		#define VALUE_EDGE_BASE_COLOR											_AdvancedDissolveEdgeBaseColor_ID3
		#define VALUE_EDGE_BASE_COLOR_INTENSITY									_AdvancedDissolveEdgeBaseColorIntensity_ID3
		#define VALUE_EDGE_BASE_COLOR_TRANSPARENCY								_AdvancedDissolveEdgeBaseColorTransparency_ID3


		//Additional Color///////////////////////////////////////////////////////////////////////////
		#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID3

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID3

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_MIPMAP						_AdvancedDissolveEdgeAdditionalColorMapMipmap_ID3

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID3

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID3

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE						_AdvancedDissolveEdgeAdditionalColorMapReverse_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID3

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR									_AdvancedDissolveEdgeAdditionalColor_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_INTENSITY 						_AdvancedDissolveEdgeAdditionalColorIntensity_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY					_AdvancedDissolveEdgeAdditionalColorTransparency_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET					_AdvancedDissolveEdgeAdditionalColorPhaseOffset_ID3
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID3

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID3

		#endif


		//UV Distortion//////////////////////////////////////////////////////////////////////////////
		#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			#define VALUE_EDGE_UV_DISTORTION_STRENGTH							_AdvancedDissolveEdgeUVDistortionStrength_ID3

			#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
				#define VALUE_EDGE_UV_DISTORTION_MAP							_AdvancedDissolveEdgeUVDistortionMap_ID3
				#define VALUE_EDGE_UV_DISTORTION_MAP_SAMPLER 					sampler_AdvancedDissolveEdgeUVDistortionMap_ID3

				#define VALUE_EDGE_UV_DISTORTION_MAP_TILING						_AdvancedDissolveEdgeUVDistortionMapTiling_ID3
				#define VALUE_EDGE_UV_DISTORTION_MAP_OFFSET						_AdvancedDissolveEdgeUVDistortionMapOffset_ID3
				#define VALUE_EDGE_UV_DISTORTION_MAP_SCROLL						_AdvancedDissolveEdgeUVDistortionMapScroll_ID3
			#endif
		#endif	


		//GI/////////////////////////////////////////////////////////////////////////////////////////
		#if defined(ADVANCED_DISSOLVE_META_PASS)
			#define VALUE_EDGE_GI_META_PASS_MULTIPLIER							_AdvancedDissolveEdgeGIMetaPassMultiplier_ID3
		#endif

	#endif

#elif defined(_AD_GLOBAL_CONTROL_ID_FOUR)

	//Cutout/////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
		
		#define VALUE_CUTOUT_STANDARD_CLIP										_AdvancedDissolveCutoutStandardClip_ID4	

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			#define VALUE_CUTOUT_STANDARD_MAP1									_AdvancedDissolveCutoutStandardMap1_ID4
			#define VALUE_CUTOUT_STANDARD_MAP1_SAMPLER							sampler_AdvancedDissolveCutoutStandardMap1_ID4
			#define VALUE_CUTOUT_STANDARD_MAP1_INVERT							_AdvancedDissolveCutoutStandardMap1Invert_ID4
		

			#define VALUE_CUTOUT_STANDARD_MAP1_TILING							_AdvancedDissolveCutoutStandardMap1Tiling_ID4
			#define VALUE_CUTOUT_STANDARD_MAP1_OFFSET							_AdvancedDissolveCutoutStandardMap1Offset_ID4
			#define VALUE_CUTOUT_STANDARD_MAP1_SCROLL							_AdvancedDissolveCutoutStandardMap1Scroll_ID4
			#define VALUE_CUTOUT_STANDARD_MAP1_INTENSITY						_AdvancedDissolveCutoutStandardMap1Intensity_ID4
			#define VALUE_CUTOUT_STANDARD_MAP1_CHANNEL							_AdvancedDissolveCutoutStandardMap1Channel_ID4


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
				#define VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE		_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace_ID4
			#elif defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)
				#define VALUE_CUTOUT_STANDARD_MAPS_SCREEN_SPACE_UV_SCALE        _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale_ID4
			#endif


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP2								_AdvancedDissolveCutoutStandardMap2_ID4
				#define VALUE_CUTOUT_STANDARD_MAP2_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap2_ID4
				#define VALUE_CUTOUT_STANDARD_MAP2_INVERT						_AdvancedDissolveCutoutStandardMap2Invert_ID4
						
				#define VALUE_CUTOUT_STANDARD_MAP2_TILING						_AdvancedDissolveCutoutStandardMap2Tiling_ID4
				#define VALUE_CUTOUT_STANDARD_MAP2_OFFSET						_AdvancedDissolveCutoutStandardMap2Offset_ID4
				#define VALUE_CUTOUT_STANDARD_MAP2_SCROLL						_AdvancedDissolveCutoutStandardMap2Scroll_ID4
				#define VALUE_CUTOUT_STANDARD_MAP2_INTENSITY					_AdvancedDissolveCutoutStandardMap2Intensity_ID4
				#define VALUE_CUTOUT_STANDARD_MAP2_CHANNEL						_AdvancedDissolveCutoutStandardMap2Channel_ID4


				#define VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE					_AdvancedDissolveCutoutStandardMapsBlendType_ID4
			#endif

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP3								_AdvancedDissolveCutoutStandardMap3_ID4
				#define VALUE_CUTOUT_STANDARD_MAP3_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap3_ID4
				#define VALUE_CUTOUT_STANDARD_MAP3_INVERT						_AdvancedDissolveCutoutStandardMap3Invert_ID4

		
				#define VALUE_CUTOUT_STANDARD_MAP3_TILING						_AdvancedDissolveCutoutStandardMap3Tiling_ID4
				#define VALUE_CUTOUT_STANDARD_MAP3_OFFSET						_AdvancedDissolveCutoutStandardMap3Offset_ID4
				#define VALUE_CUTOUT_STANDARD_MAP3_SCROLL						_AdvancedDissolveCutoutStandardMap3Scroll_ID4
				#define VALUE_CUTOUT_STANDARD_MAP3_INTENSITY					_AdvancedDissolveCutoutStandardMap3Intensity_ID4
				#define VALUE_CUTOUT_STANDARD_MAP3_CHANNEL						_AdvancedDissolveCutoutStandardMap3Channel_ID4
			#endif

		#else
			#define VALUE_CUTOUT_STANDARD_BASE_INVERT							_AdvancedDissolveCutoutStandardBaseInvert_ID4
		#endif

	#endif


	//Geometric/////////////////////////////////////////////////////////////////////////////////
	#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

		#define VALUE_GEOMETRIC_NOISE											_AdvancedDissolveCutoutGeometricNoise_ID4
		#define VALUE_GEOMETRIC_INVERT											_AdvancedDissolveCutoutGeometricInvert_ID4


		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
				
			#define VALUE_GEOMETRIC_XYZ_AXIS									_AdvancedDissolveCutoutGeometricXYZAxis_ID4
			#define VALUE_GEOMETRIC_XYZ_STYLE									_AdvancedDissolveCutoutGeometricXYZStyle_ID4
			#define VALUE_GEOMETRIC_XYZ_SPACE									_AdvancedDissolveCutoutGeometricXYZSpace_ID4
			#define VALUE_GEOMETRIC_XYZ_ROLLOUT									_AdvancedDissolveCutoutGeometricXYZRollout_ID4
			#define VALUE_GEOMETRIC_XYZ_POSITION								_AdvancedDissolveCutoutGeometricXYZPosition_ID4

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) 
			
			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID4
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID4

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID4
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID4

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID4
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID4
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID4
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID4

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position_ID4
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal_ID4
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius_ID4
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height_ID4

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
			
			#define VALUE_GEOMETRIC_SIZE		   								_AdvancedDissolveCutoutGeometric1Size_ID4
			#define VALUE_GEOMETRIC_MATRIX_TRS									_AdvancedDissolveCutoutGeometric1MatrixTRS_ID4

		#endif


		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO) || defined (_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID4
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID4
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID4
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID4
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID4
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)
				
				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position_ID4
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal_ID4
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius_ID4
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_2_SIZE 									_AdvancedDissolveCutoutGeometric2Size_ID4
				#define VALUE_GEOMETRIC_2_TRS									_AdvancedDissolveCutoutGeometric2MatrixTRS_ID4

			#endif

		#endif //Two

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)	|| defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID4
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID4
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID4
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID4
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID4
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position_ID4
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal_ID4
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius_ID4
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
				  
				#define VALUE_GEOMETRIC_3_SIZE									_AdvancedDissolveCutoutGeometric3Size_ID4
				#define VALUE_GEOMETRIC_3_TRS									_AdvancedDissolveCutoutGeometric3MatrixTRS_ID4

			#endif

		#endif //Three

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID4
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID4
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID4
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID4
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID4
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position_ID4
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal_ID4
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius_ID4
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height_ID4

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_4_SIZE 									_AdvancedDissolveCutoutGeometric4Size_ID4
				#define VALUE_GEOMETRIC_4_TRS									_AdvancedDissolveCutoutGeometric4MatrixTRS_ID4

			#endif

		#endif //Four

	#endif
		

	//Edge///////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_EDGE_BASE_SOURCE_NONE)

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_STANDARD								_AdvancedDissolveEdgeBaseWidthStandard_ID4
		#endif

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_GEOMETRIC   							_AdvancedDissolveEdgeBaseWidthGeometric_ID4
		#endif


		#define VALUE_EDGE_BASE_SHAPE											_AdvancedDissolveEdgeBaseShape_ID4
		#define VALUE_EDGE_BASE_COLOR											_AdvancedDissolveEdgeBaseColor_ID4
		#define VALUE_EDGE_BASE_COLOR_INTENSITY									_AdvancedDissolveEdgeBaseColorIntensity_ID4
		#define VALUE_EDGE_BASE_COLOR_TRANSPARENCY								_AdvancedDissolveEdgeBaseColorTransparency_ID4


		//Additional Color///////////////////////////////////////////////////////////////////////////
		#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID4

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID4

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_MIPMAP						_AdvancedDissolveEdgeAdditionalColorMapMipmap_ID4

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID4

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll_ID4

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE						_AdvancedDissolveEdgeAdditionalColorMapReverse_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID4

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR									_AdvancedDissolveEdgeAdditionalColor_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_INTENSITY 						_AdvancedDissolveEdgeAdditionalColorIntensity_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY					_AdvancedDissolveEdgeAdditionalColorTransparency_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET					_AdvancedDissolveEdgeAdditionalColorPhaseOffset_ID4
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation_ID4

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset_ID4

		#endif


		//UV Distortion//////////////////////////////////////////////////////////////////////////////
		#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			#define VALUE_EDGE_UV_DISTORTION_STRENGTH							_AdvancedDissolveEdgeUVDistortionStrength_ID4

			#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
				#define VALUE_EDGE_UV_DISTORTION_MAP							_AdvancedDissolveEdgeUVDistortionMap_ID4
				#define VALUE_EDGE_UV_DISTORTION_MAP_SAMPLER 					sampler_AdvancedDissolveEdgeUVDistortionMap_ID4

				#define VALUE_EDGE_UV_DISTORTION_MAP_TILING						_AdvancedDissolveEdgeUVDistortionMapTiling_ID4
				#define VALUE_EDGE_UV_DISTORTION_MAP_OFFSET						_AdvancedDissolveEdgeUVDistortionMapOffset_ID4
				#define VALUE_EDGE_UV_DISTORTION_MAP_SCROLL						_AdvancedDissolveEdgeUVDistortionMapScroll_ID4
			#endif
		#endif	


		//GI/////////////////////////////////////////////////////////////////////////////////////////
		#if defined(ADVANCED_DISSOLVE_META_PASS)
			#define VALUE_EDGE_GI_META_PASS_MULTIPLIER							_AdvancedDissolveEdgeGIMetaPassMultiplier_ID4
		#endif

	#endif

#else

	//Cutout/////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
		
		#define VALUE_CUTOUT_STANDARD_CLIP										_AdvancedDissolveCutoutStandardClip	

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			#define VALUE_CUTOUT_STANDARD_MAP1									_AdvancedDissolveCutoutStandardMap1
			#define VALUE_CUTOUT_STANDARD_MAP1_SAMPLER							sampler_AdvancedDissolveCutoutStandardMap1
			#define VALUE_CUTOUT_STANDARD_MAP1_INVERT							_AdvancedDissolveCutoutStandardMap1Invert
		

			#define VALUE_CUTOUT_STANDARD_MAP1_TILING							_AdvancedDissolveCutoutStandardMap1Tiling
			#define VALUE_CUTOUT_STANDARD_MAP1_OFFSET							_AdvancedDissolveCutoutStandardMap1Offset
			#define VALUE_CUTOUT_STANDARD_MAP1_SCROLL							_AdvancedDissolveCutoutStandardMap1Scroll
			#define VALUE_CUTOUT_STANDARD_MAP1_INTENSITY						_AdvancedDissolveCutoutStandardMap1Intensity
			#define VALUE_CUTOUT_STANDARD_MAP1_CHANNEL							_AdvancedDissolveCutoutStandardMap1Channel


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
				#define VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE		_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace
			#elif defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)
				#define VALUE_CUTOUT_STANDARD_MAPS_SCREEN_SPACE_UV_SCALE        _AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale
			#endif


			#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP2								_AdvancedDissolveCutoutStandardMap2
				#define VALUE_CUTOUT_STANDARD_MAP2_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap2
				#define VALUE_CUTOUT_STANDARD_MAP2_INVERT						_AdvancedDissolveCutoutStandardMap2Invert
						
				#define VALUE_CUTOUT_STANDARD_MAP2_TILING						_AdvancedDissolveCutoutStandardMap2Tiling
				#define VALUE_CUTOUT_STANDARD_MAP2_OFFSET						_AdvancedDissolveCutoutStandardMap2Offset
				#define VALUE_CUTOUT_STANDARD_MAP2_SCROLL						_AdvancedDissolveCutoutStandardMap2Scroll
				#define VALUE_CUTOUT_STANDARD_MAP2_INTENSITY					_AdvancedDissolveCutoutStandardMap2Intensity
				#define VALUE_CUTOUT_STANDARD_MAP2_CHANNEL						_AdvancedDissolveCutoutStandardMap2Channel


				#define VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE					_AdvancedDissolveCutoutStandardMapsBlendType
			#endif

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
				#define VALUE_CUTOUT_STANDARD_MAP3								_AdvancedDissolveCutoutStandardMap3
				#define VALUE_CUTOUT_STANDARD_MAP3_SAMPLER						sampler_AdvancedDissolveCutoutStandardMap3
				#define VALUE_CUTOUT_STANDARD_MAP3_INVERT						_AdvancedDissolveCutoutStandardMap3Invert

		
				#define VALUE_CUTOUT_STANDARD_MAP3_TILING						_AdvancedDissolveCutoutStandardMap3Tiling
				#define VALUE_CUTOUT_STANDARD_MAP3_OFFSET						_AdvancedDissolveCutoutStandardMap3Offset
				#define VALUE_CUTOUT_STANDARD_MAP3_SCROLL						_AdvancedDissolveCutoutStandardMap3Scroll
				#define VALUE_CUTOUT_STANDARD_MAP3_INTENSITY					_AdvancedDissolveCutoutStandardMap3Intensity
				#define VALUE_CUTOUT_STANDARD_MAP3_CHANNEL						_AdvancedDissolveCutoutStandardMap3Channel
			#endif

		#else
			#define VALUE_CUTOUT_STANDARD_BASE_INVERT							_AdvancedDissolveCutoutStandardBaseInvert
		#endif

	#endif


	//Geometric/////////////////////////////////////////////////////////////////////////////////
	#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

		#define VALUE_GEOMETRIC_NOISE											_AdvancedDissolveCutoutGeometricNoise
		#define VALUE_GEOMETRIC_INVERT											_AdvancedDissolveCutoutGeometricInvert


		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
				
			#define VALUE_GEOMETRIC_XYZ_AXIS									_AdvancedDissolveCutoutGeometricXYZAxis
			#define VALUE_GEOMETRIC_XYZ_STYLE									_AdvancedDissolveCutoutGeometricXYZStyle
			#define VALUE_GEOMETRIC_XYZ_SPACE									_AdvancedDissolveCutoutGeometricXYZSpace
			#define VALUE_GEOMETRIC_XYZ_ROLLOUT									_AdvancedDissolveCutoutGeometricXYZRollout
			#define VALUE_GEOMETRIC_XYZ_POSITION								_AdvancedDissolveCutoutGeometricXYZPosition

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) 
			
			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

			#define VALUE_GEOMETRIC_POSITION									_AdvancedDissolveCutoutGeometric1Position
			#define VALUE_GEOMETRIC_NORMAL										_AdvancedDissolveCutoutGeometric1Normal
			#define VALUE_GEOMETRIC_RADIUS										_AdvancedDissolveCutoutGeometric1Radius
			#define VALUE_GEOMETRIC_HEIGHT										_AdvancedDissolveCutoutGeometric1Height

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
			
			#define VALUE_GEOMETRIC_SIZE		   								_AdvancedDissolveCutoutGeometric1Size
			#define VALUE_GEOMETRIC_MATRIX_TRS									_AdvancedDissolveCutoutGeometric1MatrixTRS

		#endif


		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO) || defined (_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)
				
				#define VALUE_GEOMETRIC_2_POSITION								_AdvancedDissolveCutoutGeometric2Position
				#define VALUE_GEOMETRIC_2_NORMAL								_AdvancedDissolveCutoutGeometric2Normal
				#define VALUE_GEOMETRIC_2_RADIUS								_AdvancedDissolveCutoutGeometric2Radius
				#define VALUE_GEOMETRIC_2_HEIGHT								_AdvancedDissolveCutoutGeometric2Height

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_2_SIZE 									_AdvancedDissolveCutoutGeometric2Size
				#define VALUE_GEOMETRIC_2_TRS									_AdvancedDissolveCutoutGeometric2MatrixTRS

			#endif

		#endif //Two

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)	|| defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_3_POSITION								_AdvancedDissolveCutoutGeometric3Position
				#define VALUE_GEOMETRIC_3_NORMAL								_AdvancedDissolveCutoutGeometric3Normal
				#define VALUE_GEOMETRIC_3_RADIUS								_AdvancedDissolveCutoutGeometric3Radius
				#define VALUE_GEOMETRIC_3_HEIGHT								_AdvancedDissolveCutoutGeometric3Height

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
				  
				#define VALUE_GEOMETRIC_3_SIZE									_AdvancedDissolveCutoutGeometric3Size
				#define VALUE_GEOMETRIC_3_TRS									_AdvancedDissolveCutoutGeometric3MatrixTRS

			#endif

		#endif //Three

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

				#define VALUE_GEOMETRIC_4_POSITION								_AdvancedDissolveCutoutGeometric4Position
				#define VALUE_GEOMETRIC_4_NORMAL								_AdvancedDissolveCutoutGeometric4Normal
				#define VALUE_GEOMETRIC_4_RADIUS								_AdvancedDissolveCutoutGeometric4Radius
				#define VALUE_GEOMETRIC_4_HEIGHT								_AdvancedDissolveCutoutGeometric4Height

			#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

				#define VALUE_GEOMETRIC_4_SIZE 									_AdvancedDissolveCutoutGeometric4Size
				#define VALUE_GEOMETRIC_4_TRS									_AdvancedDissolveCutoutGeometric4MatrixTRS

			#endif

		#endif //Four

	#endif
		

	//Edge///////////////////////////////////////////////////////////////////////////////////////
	#if !defined(_AD_EDGE_BASE_SOURCE_NONE)

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_STANDARD								_AdvancedDissolveEdgeBaseWidthStandard
		#endif

		#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
			#define VALUE_EDGE_BASE_WIDTH_GEOMETRIC   							_AdvancedDissolveEdgeBaseWidthGeometric
		#endif


		#define VALUE_EDGE_BASE_SHAPE											_AdvancedDissolveEdgeBaseShape
		#define VALUE_EDGE_BASE_COLOR											_AdvancedDissolveEdgeBaseColor
		#define VALUE_EDGE_BASE_COLOR_INTENSITY									_AdvancedDissolveEdgeBaseColorIntensity
		#define VALUE_EDGE_BASE_COLOR_TRANSPARENCY								_AdvancedDissolveEdgeBaseColorTransparency


		//Additional Color///////////////////////////////////////////////////////////////////////////
		#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_MIPMAP						_AdvancedDissolveEdgeAdditionalColorMapMipmap

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP								_AdvancedDissolveEdgeAdditionalColorMap
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER						sampler_AdvancedDissolveEdgeAdditionalColorMap
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING						_AdvancedDissolveEdgeAdditionalColorMapTiling
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET						_AdvancedDissolveEdgeAdditionalColorMapOffset
			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL						_AdvancedDissolveEdgeAdditionalColorMapScroll

			#define VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE						_AdvancedDissolveEdgeAdditionalColorMapReverse
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR									_AdvancedDissolveEdgeAdditionalColor
			#define VALUE_EDGE_ADDITIONAL_COLOR_INTENSITY 						_AdvancedDissolveEdgeAdditionalColorIntensity
			#define VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY					_AdvancedDissolveEdgeAdditionalColorTransparency
			#define VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET					_AdvancedDissolveEdgeAdditionalColorPhaseOffset
			#define VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION				_AdvancedDissolveEdgeAdditionalColorClipInterpolation

		#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
			
			#define VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET					_AdvancedDissolveEdgeAdditionalColorAlphaOffset

		#endif


		//UV Distortion//////////////////////////////////////////////////////////////////////////////
		#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			#define VALUE_EDGE_UV_DISTORTION_STRENGTH							_AdvancedDissolveEdgeUVDistortionStrength

			#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
				#define VALUE_EDGE_UV_DISTORTION_MAP							_AdvancedDissolveEdgeUVDistortionMap
				#define VALUE_EDGE_UV_DISTORTION_MAP_SAMPLER 					sampler_AdvancedDissolveEdgeUVDistortionMap

				#define VALUE_EDGE_UV_DISTORTION_MAP_TILING						_AdvancedDissolveEdgeUVDistortionMapTiling
				#define VALUE_EDGE_UV_DISTORTION_MAP_OFFSET						_AdvancedDissolveEdgeUVDistortionMapOffset
				#define VALUE_EDGE_UV_DISTORTION_MAP_SCROLL						_AdvancedDissolveEdgeUVDistortionMapScroll
			#endif
		#endif	


		//GI/////////////////////////////////////////////////////////////////////////////////////////
		#if defined(ADVANCED_DISSOLVE_META_PASS)
			#define VALUE_EDGE_GI_META_PASS_MULTIPLIER							_AdvancedDissolveEdgeGIMetaPassMultiplier
		#endif

	#endif

#endif


#endif //ADVANCED_DISSOLVE_DEFINES_CGINC