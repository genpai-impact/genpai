Shader "Custom/ShaderForTest2"
{
	Properties
	{
		//2D���
		_MainTex("Main Texture", 2D) = "white"{}		//������
		_EdgeAlphaThreshold("Edge Alpha Threshold", Float) = 1.0		//�߽�͸���ȵ���ֵ
		_EdgeColor("Edge Color", Color) = (0,0,0,1)				//�߽����ɫ
		_EdgeDampRate("Edge Damp Rate", Float) = 2				//����ķ�ĸ
		_OriginAlphaThreshold("OriginAlphaThreshold", range(0.1, 1)) = 0.2	//ԭ�����޳�����ֵ
		[Toggle(_ShowOutline)] _ShowOutline ("Show Outline", Int) = 0		//������������Toggle

		//2D�ڷ���
		_InnerGlowWidth("Inner Glow Width", Float) = 0.1			//�ڷ���Ŀ��
		_InnerGlowColor("Inner Glow Color", Color) = (0,0,0,1)		//�ڷ������ɫ
		_InnerGlowAccuracy("Inner Glow Accuracy", Int) = 2			//�ڷ���ľ���
		_InnerGlowAlphaSumThreshold("Inner Glow Alpha Sum Threshold", Float) = 0.5		//�ڷ����͸���Ⱥ͵���ֵ
		_InnerGlowLerpRate("Inner Glow Lerp Rate", range(0, 1)) = 0.8			//�ڷ�����ɫ��ԭ��ɫ�Ĳ�ֵ

		[Toggle(_ShowInnerGlow)] _ShowInnerGlow ("Show Inner Glow", Int) = 0		//������������Toggle
	}

	SubShader
	{
		Tags{ "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Ztest Always Cull Off ZWrite Off
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature _ShowOutline
			#pragma shader_feature _ShowInnerGlow
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			half4 _MainTex_TexelSize;
			fixed _EdgeAlphaThreshold;
			fixed4 _EdgeColor;
			float _EdgeDampRate;
			float _OriginAlphaThreshold;

			float _InnerGlowWidth;
			fixed4 _InnerGlowColor;
			int _InnerGlowAccuracy;
			float _InnerGlowAlphaSumThreshold;
			float _InnerGlowLerpRate;

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv[9] : TEXCOORD0;
			};

			half CalculateAlphaSumAround(v2f i)
			{
				half texAlpha;
				half alphaSum = 0;
				for(int it = 0; it < 9; it ++)
				{
					texAlpha = tex2D(_MainTex, i.uv[it]).w;
					alphaSum += texAlpha;
				}

				return alphaSum;
			}

			float CalculateCircleSumAlpha(float2 orign, float radiu, int time)
			{

				float sum = 0;
				float perAngle = 360 / time;
				for(int i = 0; i < time; i ++)
				{
					float2 newUV = orign + radiu * float2(cos(perAngle * i), sin(perAngle * i));
					sum += tex2D(_MainTex, newUV).a;
				}

				return sum;
			}

			v2f vert(appdata_img v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				half2 uv = v.texcoord;

				o.uv[0] = uv + _MainTex_TexelSize.xy * half2(-1, -1);
				o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0, -1);
				o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1, -1);
				o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1, 0);
				o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0, 0);
				o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1, 0);
				o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1, 1);
				o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0, 1);
				o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1, 1);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 innerGlow = fixed4(0,0,0,0);
				fixed4 outline = fixed4(0,0,0,0);
				fixed4 orignColor = tex2D(_MainTex, i.uv[4]);
				//2DͼƬ������
				#if defined(_ShowOutline)
					half alphaSum = CalculateAlphaSumAround(i);
					float isNeedShow = alphaSum > _EdgeAlphaThreshold;
					float damp = saturate((alphaSum - _EdgeAlphaThreshold) * _EdgeDampRate);
					float isOrigon = orignColor.a > _OriginAlphaThreshold;
					fixed3 finalColor = lerp(_EdgeColor.rgb, fixed3(0,0,0), isOrigon);

					float finalAlpha = isNeedShow * damp * (1 - isOrigon);
					outline = fixed4(finalColor.rgb, finalAlpha);
				#endif
				//2DͼƬ���ڷ���
				#if defined(_ShowInnerGlow)
					//����͸���ȵĺ�
					float alphaCircleSum = CalculateCircleSumAlpha(i.uv[4], _InnerGlowWidth, _InnerGlowAccuracy) / _InnerGlowAccuracy;
					float innerColorAlpha = 0;
					//�����ȡ���ڷ���ĵ�͸���ȣ��������˽��䣬�ÿ����߽����ɫ����һЩ��ԭ���͸���ȵĻ�Խ��Խ��
					innerColorAlpha = 1 - saturate(alphaCircleSum - _InnerGlowAlphaSumThreshold) / (1 - _InnerGlowAlphaSumThreshold);

					//�޳�����ԭͼ�����ص���ɫ��
					if(orignColor.a <= _OriginAlphaThreshold)
					{
						innerColorAlpha = 0;
					}

					fixed3 innerColor = _InnerGlowColor.rgb * innerColorAlpha;
					innerGlow = fixed4(innerColor.rgb, innerColorAlpha);
					//return innerGlow;
				#endif

				//�����������ڷ���Ԫ��ɫ���������
				#if defined(_ShowOutline)
					float outlineAlphaDiscard = orignColor.a > _OriginAlphaThreshold;
					orignColor = outlineAlphaDiscard * orignColor;
					//��2��Ϊ�˸���ͻ���ⷢ��
					return lerp(orignColor ,innerGlow * 2, _InnerGlowLerpRate * innerGlow.a) + outline;
				#endif

				return lerp(orignColor ,innerGlow * 2, _InnerGlowLerpRate * innerGlow.a);
				//return tex2D(_MainTex, i.uv[4]) + innerGlow + outline;
			}

			ENDCG
		}
	}
}