
Shader "ProzacShader/ElectroCharge"
{
	Properties
	{
		_MainTex("MainTex",2D) = "white"{} 

	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
    

			float4 vec4(float x,float y,float z,float w){return float4(x,y,z,w);}
			float4 vec4(float x){return float4(x,x,x,x);}
			float4 vec4(float2 x,float2 y){return float4(float2(x.x,x.y),float2(y.x,y.y));}
			float4 vec4(float3 x,float y){return float4(float3(x.x,x.y,x.z),y);}


			float3 vec3(float x,float y,float z){return float3(x,y,z);}
			float3 vec3(float x){return float3(x,x,x);}
			float3 vec3(float2 x,float y){return float3(float2(x.x,x.y),y);}

			float2 vec2(float x,float y){return float2(x,y);}
			float2 vec2(float x){return float2(x,x);}

			float vec(float x){return float(x);}
    
    

			struct VertexInput
			{
				float4 vertex : POSITION;
				float2 uv:TEXCOORD0;
				float4 tangent : TANGENT;
				float3 normal : NORMAL;
				//VertexInput
			};
			struct VertexOutput
			{
				float4 pos : SV_POSITION;
				float2 uv:TEXCOORD0;
				//VertexOutput
			};
			sampler2D _MainTex; 

	
			VertexOutput vert (VertexInput v)
			{
				VertexOutput o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.uv;
				//VertexFactory
				return o;
			}
    
			// Noise animation - Electric
		// by nimitz (stormoid.com) (twitter: @stormoid)
		// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License
		// Contact the author for other licensing options

		//The domain is displaced by two fbm calls one for each axis.
		//Turbulent fbm (aka ridged) is used for better effect.

			#define time _Time.y*0.15
			#define tau 6.2831853

			float2x2 makem2(in float theta)
			{
				float c = cos(theta);
				float s = sin(theta);
				/*
				return float4x4( c,-s, 0, 0,
								 s, c, 0, 0,
								 0, 0, 1, 0,
								 0, 0, 0, 1	);
				*/
				return float2x2( c,-s, 
								 s, c);
			}
			float noise( in float2 x)
			{
				return tex2D(_MainTex, x*.01).x;
			}

			float fbm(in float2 p)
			{	
				float z=2.;
				float rz = 0.;
				float2 bp = p;
				for (float i= 1.;i < 6.;i++)
				{
					rz+= abs((noise(p)-0.5)*2.)/z;
					z = z*2.;
					p = p*2.;
				}
				return rz;
			}

			float dualfbm(in float2 p)
			{
				//get two rotated fbm calls and displace the domain
				float2 p2 = p*.7;
				float2 basis = vec2(fbm(p2-time*1.6),fbm(p2+time*1.7));
				basis = (basis-.5)*.2;
				p += basis;
	
				//coloring
				return fbm(p*makem2(time*0.2));
			}

			float circ(float2 p) 
			{
				float r = length(p);
				r = log(sqrt(r));
				return abs(fmod(r*4.,tau)-3.14)*3.+.2;

			}


    
    
			fixed4 frag(VertexOutput vertex_output) : SV_Target
			{
	
				//setup system
				float2 p = vertex_output.uv / 1-0.5;
				p.x *= 1/1;
				p*=4.;
	
				float rz = dualfbm(p);
	
				//rings
				p /= exp(fmod(time*10.,3.14159));
				rz *= pow(abs((0.1-circ(p))),.9);
	
				//final color
				float3 col = vec3(.2,0.1,0.4)/rz;
				col=pow(abs(col),vec3(.99));
				return vec4(col,1.);

			}
			ENDCG
		}
	}
}
