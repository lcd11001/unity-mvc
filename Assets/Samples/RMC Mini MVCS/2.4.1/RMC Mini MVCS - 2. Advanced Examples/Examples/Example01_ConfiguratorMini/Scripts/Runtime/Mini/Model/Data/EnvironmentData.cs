using RMC.MiniMvcs.Samples.Configurator.Utilities;
using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Mini.Model.Data
{
    /// <summary>
    /// Defines the customizable characteristics for the <see cref="Environment"/>
    /// </summary>
    [System.Serializable]
    public class EnvironmentData
    {
        //  Fields ------------------------------------
        public Color FloorColor = Color.white;
        public Color BackgroundColor = Color.white;
        
        
        //  Methods ------------------------------------
        public static EnvironmentData FromRandomValues()
        {
            var colors = CustomColorUtility.GetRandomColorsWithoutRepeat(2);
            return new EnvironmentData
            {
                FloorColor = colors[0],
                BackgroundColor = colors[1],
            };
        }

        public static EnvironmentData FromDefaultValues()
        {
            var colors = CustomColorUtility.GetColorList();
            return new EnvironmentData
            {
                FloorColor = colors[0],
                BackgroundColor = colors[1],
            };
        }
    }
}
