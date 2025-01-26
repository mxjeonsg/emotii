using Raylib_cs;

namespace Emotii.Framework;

public class EAudio {
    private bool audioCtxStarted = false;

    public EAudio() {
        Raylib.InitAudioDevice();
        if(Raylib.IsAudioDeviceReady()) this.audioCtxStarted = true;
    }

    ~EAudio() {
        Raylib.CloseAudioDevice();
    }
}

unsafe public class ESfx {
    private Sound sound;
    private EAudio audio_ctx;

    public float pan = .5f, volume = 1f, pitch = 1f;
    private float _pan = .5f, _volume = 1f, _pitch = 1f;

    private bool isAudioReady = false;

    public ESfx(EAudio actx, string spath) {
        this.audio_ctx = actx;

        if(Raylib.FileExists(ConvertString.strToSbytePtr(spath))) {
            this.sound = Raylib.LoadSound(spath);
            if(Raylib.IsSoundValid(this.sound)) {
                this.isAudioReady = true;
            }
        }
    }

    ~ESfx() {
        Raylib.UnloadSound(this.sound);
    }

    public bool isPossible() {
        return Raylib.IsAudioDeviceReady() && Raylib.IsSoundValid(this.sound) && this.isAudioReady;
    }

    public bool play() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.PlaySound(this.sound);
        return true;
    }

    public bool stop() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.StopSound(this.sound);
        return true;
    }

    public bool resume() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.ResumeSound(this.sound);
        return true;
    }

    public bool pause() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.PauseSound(this.sound);
        return true;
    }

    // This function updates all the stats (Volume, Pitch, Pan)
    // if modified, and returns `true` if a change was made.
    public bool update() {
        bool same = true;

        if(this._pan != this.pan) {
            this._pan = this.pan;
            Raylib.SetSoundPan(this.sound, this.pan);
            same = false;
        }

        if(this._pitch != this.pitch) {
            this._pitch = this.pitch;
            Raylib.SetSoundPitch(this.sound, this.pitch);
            same = false;
        }

        if(this._volume != this.volume) {
            this._volume = this.volume;
            Raylib.SetSoundVolume(this.sound, this.volume);
            same = false;
        }

        return same == true;
    }
}

unsafe public class EMusic {
    private Music music;
    private EAudio audio_ctx;

    public float pan = .5f, volume = 1f, pitch = 1f;
    public ulong length = 0;
    public ulong cursor = 0;

    private float _pan = .5f, _volume = 1f, _pitch = 1f;

    public EMusic(EAudio actx, string spath) {
        this.audio_ctx = actx;

        this.music = Raylib.LoadMusicStream(spath);
        this.length = (ulong) Raylib.GetMusicTimeLength(this.music);
    }

    ~EMusic() {
        Raylib.UnloadMusicStream(this.music);
    }

    public bool isPossible() {
        return Raylib.IsAudioDeviceReady() && Raylib.IsMusicValid(this.music);
    }

    public bool play() {
        if(!this.isPossible()) {
            return false;
        }
        
        this.update();

        Raylib.PlayMusicStream(this.music);
        return true;
    }

    public bool stop() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.StopMusicStream(this.music);
        return true;
    }

    public bool resume() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.ResumeMusicStream(this.music);
        return true;
    }

    public bool pause() {
        if(!this.isPossible()) {
            return false;
        }

        this.update();

        Raylib.PauseMusicStream(this.music);
        this.cursor = (ulong) Raylib.GetMusicTimePlayed(this.music);
        return true;
    }

    // This function updates all the stats (Volume, Pitch, Pan)
    // if modified, and returns `true` if a change was made.
    public bool update() {
        bool same = true;

        if(this._pan != this.pan) {
            this._pan = this.pan;
            Raylib.SetMusicPan(this.music, this.pan);
            same = false;
        }

        if(this._pitch != this.pitch) {
            this._pitch = this.pitch;
            Raylib.SetMusicPitch(this.music, this.pitch);
            same = false;
        }

        if(this._volume != this.volume) {
            this._volume = this.volume;
            Raylib.SetMusicVolume(this.music, this.volume);
            same = false;
        }

        this.cursor = (ulong) Raylib.GetMusicTimePlayed(this.music);

        return same == true;
    }
}