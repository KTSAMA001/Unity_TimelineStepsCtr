#ifndef ADVANCED_DISSOLVE_CORE_CGINC
#define ADVANCED_DISSOLVE_CORE_CGINC

#if defined(ADVANCED_DISSOLVE_BUILTIN_RENDER_PIPELINE)
#include "UnityCG.cginc"
#endif

#include "Defines.cginc"

#if defined(_ADVANCED_DISSOLVE_KEYWORDS_BAKED)
	#if defined(_AD_GLOBAL_CONTROL_ID_ONE) || defined(_AD_GLOBAL_CONTROL_ID_TWO) || defined(_AD_GLOBAL_CONTROL_ID_THREE) || defined(_AD_GLOBAL_CONTROL_ID_FOUR)
		#include "Variables.cginc"
	#else
		//No need to inlcude variables here, they are already defined in shader cbuffer
	#endif
#else
	#include "Variables.cginc"
#endif

#include "SpaceTransforms.cginc"



float ADBlendGradientEdges(float a, float b)
{
	return saturate(1 - min(a, b));
}

float ADBlendGradientEdges(float a, float b, float c)
{
	return saturate(1 - min(min(a, b), c));
}

float ADBlendGradientEdges(float a, float b, float c, float d)
{
	return saturate(1 - min(min(a, b), min(c, d)));
}

 inline float ProjectT(float3 origin, float3 normal, float height, float3 vertex)
            {
               return saturate(dot(vertex - origin, normal) / height); 
            }


