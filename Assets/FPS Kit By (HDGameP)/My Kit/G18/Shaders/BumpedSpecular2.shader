Shader "Bumper Specular V2"
{
	Properties 
	{
_MainColour("_MainColour", Color) = (1,1,1,1)
_MainTexture("_MainTexture", 2D) = "black" {}
_BumpTex("_BumpTex", 2D) = "bump" {}
_SpecularColour("_SpecularColour", Color) = (1,1,1,1)
_GlossValue("_GlossValue", Range(0,1) ) = 0.5
_SpecularTexture("_SpecularTexture", 2D) = "black" {}

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 2.0


float4 _MainColour;
sampler2D _MainTexture;
sampler2D _BumpTex;
float4 _SpecularColour;
float _GlossValue;
sampler2D _SpecularTexture;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}
			
			struct Input {
				float2 uv_MainTexture;
float2 uv_BumpTex;
float2 uv_SpecularTexture;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Tex2D0=tex2D(_MainTexture,(IN.uv_MainTexture.xyxy).xy);
float4 Multiply0=_MainColour * Tex2D0;
float4 Tex2D2=tex2D(_BumpTex,(IN.uv_BumpTex.xyxy).xy);
float4 UnpackNormal0=float4(UnpackNormal(Tex2D2).xyz, 1.0);
float4 Tex2D1=tex2D(_SpecularTexture,(IN.uv_SpecularTexture.xyxy).xy);
float4 Multiply2=_SpecularColour * Tex2D1;
float4 Master0_2_NoInput = float4(0,0,0,0);
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Multiply0;
o.Normal = UnpackNormal0;
o.Specular = _GlossValue.xxxx;
o.Gloss = Multiply2;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}