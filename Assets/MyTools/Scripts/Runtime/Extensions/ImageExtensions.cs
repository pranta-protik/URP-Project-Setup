using UnityEngine.UI;

namespace MyTools
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Changes alpha of an image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="alpha"></param>
        public static void ChangeAlpha(this Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}