
Shader "Custom/SpriteShader"
{
    Properties
    {
        //图片设置
        _MainTex ("贴图", 2D) = "black" {}
        _Color ("颜色", Color) = (0.3, 0.59, 0.11,1)
        //阴影设置
        _ShadowColor ("阴影颜色", Color) = (0.3, 0.59, 0.11,1)
        _ShadowOffset ("阴影偏移", Vector) = (0.3, 0.59,0,0)
        //模糊
        _BlurSize("模糊尺寸", vector) = (256,256,0,0)
        //模糊偏移
        _BlurOffset("模糊偏移", Range(1, 10)) = 1
    }
    
    SubShader
    {
        LOD 200
        Tags
        {
            //透明队列
            "Queue" = "Transparent"
            //不被任何投影或贴图影响。一般应用在sprite和GUI
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
         //阴影
        Pass
        {
            //2d关闭
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            // Offset -1, -1
            //正常透明度混合
            Blend SrcAlpha OneMinusSrcAlpha
            //ColorMaterial AmbientAndDiffuse | Emission 颜色材质 环境漫反射光照 及放射光
            // ColorMaterial AmbientAndDiffuse

            CGPROGRAM
                //顶点与片元着色器
                #pragma vertex vert
                #pragma fragment frag        
                //unity宏    
                #include "UnityCG.cginc"
                //阴影相关
                sampler2D _MainTex;
                fixed4 _ShadowColor;
                fixed4 _ShadowOffset;
                fixed4 _Color;
                //模糊
                float4 _BlurSize;
                fixed _BlurOffset;

                struct a2v
                {
                    //顶点位置
                    float4 vertex : POSITION;
                    //贴图
                    float2 texcoord : TEXCOORD0;
                    //颜色
                    fixed4 color : COLOR;
                };
        
                struct v2f
                {
                    //像素位置
                    float4 vertex : SV_POSITION;
                    //贴图
                    half2 texcoord : TEXCOORD0;
                    //颜色
                    fixed4 color : COLOR;
                };
                //对忒图滤波
                float4 filter(float3x3 filter, sampler2D tex, float2 coord, float2 texSize)
                {
                    float4 outCol = float4(0,0,0,0);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            //计算采样点，得到当前像素附近的像素的坐标
                            float2 newCoord = float2(coord.x + (i-1)*_BlurOffset, coord.y + (j-1)*_BlurOffset);
                            float2 newUV = float2(newCoord.x / texSize.x, newCoord.y / texSize.y);
                            //采样并乘以滤波器权重，然后累加
                            outCol += tex2D(tex, newUV) * filter[i][j];
                        }
                    }
                    return outCol;
                }
                //顶点着色器
                v2f vert (a2v v)
                {
                    v2f o;
                    //将顶点从模型空间转换为裁剪空间，因为2d就是平面映射。
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    //偏移
                    o.vertex.x += _ShadowOffset.x;
                    o.vertex.y += _ShadowOffset.y;
                    o.vertex.z += _ShadowOffset.z;
                    o.texcoord = v.texcoord;
                    o.color = v.color;
                    return o;
                }
                // 片元着色器
                fixed4 frag (v2f v) : COLOR
                {
                    
                    //模糊
                    //定义滤波核
                    float3x3 boxFilter = 
                    {
                        1.0f/9, 1.0f/9, 1.0f/9, 
                        1.0f/9, 1.0f/9, 1.0f/9, 
                        1.0f/9, 1.0f/9, 1.0f/9, 
                    };
                    
                    float2 coord = float2(v.texcoord.x * _BlurSize.x, v.texcoord.y * _BlurSize.y);
                    fixed4 f = filter(boxFilter, _MainTex, coord, _BlurSize);

                    


                    //像素颜色
                    // fixed4 color = tex2D(_MainTex, v.texcoord) ;
                    fixed4 color = f;
                    //像素颜色 点乘 灰度因子
                    color.rgb = _ShadowColor.rgb;
                    //透明度
                    color.a = color.a * _ShadowColor.a;

                    return color;
                }
            ENDCG
        }
        //正常着色
        Pass
        {
            //2d关闭
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            // Offset -1, -1
            //正常透明度混合
            Blend SrcAlpha OneMinusSrcAlpha
            //ColorMaterial AmbientAndDiffuse | Emission 颜色材质 环境漫反射光照 及放射光
            // ColorMaterial AmbientAndDiffuse

            CGPROGRAM
                //顶点与片元着色器
                #pragma vertex vert
                #pragma fragment frag        
                //unity宏    
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                fixed4 _Color;

                struct a2v
                {
                    //顶点位置
                    float4 vertex : POSITION;
                    //贴图
                    float2 texcoord : TEXCOORD0;
                    //颜色
                    fixed4 color : COLOR;
                };
        
                struct v2f
                {
                    //像素位置
                    float4 vertex : SV_POSITION;
                    //贴图
                    half2 texcoord : TEXCOORD0;
                    //颜色
                    fixed4 color : COLOR;
                };
                //顶点着色器
                v2f vert (a2v v)
                {
                    v2f o;
                    //将顶点从模型空间转换为裁剪空间，因为2d就是平面映射。
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = v.texcoord;
                    o.color = v.color;
                    return o;
                }
                // 片元着色器
                fixed4 frag (v2f v) : COLOR
                {
                    //像素颜色
                    fixed4 color = tex2D(_MainTex, v.texcoord) ;
                    //像素颜色 点乘 灰度因子
                    color.rgb = color.rgb + _Color.rgb;
                    //透明度
                    color.a =  color.a * _Color.a;
                    return color;
                }
            ENDCG
        }
       
    }
}
