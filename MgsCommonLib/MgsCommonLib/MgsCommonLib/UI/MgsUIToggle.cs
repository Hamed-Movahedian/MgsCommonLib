using System.Collections.Generic;
using MgsCommonLib.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MgsCommonLib.UI
{
    public class MgsUIToggle : MonoBehaviour , IPointerDownHandler
    {
        public bool IsOn = false;
        public UnityEvent OnActivate;
        public UnityEvent OnDeactivate;

        private List<MgsUIToggle> _siblingToggles;
        private MgsUIToggle _masterToggle;

        protected void Start ()
        {
            // if _masterToggle not set => set this toggle as master toggle for all sibling toggles
            if (_masterToggle==null)
            {
                // get sibling toggles
                _siblingToggles = this.GetComponentInSiblingIncludeSelf<MgsUIToggle>();

                // set master toggle for all sibling toggles
                foreach (var siblingToggle in _siblingToggles)
                {
                    siblingToggle._masterToggle = this;
                    siblingToggle._siblingToggles = _siblingToggles;
                }
	        
            }

            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // if it's already on do nothing
            if(IsOn)
                return;

            // set all sibling toggles off - include this toggle
            _siblingToggles.ForEach(st =>
            {
                if (st.IsOn)
                    st.SetOff();
            });

            // set this toggle on
            SetOn();
        }

        private void SetOn()
        {
            IsOn = true;
            OnActivate.Invoke();
        }

        private void SetOff()
        {
            IsOn = false;
            OnDeactivate.Invoke();
        }

    }
}