#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)
inline float ADReadGeometricValue(float3 positionOS, float3 positionAbsolute, float noise)
{
	float cutout = -1;

	// 1 - without invert
	//-1 - with invert enabled
	noise *= (1 - 2 * VALUE_GEOMETRIC_INVERT);


	#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)

		int xyzAxis = (int)VALUE_GEOMETRIC_XYZ_AXIS;

		float vertexPos = (lerp(positionAbsolute, positionOS, VALUE_GEOMETRIC_XYZ_SPACE))[xyzAxis];

		//Linear
		float l = vertexPos - VALUE_GEOMETRIC_XYZ_POSITION[xyzAxis] + noise + VALUE_GEOMETRIC_INVERT;
		
		//Rollout
		float r = abs(vertexPos - VALUE_GEOMETRIC_XYZ_POSITION[xyzAxis]) - VALUE_GEOMETRIC_XYZ_ROLLOUT + saturate(VALUE_GEOMETRIC_XYZ_ROLLOUT) * noise + VALUE_GEOMETRIC_INVERT;
		

		cutout = saturate(lerp(l, r, VALUE_GEOMETRIC_XYZ_STYLE));

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_PLANE)

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)
			
			float4 d = float4(dot(VALUE_GEOMETRIC_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_POSITION)), dot(VALUE_GEOMETRIC_2_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_2_POSITION)), dot(VALUE_GEOMETRIC_3_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_3_POSITION)), dot(VALUE_GEOMETRIC_4_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_4_POSITION)));

			d += noise.xxxx + VALUE_GEOMETRIC_INVERT;

			d = saturate(d);

			cutout = 1 - ADBlendGradientEdges(d.x, d.y, d.z, d.w);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)
			
			float3 d = float3(dot(VALUE_GEOMETRIC_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_POSITION)), dot(VALUE_GEOMETRIC_2_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_2_POSITION)), dot(VALUE_GEOMETRIC_3_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_3_POSITION)));

			d += noise.xxx + VALUE_GEOMETRIC_INVERT;

			d = saturate(d);

			cutout = 1 - ADBlendGradientEdges(d.x, d.y, d.z);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO)

			float2 d = float2(dot(VALUE_GEOMETRIC_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_POSITION)), dot(VALUE_GEOMETRIC_2_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_2_POSITION)));

			d += noise.xx + VALUE_GEOMETRIC_INVERT;

			d = saturate(d);

			cutout = 1 - ADBlendGradientEdges(d.x, d.y);

		#else					    
		
			float d = dot(VALUE_GEOMETRIC_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_POSITION));

			d += noise + VALUE_GEOMETRIC_INVERT;

			cutout = saturate(d);

		#endif
		
	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_SPHERE)

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)

			float4 d = float4(distance(positionAbsolute, VALUE_GEOMETRIC_POSITION), distance(positionAbsolute, VALUE_GEOMETRIC_2_POSITION), distance(positionAbsolute, VALUE_GEOMETRIC_3_POSITION), distance(positionAbsolute, VALUE_GEOMETRIC_4_POSITION));
			float4 radius = float4(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS, VALUE_GEOMETRIC_3_RADIUS, VALUE_GEOMETRIC_4_RADIUS);

			float4 e = saturate(max(0, d - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT).xxxx - saturate(radius) * noise));

			cutout = ADBlendGradientEdges(e.x, e.y, e.z, e.w);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)

			float3 d = float3(distance(positionAbsolute, VALUE_GEOMETRIC_POSITION), distance(positionAbsolute, VALUE_GEOMETRIC_2_POSITION), distance(positionAbsolute, VALUE_GEOMETRIC_3_POSITION));
			float3 radius = float3(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS, VALUE_GEOMETRIC_3_RADIUS);

			float3 e = saturate(max(0, d - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT).xxx - saturate(radius) * noise));

			cutout = ADBlendGradientEdges(e.x, e.y, e.z);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO)

			float2 d = float2(distance(positionAbsolute, VALUE_GEOMETRIC_POSITION), distance(positionAbsolute, VALUE_GEOMETRIC_2_POSITION));
			float2 radius = float2(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS);

			float2 e = saturate(max(0, d - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT).xx - saturate(radius) * noise));

			cutout = ADBlendGradientEdges(e.x, e.y);
						
		#else
				
			float d = distance(positionAbsolute, VALUE_GEOMETRIC_POSITION);

			float radius = VALUE_GEOMETRIC_RADIUS - lerp(1, 0, VALUE_GEOMETRIC_INVERT);

			cutout = 1 - saturate(max(0, d - radius - saturate(VALUE_GEOMETRIC_RADIUS) * noise));	

		#endif		

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CUBE)
		
		float boundsEdge = lerp(0, 1, VALUE_GEOMETRIC_INVERT); 

			
		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)
						
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float3 vertexInverseTransform = mul(VALUE_GEOMETRIC_MATRIX_TRS, float4(positionAbsolute, 1)).xyz;

			float3 absSize = abs(VALUE_GEOMETRIC_SIZE);
			float3 b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c1 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			vertexInverseTransform = mul(VALUE_GEOMETRIC_2_TRS, float4(positionAbsolute, 1)).xyz;

			absSize = abs(VALUE_GEOMETRIC_2_SIZE);
			b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c2 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//3////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			vertexInverseTransform = mul(VALUE_GEOMETRIC_3_TRS, float4(positionAbsolute, 1)).xyz;

			absSize = abs(VALUE_GEOMETRIC_3_SIZE);
			b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c3 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//4////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			vertexInverseTransform = mul(VALUE_GEOMETRIC_4_TRS, float4(positionAbsolute, 1)).xyz;

			absSize = abs(VALUE_GEOMETRIC_4_SIZE);
			b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c4 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//Final
			cutout = ADBlendGradientEdges(c1, c2 ,c3, c4);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)
						
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float3 vertexInverseTransform = mul(VALUE_GEOMETRIC_MATRIX_TRS, float4(positionAbsolute, 1)).xyz;

			float3 absSize = abs(VALUE_GEOMETRIC_SIZE);
			float3 b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c1 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			vertexInverseTransform = mul(VALUE_GEOMETRIC_2_TRS, float4(positionAbsolute, 1)).xyz;

			absSize = abs(VALUE_GEOMETRIC_2_SIZE);
			b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c2 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//3////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			vertexInverseTransform = mul(VALUE_GEOMETRIC_3_TRS, float4(positionAbsolute, 1)).xyz;

			absSize = abs(VALUE_GEOMETRIC_3_SIZE);
			b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c3 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//Final
			cutout = ADBlendGradientEdges(c1, c2 ,c3);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO)
						
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float3 vertexInverseTransform = mul(VALUE_GEOMETRIC_MATRIX_TRS, float4(positionAbsolute, 1)).xyz;

			float3 absSize = abs(VALUE_GEOMETRIC_SIZE);
			float3 b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c1 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			vertexInverseTransform = mul(VALUE_GEOMETRIC_2_TRS, float4(positionAbsolute, 1)).xyz;

			absSize = abs(VALUE_GEOMETRIC_2_SIZE);
			b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;
			float c2 = saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);

			//Final
			cutout = ADBlendGradientEdges(c1, c2);

		#else
				
			float3 vertexInverseTransform = mul(VALUE_GEOMETRIC_MATRIX_TRS, float4(positionAbsolute, 1)).xyz;

			float3 absSize = abs(VALUE_GEOMETRIC_SIZE);
			float3 b = abs(vertexInverseTransform) - (absSize + boundsEdge) - saturate(absSize) * noise;

			cutout = 1 - saturate(length(max(b, 0.0)) + min(max(b.x, max(b.y, b.z)), 0.0) + 1);
		#endif

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CAPSULE)
		
		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)
			
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t1 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);   
			float3 projection1 = VALUE_GEOMETRIC_POSITION + t1 * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d1 = distance(positionAbsolute, projection1);
						

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t2 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_2_POSITION, VALUE_GEOMETRIC_2_NORMAL) / VALUE_GEOMETRIC_2_HEIGHT); 
			float3 projection2 = VALUE_GEOMETRIC_2_POSITION + t2 * VALUE_GEOMETRIC_2_NORMAL * VALUE_GEOMETRIC_2_HEIGHT;
 
			float d2 = distance(positionAbsolute, projection2);

			//3////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t3 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_3_POSITION, VALUE_GEOMETRIC_3_NORMAL) / VALUE_GEOMETRIC_3_HEIGHT); 
			float3 projection3 = VALUE_GEOMETRIC_3_POSITION + t3 * VALUE_GEOMETRIC_3_NORMAL * VALUE_GEOMETRIC_3_HEIGHT;
 
			float d3 = distance(positionAbsolute, projection3);

			//4////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t4 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_4_POSITION, VALUE_GEOMETRIC_4_NORMAL) / VALUE_GEOMETRIC_4_HEIGHT); 
			float3 projection4 = VALUE_GEOMETRIC_4_POSITION + t4 * VALUE_GEOMETRIC_4_NORMAL * VALUE_GEOMETRIC_4_HEIGHT;
 
			float d4 = distance(positionAbsolute, projection4);
						
			//Sum
			float4 radius = float4(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS, VALUE_GEOMETRIC_3_RADIUS, VALUE_GEOMETRIC_4_RADIUS);
			float4 e = saturate(max(0, float4(d1, d2, d3, d4) - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT) - saturate(radius) * noise));


			cutout = ADBlendGradientEdges(e.x, e.y, e.z, e.w);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)

			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t1 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT); 
			float3 projection1 = VALUE_GEOMETRIC_POSITION + t1 * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d1 = distance(positionAbsolute, projection1);
						
			 
			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t2 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_2_POSITION, VALUE_GEOMETRIC_2_NORMAL) / VALUE_GEOMETRIC_2_HEIGHT); 
			float3 projection2 = VALUE_GEOMETRIC_2_POSITION + t2 * VALUE_GEOMETRIC_2_NORMAL * VALUE_GEOMETRIC_2_HEIGHT;
 
			float d2 = distance(positionAbsolute, projection2);

			//3////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t3 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_3_POSITION, VALUE_GEOMETRIC_3_NORMAL) / VALUE_GEOMETRIC_3_HEIGHT); 
			float3 projection3 = VALUE_GEOMETRIC_3_POSITION + t3 * VALUE_GEOMETRIC_3_NORMAL * VALUE_GEOMETRIC_3_HEIGHT;
 
			float d3 = distance(positionAbsolute, projection3);
						
			//Sum
			float3 radius = float3(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS, VALUE_GEOMETRIC_3_RADIUS);
			float3 e = saturate(max(0, float3(d1, d2, d3) - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT) - saturate(radius) * noise));	


			cutout = ADBlendGradientEdges(e.x, e.y, e.z);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO)

			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t1 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);     
			float3 projection1 = VALUE_GEOMETRIC_POSITION + t1 * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d1 = distance(positionAbsolute, projection1);
						

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t2 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_2_POSITION, VALUE_GEOMETRIC_2_NORMAL) / VALUE_GEOMETRIC_2_HEIGHT);  
			float3 projection2 = VALUE_GEOMETRIC_2_POSITION + t2 * VALUE_GEOMETRIC_2_NORMAL * VALUE_GEOMETRIC_2_HEIGHT;
 
			float d2 = distance(positionAbsolute, projection2);
						
			//Sum
			float2 radius = float2(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS);
			float2 e = saturate(max(0, float2(d1, d2) - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT) - saturate(radius) * noise));	


			cutout = ADBlendGradientEdges(e.x, e.y);

		#else

			//float coreDist = dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL);
			//float orthDist = length((positionAbsolute - VALUE_GEOMETRIC_POSITION) - VALUE_GEOMETRIC_NORMAL * coreDist);                                
			//float mainC = orthDist - VALUE_GEOMETRIC_RADIUS;

			//float top = dot(VALUE_GEOMETRIC_NORMAL, (positionAbsolute - VALUE_GEOMETRIC_POSITION));	
			//float bottom = dot(VALUE_GEOMETRIC_NORMAL, ((VALUE_GEOMETRIC_POSITION + VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT) - positionAbsolute));

			//float3 k = saturate(float3(mainC, top, bottom) + boundsEdge);
			
			//cutout = 1 - ADBlendGradientEdges((1 - k.x), k.y, k.z);



			float t = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);       
			float3 projection = VALUE_GEOMETRIC_POSITION + t * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d = distance(positionAbsolute, projection);
						
			cutout = 1 - saturate(max(0, d - VALUE_GEOMETRIC_RADIUS + lerp(1, 0, VALUE_GEOMETRIC_INVERT) - noise * saturate(VALUE_GEOMETRIC_RADIUS)));	

		#endif

	#elif defined(_AD_CUTOUT_GEOMETRIC_TYPE_CONE_SMOOTH)

		#if defined(_AD_CUTOUT_GEOMETRIC_COUNT_FOUR)
			
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t1 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);        
			float3 projection1 = VALUE_GEOMETRIC_POSITION + t1 * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d1 = distance(positionAbsolute, projection1);
						

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t2 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_2_POSITION, VALUE_GEOMETRIC_2_NORMAL) / VALUE_GEOMETRIC_2_HEIGHT);       
			float3 projection2 = VALUE_GEOMETRIC_2_POSITION + t2 * VALUE_GEOMETRIC_2_NORMAL * VALUE_GEOMETRIC_2_HEIGHT;
 
			float d2 = distance(positionAbsolute, projection2);

			//3////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t3 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_3_POSITION, VALUE_GEOMETRIC_3_NORMAL) / VALUE_GEOMETRIC_3_HEIGHT);  
			float3 projection3 = VALUE_GEOMETRIC_3_POSITION + t3 * VALUE_GEOMETRIC_3_NORMAL * VALUE_GEOMETRIC_3_HEIGHT;
 
			float d3 = distance(positionAbsolute, projection3);

			//4////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t4 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_4_POSITION, VALUE_GEOMETRIC_4_NORMAL) / VALUE_GEOMETRIC_4_HEIGHT);  
			float3 projection4 = VALUE_GEOMETRIC_4_POSITION + t4 * VALUE_GEOMETRIC_4_NORMAL * VALUE_GEOMETRIC_4_HEIGHT;
 
			float d4 = distance(positionAbsolute, projection4);
						
			//Sum
			float4 radius = lerp(float4(0, 0, 0, 0), float4(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS, VALUE_GEOMETRIC_3_RADIUS, VALUE_GEOMETRIC_4_RADIUS), float4(t1, t2, t3, t4));
			float4 e = saturate(max(float4(0, 0, 0, 0), float4(d1, d2, d3, d4) - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT).xxxx - saturate(radius) * noise));


			cutout = ADBlendGradientEdges(e.x, e.y, e.z, e.w);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_THREE)
	
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t1 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);        
			float3 projection1 = VALUE_GEOMETRIC_POSITION + t1 * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d1 = distance(positionAbsolute, projection1);
						

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t2 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_2_POSITION, VALUE_GEOMETRIC_2_NORMAL) / VALUE_GEOMETRIC_2_HEIGHT);       
			float3 projection2 = VALUE_GEOMETRIC_2_POSITION + t2 * VALUE_GEOMETRIC_2_NORMAL * VALUE_GEOMETRIC_2_HEIGHT;
 
			float d2 = distance(positionAbsolute, projection2);

			//3////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t3 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_3_POSITION, VALUE_GEOMETRIC_3_NORMAL) / VALUE_GEOMETRIC_3_HEIGHT);  
			float3 projection3 = VALUE_GEOMETRIC_3_POSITION + t3 * VALUE_GEOMETRIC_3_NORMAL * VALUE_GEOMETRIC_3_HEIGHT;
 
			float d3 = distance(positionAbsolute, projection3);
						
			//Sum
			float3 radius = lerp(float3(0, 0, 0), float3(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS, VALUE_GEOMETRIC_3_RADIUS), float3(t1, t2, t3));
			float3 e = saturate(max(float3(0, 0, 0), float3(d1, d2, d3) - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT).xxx - saturate(radius) * noise));	


			cutout = ADBlendGradientEdges(e.x, e.y, e.z);

		#elif defined(_AD_CUTOUT_GEOMETRIC_COUNT_TWO)
	
			//1////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t1 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);        
			float3 projection1 = VALUE_GEOMETRIC_POSITION + t1 * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
 
			float d1 = distance(positionAbsolute, projection1);
						

			//2////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
			float t2 = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_2_POSITION, VALUE_GEOMETRIC_2_NORMAL) / VALUE_GEOMETRIC_2_HEIGHT);       
			float3 projection2 = VALUE_GEOMETRIC_2_POSITION + t2 * VALUE_GEOMETRIC_2_NORMAL * VALUE_GEOMETRIC_2_HEIGHT;
 
			float d2 = distance(positionAbsolute, projection2);
						
			//Sum
			float2 radius = lerp(float2(0, 0), float2(VALUE_GEOMETRIC_RADIUS, VALUE_GEOMETRIC_2_RADIUS), float2(t1, t2));
			float2 e = saturate(max(float2(0, 0), float2(d1, d2) - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT).xx - saturate(radius) * noise));	


			cutout = ADBlendGradientEdges(e.x, e.y);

		#else

			float t = saturate(dot(positionAbsolute - VALUE_GEOMETRIC_POSITION, VALUE_GEOMETRIC_NORMAL) / VALUE_GEOMETRIC_HEIGHT);    
			float3 projectPosition = VALUE_GEOMETRIC_POSITION + t * VALUE_GEOMETRIC_NORMAL * VALUE_GEOMETRIC_HEIGHT;
			
			float d = distance(positionAbsolute, projectPosition);
						
			float radius = lerp(0, VALUE_GEOMETRIC_RADIUS, t);
			cutout = 1 - saturate(max(0, d - radius + lerp(1, 0, VALUE_GEOMETRIC_INVERT) - saturate(radius) * noise));						

		#endif

	#endif


	return lerp(cutout, 1 - cutout, VALUE_GEOMETRIC_INVERT);
}
#endif


