using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameInput;

public class OptionUI : MonoBehaviour {

    public static OptionUI Instance { get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransfrom;

    private Action OnClosedButtonAction;

    private void Awake() {
        Instance = this;
        Hide();
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButton.onClick.AddListener(() =>
        {  
            Hide();
            OnClosedButtonAction();
        });

        moveUpButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Interact_Alt); });
        pauseButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Gamepad_Interact); });
        gamepadInteractAltButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Gamepad_InteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => { Rebindbinding(GameInput.Binding.Gamepad_Pause); });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameUnPaused += KitchenGameManager_OnGameUnPaused;
        UpdateVisual();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectText.text = "Sound Effects : " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music : " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }
    public void Show(Action OnClosedButtonAction) {
        this.OnClosedButtonAction = OnClosedButtonAction;
        gameObject.SetActive(true);

        soundEffectButton.Select();
    }
    private void Hide() {
        gameObject.SetActive(false); 
    }
    private void ShowPressToRebindKey() { 
        pressToRebindKeyTransfrom.gameObject.SetActive(true);
    } private void HidePressToRebindKey() { 
        pressToRebindKeyTransfrom.gameObject.SetActive(false);
    }

    private void Rebindbinding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();

        });
        
    }
}
