Shader "Prozac/Tile"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineTex("Texture", 2D) = "white" {}
        _OuterContourTex("Texture", 2D) = "white" {}
        _lineWidth("lineWidth",Range(0,1)) = 1
        [HDR]_lineColor("lineColor",Color)=(1,1,1,1)
        [HDR]_OuterColor("OuterColor",Color)=(1,1,1,1)
    }
    SubShader
    {
        // 渲染队列采用 透明
        Tags{
            "Queue" = "Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _OutlineTex;
            sampler2D _OuterContourTex;

            float4 _MainTex_TexelSize;
            float _lineWidth;

            float4 _lineColor;
            float4 _OuterColor;

            fixed4 frag (VertexOutput i) : SV_Target
            {
                
                
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 flag = tex2D(_OutlineTex,i.uv);
                fixed4 outer = tex2D(_OuterContourTex,i.uv);
                /*// 采样周围4个点
                float2 up_uv = i.uv + float2(0,1) * _lineWidth * _MainTex_TexelSize.xy;
                float2 down_uv = i.uv + float2(0,-1) * _lineWidth * _MainTex_TexelSize.xy;
                float2 left_uv = i.uv + float2(-1,0) * _lineWidth * _MainTex_TexelSize.xy;
                float2 right_uv = i.uv + float2(1,0) * _lineWidth * _MainTex_TexelSize.xy;
                // 如果有一个点透明度为0 说明是边缘
                float w = tex2D(_MainTex,up_uv).a * tex2D(_MainTex,down_uv).a * tex2D(_MainTex,left_uv).a * tex2D(_MainTex,right_uv).a;
                */

                //col.rgb = lerp(_lineColor,col.rgb,w);

                //col.rgb = smoothstep(_lineColor,col.rgb,flag.x);

                col.rgb = _lineColor * (1 - outer.a) + _OuterColor * outer.a;
                col.a = pow(flag.x,1/_lineWidth);
                
                return col;
            }
            ENDCG
        }
    }
}