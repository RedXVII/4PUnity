// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Effect"
{
	Properties{
		_ColorMultiplier("Color Multiplier", Color) = (1,1,1,1)
		_ColorOffset("Color Offset", Color) = (0,0,0,0)
		_MainTex("Texture", 2D) = "white" {}
		_Mask("Culling Mask", 2D) = "white" {}
		_HorizontalSkew("Horizontal Skew", Float) = 0
		_VerticalSkew("Vertical Skew", Float) = 0
	}

		Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		AlphaTest GEqual[_Cutoff]
		Cull Off Lighting Off ZWrite Off

	SubShader{
	Pass
	{

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma target 2.0
		#pragma multi_compile_particles
		#pragma multi_compile_fog

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		fixed4 _ColorMultiplier;
		fixed4 _ColorOffset;
		float _HorizontalSkew;
		float _VerticalSkew;
		fixed4 _MainTex_ST;
		float4x4 _Matrix;
	//	SetTexture[_Mask]{ combine texture }
	//	SetTexture[_MainTex]{ combine texture, previous }

		struct appdata_t 
		{
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f 
		{
			float4 vertex : SV_POSITION;
			float2 texcoord : TEXCOORD0;
			UNITY_FOG_COORDS(1)
			UNITY_VERTEX_OUTPUT_STEREO
		};

		v2f vert(appdata_t v)
		{
			v2f o;
			UNITY_SETUP_INSTANCE_ID(v);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
			o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
		
			// Create a skew transformation matrix
			float hSkew = _HorizontalSkew;
			float vSkew = _VerticalSkew;
			float4x4 _Matrix1 = float4x4(
				0.866, 0.5, 0, 0,
				-0.5, 0.866, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1);

			float4 oVertex = mul(_Matrix, v.vertex);
			o.vertex = UnityObjectToClipPos(oVertex);

			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 r = tex2D(_MainTex, i.texcoord);
			fixed4 color = fixed4(1, 1, 1, 1);
			color.r = _ColorMultiplier.r *r.r + _ColorOffset.r;
			color.g = _ColorMultiplier.g *r.g + _ColorOffset.g;
			color.b = _ColorMultiplier.b *r.b + _ColorOffset.b;
			if (r.a > 0)
				color.a = _ColorMultiplier.a *r.a + _ColorOffset.a;
			else
				color.a = r.a;
			return color;
		}
		ENDCG
	}
	}
	}
}
