using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls.Templates;
using Avalonia.Controls.Primitives;
using System.Linq;

namespace GaugeAvalonia.Views
{
    public class ArcGauge: TemplatedControl
    {
      
        Grid bdGrid;

        static ArcGauge()
        {
           
           // DefaultStyleKeyProperty.OverrideMetadata(typeof(ArcGauge), new FrameworkPropertyMetadata(typeof(ArcGauge)));
        }
        
        public ArcGauge()
        {
            this.Loaded += ArcGauge_Loaded;
            //Width = 200;
            //Height = 200;
            SetCurrentValue(ValueProperty, 0d);
            SetCurrentValue(MinValueProperty, 0d);
            SetCurrentValue(MaxValueProperty, 100d);
        }

        private void ArcGauge_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            InitTick();
        }
        public override void Render(DrawingContext context)
        {
           
            base.Render(context);
          
            bdGrid = (Grid)this.GetTemplateChildren().Where(x => x.Name == "bdGrid").First();
            Refresh();
        

        }
        private void InitTick()
        {
            // 画大刻度
            for (int i = 0; i < 9; i++)
            {
                Line line = new Line();
                line.StartPoint = new Point(0, 0);
                line.EndPoint=new Point(0, 12);
                line.HorizontalAlignment= Avalonia.Layout.HorizontalAlignment
                    .Center;
             
                line.Stroke = Brushes.White;
                line.StrokeThickness = 2;
                line.RenderTransformOrigin = RelativePoint.Center;
              
                line.RenderTransform = new RotateTransform() { Angle = -140 + i * 35 };
                 bdGrid.Children.Add(line);
                DrawText();
            }            // 画小刻度
            for (int i = 0; i < 8; i++)
            {
                var start = -140 + 35 * i + 3.5;
                for (int j = 0; j < 9; j++)
                {
                    Line line = new Line();
                    line.StartPoint = new Point(0, 0);
                    line.EndPoint = new Point(0, 6);
                   
                    line.Stroke = Brushes.White;
                    line.StrokeThickness = 1;
                    line.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment .Center;
                    line.RenderTransformOrigin = RelativePoint.Center;
                    line.RenderTransform = new RotateTransform() { Angle = start + j * 3.5 };
                    bdGrid.Children.Add(line);
                }
            }
        }
        List<TextBlock> textLabels = new List<TextBlock>();
        private void DrawText()
        {
            foreach (var item in textLabels)
            {
                  bdGrid.Children.Remove(item);
            }
            textLabels.Clear();
            var per = MaxValue / 8;
            for (int i = 0; i < 9; i++)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"{MinValue + (per * i)}";
                textBlock.HorizontalAlignment =  Avalonia.Layout.HorizontalAlignment.Center;
                textBlock.RenderTransformOrigin = RelativePoint.Center;
                textBlock.RenderTransform = new RotateTransform() { Angle = -140 + i * 35 };
                textBlock.Margin = new Thickness(12);
                textBlock.Foreground = Brushes.White;
                bdGrid.Children.Add(textBlock);
                textLabels.Add(textBlock);
            }
        }

        [Category("值设定")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly StyledProperty<double> ValueProperty =
            AvaloniaProperty.Register<ArcGauge, double>(nameof(Value), coerce: OnValueChanged);

        private static double OnValueChanged(AvaloniaObject @object, double arg2)
        {
            ArcGauge gauge= @object  as ArcGauge;
            gauge.Refresh();
            return arg2;
        }

        [Category("值设定")]
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static readonly StyledProperty<double> MinValueProperty =
            AvaloniaProperty.Register<ArcGauge, double>(nameof(MinValue), coerce: OnValueChanged);
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly StyledProperty<double> MaxValueProperty =
           AvaloniaProperty.Register<ArcGauge, double>(nameof(MaxValue), coerce: OnValueChanged);


        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }
        public static readonly StyledProperty<double> AngleProperty =
         AvaloniaProperty.Register<ArcGauge, double>(nameof(Angle));


      
        private void Refresh()
        {

           
            if (Value > MaxValue)
            {
                Angle = 140;

            }
            else if (Value < MinValue)
            {
                Angle = -140;

            }
            else
            {
                var range = MaxValue - MinValue;
                var process = Value / range;
                var tAngle = process * 280 - 140;
                Angle = tAngle;

            }

            
        }
    }
}

