using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FormotsGUI.UserControls
{
    /// <summary>
    ///     Logique d'interaction pour SearchListPanel.xaml
    /// </summary>
    public partial class SearchListPanel : UserControl
    {
        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register("EditCommand", typeof(ICommand),
                typeof(SearchListPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty AddCommandProperty =
            DependencyProperty.Register("AddCommand", typeof(ICommand),
                typeof(SearchListPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(ICommand),
                typeof(SearchListPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty LabelNameProperty =
            DependencyProperty.Register("LabelName", typeof(string),
                typeof(SearchListPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register("LabelVisibility", typeof(bool),
                typeof(SearchListPanel), new UIPropertyMetadata(null));

        public static readonly DependencyProperty AddButtonVisibilityProperty =
            DependencyProperty.Register("AddButtonVisibility", typeof(Visibility),
                typeof(SearchListPanel), new UIPropertyMetadata(null));

        public SearchListPanel()
        {
            InitializeComponent();
        }

        public ICommand EditCommand
        {
            get => (ICommand) GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public ICommand AddCommand
        {
            get => (ICommand) GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public ICommand DeleteCommand
        {
            get => (ICommand) GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public string LabelName
        {
            get => (string) GetValue(LabelNameProperty);
            set => SetValue(LabelNameProperty, value);
        }

        public bool LabelVisibility
        {
            get => (bool)GetValue(LabelVisibilityProperty);
            set => SetValue(LabelVisibilityProperty, value);
        }

        public Visibility AddButtonVisibility
        {
            get => (Visibility)GetValue(AddButtonVisibilityProperty);
            set => SetValue(AddButtonVisibilityProperty, value);
        }
    }
}