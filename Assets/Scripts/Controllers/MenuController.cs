using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    [SerializeField] private MenuView menuView;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerModel playerModel;

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
        menuView.menuTransitionsUI.ShowPanelImageNickName();
    }


    public void OnSaveButtonClicked()
    {
        playerModel.Name = menuView.inputName.text;
        GameManager.Instance.playerName = playerModel.Name;
        menuView.PlayNicknameParticle();
        menuView.menuTransitionsUI.HidePanelImageNickName();
        StartCoroutine(gameManager.NextScene(0.7f, 1));
    }

    //Go to credits
    public void OnCreditsButtonClicked()
    {
        SceneManager.LoadScene(3);
    }

}
