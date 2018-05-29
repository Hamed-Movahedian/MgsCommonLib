using MgsCommonLib.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    [RequireComponent(typeof(Image))]
    public class MgsToggle : MonoBehaviour
    {
        public Color SelectedColor=Color.red;
        public Color DeselectedColor=Color.gray;
        public float TransitionDuration = 0.5f;

        private Toggle _toggle;
        private Image _image;

        // Use this for initialization
        void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _image = GetComponent<Image>();

            _toggle.onValueChanged.AddListener(value => _image.RunColorAnimation(
                _image.color,
                value ? SelectedColor : DeselectedColor,
                TransitionDuration,
                c => _image.color = c));
        }

        private void OnValidate()
        {
            if(_image==null)
                _image=GetComponent<Image>();

            if (_toggle == null)
                _toggle = GetComponent<Toggle>();

            _image.color = _toggle.isOn ? SelectedColor : DeselectedColor;
        }


    }
}