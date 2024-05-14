Shader "Custom/Walls"
{
    Properties {
            _MainTex ("Texture", 2D) = "white" {} // Textura opcional
        }
        SubShader {
            Tags { "RenderType"="Opaque" "Queue"="Geometry" } // Define a renderização como opaca
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
    
                struct appdata {
                    float4 vertex : POSITION;
                };
    
                struct v2f {
                    float4 pos : SV_POSITION;
                };
    
                sampler2D _MainTex;
    
                v2f vert (appdata v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    return o;
                }
    
                half4 frag (v2f i) : SV_Target {
                    return half4(0, 0, 0, 0.5); // Cor preta com alpha para transparência
                }
                ENDCG
            }
        }
    }