#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR

	#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP) || defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
		inline float4 ReadTriplanarTextureMap1(float3 coords, float3 blend, float intensity)
		{
			coords = coords * VALUE_CUTOUT_STANDARD_MAP1_TILING + VALUE_CUTOUT_STANDARD_MAP1_OFFSET + VALUE_CUTOUT_STANDARD_MAP1_SCROLL * _Time.x;

			float4 cx = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, coords.yz, intensity);
			float4 cy = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, coords.xz, intensity);
			float4 cz = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, coords.xy, intensity);

			cx.a = cx[VALUE_CUTOUT_STANDARD_MAP1_CHANNEL];
			cy.a = cy[VALUE_CUTOUT_STANDARD_MAP1_CHANNEL];
			cz.a = cz[VALUE_CUTOUT_STANDARD_MAP1_CHANNEL];

			return (cx * blend.x + cy * blend.y + cz * blend.z);
		}
	#endif

	#if defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS) || defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)
		inline float4 ReadTriplanarTextureMap2(float3 coords, float3 blend, float intensity)
		{
			coords = coords * VALUE_CUTOUT_STANDARD_MAP2_TILING + VALUE_CUTOUT_STANDARD_MAP2_OFFSET + VALUE_CUTOUT_STANDARD_MAP2_SCROLL * _Time.x;

			float4 cx = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP2, VALUE_CUTOUT_STANDARD_MAP2_SAMPLER, coords.yz, intensity);
			float4 cy = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP2, VALUE_CUTOUT_STANDARD_MAP2_SAMPLER, coords.xz, intensity);
			float4 cz = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP2, VALUE_CUTOUT_STANDARD_MAP2_SAMPLER, coords.xy, intensity);

			cx.a = cx[VALUE_CUTOUT_STANDARD_MAP2_CHANNEL];
			cy.a = cy[VALUE_CUTOUT_STANDARD_MAP2_CHANNEL];
			cz.a = cz[VALUE_CUTOUT_STANDARD_MAP2_CHANNEL];

			return (cx * blend.x + cy * blend.y + cz * blend.z);
		}
	#endif

	#ifdef _AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS
		inline float4 ReadTriplanarTextureMap3(float3 coords, float3 blend, float intensity)
		{
			coords = coords * VALUE_CUTOUT_STANDARD_MAP3_TILING + VALUE_CUTOUT_STANDARD_MAP3_OFFSET + VALUE_CUTOUT_STANDARD_MAP3_SCROLL * _Time.x;

			float4 cx = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP3, VALUE_CUTOUT_STANDARD_MAP3_SAMPLER, coords.yz, intensity);
			float4 cy = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP3, VALUE_CUTOUT_STANDARD_MAP3_SAMPLER, coords.xz, intensity);
			float4 cz = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP3, VALUE_CUTOUT_STANDARD_MAP3_SAMPLER, coords.xy, intensity);

			cx.a = cx[VALUE_CUTOUT_STANDARD_MAP3_CHANNEL];
			cy.a = cy[VALUE_CUTOUT_STANDARD_MAP3_CHANNEL];
			cz.a = cz[VALUE_CUTOUT_STANDARD_MAP3_CHANNEL];

			return (cx * blend.x + cy * blend.y + cz * blend.z);
		}
	#endif


	inline float4 ADReadCutoutSource(float3 positionOS, float3 positionAbsolute, float3 normalOS, float3 normalWS)
	{	
		float3 vertexPos  = lerp(positionAbsolute, positionOS, VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE);
		float3 vertexNorm = lerp(normalWS, normalOS, VALUE_CUTOUT_STANDARD_MAPS_TRIPLANAR_MAPPING_SPACE);
		
		float3 blend = abs(vertexNorm);
		blend /= dot(blend, 1.0);
		

		float4 alphaSource = 1;
		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP)

			alphaSource = ReadTriplanarTextureMap1(vertexPos, blend, 1);

			alphaSource.a = lerp(alphaSource.a, 1 - alphaSource.a, VALUE_CUTOUT_STANDARD_MAP1_INVERT);

		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS)

			float4 t1 = ReadTriplanarTextureMap1(vertexPos, blend, VALUE_CUTOUT_STANDARD_MAP1_INTENSITY);
			float4 t2 = ReadTriplanarTextureMap2(vertexPos, blend, VALUE_CUTOUT_STANDARD_MAP2_INTENSITY);

			t1.a = lerp(t1.a, 1 - t1.a, VALUE_CUTOUT_STANDARD_MAP1_INVERT);
			t2.a = lerp(t2.a, 1 - t2.a, VALUE_CUTOUT_STANDARD_MAP2_INVERT);

			alphaSource = lerp((t1 * t2), (t1 + t2) * 0.5, VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE);

		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)

			float4 t1 = ReadTriplanarTextureMap1(vertexPos, blend, VALUE_CUTOUT_STANDARD_MAP1_INTENSITY);
			float4 t2 = ReadTriplanarTextureMap2(vertexPos, blend, VALUE_CUTOUT_STANDARD_MAP2_INTENSITY);
			float4 t3 = ReadTriplanarTextureMap3(vertexPos, blend, VALUE_CUTOUT_STANDARD_MAP3_INTENSITY);

			t1.a = lerp(t1.a, 1 - t1.a, VALUE_CUTOUT_STANDARD_MAP1_INVERT);
			t2.a = lerp(t2.a, 1 - t2.a, VALUE_CUTOUT_STANDARD_MAP2_INVERT);
			t3.a = lerp(t3.a, 1 - t3.a, VALUE_CUTOUT_STANDARD_MAP3_INVERT);


			alphaSource = lerp((t1 * t2 * t3), (t1 + t2 + t3) / 3.0, VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE);

		#else		

			//No triplanar mapping for Basemap

		#endif


		alphaSource = saturate(alphaSource);


		#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
				alphaSource.b = ADReadGeometricValue(positionOS, positionAbsolute, noise);
			#else
				float noise = ((alphaSource.a - 0.5) * 2) * VALUE_GEOMETRIC_NOISE;

				alphaSource.b = ADReadGeometricValue(positionOS, positionAbsolute, noise * (1 - VALUE_CUTOUT_STANDARD_CLIP));
			#endif

		#endif	


		return alphaSource;
	}

