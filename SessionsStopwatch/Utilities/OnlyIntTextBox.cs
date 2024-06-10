using System.Windows.Controls;

namespace SessionsStopwatch.Utilities
{
#warning is this a factory?
    class OnlyIntTextBox{
        private readonly int _min;
        private readonly int _max;
        private readonly int _maxChar;
        private string? previous;

        private void HandleTextChanged(object sender, TextChangedEventArgs e) {
            if (sender is TextBox box) {
                if (string.IsNullOrEmpty(box.Text)) return;
                if (box.Text == "-0") box.Text = "0";

                if (!int.TryParse(box.Text, out int result) || result < _min || result > _max || box.Text.Length > _maxChar) {
                    int tempCaretInd = box.CaretIndex - 1;
                    box.Text = previous;
                    box.CaretIndex = tempCaretInd < 0 ? 0 : tempCaretInd;
                } else {
                    int tempCaretInd = box.CaretIndex;
                    box.Text = box.Text.Trim();
                    box.CaretIndex = tempCaretInd < 0 ? 0 : tempCaretInd;
                    previous = box.Text;
                }
            }
        }

        private OnlyIntTextBox(string? initial, int min, int max) {
            previous = initial;
            _min = min;
            _max = max;
            _maxChar = max.ToString().Length;
        }

        public static void Register(TextBox textBox, int min = int.MinValue, int max = int.MaxValue) {
            OnlyIntTextBox register = new(textBox.Text, min, max);

            textBox.TextChanged += register.HandleTextChanged;
        }
    }
}
