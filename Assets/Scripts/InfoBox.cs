using UnityEngine;
using UnityEngine.UIElements;

namespace Omiyage
{
    public class InfoBox : MonoBehaviour
    {
        #region Editor Variables
        
        [SerializeField] private VisualTreeAsset _infoBoxUXML;
        
        #endregion
        
        #region Member Variables
        
        private VisualElement _popupContainer;
        private Button _closeButton;
        private Button _linkedinButton;
        private Button _slackButton;
        
        #endregion
        
        
        #region Public Methods

        public VisualElement Initialize()
        {
            if (_popupContainer != null)
            {
                return _popupContainer;
            }
            
            CopyTreeAndRegisterButtons();

            return _popupContainer;
        }

        private void CopyTreeAndRegisterButtons()
        {
            _popupContainer = _infoBoxUXML.CloneTree();
            _popupContainer.AddToClassList(UiToolkitConstants.INFO_BOX_MAIN_POPUP_CONTAINER_CLASS);
            
            _closeButton = _popupContainer.Q<Button>(UiToolkitConstants.INFO_BOX_CLOSE_BUTTON_NAME);
            _linkedinButton = _popupContainer.Q<Button>(UiToolkitConstants.INFO_BOX_LINKEDIN_BUTTON_NAME);
            _slackButton = _popupContainer.Q<Button>(UiToolkitConstants.INFO_BOX_SLACK_BUTTON_NAME);

            _closeButton.clicked += Hide;
            _linkedinButton.clicked += () => Application.OpenURL(UiToolkitConstants.LINKEDIN_PROFILE_URL);
            _slackButton.clicked += () => Application.OpenURL(UiToolkitConstants.SLACK_INVITE_URL);
        }

        public void Show()
        {
            if (_popupContainer != null)
            {
                _popupContainer.style.display = DisplayStyle.Flex;
            }
        }
        
        #endregion
        
        #region Private Methods

        private void Hide()
        {
            if (_popupContainer != null)
            {
                _popupContainer.style.display = DisplayStyle.None;
            }
        }
        
        #endregion
    }
}