#else

	inline float4 ADReadCutoutSource(float4 baseAlbedo, float4 dissolveUV, float3 positionOS, float3 positionAbsolute, float customCutoutAlphaSource)
	{
		float4 alphaSource = float4(0, 0, 0, 1);


		#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
			float2 screenUV = dissolveUV.xy / dissolveUV.w;
			screenUV.x *= _ScreenParams.x / _ScreenParams.y;
			
			screenUV *= lerp(1, distance(ADGetCameraPositionWS(), ADTransformObjectToWorld(float3(0, 0, 0))) * 0.1, VALUE_CUTOUT_STANDARD_MAPS_SCREEN_SPACE_UV_SCALE);
		#endif


		#if defined(_AD_CUTOUT_STANDARD_SOURCE_CUSTOM_MAP)

			#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
				screenUV = screenUV * VALUE_CUTOUT_STANDARD_MAP1_TILING.xy + VALUE_CUTOUT_STANDARD_MAP1_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP1_SCROLL.xy * _Time.x;

				alphaSource = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, screenUV, 1);
			#else
				float2 uv1 = dissolveUV.xy * VALUE_CUTOUT_STANDARD_MAP1_TILING.xy + VALUE_CUTOUT_STANDARD_MAP1_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP1_SCROLL.xy * _Time.x;

				alphaSource = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, uv1, 1);						
			#endif

			alphaSource.a = alphaSource[VALUE_CUTOUT_STANDARD_MAP1_CHANNEL];
			alphaSource.a = lerp(alphaSource.a, 1 - alphaSource.a, VALUE_CUTOUT_STANDARD_MAP1_INVERT);

		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_TWO_CUSTOM_MAPS)

			#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
				float2 uv1 = screenUV * VALUE_CUTOUT_STANDARD_MAP1_TILING.xy + VALUE_CUTOUT_STANDARD_MAP1_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP1_SCROLL.xy * _Time.x;
				float2 uv2 = screenUV * VALUE_CUTOUT_STANDARD_MAP2_TILING.xy + VALUE_CUTOUT_STANDARD_MAP2_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP2_SCROLL.xy * _Time.x;

				float4 t1 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, uv1, VALUE_CUTOUT_STANDARD_MAP1_INTENSITY);
				float4 t2 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP2, VALUE_CUTOUT_STANDARD_MAP2_SAMPLER, uv2, VALUE_CUTOUT_STANDARD_MAP2_INTENSITY);
			#else
				float2 uv1 = dissolveUV.xy * VALUE_CUTOUT_STANDARD_MAP1_TILING.xy + VALUE_CUTOUT_STANDARD_MAP1_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP1_SCROLL.xy * _Time.x;
				float2 uv2 = dissolveUV.xy * VALUE_CUTOUT_STANDARD_MAP2_TILING.xy + VALUE_CUTOUT_STANDARD_MAP2_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP2_SCROLL.xy * _Time.x;

				float4 t1 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, uv1, VALUE_CUTOUT_STANDARD_MAP1_INTENSITY);
				float4 t2 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP2, VALUE_CUTOUT_STANDARD_MAP2_SAMPLER, uv2, VALUE_CUTOUT_STANDARD_MAP2_INTENSITY);
			#endif

			t1.a = t1[VALUE_CUTOUT_STANDARD_MAP1_CHANNEL];
			t2.a = t2[VALUE_CUTOUT_STANDARD_MAP2_CHANNEL];

			t1.a = lerp(t1.a, 1 - t1.a, VALUE_CUTOUT_STANDARD_MAP1_INVERT);
			t2.a = lerp(t2.a, 1 - t2.a, VALUE_CUTOUT_STANDARD_MAP2_INVERT);

			alphaSource = lerp((t1 * t2), (t1 + t2) * 0.5, VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE);

		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_THREE_CUSTOM_MAPS)

			#ifdef _AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE
				float2 uv1 = screenUV * VALUE_CUTOUT_STANDARD_MAP1_TILING.xy + VALUE_CUTOUT_STANDARD_MAP1_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP1_SCROLL.xy * _Time.x;
				float2 uv2 = screenUV * VALUE_CUTOUT_STANDARD_MAP2_TILING.xy + VALUE_CUTOUT_STANDARD_MAP2_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP2_SCROLL.xy * _Time.x;
				float2 uv3 = screenUV * VALUE_CUTOUT_STANDARD_MAP3_TILING.xy + VALUE_CUTOUT_STANDARD_MAP3_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP3_SCROLL.xy * _Time.x;
			#else
				float2 uv1 = dissolveUV.xy * VALUE_CUTOUT_STANDARD_MAP1_TILING.xy + VALUE_CUTOUT_STANDARD_MAP1_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP1_SCROLL.xy * _Time.x;
				float2 uv2 = dissolveUV.xy * VALUE_CUTOUT_STANDARD_MAP2_TILING.xy + VALUE_CUTOUT_STANDARD_MAP2_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP2_SCROLL.xy * _Time.x;
				float2 uv3 = dissolveUV.xy * VALUE_CUTOUT_STANDARD_MAP3_TILING.xy + VALUE_CUTOUT_STANDARD_MAP3_OFFSET.xy + VALUE_CUTOUT_STANDARD_MAP3_SCROLL.xy * _Time.x;
			#endif

			float4 t1 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP1, VALUE_CUTOUT_STANDARD_MAP1_SAMPLER, uv1, VALUE_CUTOUT_STANDARD_MAP1_INTENSITY);
			float4 t2 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP2, VALUE_CUTOUT_STANDARD_MAP2_SAMPLER, uv2, VALUE_CUTOUT_STANDARD_MAP2_INTENSITY);
			float4 t3 = READ_TEXTURE_2D(VALUE_CUTOUT_STANDARD_MAP3, VALUE_CUTOUT_STANDARD_MAP3_SAMPLER, uv3, VALUE_CUTOUT_STANDARD_MAP3_INTENSITY);

			t1.a = t1[VALUE_CUTOUT_STANDARD_MAP1_CHANNEL];
			t2.a = t2[VALUE_CUTOUT_STANDARD_MAP2_CHANNEL];
			t3.a = t3[VALUE_CUTOUT_STANDARD_MAP3_CHANNEL];

			t1.a = lerp(t1.a, 1 - t1.a, VALUE_CUTOUT_STANDARD_MAP1_INVERT);
			t2.a = lerp(t2.a, 1 - t2.a, VALUE_CUTOUT_STANDARD_MAP2_INVERT);
			t3.a = lerp(t3.a, 1 - t3.a, VALUE_CUTOUT_STANDARD_MAP3_INVERT);
		
			alphaSource = lerp((t1 * t2 * t3), (t1 + t2 + t3) / 3.0, VALUE_CUTOUT_STANDARD_MAPS_BLEND_TYPE);

		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_USER_DEFINED)

			alphaSource = saturate(customCutoutAlphaSource).xxxx;

			alphaSource.a = lerp(alphaSource.a, 1 - alphaSource.a, VALUE_CUTOUT_STANDARD_BASE_INVERT);

		#elif defined(_AD_CUTOUT_STANDARD_SOURCE_BASE_ALPHA)

			alphaSource = baseAlbedo;

			alphaSource.a = lerp(alphaSource.a, 1 - alphaSource.a, VALUE_CUTOUT_STANDARD_BASE_INVERT);

		#endif

		
		
		#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)				

			#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)
				float geometric = ADReadGeometricValue(positionOS, positionAbsolute, 0);
			#else				
				float noise = ((alphaSource.a - 0.5) * 2) * VALUE_GEOMETRIC_NOISE;

				float geometric = ADReadGeometricValue(positionOS, positionAbsolute, noise * (1 - VALUE_CUTOUT_STANDARD_CLIP));
			#endif
			
			
			#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)				
				alphaSource.a = geometric > 0 ? saturate(geometric) : 0;	
			#endif


			alphaSource.b = geometric;
			
		#endif	
		 
		return alphaSource;
	}
		
