using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class GrabGBuffer : MonoBehaviour
{
	static int instances = 0;
	static Dictionary<Camera, CommandBuffer> cameras = new Dictionary<Camera, CommandBuffer>();

	public void ConfigureCommandBuffers(Camera camera)
	{
		if (!cameras.ContainsKey(camera))
		{
			CommandBuffer commandBuffer = new CommandBuffer();
			int copy = Shader.PropertyToID("_MyGBufferCopy");
			commandBuffer.GetTemporaryRT(copy, -1, -1);
			commandBuffer.Blit(BuiltinRenderTextureType.GBuffer0, copy);
			camera.AddCommandBuffer(CameraEvent.AfterGBuffer, commandBuffer);
			cameras.Add(camera, commandBuffer);
		}
	}

	public void RemoveCommandBuffers()
	{
		foreach (Camera camera in cameras.Keys)
		{
			if (camera != null)
			{
				camera.RemoveCommandBuffer(CameraEvent.AfterGBuffer, cameras[camera]);
			}
		}
		cameras.Clear();
	}

	public void OnEnable()
	{
		if (instances == 0)
		{
			Camera.onPreRender += ConfigureCommandBuffers;
		}
		instances++;
	}

	public void OnDisable()
	{
		instances--;
		if (instances == 0)
		{
			Camera.onPreRender -= ConfigureCommandBuffers;
			RemoveCommandBuffers();
		}
	}
}
