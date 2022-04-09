Shader "Prozac/Attack"
{
    Properties
    {
        _MainTex ("MainTexture", 2D) = "white" {}
       
		_NoiseTex("NoiseTexture",2D) = "white" {}
    	
    	//RampTex("RampTex",2D) = "white" {}
		
		_Alpha("Alpha",float) = 1
    	
    	_FlashStrength("FlashStrength",range(-5,5)) = 0.05
    	
    	//_NoiseAttack("NoiseAttack",float4) = (0,0,20,20)
    	_RampHeight("RampHeight",range(0,1)) = 0.5
    	
    	//_height2("height2",range(0,1)) = 0.6
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"


            sampler2D _MainTex;
            float4 _MainTex_ST;
            
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;

            sampler2D _RampTer;
            float _FlashStrength;
            float _Alpha;

            float _RampHeight;

            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;

				//float2 uv1 : TEXCOORD1;

                float4 vertex : SV_POSITION;
            };

            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				
				//o.uv1 =   TRANSFORM_TEX(v.uv,_NoiseTex);

                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv = v.uv* _MainTex_ST.xy + _MainTex_ST.zw;
            	
            	UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
			float2 remap2(float2 map,float min_old,float max_old,float min_new,float max_new)
			{
				return (map-min_old)/(max_old-min_old) * (max_new-min_new)+min_old;
			}
			float4 remap4(float4 map,float min_old,float max_old,float min_new,float max_new)
			{
				return (map-min_old)/(max_old-min_old) * (max_new-min_new)+min_old;
			}
            fixed4 frag (v2f i) : SV_Target
            {


				float2 noise_uv = i.uv *_NoiseTex_ST.xy + _NoiseTex_ST.zw *_Time;
				//noise_uv = remap2(noise_uv,0.0,1.0,-0.5,0.5);
            	float4 noise = tex2D(_NoiseTex,noise_uv);
            	noise = remap4(noise,0,1,-0.5,0.5);
            	
            	//noise = noise -0.5f;
				//col.rgb = tex2D(_MainTex,noise_uv).rgb;

            	float2 col_uv = float2(i.uv.x,i.uv.y + _FlashStrength * noise.x);

            	float4 col;
            	
            	col = tex2D(_MainTex,col_uv);

				//col = float4(noise.rgb , col.a);
            	noise = saturate(noise);
				//float IsTran = smoothstep(i.uv.y,i.uv.y+0.01,_RampHeight);//-smoothstep(i.uv.y,i.uv.y+0.05,_RampHeight) > 0 ? 1:0;
            	
            	col.rgb = col.rgb * smoothstep(i.uv.y,i.uv.y+0.1,_RampHeight);// + float3(1,1,1) * IsTran;
            	//col = col * noise ;
            	//float st;
            	//smoothstep(i.uv.y,_height,st);
            	col.a *= _Alpha; //* smoothstep(i.uv.y,i.uv.y+0.1,_RampHeight) ;
				return col;

            }
            ENDCG
        }
    }
}
