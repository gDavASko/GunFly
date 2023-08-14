using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

//ToDo: Create audio system to inject audio plays ids
//ToDo: Make Pool for instance sound objects

public class AudioPlayer : MonoBehaviour, IAudioPlayer
{
    [SerializeField] private string _soundId = default;
    [SerializeField] private bool _loop = false;
    [SerializeField] private AudioMixer _mixer = null;

    private AudioClip _clip = null;
    private GameObject _audioObject = null;

    public void Start()
    {
        var getter = new AddressableAsyncAssetGetter<AudioClip>();
        getter.LoadResource(_soundId, (AudioClip res) =>
        {
            _clip = res;
            _audioObject = new GameObject();
            _audioObject.name = $"{nameof(AudioPlayer)}.{gameObject.name}.{_clip.name}";
            var _audio = _audioObject.AddComponent<AudioSource>();
            _audio.clip = _clip;
            _audio.loop = _loop;
            _audio.outputAudioMixerGroup = _mixer.outputAudioMixerGroup;
            _audio.playOnAwake = true;
        });
    }

    public void PlaySound()
    {
        var go = Instantiate(_audioObject);
        DestroyAfterSound(go).Forget();
    }

    private async UniTaskVoid DestroyAfterSound(GameObject soundGO)
    {
        await UniTask.WaitForSeconds(_clip.length + 0.1f);
        Destroy(soundGO);
    }
}