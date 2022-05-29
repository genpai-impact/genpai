
Shader "Prozac/Blur"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		
		_NoiseTex("NoiseTex",2D) = "white"{}
		_DisTex("DisTex",2D) = "white"{}

        _blurSizeY("BlurSizeY", Range(0,10)) = 1
		_Alpha("Alpha",range(-1,2)) =1
		
		_Saturate("Saturate",range(0,2)) = 1 
		
		
		//_RampThreshold("RampTreshould",float) =1
		
	}
 
	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"RenderType"="Transparent" 
		}

 
		Cull Off
		Lighting Off
		ZWrite Off

		Fog { Mode Off }
		Blend One OneMinusSrcAlpha

        ZTest Always
 
		Pass
		{
			CGPROGRAM
 
			#pragma vertex vert
			#pragma fragment frag
			//#pragma target 2.0
 
			#include "UnityCG.cginc"
 
			struct a2v {
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD2;

			};
 
			struct v2f {
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 uv  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				

			};
		
 			sampler2D _MainTex;
			sampler2D _NoiseTex;

			sampler2D _DisTex;
			
			float4 _MainTex_ST;
			float4 _NoiseTex_ST;
            float _blurSizeY;

			float _Alpha;
			
			float _Saturate;
		
			v2f vert (a2v i) {
				v2f o;
				
				o.uv = i.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 noise_uv = i.uv2 * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
				float4 noiseY = tex2Dlod(_NoiseTex,float4(noise_uv,0,0));
				
				//根据Noise贴图进行Y轴偏移,只向上偏移
				//o.worldPosition = i.vertex + float4(0,(noiseY.r - 0.5) * 2.0 * _blurSizeY,0,0);

				o.worldPosition = i.vertex + float4(0,noiseY.r * _blurSizeY,0,0);
				
				o.vertex = UnityObjectToClipPos(o.worldPosition);
				
				o.color = i.color ; 
				return o;
			}
		
 
			fixed4 frag (v2f i) : SV_Target
			{

				float4 color = (tex2D(_MainTex, i.uv) ) * i.color;

				
				//Gray = R*0.299 + G*0.587 + B*0.114
				float gray = color.r *0.299 + color.g * 0.587 + color.b * 0.114;

				float4 noise = tex2D(_DisTex,i.uv);
				color.rgb = (( 1 - _Saturate ) * float3(gray,gray,gray) +color.rgb * _Saturate )* step(noise.r,_Alpha);// * noise.rgb;
            	//color.a =0;//color.a * step(noise.r,_Alpha); 

				return color;
			}
			

		ENDCG
		}
	}
}