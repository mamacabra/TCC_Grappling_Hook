Shader "Custom/ProceduralMetalShaderURPPerlim"
{
    Properties
    {
        _TileA_Albedo ("Tile A Albedo", 2D) = "white" {}
        _TileA_Metallic ("Tile A Metallic", Range(0,1)) = 0.8
        _TileA_Smoothness ("Tile A Smoothness", Range(0,1)) = 0.9
        _TileB_Albedo ("Tile B Albedo", 2D) = "white" {}
        _TileB_Metallic ("Tile B Metallic", Range(0,1)) = 0.6
        _TileB_Smoothness ("Tile B Smoothness", Range(0,1)) = 0.7
        _EdgePower ("Edge Power", Range(1,10)) = 5
        _CurvatureStrength ("Curvature Strength", Range(0,1)) = 0.5
        _TileA_Color ("Tile A Color", Color) = (1,1,1,1)
        _TileB_Color ("Tile B Color", Color) = (1,1,1,1)
        _BrushScale ("Brush Noise Scale", Float) = 10
        _BrushStretch ("Brush Noise Stretch", Float) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 300

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

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
                float2 lightmapUV : TEXCOORD1;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                half3 normalWS : TEXCOORD1;
                half3 viewDirWS : TEXCOORD2;
                float2 lightmapUV : TEXCOORD3;
                half3 vertexSH : TEXCOORD4;
                float3 positionWS : TEXCOORD5;
            };

            TEXTURE2D(_TileA_Albedo);
            SAMPLER(sampler_TileA_Albedo);
            TEXTURE2D(_TileB_Albedo);
            SAMPLER(sampler_TileB_Albedo);

            CBUFFER_START(UnityPerMaterial)
                float4 _TileA_Albedo_ST;
                float _TileA_Metallic;
                float _TileA_Smoothness;
                float4 _TileB_Albedo_ST;
                float _TileB_Metallic;
                float _TileB_Smoothness;
                float _EdgePower;
                float _CurvatureStrength;
                float4 _TileA_Color;
                float4 _TileB_Color;
                float _BrushScale;
                float _BrushStretch;
            CBUFFER_END

            // Funções de Ruído Perlin
            float2 random2(float2 p) {
                return frac(sin(float2(dot(p, float2(127.1, 311.7)), dot(p, float2(269.5, 183.3)))) * 43758.5453);
            }

            float perlinNoise(float2 p) {
                float2 i = floor(p);
                float2 f = frac(p);
                float2 u = f * f * (3.0 - 2.0 * f);

                float2 a = random2(i + float2(0.0, 0.0));
                float2 b = random2(i + float2(1.0, 0.0));
                float2 c = random2(i + float2(0.0, 1.0));
                float2 d = random2(i + float2(1.0, 1.0));

                float va = dot(a, f - float2(0.0, 0.0));
                float vb = dot(b, f - float2(1.0, 0.0));
                float vc = dot(c, f - float2(0.0, 1.0));
                float vd = dot(d, f - float2(1.0, 1.0));

                return lerp(lerp(va, vb, u.x), lerp(vc, vd, u.x), u.y);
            }

            float fbm(float2 p) {
                float value = 0.0;
                float amplitude = 0.5;
                float frequency = 1.0;
                for (int i = 0; i < 4; i++) {
                    value += amplitude * perlinNoise(p * frequency);
                    p *= 2.0;
                    amplitude *= 0.5;
                }
                return value;
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _TileA_Albedo);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                float3 positionWS = TransformObjectToWorld(IN.positionOS.xyz);
                OUT.positionWS = positionWS;
                OUT.viewDirWS = GetWorldSpaceViewDir(positionWS);
                OUTPUT_LIGHTMAP_UV(IN.lightmapUV, unity_LightmapST, OUT.lightmapUV);
                OUT.vertexSH = SampleSHVertex(OUT.normalWS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half3 normalWS = normalize(IN.normalWS);
                half3 viewDirWS = normalize(IN.viewDirWS);

                float fresnel = pow(1.0 - saturate(dot(normalWS, viewDirWS)), _EdgePower);
                float2 offsetUV = IN.uv + normalWS.xy * 0.01;
                float curvatureSample = SAMPLE_TEXTURE2D(_TileB_Albedo, sampler_TileB_Albedo, offsetUV).r;
                float curvatureMask = saturate(curvatureSample);
                float combinedMask = fresnel * curvatureMask;

                float2 stretchedUV = float2(IN.uv.x * _BrushScale, IN.uv.y * _BrushScale * _BrushStretch);
                float tileC = fbm(stretchedUV);
                tileC = (tileC + 1.0) / 2.0; // Remap from [-1,1] to [0,1]

                float finalMask = lerp(combinedMask, tileC, _CurvatureStrength);

                half4 tileA_Albedo = SAMPLE_TEXTURE2D(_TileA_Albedo, sampler_TileA_Albedo, IN.uv) * _TileA_Color;
                half4 tileB_Albedo = SAMPLE_TEXTURE2D(_TileB_Albedo, sampler_TileB_Albedo, IN.uv) * _TileB_Color;

                half4 albedo = lerp(tileA_Albedo, tileB_Albedo, finalMask);
                float metallic = lerp(_TileA_Metallic, _TileB_Metallic, finalMask);
                float smoothness = lerp(_TileA_Smoothness, _TileB_Smoothness, finalMask);

                SurfaceData surfaceData;
                surfaceData.albedo = albedo.rgb;
                surfaceData.specular = half3(0,0,0);
                surfaceData.metallic = metallic;
                surfaceData.smoothness = smoothness;
                surfaceData.normalTS = half3(0,0,1);
                surfaceData.emission = half3(0,0,0);
                surfaceData.occlusion = 1.0;
                surfaceData.alpha = albedo.a;
                surfaceData.clearCoatMask = 0.0;
                surfaceData.clearCoatSmoothness = 0.0;

                InputData inputData;
                inputData.positionWS = IN.positionWS;
                inputData.normalWS = normalWS;
                inputData.viewDirectionWS = viewDirWS;
                inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
                inputData.fogCoord = ComputeFogFactor(IN.positionHCS.z);
                inputData.vertexLighting = half3(0,0,0);
                inputData.bakedGI = SAMPLE_GI(IN.lightmapUV, IN.vertexSH, normalWS);

                half4 color = UniversalFragmentPBR(inputData, surfaceData);
                color.rgb = MixFog(color.rgb, inputData.fogCoord);
                return color;
            }
            ENDHLSL
        }
    }
    FallBack "Universal Render Pipeline/Lit"
}