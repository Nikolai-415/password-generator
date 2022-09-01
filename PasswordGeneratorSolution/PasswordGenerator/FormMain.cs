namespace PasswordGenerator {
    /// <summary>������� �����</summary>
    public partial class FormMain : Form {
        /// <summary>����� ��������� ������ ��������� ������</summary>
        private const string BUTTON_GENERATE_TEXT_ENABLED = "������������� ������ �\n����������� ��� � ����� ������";

        /// <summary>����� ����������� ������ ��������� ������</summary>
        private const string BUTTON_GENERATE_TEXT_DISABLED = "���������� ������� ���� �� ���� ������� ������������ ��������";

        /// <summary>������ ��������� � ������ ������������ ��������</summary>
        private readonly List<CheckBox> UsingSymbolsCheckboxes = new();

        /// <summary>������ ������� �����</summary>
        public FormMain() {
            this.InitializeComponent();

            // ���������� ������ ���������
            foreach (Control control in this.GroupBox_UsingSymbols.Controls) {
                if (control is CheckBox control_as_checkbox) {
                    this.UsingSymbolsCheckboxes.Add(control_as_checkbox);
                }
            }

            // ���������� �������� ��� ��������� ��������� ���������
            foreach (CheckBox checkBox in this.UsingSymbolsCheckboxes) {
                checkBox.CheckedChanged += this.UsingSymbolsCheckBox_CheckedChanged;
            }

            // ��������� ��������� ������ ���������
            this.UsingSymbolsCheckBox_CheckedChanged(null, null);
        }

        /// <summary>������� ��������� ��������� �������� � ������ ������������ ��������</summary>
        private void UsingSymbolsCheckBox_CheckedChanged(object? sender, EventArgs? e) {
            // ������ ��������� ������ ����� ����������, ���� �� ������� �� ���� �� ������� ������������ ��������
            bool is_button_enabled = false;
            foreach (CheckBox checkBox in this.UsingSymbolsCheckboxes) {
                if (checkBox.Checked) {
                    is_button_enabled = true;
                }
            }

            // ������ ����������� ������, � ����� � �����
            this.Button_Generate.Enabled = is_button_enabled;
            this.Button_Generate.Text = this.Button_Generate.Enabled ? BUTTON_GENERATE_TEXT_ENABLED : BUTTON_GENERATE_TEXT_DISABLED;
        }

        /// <summary>������� ������� �� ������ ��������� ������ ������</summary>
        private void Button_Generate_Click(object sender, EventArgs e) {
            // ������ ��������, ������� ����� ���� � ������
            string alphabet = "";
            foreach (CheckBox checkBox in this.UsingSymbolsCheckboxes) {
                if (checkBox.Checked) {
                    alphabet += checkBox.Text;
                }
            }

            // ����� ������������� ������
            decimal password_length = this.NumericUpDown_PasswordLength.Value;

            // ��������� ������
            string password = "";
            for (int i = 0; i < password_length; i++) {
                int symbol_id = Random.Shared.Next(0, alphabet.Length);
                password += alphabet[symbol_id];
            }

            // ��������� ����� � ����� ������
            Clipboard.SetText(password);
        }
    }
}