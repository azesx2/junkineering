using UnityEngine;

namespace Core.Views
{
    [ExecuteAlways]
    public class UnitsView : MonoBehaviour
    {
        [SerializeField] private float _verticalSize;

        public void AddUnit(Transform unitTransform)
        {
            unitTransform.SetParent(transform);
            Refresh();
        }

        private void Update()
        {
#if UNITY_EDITOR
            Refresh();
#endif
        }

        private void Refresh()
        {
            if (transform.childCount == 0)
            {
                return;
            }

            float step = transform.childCount == 1 ? 0f : _verticalSize / (transform.childCount - 1);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = new Vector3(0f, - step * i, 0f);
            }
        }
    }
}
