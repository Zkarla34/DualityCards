using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    [Header("NickName UI")]
    public InputField inputName;
    [SerializeField] private Image imageOk;
    [SerializeField] private GameObject buttonSave;
    [SerializeField] private GameObject panelNickName;
    [SerializeField] private ParticleSystem particleEffectPanelNickName;

    [Header("Transitions")]

    public MenuTransitionsUI menuTransitionsUI;


    //Show panel nickname
    public void ShowPanelNickName(bool isShow)
    {
        panelNickName.SetActive(isShow);
        if(isShow)
        {
            particleEffectPanelNickName.Play();
        }
    }

    public void UpdateInputValidationVisuals(bool isValid)
    {
        imageOk.color = isValid ? Color.green : Color.red;
        buttonSave.SetActive(isValid);
    }

    public void PlayNicknameParticle() => particleEffectPanelNickName.Play();

}
