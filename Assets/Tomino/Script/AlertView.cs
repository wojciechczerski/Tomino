using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AlertView : MonoBehaviour
{
    public Text titleText;
    public RectTransform buttonsContainer;
    public GameObject buttonPrefab;

    void Awake() => Hide();

    public void SetTitle(string text) => titleText.text = text;

    public void AddButton(string text, UnityAction onClickAction, UnityAction pointerDownAction)
    {
        var buttonGameObject = Instantiate(buttonPrefab);
        var rectTransformComponent = buttonGameObject.GetComponent<RectTransform>();
        var buttonComponent = buttonGameObject.GetComponent<Button>();
        var textComponent = buttonGameObject.GetComponentInChildren<Text>();
        var pointerHandlerComponent = buttonGameObject.GetComponent<PointerHandler>();

        pointerHandlerComponent.onPointerDown.AddListener(pointerDownAction);
        buttonComponent.onClick.AddListener(onClickAction);
        buttonComponent.onClick.AddListener(Hide);
        textComponent.text = text;

        rectTransformComponent.SetParent(buttonsContainer, false);
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide()
    {
        for (var i = buttonsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsContainer.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
}
