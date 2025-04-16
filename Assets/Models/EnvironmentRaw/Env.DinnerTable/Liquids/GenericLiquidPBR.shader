Shader "Custom/LiquidPBR"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Normal Scale", Float) = 1.0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _SurfaceAlpha ("Surface Alpha", Range(0,1)) = 0.5
        _DepthIntensity ("Depth Intensity", Range(0,1)) = 0.5
        _MinY ("Min Y", Float) = 0.0
        _MaxY ("Max Y", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 300

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 tangentWS : TEXCOORD3;
                float objectY : TEXCOORD4;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_ST;
            TEXTURE2D(_BumpMap);
            SAMPLER(sampler_BumpMap);
            half4 _Color;
            half _Glossiness;
            half _SurfaceAlpha;
            half _DepthIntensity;
            float _MinY;
            float _MaxY;
            float _BumpScale;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.tangentWS = float4(TransformObjectToWorldDir(IN.tangentOS.xyz), IN.tangentOS.w);
                OUT.objectY = IN.positionOS.y;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Amostra textura e cor
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _Color;
                half3 normalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, IN.uv), _BumpScale);

                // Calcula a profundidade
                float depth = (_MaxY - IN.objectY) / (_MaxY - _MinY);
                depth = saturate(depth);

                // Calcula a transparência baseada na profundidade
                float alpha = lerp(_SurfaceAlpha, 1.0, depth * _DepthIntensity);
                alpha = saturate(alpha);

                // Configura SurfaceData para PBR
                SurfaceData surfaceData;
                surfaceData.albedo = color.rgb;
                surfaceData.specular = half3(0,0,0);
                surfaceData.metallic = 0.0;
                surfaceData.smoothness = _Glossiness;
                surfaceData.normalTS = normalTS;
                surfaceData.emission = half3(0,0,0);
                surfaceData.occlusion = 1.0;
                surfaceData.alpha = alpha;
                surfaceData.clearCoatMask = 0.0;
                surfaceData.clearCoatSmoothness = 0.0;

                // Configura InputData para iluminação
                InputData inputData;
                inputData.positionWS = IN.positionWS;
                inputData.normalWS = IN.normalWS;
                inputData.viewDirectionWS = GetWorldSpaceNormalizeViewDir(IN.positionWS);
                inputData.shadowCoord = TransformWorldToShadowCoord(IN.positionWS);
                inputData.fogCoord = ComputeFogFactor(IN.positionHCS.z);
                inputData.vertexLighting = half3(0,0,0);
                inputData.bakedGI = SAMPLE_GI(0, half3(0,0,0), IN.normalWS);
                inputData.tangentToWorld = float3x3(IN.tangentWS.xyz, cross(IN.normalWS, IN.tangentWS.xyz) * IN.tangentWS.w, IN.normalWS);

                // Calcula cor final com iluminação PBR
                half4 finalColor = UniversalFragmentPBR(inputData, surfaceData);
                return finalColor;
            }
            ENDHLSL
        }
    }
}