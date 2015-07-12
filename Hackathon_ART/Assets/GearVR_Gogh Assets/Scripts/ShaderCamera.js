#pragma strict

@script ExecuteInEditMode
@script RequireComponent (Camera)

class ShaderCamera extends PostEffectsBase {	

	public var sensitivityDepth : float = 1.0f;
	public var sensitivityNormals : float = 1.0f;
	public var lumThreshhold : float = 0.2f;
	public var edgeExp : float = 1.0f;
	public var sampleDist : float = 1.0f;

	public var edgeDetectShader : Shader;
	private var edgeDetectMaterial : Material = null;

	function CheckResources () : boolean {	
		CheckSupport (true);
	
		edgeDetectMaterial = CheckShaderAndCreateMaterial (edgeDetectShader,edgeDetectMaterial);
		

		if (!isSupported)
			ReportAutoDisable ();
		return isSupported;				
	}

	function Start () {
	
	}

	function SetCameraFlag () {
	
	}

	function OnEnable() {
		SetCameraFlag();
	}
	
	@ImageEffectOpaque
	function OnRenderImage (source : RenderTexture, destination : RenderTexture) {	
		if (CheckResources () == false) {
			Graphics.Blit (source, destination);
			return;
		}
				
		var sensitivity : Vector2 = Vector2 (sensitivityDepth, sensitivityNormals);		
		edgeDetectMaterial.SetVector ("_Sensitivity", Vector4 (sensitivity.x, sensitivity.y, 1.0, sensitivity.y));		
		edgeDetectMaterial.SetFloat ("_SampleDistance", sampleDist);		
		edgeDetectMaterial.SetVector ("_BgColor", Color.white);	
		edgeDetectMaterial.SetFloat ("_Exponent", edgeExp);
		edgeDetectMaterial.SetFloat ("_Threshold", lumThreshhold);
		
		Graphics.Blit (source, destination, edgeDetectMaterial);
	}
}