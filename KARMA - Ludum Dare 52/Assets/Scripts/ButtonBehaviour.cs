using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public Button button;

    private void Start()
    {
        ResetText();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateTextOnHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Mettre à jour la couleur du texte lorsque l'utilisateur clique sur le bouton
        text.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetText();
    }

    private void UpdateTextOnHover()
    {
        text.color = new Color32(231, 204, 145, 255);
        text.fontStyle = FontStyles.Underline;
    }

    private void ResetText()
    {
        text.color = Color.black;
        text.fontStyle = FontStyles.Normal;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
