using UnityEngine;

namespace VRShooting
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private RhythmManager rhythmManager; // RhythmManager 연결
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform gunBarrelEnd;
        [SerializeField] ParticleSystem gunParticle;
        [SerializeField] AudioSource gunAudioSource;

        [Header("Shotgun Settings")]
        [SerializeField] int bulletCount = 10;       // 한 번에 발사할 총알 개수
        [SerializeField] float spreadAngle = 15f;  // 산탄 퍼짐 각도 (구형 퍼짐)

        void Start()
        {
            if (rhythmManager == null)
            {
                rhythmManager = FindObjectOfType<RhythmManager>();

                if (rhythmManager == null)
                {
                    Debug.LogError("RhythmManager를 찾을 수 없습니다. 씬에 RhythmManager를 추가하고 연결하세요.");
                }
                else
                {
                    Debug.Log("RhythmManager가 성공적으로 연결되었습니다.");
                }
            }
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (rhythmManager != null && rhythmManager.IsTimingCorrect())
                {
                    Shoot();
                }
                else
                {
                    Debug.Log("타이밍이 맞지 않습니다.");
                }
            }
        }

        void Shoot()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                // 랜덤 구면 퍼짐
                Vector3 randomDirection = gunBarrelEnd.forward;
                randomDirection += new Vector3(
                    Random.Range(-spreadAngle, spreadAngle) / 100f,
                    Random.Range(-spreadAngle, spreadAngle) / 100f,
                    Random.Range(-spreadAngle, spreadAngle) / 100f
                );

                randomDirection.Normalize(); // 방향 벡터 정규화

                Quaternion spreadRotation = Quaternion.LookRotation(randomDirection);

                // 총알 생성
                Instantiate(bulletPrefab, gunBarrelEnd.position, spreadRotation);
            }

            gunParticle.Play();
            gunAudioSource.Play();
        }
    }
}
