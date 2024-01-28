using UnityEngine;

public class SFXSingleton : MonoBehaviour
{
	public static SFXSingleton Instance => s_instance;
	private static SFXSingleton s_instance = null;

	public AudioSource AudioSource;

    public AudioClip PlipClip1;
    public AudioClip PlipClip2;
    public AudioClip PlipClip3;

    public AudioClip SplashClip1;
	public AudioClip SplashClip2;
	public AudioClip SplashClip3;

	public AudioClip RodClip;

	public AudioClip ThunkClip1;
	public AudioClip ThunkClip2;
	public AudioClip ThunkClip3;

    private AudioClip[] m_plipClips;
    private AudioClip[] m_splashClips;
	private AudioClip[] m_thunkClips;

	private void Awake()
	{
		if (s_instance != null)
		{
			Destroy(gameObject);
			return;
		}

		s_instance = this;

        m_plipClips = new AudioClip[] {
            PlipClip1, PlipClip2, PlipClip3};
        m_splashClips = new AudioClip[] {
            SplashClip1, SplashClip2, SplashClip3};
		m_thunkClips = new AudioClip[] {
			ThunkClip1, ThunkClip2, ThunkClip3};
	}

	private void PlayRandomSFX(AudioClip[] clips, float volumeScale = 1.0f)
	{
		int random = Random.Range(0, clips.Length);
		AudioSource.PlayOneShot(clips[random], volumeScale);
    }

    public void PlayRodSFX(float volumeScale = 1.0f)
    {
        AudioSource.PlayOneShot(RodClip, volumeScale);
    }

    public void PlayPlipSFX(float volumeScale = 1.0f)
	{
		PlayRandomSFX(m_plipClips, volumeScale);
	}

	public void PlaySplashSFX(float volumeScale = 1.0f)
    {
        PlayRandomSFX(m_splashClips, volumeScale);
    }

	public void PlayThunkSFX(float volumeScale = 1.0f)
	{
		PlayRandomSFX(m_thunkClips, volumeScale);
	}

	private void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}
}
