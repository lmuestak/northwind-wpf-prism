using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Northwind.Modules.Controls
{
    /// <summary>
    /// Interaction logic for SimpleInfoControl.xaml
    /// </summary>
    public partial class SimpleInfoControl : UserControl
    {

        // Dependency Properties
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(PackIconKind), typeof(SimpleInfoControl),
            new PropertyMetadata(PackIconKind.Home));


        public static readonly DependencyProperty SubIconProperty =
DependencyProperty.Register(nameof(SubIcon), typeof(PackIconKind), typeof(SimpleInfoControl),
new PropertyMetadata(PackIconKind.Info));


        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(SimpleInfoControl),
        new PropertyMetadata(string.Empty));


        public static readonly DependencyProperty SubTitleProperty =
        DependencyProperty.Register(nameof(SubTitle), typeof(string), typeof(SimpleInfoControl),
        new PropertyMetadata(string.Empty));


        public static readonly DependencyProperty InfoTextProperty =
        DependencyProperty.Register(nameof(InfoText), typeof(string), typeof(SimpleInfoControl),
        new PropertyMetadata(string.Empty));


        public static readonly DependencyProperty SubInfoTextProperty =
        DependencyProperty.Register(nameof(SubInfoText), typeof(string), typeof(SimpleInfoControl),
        new PropertyMetadata(string.Empty));


        public static readonly DependencyProperty IconBackgroundProperty =
        DependencyProperty.Register(nameof(IconBackground), typeof(Brush), typeof(SimpleInfoControl),
        new PropertyMetadata(Brushes.Transparent));


        public static readonly DependencyProperty IconForegroundProperty =
        DependencyProperty.Register(nameof(IconForeground), typeof(Brush), typeof(SimpleInfoControl),
        new PropertyMetadata(Brushes.Transparent));


        public static readonly DependencyProperty SubIconForegroundProperty =
        DependencyProperty.Register(nameof(SubIconForeground), typeof(Brush), typeof(SimpleInfoControl),
        new PropertyMetadata(Brushes.Transparent));


        // CLR wrappers
        [Category("Appearance"), Description("Main icon kind.")]
        public PackIconKind Icon { get => (PackIconKind)GetValue(IconProperty); set => SetValue(IconProperty, value); }


        [Category("Appearance"), Description("Secondary icon kind.")]
        public PackIconKind SubIcon { get => (PackIconKind)GetValue(SubIconProperty); set => SetValue(SubIconProperty, value); }


        [Category("Content"), Description("Primary title text.")]
        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }


        [Category("Content"), Description("Secondary title text.")]
        public string SubTitle { get => (string)GetValue(SubTitleProperty); set => SetValue(SubTitleProperty, value); }


        [Category("Content"), Description("Primary info text.")]
        public string InfoText { get => (string)GetValue(InfoTextProperty); set => SetValue(InfoTextProperty, value); }


        [Category("Content"), Description("Secondary info text.")]
        public string SubInfoText { get => (string)GetValue(SubInfoTextProperty); set => SetValue(SubInfoTextProperty, value); }


        [Category("Brushes"), Description("Background brush for the icon container.")]
        public Brush IconBackground { get => (Brush)GetValue(IconBackgroundProperty); set => SetValue(IconBackgroundProperty, value); }


        [Category("Brushes"), Description("Foreground brush for the main icon.")]
        public Brush IconForeground { get => (Brush)GetValue(IconForegroundProperty); set => SetValue(IconForegroundProperty, value); }


        [Category("Brushes"), Description("Foreground brush for the secondary icon.")]
        public Brush SubIconForeground { get => (Brush)GetValue(SubIconForegroundProperty); set => SetValue(SubIconForegroundProperty, value); }

        public SimpleInfoControl()
        {
            InitializeComponent();
        }
    }
}
