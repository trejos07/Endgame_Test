// Directorio /Nombre del shader
Shader "Custom/Recolor/MaskReColor" {

	// Variables disponibles en el inspector (Propiedades)
	Properties { 
		_Color1 ("Color 1", Color) = (1,1,1,1)
		_Texture1 ("Texture 1", 2D) = "white"{}
		_Texture2 ("Texture 2", 2D) = "white"{}
	}

	// Primer subshader
	SubShader { 
		LOD 200
		
		CGPROGRAM
		// Método para el cálculo de la luz
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		// Declaración de variables
		sampler2D _Texture1;
		sampler2D _Texture2;
		float4 _Color1;

		// Información adicional provista por el juego
		struct Input {
			float2 uv_Texture1;
			float2 uv_Texture2;
		};


		// Nucleo del programa
		void surf (Input IN, inout SurfaceOutputStandard o) {
			float4 c1 = tex2D(_Texture1, IN.uv_Texture1);
			float4 c2 = tex2D(_Texture2, IN.uv_Texture2);
			float4 c = c1*(1-c2)+c2*_Color1;

			o.Albedo = c.rgb ;

		}

		
		ENDCG

	}// Final del primer subshader

	


	// Segundo subshader si existe alguno
	// Tercer subshader...

	// Si no es posible ejecutar ningún subshader ejecute Diffuse
	FallBack "Diffuse"
}
