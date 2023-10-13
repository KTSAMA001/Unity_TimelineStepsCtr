Shader "GUIStudy"
{
    Properties
    { 
        _MainTex("主贴图",2D) = "white"{}
        _MainColor("主颜色",color) = (1,1,1,1)
        _Range("测试Range",range(0,1)) = 0
        _Float("测试Float",float) = 0
        [Toggle(_RED_ON)] _Red("红色！",float) = 0
    }
    SubShader
    {
        Tags {"Queue"="Geometry" "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }
        Pass
        {
            HLSLPROGRAM

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma shader_feature_local _RED_ON

           

            struct Attributes
            {
                float4 positionOS   : POSITION;  
                float2 uv : TEXCOORD0;              
            };
            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                float2 uv : TEXCOORD0;
                float fogCoord : TEXCOORD1;
            };            
            
       

            Varyings vert(Attributes v)
            {
                Varyings o;
                 o.positionHCS = 1;
                // o.uv = TRANSFORM_TEX(v.uv,_MainTex);
                // o.fogCoord = ComputeFogFactor(o.positionHCS.z);
                return o;
            }    
            half4 frag(Varyings i) : SV_Target
            {
                // half4 c = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv);
                // c *= _MainColor;
                // c.rgb = MixFog(c.rgb,i.fogCoord);
                // c = lerp(c,saturate(_Float),_Range);

                // #if defined(_RED_ON)
                // {
                //     c.rgb = half3(1,0,0);
                // }
                // #endif

                // return c;
                return 1;
            }
            ENDHLSL
        }
    }
    CustomEditor "XXX"
}