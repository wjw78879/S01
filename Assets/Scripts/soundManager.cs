using UnityEngine;

public class soundManager : MonoBehaviour
{

    public AudioClip ClearBGM, jump, buttonSound, button_click, shoot, hit, deathsfx, gemsfx, takeDamage, healthRecover, land;
    public AudioSource audioSource;

    void start()
    {
        /*ClearBGM = Resources.Load<AudioClip>("ClearBGM");
        jump = Resources.Load<AudioClip>("Jump");
        buttonSound = Resources.Load<AudioClip>("buttonSound");
        button_click = Resources.Load<AudioClip>("button_click");
        shoot = Resources.Load<AudioClip>("Shoot");
        hit = Resources.Load<AudioClip>("Hit");
        deathsfx = Resources.Load<AudioClip>("deathsfx");
        gemsfx = Resources.Load<AudioClip>("gemsfx");
        takeDamage = Resources.Load<AudioClip>("takeDamage");
        healthRecover = Resources.Load<AudioClip>("healthRecover");
        land = Resources.Load<AudioClip>("land");*/
    }

    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "ClearBGM": audioSource.PlayOneShot(ClearBGM);break;
            case "Jump":audioSource.PlayOneShot(jump);break;
            case "buttonSound":audioSource.PlayOneShot(buttonSound);break;
            case "button_click":audioSource.PlayOneShot(button_click);break;
            case "Shoot": audioSource.PlayOneShot(shoot); break;
            case "Hit": audioSource.PlayOneShot(hit); break;
            case "deathsfx": audioSource.PlayOneShot(deathsfx);break;
            case "gemsfx": audioSource.PlayOneShot(gemsfx);break;
            case "takeDamage":audioSource.PlayOneShot(takeDamage);break;
            case "healthRecover":audioSource.PlayOneShot(healthRecover);break;
            case "land":audioSource.PlayOneShot(land);break;
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
