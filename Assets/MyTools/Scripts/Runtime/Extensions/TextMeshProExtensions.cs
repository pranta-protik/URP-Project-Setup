using TMPro;

namespace MyTools
{
    public static class TextMeshProExtensions
    {
        /// <summary>
        /// Changes alpha of a textMesh
        /// </summary>
        /// <param name="textMeshPro"></param>
        /// <param name="alpha"></param>
        public static void ChangeAlpha(this TextMeshPro textMeshPro, float alpha)
        {
            var color = textMeshPro.color;
            color.a = alpha;
            textMeshPro.color = color;
        }

        /// <summary>
        /// Changes alpha of a UI text
        /// </summary>
        /// <param name="textMeshProUGUI"></param>
        /// <param name="alpha"></param>
        public static void ChangeAlpha(this TextMeshProUGUI textMeshProUGUI, float alpha)
        {
            var color = textMeshProUGUI.color;
            color.a = alpha;
            textMeshProUGUI.color = color;
        }
    }
}