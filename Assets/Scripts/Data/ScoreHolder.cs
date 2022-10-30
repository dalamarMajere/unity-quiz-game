using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Create ScoreHolder", fileName = "ScoreHolder", order = 0)]
    public class ScoreHolder : ScriptableObject
    {
        public int Score { get; set; }
    }
}