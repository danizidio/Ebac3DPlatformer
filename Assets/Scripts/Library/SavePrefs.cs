using UnityEngine;

namespace CommonMethodsLibrary
{
    public enum SaveStrings
    {
        MONETARY_VALUE
        ,HIGHSCORE
        ,BLOOM
        ,FILMGRAIN
        ,CHROMATIC_ABERRATION
        ,VOLUME
        ,RESOLUTION
        ,FULLSCREEN
        ,SFX
        ,FIRSTUSE
    }

    public class SavePrefs
    {
        public static void PlayerSaveNumber(SaveStrings stringSave, float value)
        {
            PlayerPrefs.SetFloat(stringSave.ToString(), value);
            PlayerPrefs.Save();
        }

        public static void PlayerSaveBool(SaveStrings stringSave, bool value)
        {
            PlayerPrefs.SetString(stringSave.ToString(), value.ToString());
            PlayerPrefs.Save();
        }

        public static void PlayerSaveString(SaveStrings stringSave, string value)
        {
            PlayerPrefs.SetString(stringSave.ToString(), value.ToString());
            PlayerPrefs.Save();
        }
    }
    public class LoadPrefs
    {
        public static bool LoadingBool(SaveStrings stringSave)
        {
            return PlayerPrefs.GetString(stringSave.ToString()) == "True";
        }

        public static float PlayerLoadNumber(SaveStrings stringSave)
        {
            if(PlayerPrefs.HasKey(stringSave.ToString()))
            {
                return PlayerPrefs.GetFloat(stringSave.ToString());
            }
            else
            {
                return 0;
            }
        }

        public static string PlayerLoadString(SaveStrings stringSave)
        {
            if(PlayerPrefs.HasKey(stringSave.ToString()))
            {
                return PlayerPrefs.GetString(stringSave.ToString());
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
