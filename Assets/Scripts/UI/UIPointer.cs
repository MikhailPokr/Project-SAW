using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectSAW
{

    public class UIPointer : MonoBehaviour
    {
        private static GameObject _pointer;
        public void SetPointer(RectTransform button)
        {
            if (_pointer != null)
                HidePoinder();
            _pointer = Instantiate(gameObject, button);
            _pointer.transform.SetParent(button);
            _pointer.transform.localPosition = new Vector3(button.rect.xMin, 0, 0);
        }

        public static void HidePoinder()
        {
            if (_pointer != null)
                Destroy(_pointer);
            _pointer = null;
        }
    }
}
