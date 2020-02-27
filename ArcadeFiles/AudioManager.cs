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

        Sound sound = new Sound("Audio/Hamsters.mp3", true);
        sound.Play();

        //PlaySound("Getting_Damaged.mp3");
    }

    public void PlaySound(string fileName) {
        if (SoundDictionary.ContainsKey(fileName))
        {

            Sound tempSound = SoundDictionary[fileName];
            tempSound.Play();
            //Sound sound = new Sound(fileName);
            //SoundDictionary.Add(fileName, sound);
            //sound.Play();

        }
        else {
            Sound sound = new Sound(fileName);
            SoundDictionary.Add(fileName, sound);
            sound.Play();
        }
        

        //Sound sound = new Sound(fileName);
        //sound.Play();
        //SoundDictionary[fileName].Play();


        /*if (SoundDictionary.ContainsKey(fileName))
        {
        }
        else {
            SoundDictionary.Add(fileName,new Sound(fileName));
            
        }
        SoundDictionary[fileName].Play();
        Console.WriteLine(fileName);
        */
    }

    public void AddBackgroundSound(string fileName) {
        Sound sound = new Sound(fileName, true);
        SoundDictionary.Add(fileName, sound);
        var obj =sound.Play();
    }



}
