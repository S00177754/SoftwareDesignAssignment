using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DsaaCAassessment2019.Classes
{
    public static class AssetManager
    {
        //Static dictionaries for textures,sound effects and song files
        static private Dictionary<string, SoundEffect> SoundEffects { get; set; } = new Dictionary<string, SoundEffect>();
        static private SoundEffectInstance SEI;
        static private Dictionary<string, Song> Songs { get; set; } = new Dictionary<string, Song>();
        static private Dictionary<string, Texture2D> Textures { get; set; } = new Dictionary<string, Texture2D>();

        //Add asset method decides dictionary based on parameter type
        public static void AddAsset(string name,Texture2D texture)
        {
            Textures.Add(name,texture);
        }
        public static void AddAsset(string name, SoundEffect soundEffect)
        {
            SoundEffects.Add(name, soundEffect);
        }
        public static void AddAsset(string name, Song song)
        {
            Songs.Add(name, song);
        }


        public static void PlaySoundEffect(string soundEffectName)
        {
            SEI = SoundEffects[soundEffectName].CreateInstance();
            SEI.IsLooped = false;
            SEI.Play();
        }

        public static void PlaySong(string songName)
        {
            if (MediaPlayer.State == MediaState.Stopped || MediaPlayer.State == MediaState.Paused)
            MediaPlayer.Play(Songs[songName]);
        }
        public static void PlaySong(string songName, bool loop)
        {
            
            if (MediaPlayer.State == MediaState.Stopped || MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Play(Songs[songName]);

                if (loop)
                    MediaPlayer.IsRepeating = true;
                else
                    MediaPlayer.IsRepeating = false;
            }
        }

        public static Texture2D GetTexture(string textureName)
        {
            return Textures[textureName];
        }
    }
}
