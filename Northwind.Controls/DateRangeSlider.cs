using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Northwind.Controls
{
    [TemplatePart(Name = PartTrack, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PartLeftThumb, Type = typeof(Thumb))]
    [TemplatePart(Name = PartRightThumb, Type = typeof(Thumb))]
    [TemplatePart(Name = PartSelectedRange, Type = typeof(Border))]
    [TemplatePart(Name = PartTicks, Type = typeof(Canvas))]
    public class DateRangeSlider : Control
    {

        private const string PartTrack = "PART_Track";
        private const string PartLeftThumb = "PART_LeftThumb";
        private const string PartRightThumb = "PART_RightThumb";
        private const string PartSelectedRange = "PART_SelectedRange";
        private const string PartTicks = "PART_Ticks";

        private FrameworkElement _track;
        private Thumb _leftThumb;
        private Thumb _rightThumb;
        private Border _selectedRange;
        private Canvas _ticks;
        private ToolTip _hoverTip;

        static DateRangeSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DateRangeSlider), new FrameworkPropertyMetadata(typeof(DateRangeSlider)));
        }

        public DateRangeSlider()
        {
            SizeChanged += (_, __) => { UpdateThumbsFromValues(); UpdateTicks(); };
        }

        #region Dependency Properties

        public bool ShowMinMaxPickers { get => (bool)GetValue(ShowMinMaxPickersProperty); set => SetValue(ShowMinMaxPickersProperty, value); }
        public bool ShowMinMaxPickerLabels { get => (bool)GetValue(ShowMinMaxPickerLabelsProperty); set => SetValue(ShowMinMaxPickerLabelsProperty, value); }
        public double MinPickerWidth { get => (double)GetValue(MinPickerWidthProperty); set => SetValue(MinPickerWidthProperty, value); }
        public double MaxPickerWidth { get => (double)GetValue(MaxPickerWidthProperty); set => SetValue(MaxPickerWidthProperty, value); }
        public string MinPickerWatermark { get => (string)GetValue(MinPickerWatermarkProperty); set => SetValue(MinPickerWatermarkProperty, value); }
        public string MaxPickerWatermark { get => (string)GetValue(MaxPickerWatermarkProperty); set => SetValue(MaxPickerWatermarkProperty, value); }

        public static readonly DependencyProperty ShowMinMaxPickersProperty = DependencyProperty.Register(nameof(ShowMinMaxPickers), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty MinPickerWatermarkProperty = DependencyProperty.Register(nameof(MinPickerWatermark), typeof(string), typeof(DateRangeSlider), new FrameworkPropertyMetadata("Min date"));
        public static readonly DependencyProperty MaxPickerWatermarkProperty = DependencyProperty.Register(nameof(MaxPickerWatermark), typeof(string), typeof(DateRangeSlider), new FrameworkPropertyMetadata("Max date"));
        public static readonly DependencyProperty MinPickerWidthProperty = DependencyProperty.Register(nameof(MinPickerWidth), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(160.0));
        public static readonly DependencyProperty MaxPickerWidthProperty = DependencyProperty.Register(nameof(MaxPickerWidth), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(160.0));
        public static readonly DependencyProperty ShowMinMaxPickerLabelsProperty = DependencyProperty.Register(nameof(ShowMinMaxPickerLabels), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public bool ShowSelectionPickers { get => (bool)GetValue(ShowSelectionPickersProperty); set => SetValue(ShowSelectionPickersProperty, value); }
        public bool ShowSelectionPickerLabels { get => (bool)GetValue(ShowSelectionPickerLabelsProperty); set => SetValue(ShowSelectionPickerLabelsProperty, value); }
        public string StartDatePickerWatermark { get => (string)GetValue(StartDatePickerWatermarkProperty); set => SetValue(StartDatePickerWatermarkProperty, value); }
        public string EndDatePickerWatermark { get => (string)GetValue(EndDatePickerWatermarkProperty); set => SetValue(EndDatePickerWatermarkProperty, value); }
        public double StartDatePickerWidth { get => (double)GetValue(StartDatePickerWidthProperty); set => SetValue(StartDatePickerWidthProperty, value); }
        public double EndDatePickerWidth { get => (double)GetValue(EndDatePickerWidthProperty); set => SetValue(EndDatePickerWidthProperty, value); }

        public static readonly DependencyProperty ShowSelectionPickersProperty = DependencyProperty.Register(nameof(ShowSelectionPickers), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty ShowSelectionPickerLabelsProperty = DependencyProperty.Register(nameof(ShowSelectionPickerLabels), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));
        public static readonly DependencyProperty StartDatePickerWatermarkProperty = DependencyProperty.Register(nameof(StartDatePickerWatermark), typeof(string), typeof(DateRangeSlider), new FrameworkPropertyMetadata("Start date"));
        public static readonly DependencyProperty EndDatePickerWatermarkProperty = DependencyProperty.Register(nameof(EndDatePickerWatermark), typeof(string), typeof(DateRangeSlider), new FrameworkPropertyMetadata("End date"));
        public static readonly DependencyProperty StartDatePickerWidthProperty = DependencyProperty.Register(nameof(StartDatePickerWidth), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(160.0));
        public static readonly DependencyProperty EndDatePickerWidthProperty = DependencyProperty.Register(nameof(EndDatePickerWidth), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(160.0));

        public DateRangeUnit SlideMode { get => (DateRangeUnit)GetValue(SlideModeProperty); set => SetValue(SlideModeProperty, value); }
        public bool ShowModeLabel { get => (bool)GetValue(ShowModeLabelProperty); set => SetValue(ShowModeLabelProperty, value); }
        public bool ShowModeSelector { get => (bool)GetValue(ShowModeSelectorProperty); set => SetValue(ShowModeSelectorProperty, value); }
        public string ModeWatermark { get => (string)GetValue(ModeWatermarkProperty); set => SetValue(ModeWatermarkProperty, value); }
        public DateTime MinimumDate { get => (DateTime)GetValue(MinimumDateProperty); set => SetValue(MinimumDateProperty, value); }
        public DateTime MaximumDate { get => (DateTime)GetValue(MaximumDateProperty); set => SetValue(MaximumDateProperty, value); }
        public DateTime StartDate { get => (DateTime)GetValue(StartDateProperty); set => SetValue(StartDateProperty, value); }
        public DateTime EndDate { get => (DateTime)GetValue(EndDateProperty); set => SetValue(EndDateProperty, value); }
        public double ThumbWidth { get => (double)GetValue(ThumbWidthProperty); set => SetValue(ThumbWidthProperty, value); }
        public Thickness TrackPadding { get => (Thickness)GetValue(TrackPaddingProperty); set => SetValue(TrackPaddingProperty, value); }

        public static readonly DependencyProperty ShowModeLabelProperty = DependencyProperty.Register(nameof(ShowModeLabel), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty ShowModeSelectorProperty = DependencyProperty.Register(nameof(ShowModeSelector), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty ModeWatermarkProperty = DependencyProperty.Register(nameof(ModeWatermark), typeof(string), typeof(DateRangeSlider), new FrameworkPropertyMetadata("Mode:"));
        public static readonly DependencyProperty SlideModeProperty = DependencyProperty.Register(nameof(SlideMode), typeof(DateRangeUnit), typeof(DateRangeSlider), new FrameworkPropertyMetadata(DateRangeUnit.Day, OnSlideModeChanged));
        public static readonly DependencyProperty MinimumDateProperty = DependencyProperty.Register(nameof(MinimumDate), typeof(DateTime), typeof(DateRangeSlider), new FrameworkPropertyMetadata(DateTime.Today.AddMonths(-1), OnRangeBoundsChanged, CoerceMinimum));
        public static readonly DependencyProperty MaximumDateProperty = DependencyProperty.Register(nameof(MaximumDate), typeof(DateTime), typeof(DateRangeSlider), new FrameworkPropertyMetadata(DateTime.Today, OnRangeBoundsChanged, CoerceMaximum));
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register(nameof(StartDate), typeof(DateTime), typeof(DateRangeSlider), new FrameworkPropertyMetadata(DateTime.Today.AddDays(-7), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnStartChanged, CoerceStart));
        public static readonly DependencyProperty EndDateProperty = DependencyProperty.Register(nameof(EndDate), typeof(DateTime), typeof(DateRangeSlider), new FrameworkPropertyMetadata(DateTime.Today, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnEndChanged, CoerceEnd));
        public static readonly DependencyProperty ThumbWidthProperty = DependencyProperty.Register(nameof(ThumbWidth), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(8.0));
        public static readonly DependencyProperty TrackPaddingProperty = DependencyProperty.Register(nameof(TrackPadding), typeof(Thickness), typeof(DateRangeSlider), new FrameworkPropertyMetadata(new Thickness(8, 0, 8, 0)));

        // Ticks & labels
        public bool ShowTicks { get => (bool)GetValue(ShowTicksProperty); set => SetValue(ShowTicksProperty, value); }
        public int TickStep { get => (int)GetValue(TickStepProperty); set => SetValue(TickStepProperty, value); }
        private static object CoerceTickStep(DependencyObject d, object v) => Math.Max(1, (int)v);
        public bool AutoTickDensity { get => (bool)GetValue(AutoTickDensityProperty); set => SetValue(AutoTickDensityProperty, value); }
        public double MinLabelPixelSpacing { get => (double)GetValue(MinLabelPixelSpacingProperty); set => SetValue(MinLabelPixelSpacingProperty, value); }
        public double TickHeight { get => (double)GetValue(TickHeightProperty); set => SetValue(TickHeightProperty, value); }
        public Style TickLabelStyle { get => (Style)GetValue(TickLabelStyleProperty); set => SetValue(TickLabelStyleProperty, value); }
        public string TickLabelFormat { get => (string)GetValue(TickLabelFormatProperty); set => SetValue(TickLabelFormatProperty, value); }
        public bool ShowGridlines { get => (bool)GetValue(ShowGridlinesProperty); set => SetValue(ShowGridlinesProperty, value); }
        public Brush GridlineBrush { get => (Brush)GetValue(GridlineBrushProperty); set => SetValue(GridlineBrushProperty, value); }

        public static readonly DependencyProperty ShowTicksProperty = DependencyProperty.Register(nameof(ShowTicks), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty TickStepProperty = DependencyProperty.Register(nameof(TickStep), typeof(int), typeof(DateRangeSlider), new FrameworkPropertyMetadata(1, (d, e) => ((DateRangeSlider)d).UpdateTicks(), CoerceTickStep));
        public static readonly DependencyProperty AutoTickDensityProperty = DependencyProperty.Register(nameof(AutoTickDensity), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(true, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty MinLabelPixelSpacingProperty = DependencyProperty.Register(nameof(MinLabelPixelSpacing), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(64.0, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty TickHeightProperty = DependencyProperty.Register(nameof(TickHeight), typeof(double), typeof(DateRangeSlider), new FrameworkPropertyMetadata(8.0, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty TickLabelStyleProperty = DependencyProperty.Register(nameof(TickLabelStyle), typeof(Style), typeof(DateRangeSlider), new FrameworkPropertyMetadata(null, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty TickLabelFormatProperty = DependencyProperty.Register(nameof(TickLabelFormat), typeof(string), typeof(DateRangeSlider), new FrameworkPropertyMetadata(null, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty ShowGridlinesProperty = DependencyProperty.Register(nameof(ShowGridlines), typeof(bool), typeof(DateRangeSlider), new FrameworkPropertyMetadata(false, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        public static readonly DependencyProperty GridlineBrushProperty = DependencyProperty.Register(nameof(GridlineBrush), typeof(Brush), typeof(DateRangeSlider), new FrameworkPropertyMetadata(Brushes.LightGray, (d, e) => ((DateRangeSlider)d).UpdateTicks()));
        
        #endregion

        #region Events

        public static readonly RoutedEvent RangeChangedEvent = EventManager.RegisterRoutedEvent(nameof(RangeChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DateRangeSlider));
        public event RoutedEventHandler RangeChanged { add => AddHandler(RangeChangedEvent, value); remove => RemoveHandler(RangeChangedEvent, value); }
        private void RaiseRangeChanged() => RaiseEvent(new RoutedEventArgs(RangeChangedEvent));

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_leftThumb != null)
            {
                _leftThumb.DragDelta -= LeftThumbOnDragDelta;
                _leftThumb.DragCompleted -= ThumbOnDragCompleted;
            }
            if (_rightThumb != null)
            {
                _rightThumb.DragDelta -= RightThumbOnDragDelta;
                _rightThumb.DragCompleted -= ThumbOnDragCompleted;
            }
            if (_track != null)
            {
                _track.MouseMove -= TrackOnMouseMove;
                _track.MouseEnter -= TrackOnMouseEnter;
                _track.MouseLeave -= TrackOnMouseLeave;
            }


            _track = GetTemplateChild(PartTrack) as FrameworkElement;
            _leftThumb = GetTemplateChild(PartLeftThumb) as Thumb;
            _rightThumb = GetTemplateChild(PartRightThumb) as Thumb;
            _selectedRange = GetTemplateChild(PartSelectedRange) as Border;
            _ticks = GetTemplateChild(PartTicks) as Canvas;


            if (_leftThumb != null)
            {
                _leftThumb.DragDelta += LeftThumbOnDragDelta;
                _leftThumb.DragCompleted += ThumbOnDragCompleted;
            }
            if (_rightThumb != null)
            {
                _rightThumb.DragDelta += RightThumbOnDragDelta;
                _rightThumb.DragCompleted += ThumbOnDragCompleted;
            }
            if (_track != null)
            {
                _track.MouseMove += TrackOnMouseMove;
                _track.MouseEnter += TrackOnMouseEnter;
                _track.MouseLeave += TrackOnMouseLeave;
            }


            _hoverTip = new ToolTip { Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse }; // follows cursor
            ToolTipService.SetToolTip(_track, _hoverTip);


            UpdateThumbsFromValues();
            UpdateTicks();
        }

        private static void OnRangeBoundsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (DateRangeSlider)d;
            c.CoerceValue(StartDateProperty);
            c.CoerceValue(EndDateProperty);
            c.UpdateThumbsFromValues();
            c.UpdateTicks();
        }

        private static object CoerceMinimum(DependencyObject d, object baseValue)
        {
            var c = (DateRangeSlider)d;
            var min = (DateTime)baseValue;
            return min > c.MaximumDate ? c.MaximumDate : min;
        }

        private static object CoerceMaximum(DependencyObject d, object baseValue)
        {
            var c = (DateRangeSlider)d;
            var max = (DateTime)baseValue;
            return max < c.MinimumDate ? c.MinimumDate : max;
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (DateRangeSlider)d;
            c.CoerceValue(EndDateProperty);
            c.UpdateThumbsFromValues();
            c.RaiseRangeChanged();
        }

        private static object CoerceStart(DependencyObject d, object baseValue)
        {
            var c = (DateRangeSlider)d;
            var v = (DateTime)baseValue;
            if (v < c.MinimumDate) v = c.MinimumDate;
            if (v > c.EndDate) v = c.EndDate;
            return c.RoundToUnit(v);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (DateRangeSlider)d;
            c.CoerceValue(StartDateProperty);
            c.UpdateThumbsFromValues();
            c.RaiseRangeChanged();
        }

        private static object CoerceEnd(DependencyObject d, object baseValue)
        {
            var c = (DateRangeSlider)d;
            var v = (DateTime)baseValue;
            if (v > c.MaximumDate) v = c.MaximumDate;
            if (v < c.StartDate) v = c.StartDate;
            return c.RoundToUnit(v);
        }

        private static void OnSlideModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var c = (DateRangeSlider)d;
            c.MinimumDate = c.RoundToUnit(c.MinimumDate);
            c.MaximumDate = c.RoundToUnit(c.MaximumDate);
            c.StartDate = c.RoundToUnit(c.StartDate);
            c.EndDate = c.RoundToUnit(c.EndDate);
            c.UpdateThumbsFromValues();
            c.UpdateTicks();
        }

        // === Drag logic (snaps to current unit at end) ===
        private void LeftThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (_track == null) return;
            double newX = DateToX(StartDate) + e.HorizontalChange;
            double rightX = DateToX(EndDate);
            newX = Math.Max(0, Math.Min(newX, rightX));
            StartDate = XToDate(newX);
        }

        private void RightThumbOnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (_track == null) return;
            double newX = DateToX(EndDate) + e.HorizontalChange;
            double leftX = DateToX(StartDate);
            newX = Math.Max(leftX, Math.Min(newX, UsableWidth));
            EndDate = XToDate(newX);
        }

        private void ThumbOnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            StartDate = RoundToUnit(StartDate);
            EndDate = RoundToUnit(EndDate);
        }

        // === Hover tooltip ===
        private void TrackOnMouseEnter(object sender, MouseEventArgs e) => UpdateHoverTip(e);

        private void TrackOnMouseMove(object sender, MouseEventArgs e) => UpdateHoverTip(e);

        private void TrackOnMouseLeave(object sender, MouseEventArgs e) { if (_hoverTip != null) _hoverTip.IsOpen = false; }

        private void UpdateHoverTip(MouseEventArgs e)
        {
            if (_track == null || _hoverTip == null) return;
            var p = e.GetPosition(_track);
            var dt = XToDate(p.X - TrackPadding.Left);
            _hoverTip.Content = FormatTickLabel(dt);
            _hoverTip.IsOpen = true;
        }

        // === Geometry helpers ===
        private double UsableWidth => _track == null ? 0 : Math.Max(0, _track.ActualWidth - TrackPadding.Left - TrackPadding.Right);

        private double DateToX(DateTime date)
        {
            int total = Math.Max(UnitsBetween(MinimumDate, MaximumDate), 0);
            if (total == 0) return 0;
            int index = IndexFromMin(date);
            double ratio = Math.Max(0, Math.Min(1, (double)index / total));
            return ratio * UsableWidth;
        }

        private DateTime XToDate(double x)
        {
            int total = Math.Max(UnitsBetween(MinimumDate, MaximumDate), 0);
            if (total == 0) return MinimumDate;
            x = Math.Max(0, Math.Min(UsableWidth, x));
            double ratio = UsableWidth == 0 ? 0 : x / UsableWidth;
            int index = (int)Math.Round(ratio * total);
            var dt = MinPlusUnits(index);
            if (dt < MinimumDate) dt = MinimumDate; if (dt > MaximumDate) dt = MaximumDate;
            return RoundToUnit(dt);
        }

        private void UpdateThumbsFromValues()
        {
            if (_track == null || _leftThumb == null || _rightThumb == null || _selectedRange == null) return;
            double left = DateToX(StartDate) + TrackPadding.Left;
            double right = DateToX(EndDate) + TrackPadding.Left;
            if (_leftThumb.Parent is Canvas)
            {
                Canvas.SetLeft(_leftThumb, left - (_leftThumb.ActualWidth / 2));
                Canvas.SetLeft(_rightThumb, right - (_rightThumb.ActualWidth / 2));
                Canvas.SetLeft(_selectedRange, left);
                _selectedRange.Width = Math.Max(0, right - left);
            }
        }

        // === Ticks & labels ===
        private void UpdateTicks()
        {
            if (_ticks == null) return;
            _ticks.Children.Clear();
            if (!ShowTicks) return;
            double usable = UsableWidth; if (usable <= 0) return;


            int totalUnits = Math.Max(UnitsBetween(MinimumDate, MaximumDate), 0);
            if (totalUnits == 0) return;


            int step = TickStep;
            if (AutoTickDensity)
            {
                int maxLabels = (int)Math.Max(1, Math.Floor(usable / MinLabelPixelSpacing));
                step = (int)Math.Ceiling((double)totalUnits / maxLabels);
                step = Math.Max(1, step);
                // make steps pleasant per mode
                if (SlideMode == DateRangeUnit.Month && step >= 3 && step % 3 != 0) step += 3 - (step % 3); // prefer quarters
                if (SlideMode == DateRangeUnit.Year && step >= 5 && step % 5 != 0) step += 5 - (step % 5);
            }
            double total = totalUnits;
            for (int i = 0; i <= totalUnits; i += step)
            {
                double ratio = total == 0 ? 0 : i / total;
                double x = TrackPadding.Left + ratio * usable;


                // Gridline (optional)
                if (ShowGridlines)
                {
                    var gl = new Line
                    {
                        X1 = x,
                        X2 = x,
                        Y1 = 0,
                        Y2 = _ticks.ActualHeight <= 0 ? 28 : _ticks.ActualHeight + 6,
                        Stroke = GridlineBrush,
                        StrokeThickness = 1,
                        SnapsToDevicePixels = true
                    };
                    _ticks.Children.Add(gl);
                }
                // Tick line
                var line = new Line { X1 = x, X2 = x, Y1 = 0, Y2 = TickHeight, Stroke = Brushes.Gray, StrokeThickness = 1 };
                _ticks.Children.Add(line);


                // Label
                var dt = MinPlusUnits(i);
                string label = FormatTickLabel(dt);
                var tb = new TextBlock { Text = label, Margin = new Thickness(2, TickHeight, 0, 0) };
                if (TickLabelStyle != null) tb.Style = TickLabelStyle;
                _ticks.Children.Add(tb);
                Canvas.SetLeft(tb, x + 2);
                Canvas.SetTop(tb, 0);
            }
        }

        private string FormatTickLabel(DateTime dt)
        {
            if (!string.IsNullOrEmpty(TickLabelFormat)) return dt.ToString(TickLabelFormat);
            switch (SlideMode)
            {
                case DateRangeUnit.Day: return dt.ToString("dd MMM");
                case DateRangeUnit.Month: return dt.ToString("MMM yyyy");
                case DateRangeUnit.Quarter: return $"Q{((dt.Month - 1) / 3) + 1} {dt:yyyy}";
                case DateRangeUnit.Year: return dt.ToString("yyyy");
                default: return dt.ToShortDateString();
            }
        }

        // === Unit math ===
        private int UnitsBetween(DateTime a, DateTime b)
        {
            return SlideMode switch
            {
                DateRangeUnit.Day => (b.Date - a.Date).Days,
                DateRangeUnit.Month => MonthsBetween(a, b),
                DateRangeUnit.Quarter => MonthsBetween(a, b) / 3,
                DateRangeUnit.Year => b.Year - a.Year,
                _ => (b.Date - a.Date).Days,
            };
        }

        private int IndexFromMin(DateTime dt)
        {
            var a = MinimumDate; var b = dt;
            return SlideMode switch
            {
                DateRangeUnit.Day => (b.Date - a.Date).Days,
                DateRangeUnit.Month => MonthsBetween(a, b),
                DateRangeUnit.Quarter => MonthsBetween(a, b) / 3,
                DateRangeUnit.Year => b.Year - a.Year,
                _ => (b.Date - a.Date).Days,
            };
        }

        private DateTime MinPlusUnits(int units)
        {
            var min = MinimumDate;
            return SlideMode switch
            {
                DateRangeUnit.Day => min.Date.AddDays(units),
                DateRangeUnit.Month => new DateTime(min.Year, min.Month, 1).AddMonths(units),
                DateRangeUnit.Quarter => new DateTime(min.Year, min.Month, 1).AddMonths(units * 3),
                DateRangeUnit.Year => new DateTime(min.Year, 1, 1).AddYears(units),
                _ => min.Date.AddDays(units),
            };
        }

        private DateTime RoundToUnit(DateTime dt)
        {
            switch (SlideMode)
            {
                case DateRangeUnit.Day: return dt.Date;
                case DateRangeUnit.Month: return new DateTime(dt.Year, dt.Month, 1);
                case DateRangeUnit.Quarter: int q = (dt.Month - 1) / 3; return new DateTime(dt.Year, q * 3 + 1, 1);
                case DateRangeUnit.Year: return new DateTime(dt.Year, 1, 1);
                default: return dt.Date;
            }
        }

        private int MonthsBetween(DateTime a, DateTime b)
        {
            a = new DateTime(a.Year, a.Month, 1);
            b = new DateTime(b.Year, b.Month, 1);
            return (b.Year - a.Year) * 12 + (b.Month - a.Month);
        }


    }
}