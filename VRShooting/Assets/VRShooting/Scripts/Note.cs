using UnityEngine;
using UnityEngine.UI;

namespace VRShooting
{
    public class Note : MonoBehaviour
    {
        public float speed = 200f; // 노트 이동 속도
        private RectTransform rectTransform;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            // 노트를 아래로 이동
            rectTransform.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);

            // 화면 밖으로 나가면 삭제
            if (rectTransform.anchoredPosition.y < -500)
            {
                Destroy(gameObject);
            }
        }
    }
}
