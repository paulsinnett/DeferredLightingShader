using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GrabGBuffer : MonoBehaviour
{
	new Camera camera;
	void Start()
	{
		camera = GetComponent<Camera>();
		CommandBuffer cb = new CommandBuffer();
		int temp = Shader.PropertyToID("_MyGBufferCopy");
		cb.GetTemporaryRT(temp, -1, -1);
		cb.Blit(BuiltinRenderTextureType.GBuffer0, temp);
		camera.AddCommandBuffer(CameraEvent.AfterGBuffer, cb); 
	}
}