#endif


inline void AdvancedDissolveClip(float4 cutoutSource)
{
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)

		#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)
			clip(cutoutSource.a > 0 ? 1 : -1);
		#else
			//Noting to clip from
		#endif

	#else

		#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

			float cutoutEdge = (cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP * CUTOUT_CLIP_COEF);
			float geometricEdge = cutoutSource.b > 0 ? 0 : CUTOUT_CLIP_COEF;			
				
			clip(cutoutEdge - geometricEdge);

		#else

			clip(cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP * CUTOUT_CLIP_COEF);

		#endif

	#endif
}

void AdvancedDissolveCalculateAlphaAndClip(float4 cutoutSource, inout float alpha, inout float alphaClip)
{
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)

		#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

			float blend = cutoutSource.a > 0 ? 0 : 1;

			alpha = lerp(alpha, 0, blend);
			alphaClip = lerp(alphaClip, 1, blend);

		#else
			//Noting to clip from
		#endif

	#else

		#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

			float standardBlend = cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP * CUTOUT_CLIP_COEF;
			float geometricBlend = cutoutSource.b > 0 ? 0 : CUTOUT_CLIP_COEF;

			float blend = (standardBlend - geometricBlend) < 0 ? 1 : 0;
			float bledLerp = max(standardBlend, geometricBlend);

			alpha = lerp(alpha, lerp(cutoutSource.a, 0, bledLerp), blend);
			alphaClip = lerp(alphaClip, lerp(VALUE_CUTOUT_STANDARD_CLIP * CUTOUT_CLIP_COEF, 1, bledLerp), blend);

		#else			
		
			float blend = (cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP * CUTOUT_CLIP_COEF) < 0 ? 1 : 0;

			alpha = lerp(alpha, cutoutSource.a, blend);
			alphaClip = lerp(alphaClip, VALUE_CUTOUT_STANDARD_CLIP * CUTOUT_CLIP_COEF, blend);

		#endif

	#endif
}

