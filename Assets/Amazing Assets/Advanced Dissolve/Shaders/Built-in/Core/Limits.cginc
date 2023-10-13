#ifndef ADVANCED_DISSOLVE_LIMITS_CGINC
#define ADVANCED_DISSOLVE_LIMITS_CGINC


//Standard 
#if SHADER_TARGET < 40

	//Not detail and secondary textures
	#if defined(_DETAIL_MULX2)
	#undef _DETAIL_MULX2
	#endif

	#if defined(_DETAIL_MUL)
	#undef _DETAIL_MUL
	#endif

	#if defined(_DETAIL_ADD)
	#undef _DETAIL_ADD
	#endif

	#if defined(_DETAIL_LERP)
	#undef _DETAIL_LERP
	#endif

	//No paralax
	#if defined(_PARALLAXMAP)
	#undef _PARALLAXMAP
	#endif

#endif


//Unlit & One Directional Light 
#if SHADER_TARGET < 30

	//No reflection with secondary textures and emission
	#if defined(_TEXTUREMIX_BY_MAIN_ALPHA) || defined(_TEXTUREMIX_BY_SECONDARY_ALPHA) || defined(_TEXTUREMIX_MULTIPLE) || defined(_TEXTUREMIX_ADDITIVE) || defined(_TEXTUREMIX_BY_VERTEX_ALPHA)
		#if defined(_EMISSION) && defined(_REFLECTION)
		#undef _REFLECTION
		#endif
	#endif

#endif


//All
#if SHADER_TARGET < 30
	
	//No bump maps
	#if defined(_NORMALMAP)
	#undef _NORMALMAP
	#endif

#endif


#endif	//ADVANCED_DISSOLVE_LIMITS_CGINC


//https://docs.unity3d.com/ScriptReference/SystemInfo-graphicsShaderLevel.html