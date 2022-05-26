using AlohaKit.Controls.ProgressBar;

namespace AlohaKit.Controls
{
    public class HorizontalProgressBar : BaseProgressBar
    {
        public HorizontalProgressBar()
        {
            HeightRequest = 18;
            WidthRequest = 120;

            Drawable = ProgressBarDrawable = new HorizontalProgressBarDrawable();
        }

    }
}