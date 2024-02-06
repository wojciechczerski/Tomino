using UnityEngine;

namespace Tomino.Model
{
    [CreateAssetMenu(fileName = "LocalizationProvider", menuName = "Tomino/LocalizationProvider", order = 2)]
    public class LocalizationProvider : ScriptableObject
    {
        public Localization currentLocalization;
    }
}
