using Data;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreShow : MonoBehaviour
    {
        [SerializeField] private ScoreHolder scoreHolder;
        [SerializeField] private QuestionsData questionData;

        private void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"{scoreHolder.Score}/{questionData.GetQuestionsAmount()}";
        }
    }
}