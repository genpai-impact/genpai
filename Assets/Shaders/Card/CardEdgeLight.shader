Shader "Prozac/RimLight"
{
	//属性
	Properties{
		[HDR]_Diffuse("Diffuse", Color) = (1,1,1,1)
		[HDR]_RimColor("RimColor", Color) = (1,1,1,1)
		_RimPower("RimPower", Range(0.000001, 3.0)) = 0.1
		_MainTex("Base 2D", 2D) = "white"{}
	}
 
	//子着色器	
	SubShader
	{
		Pass
		{
			//定义Tags
			Tags{ "RenderType" = "Opaque" }
 
			CGPROGRAM
			//引入头文件
			#include "Lighting.cginc"
			//定义Properties中的变量
			fixed4 _Diffuse;
			sampler2D _MainTex;
			//使用了TRANSFROM_TEX宏就需要定义XXX_ST
			float4 _MainTex_ST;
			fixed4 _RimColor;
			float _RimPower;
 
			//定义结构体：vertex shader阶段输出的内容
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float2 uv : TEXCOORD1;
				//在vertex shader中计算观察方向传递给fragment shader
				float3 worldViewDir : TEXCOORD2;
			};
 
			//定义顶点shader,参数直接使用appdata_base（包含position, noramal, texcoord）
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//通过TRANSFORM_TEX宏转化纹理坐标，主要处理了Offset和Tiling的改变,默认时等同于o.uv = v.texcoord.xy;
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				//顶点转化到世界空间
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				//可以把计算计算ViewDir的操作放在vertex shader阶段，毕竟逐顶点计算比较省
				o.worldViewDir = _WorldSpaceCameraPos.xyz - worldPos;
				return o;
			}
 
			//定义片元shader
			fixed4 frag(v2f i) : SV_Target
			{
				//unity自身的diffuse也是带了环境光，这里我们也增加一下环境光
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * _Diffuse.xyz;
				//归一化法线，即使在vert归一化也不行，从vert到frag阶段有差值处理，传入的法线方向并不是vertex shader直接传出的
				fixed3 worldNormal = normalize(i.worldNormal);
				//把光照方向归一化
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				//根据半兰伯特模型计算像素的光照信息
				fixed3 lambert = 0.5 * dot(worldNormal, worldLightDir) + 0.5;
				//最终输出颜色为lambert光强*材质diffuse颜色*光颜色
				fixed3 diffuse = lambert * _Diffuse.xyz * _LightColor0.xyz + ambient;
				//进行纹理采样
				fixed4 color = tex2D(_MainTex, i.uv);
				
				//以下为本篇主题：计算RimLight
				//把视线方向归一化
				float3 worldViewDir = normalize(i.worldViewDir);
				//计算视线方向与法线方向的夹角，夹角越大，dot值越接近0，说明视线方向越偏离该点，也就是平视，该点越接近边缘
				float rim = 1 - max(0, dot(worldViewDir, worldNormal));
				//计算rimLight
				fixed3 rimColor = _RimColor * pow(rim, 1 / _RimPower);
				//输出颜色+边缘光颜色
				color.rgb = color.rgb* diffuse + rimColor;
				
				return fixed4(color);
			}
 
			//使用vert函数和frag函数
			#pragma vertex vert
			#pragma fragment frag	
 
			ENDCG
		}
	}
	//前面的Shader失效的话，使用默认的Diffuse
	FallBack "Diffuse"
}