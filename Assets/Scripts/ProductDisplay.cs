using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Omiyage
{
    public class ProductDisplay : MonoBehaviour
    {
        #region Editor Variables
        
        [SerializeField] private List<Product> products;
        
        #endregion
        
        #region Member Variables
        
        private VisualElement _middleContainer;
        private Button _enButton;
        private Button _jpButton;
        private Label _descriptionLabel;
        private bool _isEnglish = true;
        private int _currentIndex = 0; 
        
        #endregion

        #region Unity Methods
        
        private void Start()
        {
            SetupVisualElements();
            DisplayProduct();
        }

        #endregion

        #region Private Methods
        
        private void SetupVisualElements()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _descriptionLabel = new Label();
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
            
            // Initialize the selected state
            UpdateLanguageButtons();
        }
        
        private void DisplayProduct()
        {
            var product = products[_currentIndex];

            _middleContainer.Clear();
            var imageElement = new Image { sprite = product.ProductImage };
            _middleContainer.Add(imageElement);
            _descriptionLabel.text = _isEnglish ? product.DescriptionEn : product.DescriptionJp;
        }

        private void ShowPreviousProduct()
        {
            _currentIndex = (_currentIndex - 1 + products.Count) % products.Count;
            DisplayProduct();
        }

        private void ShowNextProduct()
        {
            _currentIndex = (_currentIndex + 1) % products.Count;
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
                _enButton.AddToClassList("selected");
                _jpButton.RemoveFromClassList("selected");
            }
            else
            {
                _enButton.RemoveFromClassList("selected");
                _jpButton.AddToClassList("selected");
            }
        }
        
        #endregion
    }
}
