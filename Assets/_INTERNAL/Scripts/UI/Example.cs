using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Example : MonoBehaviour
{
    [SerializeField] private UIDocument _mainUI;

    private VisualElement _root;
    private PlayerHealth _playerHealth;

    private InputAction _damageAction;
    private InputAction _healAction;

    private void Awake()
    {
        _damageAction = new(type: InputActionType.Button, binding: "<Keyboard>/d");
        _healAction = new(type: InputActionType.Button, binding: "<Keyboard>/h");

        _damageAction.Enable();
        _healAction.Enable();
    }

    private void OnDestroy()
    {
        _damageAction?.Dispose();
        _healAction?.Dispose();
    }

    private void Start()
    {
        _playerHealth = new PlayerHealth();

        _root = _mainUI.rootVisualElement;
        _root.dataSource = _playerHealth;
        int count = 0;

        var playButton = _root.Q<VisualElement>("PlayButton");
        playButton.RegisterCallback<ClickEvent, int>(OnClickEvent, count);

        var sliderBox = _root.Q<VisualElement>("SliderBox");
        sliderBox.style.visibility = Visibility.Hidden;
    }

    private void Update()
    {
        if (_damageAction.WasPerformedThisFrame())
            _playerHealth.Health -= 10;
        else if (_healAction.WasPerformedThisFrame())
            _playerHealth.Health += 10;
    }

    private void OnClickEvent(ClickEvent @event, int count)
    {
        Debug.Log($"Play button clicked: {count}");
    }
}

public class PlayerHealth
{
    [field: SerializeField, Range(0, 100)] public int Health { get; set; } = 100;
}