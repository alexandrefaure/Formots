using System;
using System.Windows;
using System.Windows.Controls;

namespace FormotsGUI.UserControls
{
    /// <summary>
    ///     Logique d'interaction pour SearchListPanel.xaml
    /// </summary>
    public partial class DatesEntretiensPanel : UserControl
    {
        public DatesEntretiensPanel()
        {
            InitializeComponent();
        }

        public DateTime? DateDebutEntretien
        {
            get => (DateTime?)GetValue(DateDebutEntretienProperty);
            set => SetValue(DateDebutEntretienProperty, value);
        }

        public static readonly DependencyProperty DateDebutEntretienProperty =
            DependencyProperty.Register(nameof(DateDebutEntretien), typeof(DateTime?),
                typeof(DatesEntretiensPanel), new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public DateTime? DateFinEntretien
        {
            get => (DateTime?)GetValue(DateFinEntretienProperty);
            set => SetValue(DateFinEntretienProperty, value);
        }

        public static readonly DependencyProperty DateFinEntretienProperty =
            DependencyProperty.Register(nameof(DateFinEntretien), typeof(DateTime?),
                typeof(DatesEntretiensPanel), new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}