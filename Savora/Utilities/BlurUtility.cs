using System.Drawing;
using System.Windows.Forms;

namespace Savora.Utilities
{
    internal class BlurUtility
    {
        public static void BlurBackground(Form Model)
        {
            Form Background = new Form();

            using (Model)
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = Dashboard.Instance.Size;
                Background.Location = Dashboard.Instance.Location;
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background; // Set the main form as a child of the background form
                Model.ShowDialog(Background);
                Background.Dispose();
            }

        }

    }
}
