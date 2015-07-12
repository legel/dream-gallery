using UnityEngine;
using System.Collections;

public class ShaderCamera_Script : MonoBehaviour
{

		public Material shaderMaterial;

		void Start ()
		{

		}

		void Update ()
		{

		}

		private void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
				if (shaderMaterial == null)
						return;
				Graphics.Blit (source, destination, shaderMaterial);
		}

}
