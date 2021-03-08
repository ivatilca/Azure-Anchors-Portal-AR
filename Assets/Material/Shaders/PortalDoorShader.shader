Shader "Unlit/PortalDoorShader"
{
	
	SubShader
	{
	    Zwrite off
		ColorMask 0
		Cull Off

	



		Stencil
		{
		Ref 1
		Pass replace

		}
		Pass
		{
			
		}
	}
}
