Shader "Custom/Outline" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Modifier ("Modifier", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf MyLambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float _Modifier;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			//o.Emission = c.rgb;
			o.Alpha = c.a;
		}

		half4 LightingMyLambert_Deferred (SurfaceOutput s, UnityGI gi, out half4 outGBuffer0, out half4 outGBuffer1, out half4 outGBuffer2)
		{
			UnityStandardData data;
			data.diffuseColor   = s.Albedo;
			data.occlusion      = 1;
			data.specularColor  = 0;
			data.smoothness     = 0;
			data.normalWorld    = s.Normal;

			UnityStandardDataToGbuffer(data, outGBuffer0, outGBuffer1, outGBuffer2);

			half4 emission = half4(s.Emission, 1);

			#ifdef UNITY_LIGHT_FUNCTION_APPLY_INDIRECT
				emission.rgb += gi.indirect.diffuse;
			#endif

			return emission;
		}

		void LightingMyLambert_GI (
			SurfaceOutput s,
			UnityGIInput data,
			inout UnityGI gi)
		{
			gi = UnityGlobalIllumination (data, 1.0, s.Normal);
		}


		ENDCG
	}
	FallBack "Diffuse"
}
