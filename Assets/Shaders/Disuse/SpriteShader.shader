
Shader "Custom/SpriteShader"
{
    Properties
    {
        //ͼƬ����
        _MainTex ("��ͼ", 2D) = "black" {}
        _Color ("��ɫ", Color) = (0.3, 0.59, 0.11,1)
        //��Ӱ����
        _ShadowColor ("��Ӱ��ɫ", Color) = (0.3, 0.59, 0.11,1)
        _ShadowOffset ("��Ӱƫ��", Vector) = (0.3, 0.59,0,0)
        //ģ��
        _BlurSize("ģ���ߴ�", vector) = (256,256,0,0)
        //ģ��ƫ��
        _BlurOffset("ģ��ƫ��", Range(1, 10)) = 1
    }
    
    SubShader
    {
        LOD 200
        Tags
        {
            //͸������
            "Queue" = "Transparent"
            //�����κ�ͶӰ����ͼӰ�졣һ��Ӧ����sprite��GUI
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
         //��Ӱ
        Pass
        {
            //2d�ر�
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            // Offset -1, -1
            //����͸���Ȼ��
            Blend SrcAlpha OneMinusSrcAlpha
            //ColorMaterial AmbientAndDiffuse | Emission ��ɫ���� ������������� �������
            // ColorMaterial AmbientAndDiffuse

            CGPROGRAM
                //������ƬԪ��ɫ��
                #pragma vertex vert
                #pragma fragment frag        
                //unity��    
                #include "UnityCG.cginc"
                //��Ӱ���
                sampler2D _MainTex;
                fixed4 _ShadowColor;
                fixed4 _ShadowOffset;
                fixed4 _Color;
                //ģ��
                float4 _BlurSize;
                fixed _BlurOffset;

                struct a2v
                {
                    //����λ��
                    float4 vertex : POSITION;
                    //��ͼ
                    float2 texcoord : TEXCOORD0;
                    //��ɫ
                    fixed4 color : COLOR;
                };
        
                struct v2f
                {
                    //����λ��
                    float4 vertex : SV_POSITION;
                    //��ͼ
                    half2 texcoord : TEXCOORD0;
                    //��ɫ
                    fixed4 color : COLOR;
                };
                //��߯ͼ�˲�
                float4 filter(float3x3 filter, sampler2D tex, float2 coord, float2 texSize)
                {
                    float4 outCol = float4(0,0,0,0);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            //��������㣬�õ���ǰ���ظ��������ص�����
                            float2 newCoord = float2(coord.x + (i-1)*_BlurOffset, coord.y + (j-1)*_BlurOffset);
                            float2 newUV = float2(newCoord.x / texSize.x, newCoord.y / texSize.y);
                            //�����������˲���Ȩ�أ�Ȼ���ۼ�
                            outCol += tex2D(tex, newUV) * filter[i][j];
                        }
                    }
                    return outCol;
                }
                //������ɫ��
                v2f vert (a2v v)
                {
                    v2f o;
                    //�������ģ�Ϳռ�ת��Ϊ�ü��ռ䣬��Ϊ2d����ƽ��ӳ�䡣
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    //ƫ��
                    o.vertex.x += _ShadowOffset.x;
                    o.vertex.y += _ShadowOffset.y;
                    o.vertex.z += _ShadowOffset.z;
                    o.texcoord = v.texcoord;
                    o.color = v.color;
                    return o;
                }
                // ƬԪ��ɫ��
                fixed4 frag (v2f v) : COLOR
                {
                    
                    //ģ��
                    //�����˲���
                    float3x3 boxFilter = 
                    {
                        1.0f/9, 1.0f/9, 1.0f/9, 
                        1.0f/9, 1.0f/9, 1.0f/9, 
                        1.0f/9, 1.0f/9, 1.0f/9, 
                    };
                    
                    float2 coord = float2(v.texcoord.x * _BlurSize.x, v.texcoord.y * _BlurSize.y);
                    fixed4 f = filter(boxFilter, _MainTex, coord, _BlurSize);

                    


                    //������ɫ
                    // fixed4 color = tex2D(_MainTex, v.texcoord) ;
                    fixed4 color = f;
                    //������ɫ ��� �Ҷ�����
                    color.rgb = _ShadowColor.rgb;
                    //͸����
                    color.a = color.a * _ShadowColor.a;

                    return color;
                }
            ENDCG
        }
        //������ɫ
        Pass
        {
            //2d�ر�
            Cull Off
            Lighting Off
            ZWrite Off
            Fog { Mode Off }
            // Offset -1, -1
            //����͸���Ȼ��
            Blend SrcAlpha OneMinusSrcAlpha
            //ColorMaterial AmbientAndDiffuse | Emission ��ɫ���� ������������� �������
            // ColorMaterial AmbientAndDiffuse

            CGPROGRAM
                //������ƬԪ��ɫ��
                #pragma vertex vert
                #pragma fragment frag        
                //unity��    
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                fixed4 _Color;

                struct a2v
                {
                    //����λ��
                    float4 vertex : POSITION;
                    //��ͼ
                    float2 texcoord : TEXCOORD0;
                    //��ɫ
                    fixed4 color : COLOR;
                };
        
                struct v2f
                {
                    //����λ��
                    float4 vertex : SV_POSITION;
                    //��ͼ
                    half2 texcoord : TEXCOORD0;
                    //��ɫ
                    fixed4 color : COLOR;
                };
                //������ɫ��
                v2f vert (a2v v)
                {
                    v2f o;
                    //�������ģ�Ϳռ�ת��Ϊ�ü��ռ䣬��Ϊ2d����ƽ��ӳ�䡣
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.texcoord = v.texcoord;
                    o.color = v.color;
                    return o;
                }
                // ƬԪ��ɫ��
                fixed4 frag (v2f v) : COLOR
                {
                    //������ɫ
                    fixed4 color = tex2D(_MainTex, v.texcoord) ;
                    //������ɫ ��� �Ҷ�����
                    color.rgb = color.rgb + _Color.rgb;
                    //͸����
                    color.a =  color.a * _Color.a;
                    return color;
                }
            ENDCG
        }
       
    }
}
