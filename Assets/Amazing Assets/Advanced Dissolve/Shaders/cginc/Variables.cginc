#ifndef ADVANCED_DISSOLVE_VARIABLES_CGINC
#define ADVANCED_DISSOLVE_VARIABLES_CGINC



//Cutout/////////////////////////////////////////////////////////////////////////////////////
#if !defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)

	DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutStandardClip)	

	#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
		DECLARE_TEXTURE_2D(_AdvancedDissolveCutoutStandardMap1)		
		DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMap1Invert)

		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap1Tiling)
		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap1Offset)
		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap1Scroll)										
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutStandardMap1Intensity)										
		DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMap1Channel)	


		#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
			DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMapsTriplanarMappingSpace)
		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutStandardMapsScreenSpaceUVScale)	
		#endif


		#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			DECLARE_TEXTURE_2D(_AdvancedDissolveCutoutStandardMap2)       
			DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMap2Invert)
	
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap2Tiling)
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap2Offset)
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap2Scroll)							
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutStandardMap2Intensity)										
			DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMap2Channel)		
	

			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutStandardMapsBlendType)	
		#endif

		#if defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
			DECLARE_TEXTURE_2D(_AdvancedDissolveCutoutStandardMap3)   
			DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMap3Invert)

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap3Tiling)
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap3Offset)
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutStandardMap3Scroll)								
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutStandardMap3Intensity)										
			DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardMap3Channel)					                    
		#endif  

	#else
		DISSOLVE_PROP_INT(_AdvancedDissolveCutoutStandardBaseInvert)		
	#endif

#endif


//Geometric/////////////////////////////////////////////////////////////////////////////////
#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

	DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometricNoise)
	DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometricInvert)	
	

	#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
			
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometricXYZAxis)		
		DISSOLVE_PROP_INT(_AdvancedDissolveCutoutGeometricXYZStyle)		
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometricXYZSpace)					
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometricXYZRollout)		
		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometricXYZPosition)					

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Position)					
		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Normal)		
		
	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Position)						
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric1Radius)		

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Position)					
		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Normal)						
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric1Radius)						
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric1Height)	

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Position)					
		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Normal)						
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric1Radius)						
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric1Height)	

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

		DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric1Size)				
		DISSOLVE_PROP_FLOAT4X4(_AdvancedDissolveCutoutGeometric1MatrixTRS)					

	#endif



	#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO) || defined (_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE) 

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Normal)		
			
		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Position)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric2Radius)	

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Normal)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric2Radius)					
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric2Height)	

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Normal)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric2Radius)					
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric2Height)	
		
		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric2Size)			
			DISSOLVE_PROP_FLOAT4X4(_AdvancedDissolveCutoutGeometric2MatrixTRS)				

		#endif	
		
	#endif	//Two

	#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE) || defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)
	
		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)			

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Normal)	
			
		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)				

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Position)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric3Radius)	

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)				

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Normal)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric3Radius)					
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric3Height)

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)				

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Normal)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric3Radius)					
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric3Height)

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)				

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric3Size)			
			DISSOLVE_PROP_FLOAT4X4(_AdvancedDissolveCutoutGeometric3MatrixTRS)				

		#endif

	#endif	//Three

	#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)
	
		#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)				

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Normal)			
			
		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)			

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Position)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric4Radius)	

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)			

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Normal)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric4Radius)					
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric4Height)		

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)				

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Position)				
			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Normal)				
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric4Radius)					
			DISSOLVE_PROP_FLOAT(_AdvancedDissolveCutoutGeometric4Height)		

		#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)			

			DISSOLVE_PROP_FLOAT3(_AdvancedDissolveCutoutGeometric4Size)			
			DISSOLVE_PROP_FLOAT4X4(_AdvancedDissolveCutoutGeometric4MatrixTRS)				

		#endif

	#endif	//Four

#endif


//Edge///////////////////////////////////////////////////////////////////////////////////////
#if !defined(_AD_EDGE_BASE_SOURCE_NONE)

	#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_ALL)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeBaseWidthStandard)		
	#endif

	#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeBaseWidthGeometric)		
	#endif

	DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeBaseShape)	
	DISSOLVE_PROP_FLOAT4(_AdvancedDissolveEdgeBaseColor)					
	DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeBaseColorIntensity)
	DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeBaseColorTransparency)


	//Additional Color///////////////////////////////////////////////////////////////////////////
	#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)

		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorAlphaOffset)

	#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)
	
		DECLARE_TEXTURE_2D(_AdvancedDissolveEdgeAdditionalColorMap)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorAlphaOffset)

		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorMapTiling)
		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorMapOffset)
		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorMapScroll)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorMapMipmap)

	#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)

		DECLARE_TEXTURE_2D(_AdvancedDissolveEdgeAdditionalColorMap)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorAlphaOffset)

		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorMapTiling)
		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorMapOffset)
		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorMapScroll)

		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorMapReverse)		
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorClipInterpolation)	

	#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)

		DISSOLVE_PROP_FLOAT4(_AdvancedDissolveEdgeAdditionalColor)	
		DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeAdditionalColorIntensity)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorTransparency)	
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorPhaseOffset)	
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorClipInterpolation)		

	#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
	
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeAdditionalColorAlphaOffset)

	#endif

	//UV Distortion//////////////////////////////////////////////////////////////////////////////
	#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeUVDistortionStrength)	

		#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
			DECLARE_TEXTURE_2D(_AdvancedDissolveEdgeUVDistortionMap)
			DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeUVDistortionMapTiling)
			DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeUVDistortionMapOffset)
			DISSOLVE_PROP_FLOAT2(_AdvancedDissolveEdgeUVDistortionMapScroll)
		#endif
	#endif


	//GI/////////////////////////////////////////////////////////////////////////////////////////
	#if defined(ADVANCED_DISSOLVE_META_PASS)
		DISSOLVE_PROP_FLOAT(_AdvancedDissolveEdgeGIMetaPassMultiplier)				
	#endif

#endif


#endif //ADVANCED_DISSOLVE_VARIABLES_CGINC