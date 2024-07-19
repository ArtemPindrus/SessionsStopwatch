using System.Windows.Controls;

namespace SessionsStopwatch.Utilities {
    /// <summary>
    /// Used to register a <see cref="TextBox"/> as containing only integers.
    /// </summary>
    public class OnlyIntTextBox {
        private readonly int min;
        private readonly int max;
        private readonly int maxChar;
        private string? previous;

        private OnlyIntTextBox(string? initial, int min, int max) {
            previous = initial;
            this.min = min;
            this.max = max;
            maxChar = max.ToString().Length;
        }

        /// <summary>
        /// Registeres a <see cref="TextBox"/> to contain only integers.
        /// </summary>
        /// <param name="textBox"><see cref="TextBox"/> to register.</param>
        /// <param name="min">Min <see cref="int"/> box can contain.</param>
        /// <param name="max">Max <see cref="int"/> box can contain.</param>
        public static void Register(TextBox textBox, int min = int.MinValue, int max = int.MaxValue) {
            OnlyIntTextBox register = new(textBox.Text, min, max);

            textBox.TextChanged += register.HandleTextChanged;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e) {
            if (sender is TextBox box) {
                if (string.IsNullOrEmpty(box.Text)) return;
                if (box.Text == "-0") box.Text = "0";

                if (!int.TryParse(box.Text, out int result) || result < min || result > max || box.Text.Length > maxChar) {
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
    }
}