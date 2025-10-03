using UnityEngine;
using UnityEngine.UIElements;

namespace UI.RootContainers
{
    public class UIMainRootContainer : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiMainContainer;

        private void Awake()
        {
            if(_uiMainContainer == null)
                _uiMainContainer = GetComponent<UIDocument>();
        }
    }
}