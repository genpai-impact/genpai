//不外扩 向内边缘发光 高动态范围
//Prozac
Shader "Prozac/HighLight"
{
    Properties
    {
        _MainTex("main tex",2D) = "black"{}
        [HDR]_RimColor("Rim color",Color) = (1,1,1,1)//边缘颜色
        _RimPower ("Rim power",range(1,10)) = 2//边缘强度
    }
 
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include"UnityCG.cginc"
 
            struct v2f
            {
                float4 vertex:POSITION;
                float4 uv:TEXCOORD0;
                float4 NdotV:COLOR;//normal*view
            };
 
            sampler2D _MainTex;
            float4 _RimColor;
            float _RimPower;
 
            v2f vert(appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                float3  output = mul(unity_WorldToObject,WorldSpaceViewDir(v.vertex));
                o.NdotV.x = saturate(dot(v.normal,normalize(output)));//必须在同一坐标系才能正确做点乘运算
                return o;
            }
 
            half4 frag(v2f IN):COLOR
            {
                half4 c = tex2D(_MainTex,IN.uv);
                //用视方向和法线方向做点乘，越边缘的地方，法线和视方向越接近90度，点乘越接近0.
                //用（1- 上面点乘的结果）*颜色，来反映边缘颜色情况
                c.rgb += pow((1-IN.NdotV.x) ,_RimPower)* _RimColor.rgb;
                return c;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}