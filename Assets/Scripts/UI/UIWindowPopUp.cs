using UnityEngine;
using UnityEngine.Events;

namespace ProjectSAW
{
    public class UIWindowPopUp : MonoBehaviour
    {
        public UnityEvent<GameObject> WindowClosed;
        public UnityEvent<GameObject> WindowHid;
        public UIWindowPopUp Create(RectTransform parent, float x, float y, float width, float height)
        {
            RectTransform window = Instantiate(gameObject, parent).GetComponent<RectTransform>();
            window.transform.localScale = new Vector3(1,1,1);
            window.localPosition = new Vector3(x,y,0);
            window.sizeDelta = new Vector3(width,height,0);

            return window.GetComponent<UIWindowPopUp>();
        }
        public void Close()
        {
            WindowClosed.Invoke(gameObject);
            WindowClosed.RemoveAllListeners();
            WindowHid.RemoveAllListeners();
            Destroy(gameObject);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            WindowHid.Invoke(gameObject);
        }
        public void HideChildren()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform trans = transform.GetChild(i);
                if (trans.GetComponent<CanvasRenderer>() == null)
                {
                    trans.gameObject.SetActive(false);
                }
            }
            transform.gameObject.SetActive(false);
        }
    }
}