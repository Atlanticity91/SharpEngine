using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

using System.Collections.Generic;

namespace SharpEngine.Audio {

    public class AudioManager {

        public static AudioManager INSTANCE;

        private ContentManager ContentManager;
        private int SoundCount;
        private int MusicCount;
        private List<SoundEffect> Sounds;
        private List<Song> Musics;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content_manager" >Current game content manager</param>
        public AudioManager( ContentManager content_manager ) {
            INSTANCE = this;

            this.ContentManager = content_manager;
            this.SoundCount = 0;
            this.MusicCount = 0;
            this.Sounds = new List<SoundEffect>( );
            this.Musics = new List<Song>( );
        }

        /// <summary>
        /// Load a game sound from content.
        /// </summary>
        /// <param name="path" >Path to the sound from content folder</param>
        public void LoadSound( string path ) {
            if ( path != "" && path != " " ) {
                this.SoundCount++;
                this.Sounds.Add( this.ContentManager.Load<SoundEffect>( path ) );
            }
        }

        /// <summary>
        /// Load a game music from content.
        /// </summary>
        /// <param name="path" >Path to the sound from content folder</param>
        public void LoadMusic( string path ) {
            if ( path != "" && path != " " ) {
                this.MusicCount++;
                this.Musics.Add( this.ContentManager.Load<Song>( path ) );
            }
        }

        /// <summary>
        /// Get a sound from the current audio manager.
        /// </summary>
        /// <param name="index" >Index of the query sound</param>
        /// <returns>The query sound</returns>
        public SoundEffect GetSound( int index ) {
            if ( index > -1 && index < this.SoundCount )
                return this.Sounds[ index ];

            return null;
        }

        /// <summary>
        /// Get a music from the current audio manager.
        /// </summary>
        /// <param name="index" >Index of the query music</param>
        /// <returns>The query music</returns>
        public Song GetMusic( int index ) {
            if ( index > -1 && index < this.MusicCount )
                return this.Musics[ index ];

            return null;
        }

    }

}
