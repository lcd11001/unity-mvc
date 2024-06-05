using RMC.MiniMvcs.Samples.Configurator.Utilities;
using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Mini.Model.Data
{
    /// <summary>
    /// Defines the customizable characteristics for the <see cref="Player"/>
    /// </summary>
    [System.Serializable]
    public class CharacterData
    {
        //  Fields ------------------------------------
        public Color HeadColor = Color.white;
        public Color ChestColor = Color.white;
        public Color LegsColor = Color.white;
        
        //  Methods ------------------------------------
        public static CharacterData FromRandomValues()
        {
            var colors = CustomColorUtility.GetRandomColorsWithoutRepeat(3);
            return new CharacterData
            {
                HeadColor = colors[0],
                ChestColor = colors[1],
                LegsColor = colors[2],
            };
        }

        public static CharacterData FromDefaultValues()
        {
            var colors = CustomColorUtility.GetColorList();
            return new CharacterData
            {
                HeadColor = colors[0],
                ChestColor = colors[1],
                LegsColor = colors[2],
            };
        }
    }
}
