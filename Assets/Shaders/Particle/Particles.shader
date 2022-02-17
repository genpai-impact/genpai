Shader "Prozac/Particles" 
{
	Properties 
	{
		_NoiseTex ("Noise Texture (RG)", 2D) = "white" {}		
		_AlphaTex ("Alpha (A)", 2D) = "white" {}
		_HeatTime  ("Heat Time", range (0,1.5)) = 1
		_HeatForce  ("Heat Force", range (0,3)) = 0.1
	}

    SubShader 
	{
		Tags { "Queue"="Transparent-1" "IgnoreProjector"="True" "RenderType"="Transparent+10" "PreviewType"="Plane" }

		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off Lighting Off ZWrite Off
	
		LOD 200

		GrabPass{ "_DistortTexture" }

        Pass 
		{        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_particles           
            #include "UnityCG.cginc"
            
			sampler2D _AlphaTex;
			float4 _AlphaTex_ST;

			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;

			sampler2D _DistortTexture;		
			float _HeatForce;
			float _HeatTime;

            struct appdata
			{
                float4 vertex		: POSITION;
                fixed4 color		: COLOR;
                float4 texcoord		: TEXCOORD0;		//xy放uv,zw放lifetime和自定义数据
            };

            struct v2f 
			{
                float4 vertex		: SV_POSITION;
                fixed4 color		: COLOR;   
				float4 texcoords	: TEXCOORD0;		//xy采样AlphaTex,zw采样NoiseTex
				float4 grabPos		: TEXCOORD1;
				float2 customData	: TEXCOORD2;		//x放lifetime,y放自定义数据
            };
                        

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
				o.texcoords.xy = TRANSFORM_TEX(v.texcoord.xy, _AlphaTex);
				o.texcoords.zw = TRANSFORM_TEX(v.texcoord.xy, _NoiseTex);
				o.grabPos = ComputeNonStereoScreenPos(o.vertex);
				o.customData = v.texcoord.zw;

                return o;
            }

            
            
            fixed4 frag (v2f i) : SV_Target
            {               
                fixed heatForce = i.customData.y * _HeatForce;

				float alpha = tex2D(_AlphaTex, i.texcoords.xy).a;

				half offset1 = tex2D(_NoiseTex, i.texcoords.zw + i.customData.xx * _HeatTime);
				half offset2 = tex2D(_NoiseTex, i.texcoords.zw - i.customData.xx * _HeatTime);

				i.grabPos.x += (offset1 + offset2 - 1) * heatForce * alpha;
				i.grabPos.y += (offset1 + offset2 - 1) * heatForce * alpha;

				half4 grabCol = tex2D(_DistortTexture, i.grabPos.xy/i.grabPos.w);

                return fixed4(grabCol.rgb, alpha);
            }
            ENDCG 
		}
	}   
}
