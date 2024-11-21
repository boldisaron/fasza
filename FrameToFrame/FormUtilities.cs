using System;
using System.Windows.Forms;
using System.Drawing;

public class FormUtilities // Az osztály nem statikus
{
    // Statikus metódus, amely a megadott formot középre igazítja
    public static void CenterForm(Form form)
    {
        if (form == null)
        {
            throw new ArgumentNullException(nameof(form), "A form paraméter nem lehet null.");
        }

        // A képernyő méretének lekérdezése
        Rectangle screenBounds = Screen.FromControl(form).Bounds;

        // A form pozíciójának kiszámítása a képernyő közepére
        int x = (screenBounds.Width - form.Width) / 2;
        int y = (screenBounds.Height - form.Height) / 2;

        // Form pozíciójának beállítása
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(x, y);
    }
}
