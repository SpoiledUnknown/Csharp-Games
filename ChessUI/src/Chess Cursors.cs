using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChessUI.src
{
    public static class ChessCursors
    {
        public static readonly Cursor WhiteCursor = LoadCursor("Assets/CursorW.cur");
        public static readonly Cursor BlackCursor = LoadCursor("Assets/CursorB.cur");

        private static Cursor LoadCursor(string path)
        {
            Stream stream = Application.GetResourceStream(new Uri(path, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
