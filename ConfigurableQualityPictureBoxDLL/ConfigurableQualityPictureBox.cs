using System.Windows.Forms;

namespace ConfigurableQualityPictureBoxDLL
{
    public class ConfigurableQualityPictureBox : PictureBox
    {
        public System.Drawing.Drawing2D.SmoothingMode? SmoothingMode { get; set; }

        public System.Drawing.Drawing2D.CompositingQuality? CompositingQuality { get; set; }

        public System.Drawing.Drawing2D.InterpolationMode? InterpolationMode { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (SmoothingMode.HasValue)
            {
                pe.Graphics.SmoothingMode = SmoothingMode.Value;
            }

            if (CompositingQuality.HasValue)
            {
                pe.Graphics.CompositingQuality = CompositingQuality.Value;
            }

            if (InterpolationMode.HasValue)
            {
                pe.Graphics.InterpolationMode = InterpolationMode.Value;
            }

            // this line is needed for .net to draw the contents.
            base.OnPaint(pe);
        }
    }
}
