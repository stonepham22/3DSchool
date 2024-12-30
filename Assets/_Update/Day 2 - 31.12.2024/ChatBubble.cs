using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MyGameObjBehaviour
{
    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text)
    {
        Transform chatBubble = Instantiate(GameAsset.Instance.bubblePrefab, parent);
        chatBubble.localPosition = localPosition;
        chatBubble.GetComponent<ChatBubble>().Setup(iconType, text);
        
        chatBubble.localRotation = Quaternion.Euler(0, -parent.localRotation.eulerAngles.y, 0);

        Destroy(chatBubble.gameObject, 3f);    
    }
    
    public enum IconType
    {
        Happy,
        Neutral,
        Angry
    }

    [SerializeField] private Sprite _happyIcon;
    [SerializeField] private Sprite _neutralIcon;
    [SerializeField] private Sprite _angryIcon;

    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Vector2 _padding;
    // [SerializeField] private Transform _bubblePrefab;

    protected override void LoadComponents()
    {
        MyGetReference.Get<SpriteRenderer>(transform.Find("Background"), ref _background);
        MyGetReference.Get<SpriteRenderer>(transform.Find("Icon"), ref _icon);
        MyGetReference.Get<TextMeshPro>(transform.Find("Text"), ref _text);
    }

    // private void Start()
    // {
    //     Setup(IconType.Angry,"Hello World! Say hello to my little friend!");
    // }

    private void Setup(IconType iconType, string text)
    {
        _text.SetText(text);
        _text.ForceMeshUpdate();
        Vector2 textSize = _text.GetRenderedValues(false);

        // Vector2 padding = new Vector2(7f, 2f);
        _background.size = textSize + _padding;

        // Vector3 offset = new Vector2(-2f, 0f);
        // _background.transform.localPosition =
        //     new Vector3(_background.size.x / 2f, 0f)+offset;
        _icon.sprite = GetIcon(iconType);
    }


    private Sprite GetIcon(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Happy: return _happyIcon;
            case IconType.Neutral: return _neutralIcon;
            case IconType.Angry: return _angryIcon;
        }
    }


}
