Shader "Prozac/CharaDissolved"
{
    Properties
    {
        _MainTex ("MainTexture", 2D) = "white" {}
        
        [Header(Dissolved)]
        _Dissolved("_Dissolved",Range(0,1)) = 0.5
        _DissolvedThreshold("_DissolvedTreshold",Range(0,0.1)) = 0.05
        
        _NoiseTex("NoiseTex",2D) = "white" {}
        
        //_RampTex("RampTex",2D) = "white" {}
        [HDR]_Color1("Color1",Color) = (1,1,1,1)
        [HDR]_Color2("Color2",Color) = (1,1,0,1)
        [HDR]_Color3("Color3",Color) = (0,0,0,1)
        _Degree("Degree",Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags 
        { 
            //"RenderType"="Transparent" 
            "Queue"="Transparent"
        }
        //LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"


            ///定义Properties中的相关参数
            ///
            ///
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _Dissolved;
            float _DissolvedThreshold;

            sampler2D _NoiseTex;
            //sampler2D _RampTex;

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float _Degree;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };


            float3 ColorRamp(float ramp)
            {
                 float3 color;
                    color.rgb = _Color1 * (1 - ramp)
                                + _Color2 * (1 - 2 * abs(ramp - 0.5))
                                + _Color3 * ramp;

                return color;

            }
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 noise = tex2D(_NoiseTex,i.uv);

                float4 col = tex2D(_MainTex,i.uv);



               
                float3 col1 = step(noise.r,_Dissolved - 0.1f) ;

                
                float3 col2 = step(noise.r,_Dissolved ) ;

                

                col.rgb = (col2 - col1) * ColorRamp( saturate((_Dissolved-noise.r) / 0.1f))
                        +  col1.r * col.rgb;
                col.a = col.a * col2.r;

                return col;
            }
            ENDCG
        }
    }
}
