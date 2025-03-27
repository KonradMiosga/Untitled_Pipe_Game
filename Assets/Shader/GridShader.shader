Shader "Custom/WorldGridOverlay"
{
    Properties
    {
        _Color ("Grid Color", Color) = (0.2, 0.6, 1, 1)
        _LineWidth ("Line Width", Range(0.001, 0.1)) = 0.02
        _GridScale ("Grid Scale", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float _GridScale;
            float4 _Color;
            float _LineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float3 grid = abs(frac(i.worldPos / _GridScale) - 0.5);
                float gridLine = min(min(grid.x, grid.y), grid.z);
                float mask = step(_LineWidth, gridLine);
                return lerp(_Color, float4(0, 0, 0, 1), 1 - mask); // grid lines = black
            }
            ENDCG
        }
    }
}
