Shader "Custom/BurnClip" {

	Properties{
	  _AmountOfClip("Clip amount", Range(0,10)) = 0
	}
		SubShader{
		  Tags { "RenderType" = "Opaque" }
		  Cull Off
		  CGPROGRAM
		  #pragma surface surf Lambert
		  
		  struct Input 
		  {
			  //Getting the worldPos from unity
			  float3 worldPos;
		  };

		  float _AmountOfClip;
		  
		  void surf(Input IN, inout SurfaceOutput o) 
		  {
			  //Clips the current pixel depending on where the model is in world space
			  clip(frac((IN.worldPos.y + IN.worldPos.z * 10) * _AmountOfClip) - 0.5);

		  }



		  ENDCG
	}
		Fallback "Diffuse"
}