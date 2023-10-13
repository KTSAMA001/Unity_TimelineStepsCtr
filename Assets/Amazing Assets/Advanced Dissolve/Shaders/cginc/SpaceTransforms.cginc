#ifndef ADVANCED_DISSOLVE_SPACE_TRANSFORMS_CGINC
#define ADVANCED_DISSOLVE_SPACE_TRANSFORMS_CGINC


float3 ADTransformObjectToWorld(float3 positionOS)
{	
	#if defined(ADVANCED_DISSOLVE_HIGH_DEFINITION_RENDER_PIPELINE) || defined(ADVANCED_DISSOLVE_UNIVERSAL_RENDER_PIPELINE)

		#ifdef ADVANCED_DISSOLVE_SHADER_GRAPH
			return GetAbsolutePositionWS(TransformObjectToWorld(positionOS.xyz));
		#elif defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)
			return mul(unity_ObjectToWorld, float4(positionOS, 1)).xyz;
		#else
			return TransformObjectToWorld(positionOS);
		#endif

	#else
		
		return mul(unity_ObjectToWorld, float4(positionOS, 1)).xyz;

	#endif
}

float3 ADTransformObjectToWorldNormal(float3 positionOS)
{	
	#if defined(ADVANCED_DISSOLVE_HIGH_DEFINITION_RENDER_PIPELINE) || defined(ADVANCED_DISSOLVE_UNIVERSAL_RENDER_PIPELINE)

		#ifdef ADVANCED_DISSOLVE_SHADER_GRAPH
			return 0;
		#elif defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)
			return mul(unity_ObjectToWorld, float4(positionOS, 0)).xyz;
		#else
			return TransformObjectToWorldNormal(positionOS);
		#endif

	#else
		
		return mul(unity_ObjectToWorld, float4(positionOS, 0)).xyz;

	#endif
}

float3 ADTransformWorldToObject(float3 positionWS)
{
	#if defined(ADVANCED_DISSOLVE_HIGH_DEFINITION_RENDER_PIPELINE) || defined(ADVANCED_DISSOLVE_UNIVERSAL_RENDER_PIPELINE)

		#ifdef ADVANCED_DISSOLVE_SHADER_GRAPH
			return GetAbsolutePositionWS(TransformObjectToWorld(positionWS.xyz));
		#elif defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)
			return mul(unity_WorldToObject, float4(positionWS, 1)).xyz;
		#else
			return TransformObjectToWorld(positionWS);
		#endif

	#else		

		return mul(unity_WorldToObject, float4(positionWS, 1)).xyz;

	#endif
}

float3 ADTransformWorldToObjectNormal(float3 positionWS)
{
	#if defined(ADVANCED_DISSOLVE_HIGH_DEFINITION_RENDER_PIPELINE) || defined(ADVANCED_DISSOLVE_UNIVERSAL_RENDER_PIPELINE)

		#ifdef ADVANCED_DISSOLVE_SHADER_GRAPH
			return GetAbsolutePositionWS(TransformObjectToWorld(positionWS.xyz));
		#elif defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)
			return mul(unity_WorldToObject, float4(positionWS, 0)).xyz;
		#else
			return TransformObjectToWorld(positionWS);
		#endif

	#else		

		return mul(unity_WorldToObject, float4(positionWS, 0)).xyz;

	#endif
}

float3 ADGetCameraPositionWS()
{
	#if defined(ADVANCED_DISSOLVE_HIGH_DEFINITION_RENDER_PIPELINE) || defined(ADVANCED_DISSOLVE_UNIVERSAL_RENDER_PIPELINE)

		#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			return _WorldSpaceCameraPos;
		#elif defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)
			return _WorldSpaceCameraPos;
		#else
			return GetCameraPositionWS();
		#endif

	#else

		return _WorldSpaceCameraPos;

	#endif
}


#endif //ADVANCED_DISSOLVE_SPACE_TRANSFORMS_CGINC