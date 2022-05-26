using AlohaKit.Controls.ProgressBar;

namespace AlohaKit.Controls
{
    public class VerticalProgressBar : BaseProgressBar
    {
        public VerticalProgressBar()
        {
            HeightRequest = 120;
            WidthRequest = 18;

            Drawable = ProgressBarDrawable = new VerticalProgressBarDrawable();
        }
    }
}