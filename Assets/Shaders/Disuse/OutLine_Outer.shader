Shader "Custom/ShaderForTest2"
{
	Properties
	{
		//2D描边
		_MainTex("Main Texture", 2D) = "white"{}		//主纹理
		_EdgeAlphaThreshold("Edge Alpha Threshold", Float) = 1.0		//边界透明度的阈值
		[HDR]_EdgeColor("Edge Color", Color) = (0,0,0,1)				//边界的颜色
		_EdgeDampRate("Edge Damp Rate", Float) = 2				//渐变的分母
		_OriginAlphaThreshold("OriginAlphaThreshold", range(0.1, 1)) = 0.2	//原像素剔除的阈值
		[Toggle(_ShowOutline)] _ShowOutline ("Show Outline", Int) = 0		//开启外轮廓的Toggle

		//2D内发光
		_InnerGlowWidth("Inner Glow Width", Float) = 0.1			//内发光的宽度
		_InnerGlowColor("Inner Glow Color", Color) = (0,0,0,1)		//内发光的颜色
		_InnerGlowAccuracy("Inner Glow Accuracy", Int) = 2			//内发光的精度
		_InnerGlowAlphaSumThreshold("Inner Glow Alpha Sum Threshold", Float) = 0.5		//内发光的透明度和的阈值
		_InnerGlowLerpRate("Inner Glow Lerp Rate", range(0, 1)) = 0.8			//内发光颜色和原颜色的差值

		[Toggle(_ShowInnerGlow)] _ShowInnerGlow ("Show Inner Glow", Int) = 0		//开启外轮廓的Toggle
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
				//2D图片外轮廓
				#if defined(_ShowOutline)
					half alphaSum = CalculateAlphaSumAround(i);
					float isNeedShow = alphaSum > _EdgeAlphaThreshold;
					float damp = saturate((alphaSum - _EdgeAlphaThreshold) * _EdgeDampRate);
					float isOrigon = orignColor.a > _OriginAlphaThreshold;
					fixed3 finalColor = lerp(_EdgeColor.rgb, fixed3(0,0,0), isOrigon);

					float finalAlpha = isNeedShow * damp * (1 - isOrigon);
					outline = fixed4(finalColor.rgb, finalAlpha);
				#endif
				//2D图片的内发光
				#if defined(_ShowInnerGlow)
					//计算透明度的和
					float alphaCircleSum = CalculateCircleSumAlpha(i.uv[4], _InnerGlowWidth, _InnerGlowAccuracy) / _InnerGlowAccuracy;
					float innerColorAlpha = 0;
					//这里获取到内发光的的透明度，并且做了渐变，让靠近边界的颜色更亮一些，原理的透明度的会越来越低
					innerColorAlpha = 1 - saturate(alphaCircleSum - _InnerGlowAlphaSumThreshold) / (1 - _InnerGlowAlphaSumThreshold);

					//剔除超出原图的像素的颜色。
					if(orignColor.a <= _OriginAlphaThreshold)
					{
						innerColorAlpha = 0;
					}

					fixed3 innerColor = _InnerGlowColor.rgb * innerColorAlpha;
					innerGlow = fixed4(innerColor.rgb, innerColorAlpha);
					//return innerGlow;
				#endif

				//将外轮廓和内发光元颜色叠加输出。
				#if defined(_ShowOutline)
					float outlineAlphaDiscard = orignColor.a > _OriginAlphaThreshold;
					orignColor = outlineAlphaDiscard * orignColor;
					//乘2是为了更加突出外发光
					return lerp(orignColor ,innerGlow * 2, _InnerGlowLerpRate * innerGlow.a) + outline;
				#endif

				return lerp(orignColor ,innerGlow * 2, _InnerGlowLerpRate * innerGlow.a);
				//return tex2D(_MainTex, i.uv[4]) + innerGlow + outline;
			}

			ENDCG
		}
	}
}