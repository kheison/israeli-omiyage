using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Omiyage
{
    public class MainView : MonoBehaviour
    {
        #region Editor Variables
        
        [SerializeField] private List<Product> _products;
        [SerializeField] private InfoBox _infoBox;
        
        #endregion
        
        #region Member Variables
        
        private VisualElement _middleContainer;
        private Button _enButton;
        private Button _jpButton;
        private Label _descriptionLabel;
        private bool _isEnglish = true;
        private int _currentIndex = 0; 
        private VisualElement _infoBoxElement; 
        
        #endregion

        #region Unity Methods
        
        private void Start()
        {
            SetupVisualElements();
            DisplayProduct();
            InitializeInfoBox();
        }

        #endregion

        #region Private Methods
        
        private void SetupVisualElements()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _descriptionLabel = new Label();
            _descriptionLabel.AddToClassList(UiToolkitConstants.DESCRIPTION_LABEL_CLASS);
            _middleContainer = root.Q<VisualElement>(UiToolkitConstants.MIDDLE_CONTAINER_NAME);

            root.Q<VisualElement>(UiToolkitConstants.SCROLL_VIEW_NAME).Add(_descriptionLabel);

            var prevButton = root.Q<Button>(UiToolkitConstants.PREVIOUS_BUTTON_NAME);
            var nextButton = root.Q<Button>(UiToolkitConstants.NEXT_BUTTON_NAME);

            _enButton = root.Q<Button>(UiToolkitConstants.ENGLISH_BUTTON_NAME);
            _jpButton = root.Q<Button>(UiToolkitConstants.JAPANESE_BUTTON_NAME);

            prevButton.clicked += ShowPreviousProduct;
            nextButton.clicked += ShowNextProduct;
            _enButton.clicked += () => SwitchLanguage(true);
            _jpButton.clicked += () => SwitchLanguage(false);
            
            var infoButton = root.Q<Button>(UiToolkitConstants.INFO_BUTTON_NAME);
            infoButton.clicked += HandleInfoBoxRequest;
            
            UpdateLanguageButtons();
        }
        
        private void DisplayProduct()
        {
            var product = _products[_currentIndex];

            _middleContainer.Clear();
            var imageElement = new Image { sprite = product.ProductImage };
            _middleContainer.Add(imageElement);
            _descriptionLabel.text = _isEnglish ? product.DescriptionEn : product.DescriptionJp;
        }

        private void ShowPreviousProduct()
        {
            _currentIndex = (_currentIndex - 1 + _products.Count) % _products.Count;
            DisplayProduct();
        }

        private void ShowNextProduct()
        {
            _currentIndex = (_currentIndex + 1) % _products.Count;
            DisplayProduct();
        }

        private void SwitchLanguage(bool toEnglish)
        {
            _isEnglish = toEnglish;
            UpdateLanguageButtons();
            DisplayProduct();
        }

        private void UpdateLanguageButtons()
        {
            if (_isEnglish)
            {
                _enButton.AddToClassList(UiToolkitConstants.SELECTED_LANGUAGE_CLASS);
                _jpButton.RemoveFromClassList(UiToolkitConstants.SELECTED_LANGUAGE_CLASS);
            }
            else
            {
                _enButton.RemoveFromClassList(UiToolkitConstants.SELECTED_LANGUAGE_CLASS);
                _jpButton.AddToClassList(UiToolkitConstants.SELECTED_LANGUAGE_CLASS);
            }
        }

        private void InitializeInfoBox()
        {
            _infoBoxElement = _infoBox.Initialize();
            var root = GetComponent<UIDocument>().rootVisualElement;
            root.Q(UiToolkitConstants.MAIN_VIEW_CONTAINER_NAME).Add(_infoBoxElement);
        }

        private void HandleInfoBoxRequest()
        {
            _infoBox.Show();
        }
        
        #endregion
    }
}