//cutoutSource.rg - uv distortion
//cutoutSource.b - geometric 
//cutoutSource.a - texture

#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
float AdvancedDissolveAlbedoEmission(float4 cutoutSource, float4 baseAlbedo, float4 customEdgeColor, inout float3 albedo, inout float3 emission, inout float2 uv)
#else
float AdvancedDissolveAlbedoEmission(float4 cutoutSource, float4 baseAlbedo, inout float3 albedo, inout float3 emission, inout float2 uv)
#endif
{	
	float retValue = 0;

	#if defined(_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD) || defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC) || defined(_AD_EDGE_BASE_SOURCE_ALL)
		
		float dissolveValue = 0;
		float dissolveEdgeWidth = 0;


		#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)

			#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

				dissolveValue = cutoutSource.a;
				dissolveEdgeWidth = VALUE_EDGE_BASE_WIDTH_GEOMETRIC;

				#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
					dissolveEdgeWidth *= lerp(1, saturate(VALUE_GEOMETRIC_XYZ_ROLLOUT * 5), VALUE_GEOMETRIC_XYZ_STYLE);
				#endif

			#endif

		#else

			#if defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)

				#if defined (_AD_EDGE_BASE_SOURCE_CUTOUT_STANDARD)
				
					dissolveValue = cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP;
					dissolveEdgeWidth = VALUE_EDGE_BASE_WIDTH_STANDARD * (VALUE_CUTOUT_STANDARD_CLIP < 0.1 ? (VALUE_CUTOUT_STANDARD_CLIP * 10) : 1);

				#elif defined(_AD_EDGE_BASE_SOURCE_CUTOUT_GEOMETRIC)

					dissolveValue = cutoutSource.b;
					dissolveEdgeWidth = VALUE_EDGE_BASE_WIDTH_GEOMETRIC;

					#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
						dissolveEdgeWidth *= lerp(1, saturate(VALUE_GEOMETRIC_XYZ_ROLLOUT * 5), VALUE_GEOMETRIC_XYZ_STYLE);
					#endif

				#elif defined(_AD_EDGE_BASE_SOURCE_ALL)
			
					//Cutout
					float dissolveValue1 = cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP;
					float dissolveEdgeWidth1 = VALUE_EDGE_BASE_WIDTH_STANDARD * (VALUE_CUTOUT_STANDARD_CLIP < 0.1 ? (VALUE_CUTOUT_STANDARD_CLIP * 10) : 1);


					//Geometric
					float dissolveValue2 = cutoutSource.b;
					float dissolveEdgeWidth2 = VALUE_EDGE_BASE_WIDTH_GEOMETRIC;

					float xyzAxisCoef = 0.998;
					#if defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
						xyzAxisCoef *= lerp(1, saturate(VALUE_GEOMETRIC_XYZ_ROLLOUT * 5), VALUE_GEOMETRIC_XYZ_STYLE);
						dissolveEdgeWidth2 *= xyzAxisCoef;
					#endif


					//Lerp
					float lerpValue = (cutoutSource.b - VALUE_EDGE_BASE_WIDTH_GEOMETRIC * xyzAxisCoef);
					lerpValue = lerpValue > 0 ? 1 : 0;


					dissolveValue = lerp(dissolveValue2, dissolveValue1, lerpValue);
					dissolveEdgeWidth = lerp(dissolveEdgeWidth2, dissolveEdgeWidth1, lerpValue);

				#endif

			#else

				dissolveValue = cutoutSource.a - VALUE_CUTOUT_STANDARD_CLIP;
				dissolveEdgeWidth = VALUE_EDGE_BASE_WIDTH_STANDARD * (VALUE_CUTOUT_STANDARD_CLIP < 0.1 ? (VALUE_CUTOUT_STANDARD_CLIP * 10) : 1);

			#endif

		#endif

		


		if (dissolveEdgeWidth > 0 && dissolveEdgeWidth > dissolveValue)
		{
			float edgeGradient = saturate(dissolveValue) * (1.0 / saturate(dissolveEdgeWidth));
			float invertGradient = 1 - edgeGradient;


			//Only 'Solid' shape
			#if defined(_AD_EDGE_BASE_SOURCE_ALL)
				float shapeStyle = 1;
			#else
				float3 shape;
				shape.x = 1;
				shape.y = invertGradient;
				shape.z = invertGradient * invertGradient * invertGradient;
				float shapeStyle = shape[(int)VALUE_EDGE_BASE_SHAPE];
			#endif



			#if !defined(ADVANCED_DISSOLVE_SHADER_GRAPH) && !defined(ADVANCED_DISSOLVE_TEXT_MESH_PRO)
			
				float2 distortion = 0;
				#if defined(_AD_EDGE_UV_DISTORTION_SOURCE_CUSTOM_MAP)
					distortion = READ_TEXTURE_2D(VALUE_EDGE_UV_DISTORTION_MAP, VALUE_EDGE_UV_DISTORTION_MAP_SAMPLER, uv * VALUE_EDGE_UV_DISTORTION_MAP_TILING + VALUE_EDGE_UV_DISTORTION_MAP_OFFSET + VALUE_EDGE_UV_DISTORTION_MAP_SCROLL * _Time.x, 1).rg;
				#else
					distortion = cutoutSource.rg;
				#endif

				uv += (distortion - 0.5) * VALUE_EDGE_UV_DISTORTION_STRENGTH * shapeStyle;
			#endif


			float4 edgeColor = float4(VALUE_EDGE_BASE_COLOR.rgb, VALUE_EDGE_BASE_COLOR_TRANSPARENCY);	
			float colorIntensity = VALUE_EDGE_BASE_COLOR_INTENSITY.y * VALUE_EDGE_BASE_COLOR_TRANSPARENCY * (shapeStyle * shapeStyle);	//x - default value, y - exponental
		

			#if defined(_AD_EDGE_ADDITIONAL_COLOR_BASE_COLOR)

				float4 edgeAdditionalColor = baseAlbedo;

				edgeAdditionalColor.a = saturate(edgeAdditionalColor.a + VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET);

				edgeColor *= edgeAdditionalColor;

			#elif defined(_AD_EDGE_ADDITIONAL_COLOR_CUSTOM_MAP)

				float4 edgeAdditionalColor = READ_TEXTURE_2D_LOD(VALUE_EDGE_ADDITIONAL_COLOR_MAP, VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER, uv * VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING + VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET + VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL * _Time.x, VALUE_EDGE_ADDITIONAL_COLOR_MAP_MIPMAP);

				edgeAdditionalColor.a = saturate(edgeAdditionalColor.a + VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET);

				edgeColor *= edgeAdditionalColor;

			#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_MAP)
				
				#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)	 
					float u1 = lerp(invertGradient, edgeGradient, VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE);
					u1 = u1 * VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING.x +  VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET.x + VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL.x * _Time.x;

					float4 edgeAdditionalColor = READ_TEXTURE_2D(VALUE_EDGE_ADDITIONAL_COLOR_MAP, VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER, float2(u1, 0.5), 1);
				#else
					float u1 = lerp(invertGradient, edgeGradient, VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE);
					u1 = u1 * VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING.x +  VALUE_EDGE_ADDITIONAL_COLOR_MAP_OFFSET.x + VALUE_EDGE_ADDITIONAL_COLOR_MAP_SCROLL.x * _Time.x;

					float u2 = lerp(VALUE_CUTOUT_STANDARD_CLIP, 1 - VALUE_CUTOUT_STANDARD_CLIP, VALUE_EDGE_ADDITIONAL_COLOR_MAP_REVERSE);
					u2 *= VALUE_EDGE_ADDITIONAL_COLOR_MAP_TILING.x;

					float4 edgeAdditionalColor = READ_TEXTURE_2D(VALUE_EDGE_ADDITIONAL_COLOR_MAP, VALUE_EDGE_ADDITIONAL_COLOR_MAP_SAMPLER, float2(lerp(u1, u2, VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION), 0.5), 1);
				#endif
			 
				edgeAdditionalColor.a = saturate(edgeAdditionalColor.a + VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET);	
			
				edgeColor *= edgeAdditionalColor;

			#elif defined(_AD_EDGE_ADDITIONAL_COLOR_GRADIENT_COLOR)

				#if defined(_AD_CUTOUT_STANDARD_SOURCE_NONE)	
					float lerpValue = saturate(edgeGradient + VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET);
				#else
					float lerpValue = lerp(saturate(edgeGradient + VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET), VALUE_CUTOUT_STANDARD_CLIP + VALUE_EDGE_ADDITIONAL_COLOR_PHASE_OFFSET, VALUE_EDGE_ADDITIONAL_COLOR_CLIP_INTERPOLATION);
				#endif

				float3 c1 = lerp(float3(0, 0, 0), edgeColor.rgb, edgeColor.a);
				float3 c2 = lerp(float3(0, 0, 0), VALUE_EDGE_ADDITIONAL_COLOR.rgb, VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY);

				edgeColor = lerp(float4(c1, edgeColor.a), float4(c2, VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY), lerpValue);
				colorIntensity = lerp(colorIntensity, VALUE_EDGE_ADDITIONAL_COLOR_INTENSITY.y * VALUE_EDGE_ADDITIONAL_COLOR_TRANSPARENCY, lerpValue);

			#elif defined(_AD_EDGE_ADDITIONAL_COLOR_USER_DEFINED)
			
				float4 edgeAdditionalColor = customEdgeColor;

				edgeAdditionalColor.a = saturate(edgeAdditionalColor.a + VALUE_EDGE_ADDITIONAL_COLOR_ALPHA_OFFSET);	

				edgeColor *= edgeAdditionalColor;

			#endif


		
			edgeColor.a *= shapeStyle;
			edgeColor = saturate(edgeColor);			

			albedo = edgeColor.rgb;
				
			#ifdef ADVANCED_DISSOLVE_META_PASS
				emission = edgeColor.rgb * (1 + colorIntensity) * VALUE_EDGE_GI_META_PASS_MULTIPLIER;					
			#else
				emission = edgeColor.rgb * colorIntensity;
			#endif
			

			retValue = edgeColor.a;
		}


		#ifdef ADVANCED_DISSOLVE_META_PASS
			emission *= dissolveValue <= 0 ? 0 : 1; 
		#endif

	#endif

	return retValue;
}


