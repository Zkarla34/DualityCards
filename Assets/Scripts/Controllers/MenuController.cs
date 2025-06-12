using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuView menuView;
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private MenuControllerTransitions menuControllerTransitions;
    [SerializeField] private SceneTransition SceneTransition;

    private void Start()
    {
        menuView.ShowPanelNickName(false);
        menuView.UpdateInputValidationVisuals(false);
    }

    private void Update()
    {
       bool isValid = menuView.inputName.text.Length >=3;
        menuView.UpdateInputValidationVisuals(isValid);
    }

    //Play Button
    public void OnPlayButtonClicked()
    {
        menuView.ShowPanelNickName(true);
        menuControllerTransitions.OnOpenNicknamePanel();
    }

    //Button Panel NickName
    public void OnSaveButtonClicked()
    {
        playerModel.Name = menuView.inputName.text;
        GameManager.Instance.playerName = playerModel.Name;
        menuView.PlayNicknameParticle();
        menuControllerTransitions.OnCloseNicknamePanel();
        menuControllerTransitions.OnStartGameButtonPressed();
    }

    //Go to credits
    public void OnCreditsButtonClicked()
    {
        SceneTransition.FadeToScene("Credits");
    }

}
