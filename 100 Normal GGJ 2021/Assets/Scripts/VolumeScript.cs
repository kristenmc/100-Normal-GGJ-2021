using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] Slider this_Slider;
    [SerializeField] float master_Volume;
    
    [SerializeField] float music_Volume;
    [SerializeField] float sfx_Volume;

    [SerializeField] string slider_String;

    public void Start()
    {
        //Set_Specific_Volume(slider_String, Get_Specific_Volume(slider_String)); 
    }
    
    
    public void Set_Specific_Volume(string what_Value)
    {
        float value = this_Slider.value;

        if(what_Value == "Master")
        {
            master_Volume = value;
            AkSoundEngine.SetRTPCValue("MasterVolume", master_Volume);
        }
        else if(what_Value == "Music")
        {
            music_Volume = value;
            AkSoundEngine.SetRTPCValue("MusicVolume", music_Volume);
        }
        else if(what_Value == "SFX")
        {
            sfx_Volume = value;
            AkSoundEngine.SetRTPCValue("SFXVolume", sfx_Volume);

        }
    }

    public void Set_Specific_Volume(string what_Value, float num_Value)
    {

        if(what_Value == "Master")
        {
            master_Volume = num_Value;
            AkSoundEngine.SetRTPCValue("MasterVolume", master_Volume);
        }
        else if(what_Value == "Music")
        {
            music_Volume = num_Value;
            AkSoundEngine.SetRTPCValue("MusicVolume", music_Volume);
        }
        else if(what_Value == "SFX")
        {
            sfx_Volume = num_Value;
            AkSoundEngine.SetRTPCValue("SFXVolume", sfx_Volume);

        }
        this_Slider.value = num_Value;
    }

    public float Get_Specific_Volume(string what_Value)
    {

        int type = 1;
        float value = -1;

        if(what_Value == "Master")
        {
            AkSoundEngine.GetRTPCValue("MasterVolume", gameObject, 0, out value, ref type);
        }
        else if(what_Value == "Music")
        {
            AkSoundEngine.GetRTPCValue("MusicVolume", gameObject, 0, out value, ref type);
        }
        else if(what_Value == "SFX")
        {
            AkSoundEngine.GetRTPCValue("SFXVolume", gameObject, 0, out value, ref type);
        }
        return value;
    }
}
