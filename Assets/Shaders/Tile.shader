Shader "Prozac/Tile"
{
    Properties
    {
        [PerRendererData]_MainTex("MainTexture", 2D) = "white" {}
        _OutlineTex("OutlineTexture", 2D) = "white" {}
        //_OuterContourTex("OuterContourTexture", 2D) = "white" {}
        _lineWidth("lineWidth",Range(0,1)) = 1
        //_OutLineWidth("OutLineWidth",Range(0,30)) = 1

        _InsideColor("InsideColor",Color)=(1,1,1,1)
        _InsideColorStrength("InsideColorStrength",Float) = 2.5
        _OutsideColor("OutsideColor",Color)=(1,1,1,1)
        _OutsideColorStrength("OutsideColorStrength",Float) = 5
        
        //[HDR]_OutLineColor("OutLineColor",Color)=(1,1,1,1)
        
        _NoiseTex("NoiseTexture",2D) = "white"{}
        //_NoiseStrength("NoiseStrength",range(0,1)) = 0.5
        
        _FlowMap("FlowMap",2D) = "white"{}
        _FlowSpeed("FlowSpeed",range(0,10)) = 1
    }
    SubShader
    {
        // 渲染队列采用 透明
        Tags{
            "Queue" = "Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        //Zwrite On
        //Ztest On

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
            //float _OutLineWidth;

            float4 _InsideColor;
            float4 _OutsideColor;
            //float4 _OutLineColor;

            float _InsideColorStrength;
            float _OutsideColorStrength;
            
            sampler2D _NoiseTex;
            //float _NoiseStrength;

            sampler2D _FlowMap;
            float _FlowSpeed;
            
            fixed4 frag (VertexOutput i) : SV_Target
            {
                //(155,155,155)(255,255,255)
                fixed3 flowDir = tex2D(_FlowMap,i.uv) * 2.0 - 1.0;
                
                flowDir *= _FlowSpeed;
                
                float phase0 = frac(_Time * _FlowSpeed);
                float phase1 = frac(_Time * _FlowSpeed + 0.5);
                //fixed4 c = tex2D(_MainTex,i.uv);
                fixed4 col0 = tex2D(_MainTex, i.uv);
                //fixed4 col1 = tex2D(_MainTex, i.uv + flowDir.xy * phase0);
                //fixed4 col2 = tex2D(_MainTex, i.uv + flowDir.xy * phase1);

                //col1 = col1.r < 0.9f ? col1 : float4(0,0,0,0);
                //col2 = col2.r < 0.9f ? col2 : float4(0,0,0,0);
                float4 noise0 = tex2D(_NoiseTex,i.uv + flowDir.xy * phase0);
                float4 noise1 = tex2D(_NoiseTex,i.uv + flowDir.xy * phase1);
               // float flash = step(noise.r,c.r);
                
                fixed4 flag = tex2D(_OutlineTex,i.uv);
                //fixed4 outer = tex2D(_OuterContourTex,i.uv);                
                
                float flowLerp = abs((phase0 - 0.5f)/0.5f);


                
                col0.rgb *= (col0.r == 1.0f) ?
                    _OutsideColor.rgb * pow(2,_OutsideColorStrength):
                    (_InsideColor.rgb * lerp(noise0,noise1,flowLerp)* pow(2,_InsideColorStrength)  );//
                col0.a = col0.a * pow(flag.r,1/_lineWidth);
                
               
                 //col0 = lerp(noise0,noise1,flowLerp) * col0 * pow(flag.r,1/_lineWidth) ;
                
                //col = outer.r > col.r ? outer * _OuterColor : col * _lineColor;
                //fixed4 noise = tex2D(_NoiseTex,i.uv);
                /*// 采样周围4个点
                float2 up_uv = i.uv + float2(0,1) * _OutLineWidth * _MainTex_TexelSize.xy;
                float2 down_uv = i.uv + float2(0,-1) * _OutLineWidth * _MainTex_TexelSize.xy;
                float2 left_uv = i.uv + float2(-1,0) * _OutLineWidth * _MainTex_TexelSize.xy;
                float2 right_uv = i.uv + float2(1,0) * _OutLineWidth * _MainTex_TexelSize.xy;
                // 如果有一个点透明度为0 说明是边缘
                float w = tex2D(_MainTex,up_uv).a * tex2D(_MainTex,down_uv).a * tex2D(_MainTex,left_uv).a * tex2D(_MainTex,right_uv).a;
                */
                //col =  _lineColor * (1 - outer.r)  + _OuterColor * outer.r ;

                //col.rgb = lerp(_OutLineColor,col.rgb,w);

                // 弃用 col.rgb = smoothstep(_lineColor,col.rgb,flag.x);


                //col.a = col.a *  ;
                
                return col0;
            }
            ENDCG
        }
    }
}