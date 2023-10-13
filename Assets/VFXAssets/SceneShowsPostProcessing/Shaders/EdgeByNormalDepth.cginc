//检测该cginc是否被声明
// Upgrade NOTE: excluded shader from  OpenGL ES 2.0 because it uses unsized arrays
 #pragma exclude_renderers gles
#ifndef EDGE_NORMAL_DEPTH_INCLUDED
#define EDGE_NORMAL_DEPTH_INCLUDED

uniform sampler2D _CameraGBufferTexture2;
uniform sampler2D _CameraDepthTexture;
float valueStep(float stepValue,float value)
{
    //return step(stepValue,value);
    return smoothstep(stepValue-0.1,stepValue+0.1,value);
}
half edgeByNormalDepth(float2 uv,float4 ScreenParams,float _Sensitivity_Normal,float _Sensitivity_Depth)
{
_Sensitivity_Depth/=1000;
//_Sensitivity_Normal/=1000;
    float2 uvs[5]; 
    uvs[0] = uv;
    uvs[1] = uv + ScreenParams.xy*half2(1,1);
    uvs[2] = uv + ScreenParams.xy*half2(-1,-1);
    uvs[3] = uv + ScreenParams.xy*half2(-1,1);
    uvs[4] = uv + ScreenParams.xy*half2(1,-1);

    float3 sampleNormal1=tex2D(_CameraGBufferTexture2,uvs[1]);
    float3 sampleNormal2=tex2D(_CameraGBufferTexture2,uvs[2]);
    //判断法线差异
    float3 normalDiff1=abs(sampleNormal1-sampleNormal2);
    //取法线值rgb的明度值
    float normalGray1= 0.3 * normalDiff1.r + 0.6 * normalDiff1.g + 0.1 * normalDiff1.b;
//法线差异是否小于一个范围
   // float normalGray1_Final=step(_Sensitivity_Normal,normalGray1);     //normalGray1_Final
        float normalGray1_Final=valueStep(_Sensitivity_Normal,normalGray1);     //normalGray1_Final

    float3 sampleNormal3=tex2D(_CameraGBufferTexture2,uvs[3]);
    float3 sampleNormal4=tex2D(_CameraGBufferTexture2,uvs[4]);
    //判断法线差异
    float3 normalDiff2=abs(sampleNormal3-sampleNormal4);
    //取法线值的灰度值
    float normalGray2=0.3 * normalDiff2.r + 0.6 * normalDiff2.g + 0.1 * normalDiff2.b;
//法线差异是否小于一个范围
    float normalGray2_Final=valueStep(_Sensitivity_Normal,normalGray2);     //normalGray2_Final

    float sampleDepth1=tex2D(_CameraDepthTexture,uvs[1]).r;
    sampleDepth1=LinearEyeDepth(sampleDepth1);
    float sampleDepth2=tex2D(_CameraDepthTexture,uvs[2]).r;
    sampleDepth2=LinearEyeDepth(sampleDepth2);
    float depthDiff1=abs(sampleDepth1-sampleDepth2);
 
    
    //深度差异是否小于一个范围
    float depthDiff1_Final=valueStep(_Sensitivity_Depth,depthDiff1);
    
    float sampleDepth3=tex2D(_CameraDepthTexture,uvs[3]).r;
    sampleDepth3=LinearEyeDepth(sampleDepth3);
    float sampleDepth4=tex2D(_CameraDepthTexture,uvs[4]).r;
    sampleDepth4=LinearEyeDepth(sampleDepth4);
    //判断深度差异
    float depthDiff2=abs(sampleDepth3-sampleDepth4);
    //深度差异是否小于一个范围
    float depthDiff2_Final=valueStep(_Sensitivity_Depth,depthDiff2);

    depthDiff1_Final*=depthDiff2_Final;
    normalGray1_Final*=normalGray2_Final;

return normalGray1_Final*depthDiff1_Final;
}



#endif//EDGE_NORMAL_DEPTH_INCLUDED