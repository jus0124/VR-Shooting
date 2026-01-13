using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VRShooting
{
    public class RhythmManager : MonoBehaviour
    {
        public string chartFilePath; // 채보 파일 경로 (.txt 파일)
        public float offset;         // 오프셋 (초 단위)
        public float timingWindow = 0.2f; // 타이밍 허용 범위 (초 단위)
        private float elapsedTime;   // 경과 시간
        private int currentNoteIndex = 0; // 현재 처리 중인 노트 인덱스

        public List<HitObject> hitObjects = new List<HitObject>();

        [Serializable]
        public class HitObject
        {
            public int lane;      // 레인 번호
            public float timing;  // 히트 타이밍 (초 단위)
        }

        void Start()
        {
            if (!string.IsNullOrEmpty(chartFilePath))
            {
                LoadOsuChart(chartFilePath);
            }
            else
            {
                Debug.LogError("chartFilePath가 설정되지 않았습니다.");
            }
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;
        }

        public bool IsTimingCorrect()
        {
            if (hitObjects == null || currentNoteIndex >= hitObjects.Count) return false;

            var currentNote = hitObjects[currentNoteIndex];
            float noteTiming = currentNote.timing + offset;

            if (Mathf.Abs(elapsedTime - noteTiming) <= timingWindow)
            {
                Debug.Log($"Perfect Timing! Note Timing: {noteTiming}, Elapsed Time: {elapsedTime}");
                currentNoteIndex++;
                return true;
            }

            if (elapsedTime > noteTiming + timingWindow)
            {
                Debug.Log($"Missed Note! Note Timing: {noteTiming}, Elapsed Time: {elapsedTime}");
                currentNoteIndex++;
            }

            return false;
        }

        public void LoadOsuChart(string filePath)
        {
            hitObjects.Clear();
            bool hitObjectsSection = false;

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (line.StartsWith("[HitObjects]"))
                    {
                        hitObjectsSection = true;
                        continue;
                    }

                    if (hitObjectsSection && !string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length >= 3)
                        {
                            int x = int.Parse(parts[0]);
                            float timingMs = float.Parse(parts[2]);

                            int lane = DetermineLane(x);
                            float timing = timingMs / 1000.0f;

                            hitObjects.Add(new HitObject { lane = lane, timing = timing });
                        }
                    }
                }

                Debug.Log($"채보 로드 완료: {hitObjects.Count}개의 노트.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"채보 로드 중 오류 발생: {ex.Message}");
            }
        }

        private int DetermineLane(int xPosition)
        {
            if (xPosition < 128) return 0; // 첫 번째 레인
            if (xPosition < 256) return 1; // 두 번째 레인
            if (xPosition < 384) return 2; // 세 번째 레인
            return 3;                      // 네 번째 레인
        }
    }
}