#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
float4 ADCalculateDissolveUV(float2 texcoord, float3 positionWS)
#else
float4 ADCalculateDissolveUV(float2 texcoord, float4 positionCS)
#endif
{
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_SCREEN_SPACE)

		#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
			return ComputeScreenPos(TransformWorldToHClip(positionWS), _ProjectionParams.x);
		#else
			return ComputeScreenPos(positionCS);
		#endif

	#else
		
		return float4(texcoord, 0, 0);

	#endif
}


float4 ADCalculateCutoutSource(float4 baseAlbedo, float4 dissolveUV, float3 positionOS, float3 positionAbsolute, float3 normalOS, float3 normalWS, float customCutoutAlphaSource)
{
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
		return ADReadCutoutSource(positionOS, positionAbsolute, normalOS, normalWS);
	#else
		return ADReadCutoutSource(baseAlbedo, dissolveUV, positionOS, positionAbsolute, customCutoutAlphaSource);
	#endif		
}

float4 ADCalculateCutoutSourceOS(float4 baseAlbedo, float4 dissolveUV, float3 positionOS, float3 normalOS)
{	
	float3 positionAbsolute = positionOS;
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR) || defined(ADVANCED_DISSOLVE_CUTOUT_GEOMETRIC_ENABLED)
		positionAbsolute = ADTransformObjectToWorld(positionOS);
	#endif

	float3 normalWS = normalOS;
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
		normalWS = ADTransformObjectToWorldNormal(float3(normalOS));
	#endif

		
	return ADCalculateCutoutSource(baseAlbedo, dissolveUV, positionOS, positionAbsolute, normalOS, normalWS, 0);		
}

