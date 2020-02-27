using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class AudioManager
{
    static AudioManager _instance;

    public static AudioManager Instance
    {
        get {
            if (_instance == null) {
                throw new Exception("Instance of audio Manager is Missing");
            }
            return _instance;
        }
    }

    Dictionary<String, Sound> SoundDictionary = new Dictionary<string, Sound>();

    public AudioManager() {
        _instance = this;
    }

    public void PlaySound(string fileName) {
        if (SoundDictionary.ContainsKey(fileName))
        {
            SoundDictionary[fileName].Play();
        }
        else {
            SoundDictionary.Add(fileName,new Sound(fileName));
            SoundDictionary[fileName].Play();
        }

    }

    public void AddBackgroundSound(string fileName) {
        Sound sound = new Sound(fileName, true);
        SoundDictionary.Add(fileName, sound);
        var obj =sound.Play();
    }



}
