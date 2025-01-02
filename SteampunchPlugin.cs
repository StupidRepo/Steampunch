using System.Reflection;
using HarmonyLib;
using Steamworks;
using Unity.Mathematics;
using UnityEngine;
using Zorro.Settings;
using Object = UnityEngine.Object;

namespace Steampunch
{
    [ContentWarningPlugin("stupidrepo.Steampunch", "0.1", true)]
    public class SteampunchPlugin
    {
        static SteampunchPlugin()
        {
            var myPluginPath = Assembly.GetExecutingAssembly().Location;
            var myPluginDir = Path.GetDirectoryName(myPluginPath);
            
            if(myPluginDir == null)
            {
                Debug.LogError("Failed to get my Content Warning plugin directory!");
                return;
            }
            
            var myExtrasPath = Path.Combine(myPluginDir, "extras");
            if(!Directory.Exists(myExtrasPath))
            {
                Debug.LogError("Failed to find my 'extras' directory!");
                return;
            }
            
            var myFacepunchPath = Path.Combine(myExtrasPath, "Facepunch.Steamworks.Win64.dll");
            if(!File.Exists(myFacepunchPath))
            {
                Debug.LogError("Failed to find my Facepunch.Steamworks.Win64.dll!");
                return;
            }
            
            var fpSwAssembly = Assembly.LoadFile(myFacepunchPath);
            Debug.Log($"Loaded Facepunch.Steamworks from {fpSwAssembly.Location}");
            
            var permaGameObj = new GameObject("SteampunchPlugin");
            Object.DontDestroyOnLoad(permaGameObj);
            
            permaGameObj.hideFlags = HideFlags.HideAndDontSave;
            permaGameObj.AddComponent<SteampunchPluginBehaviour>();
        }
    }

// Don't forget to inherit from IExposedSetting too!
    // [ContentWarningSetting]
    // public class ExampleSetting : FloatSetting, IExposedSetting
    // {
    //     public override void ApplyValue() => Debug.Log($"omg, mod setting changed to {Value}");
    //
    //     public override float GetDefaultValue() => 100;
    //     public override float2 GetMinMaxValue() => new(0, 100);
    //
    //     // Prefer using the Mods category
    //     public SettingCategory GetSettingCategory() => SettingCategory.Mods;
    //
    //     public string GetDisplayName() => "Example mod setting";
    // }
}