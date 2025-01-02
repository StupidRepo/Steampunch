using System.Collections;
using Steamworks;
using UnityEngine;

namespace Steampunch;

public class SteampunchPluginBehaviour : MonoBehaviour
{
	private bool weAreShuttingDown = false;
	
	private void Start()
	{
		try {
			SteamClient.Init(2881650, false);
			StartCoroutine(DoSteamCallbacksWhenInit());
		} catch (System.Exception e) {
			Debug.LogError("Uh oh! Failed to initialize Steamworks: " + e);
		}
	}
	
	private IEnumerator DoSteamCallbacksWhenInit()
	{
		while(!SteamClient.IsValid)
		{
			yield return null;
		}
		
		Debug.Log("Steamworks initialized!");
		while (SteamClient.IsValid && !weAreShuttingDown)
		{
			SteamClient.RunCallbacks();
			yield return null;
		}
		
		Debug.LogWarning("Steamworks shutdown!");
	}
	
	private void OnApplicationQuit()
	{
		Debug.LogWarning("Shutting down Steamworks!");
		
		weAreShuttingDown = true;
		SteamClient.Shutdown();
	}
}