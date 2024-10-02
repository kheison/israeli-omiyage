using UnityEngine;

namespace Omiyage
{
    [CreateAssetMenu(fileName = "Product", menuName = "Product")]
    public class Product : ScriptableObject
    {
        #region Constants
        
        private const int MIN_LINE_LENGTH = 20;
        
        #endregion
        
        #region Editor Variables
        
        [SerializeField] private Sprite productImage;
        [SerializeField, TextArea(MIN_LINE_LENGTH, int.MaxValue)] private string descriptionEn;
        [SerializeField, TextArea(MIN_LINE_LENGTH, int.MaxValue)] private string descriptionJp;
        
        #endregion
        
        #region Properties
        
        public Sprite ProductImage
        {
            get => productImage;
        }
        
        public string DescriptionEn
        {
            get => descriptionEn;
        }
        
        public string DescriptionJp
        {
            get => descriptionJp;
        }
        
        #endregion
    }
}