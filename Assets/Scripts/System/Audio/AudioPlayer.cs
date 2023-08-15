using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

//ToDo: Create audio system to inject audio plays ids
//ToDo: Make Pool for instance sound objects

public class AudioPlayer : MonoBehaviour, IAudioPlayer
{
    [SerializeField] private string _soundId = default;
    [SerializeField] private AudioMixer _mixer = null;
    [SerializeField] private string _goSoundId = "SoundInstance";

    private AudioClip _clip = null;
    private GameObject _audioObject = null;

    public void Start()
    {
        var getter = new AddressableAsyncAssetGetter<AudioClip>();
        getter.LoadResource(_soundId, CompleteLoadAudioClip);
    }

    private void CompleteLoadAudioClip(AudioClip res)
    {
        _clip = res;
        var getter = new AddressableAsyncAssetGetter<GameObject>();
        getter.LoadResource(_goSoundId, CompleteLoadAudioInstance);
    }

    void CompleteLoadAudioInstance(GameObject audioObject)
    {
        audioObject.name = $"{nameof(AudioPlayer)}.{gameObject.name}.{_clip.name}";
        var _audio = audioObject.GetComponent<AudioSource>();
        _audio.outputAudioMixerGroup = _mixer.outputAudioMixerGroup;
        _audioObject = audioObject;
    }

    public void PlaySound()
    {
        if (_audioObject == null)
        {
            Debug.LogError($"[{nameof(AudioPlayer)}] Don't loaded audio object with id {_soundId}");
            return;
        }

        var audio = Instantiate(_audioObject).GetComponent<AudioSource>();
        audio.clip = _clip;
        audio.Play();

        DestroyAfterSound(audio.gameObject).Forget();
    }

    private async UniTaskVoid DestroyAfterSound(GameObject soundGO)
    {
        await UniTask.WaitForSeconds(_clip.length + 0.1f);
        Destroy(soundGO);
    }
}