float4 ADCalculateCutoutSourceWS(float4 baseAlbedo, float4 dissolveUV, float3 positionAbsolute, float3 normalWS)
{	
	float3 positionOS = 0;
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR) || defined(_AD_CUTOUT_GEOMETRIC_TYPE_XYZ)
		positionOS = ADTransformWorldToObject(positionAbsolute);
	#endif

	float3 normalOS = 0;
	#if defined(_AD_CUTOUT_STANDARD_SOURCE_MAPS_MAPPING_TYPE_TRIPLANAR)
		normalOS = ADTransformWorldToObjectNormal(normalWS);
	#endif


	return ADCalculateCutoutSource(baseAlbedo, dissolveUV, positionOS, positionAbsolute, normalOS, normalWS, 0);		
}



#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
	#define ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE(baseAlbedo, texcoord, positionOS, positionWS, positionAbsolute, normalOS, normalWS, customCutoutAlphaSource)	float4 advancedDissolveUV = ADCalculateDissolveUV(texcoord, positionWS);	\
																																								float4 cutoutSource = ADCalculateCutoutSource(baseAlbedo, advancedDissolveUV, positionOS, positionAbsolute, normalOS, normalWS, customCutoutAlphaSource);
#else
	#define ADVANCED_DISSOLVE_UV(index)																															float4 advancedDissolveUV : TEXCOORD##index;
	#define ADVANCED_DISSOLVE_INIT_UV(i, texcoord, positionCS)																									i.advancedDissolveUV = ADCalculateDissolveUV(texcoord.xy, positionCS);
	#define ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE_USING_OS(i, baseAlbedo, positionOS, normalOS)																	float4 cutoutSource = ADCalculateCutoutSourceOS(baseAlbedo, i.advancedDissolveUV, positionOS, normalOS);
	#define ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE_USING_WS(i, baseAlbedo, positionAbsolute, normalWS)															float4 cutoutSource = ADCalculateCutoutSourceWS(baseAlbedo, i.advancedDissolveUV, positionAbsolute, normalWS);
#endif 

	
#if defined(ADVANCED_DISSOLVE_SHADER_GRAPH)
	void AdvancedDissolveShaderGraph(float2 uv, float3 positionOS, float3 positionWS, float3 positionAbsolute, float3 normalOS, float3 normalWS, float customCutoutAlphaSource, float4 customEdgeColor, inout float3 color, inout float3 emission, inout float alpha, inout float alphaClip)
	{
		#if defined(_AD_STATE_ENABLED)
			float4 baseAlbedo = float4(color, alpha);
					
			ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE(baseAlbedo, uv, positionOS, positionWS, positionAbsolute, normalOS, normalWS, customCutoutAlphaSource)

			float3 dissolveAlbedo = 0;
			float3 dissolveEmission = 0;
			float dissolveBlend = AdvancedDissolveAlbedoEmission(cutoutSource, baseAlbedo, customEdgeColor, dissolveAlbedo, dissolveEmission, uv);

			color = lerp(color, dissolveAlbedo, dissolveBlend);
			emission = lerp(emission, dissolveEmission, dissolveBlend);

			AdvancedDissolveCalculateAlphaAndClip(cutoutSource, alpha, alphaClip);
		#endif	
	}

	void AdvancedDissolveShaderGraph(float2 uv, float3 positionOS, float3 positionWS, float3 positionAbsolute, float3 normalOS, float3 normalWS, float customCutoutAlphaSource, float4 customEdgeColor, inout float3 color, inout float alpha, inout float alphaClip)
	{
		#if defined(_AD_STATE_ENABLED)
			float4 baseAlbedo = float4(color, alpha);
					
			ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE(baseAlbedo, uv, positionOS, positionWS, positionAbsolute, normalOS, normalWS, customCutoutAlphaSource)

			float3 dissolveAlbedo = 0;
			float3 dissolveEmission = 0;
			float dissolveBlend = AdvancedDissolveAlbedoEmission(cutoutSource, baseAlbedo, customEdgeColor, dissolveAlbedo, dissolveEmission, uv);

			color = lerp(color, dissolveAlbedo, dissolveBlend);
			color += lerp(float3(0, 0, 0), dissolveEmission, dissolveBlend);

			AdvancedDissolveCalculateAlphaAndClip(cutoutSource, alpha, alphaClip);
		#endif	
	} 

	void AdvancedDissolveShaderGraph(float2 uv, float3 positionOS, float3 positionWS, float3 positionAbsolute, float3 normalOS, float3 normalWS, float customCutoutAlphaSource, float4 customEdgeColor, inout float alpha, inout float alphaClip)
	{
		#if defined(_AD_STATE_ENABLED)
			float4 baseAlbedo = float4(0, 0, 0, alpha);
					
			ADVANCED_DISSOLVE_SETUP_CUTOUT_SOURCE(baseAlbedo, uv, positionOS, positionWS, positionAbsolute, normalOS, normalWS, customCutoutAlphaSource)

			AdvancedDissolveCalculateAlphaAndClip(cutoutSource, alpha, alphaClip);
		#endif
	}
#endif

#endif //ADVANCED_DISSOLVE_CORE_CGINC