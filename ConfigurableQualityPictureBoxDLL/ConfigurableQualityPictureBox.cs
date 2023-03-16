using System.Windows.Forms;

namespace ConfigurableQualityPictureBoxDLL
{
    public class ConfigurableQualityPictureBox : PictureBox
    {
        private System.Drawing.Drawing2D.InterpolationMode? interpolationMode;

        public System.Drawing.Drawing2D.SmoothingMode? SmoothingMode { get; set; }

        public System.Drawing.Drawing2D.CompositingQuality? CompositingQuality { get; set; }

        public System.Drawing.Drawing2D.InterpolationMode? InterpolationMode
        {
            get => interpolationMode;
            set => interpolationMode = value;
        }

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

            if (interpolationMode.HasValue)
            {
                pe.Graphics.InterpolationMode = interpolationMode.Value;
            }

            // this line is needed for .net to draw the contents.
            base.OnPaint(pe);
        }
    }
}
