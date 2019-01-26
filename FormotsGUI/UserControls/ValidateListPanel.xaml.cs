using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FormotsGUI.UserControls
{
    /// <summary>
    ///     Logique d'interaction pour SearchListPanel.xaml
    /// </summary>
    public partial class ValidateListPanel : UserControl
    {
        public static readonly DependencyProperty GenerateCommandProperty =
            DependencyProperty.Register("GenerateCommand", typeof(ICommand),
                typeof(ValidateListPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty LabelNameProperty =
            DependencyProperty.Register("LabelName", typeof(string),
                typeof(ValidateListPanel), new UIPropertyMetadata(null));

        public ValidateListPanel()
        {
            InitializeComponent();
        }

        public ICommand GenerateCommand
        {
            get => (ICommand) GetValue(GenerateCommandProperty);
            set => SetValue(GenerateCommandProperty, value);
        }

        public string LabelName
        {
            get => (string) GetValue(LabelNameProperty);
            set => SetValue(LabelNameProperty, value);
        }
    }
}