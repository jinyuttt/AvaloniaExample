using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using Avalonia.Animation;

namespace GaugeAvalonia
{
    public partial class ArcGaugea : UserControl
    {
        public class ArcGauge : Control
        {
            static ArcGauge()
            { 

            }
            //  DefaultStyleKeyProperty.OverrideMetadata(typeof(ArcGauge), new FrameworkPropertyMetadata(typeof(ArcGauge)));


            public ArcGaugea()
            {
                InitializeComponent();
                //Width = 200;
                //Height = 200;
                //SetCurrentValue(ValueProperty, 0d);
                //SetCurrentValue(MinValueProperty, 0d);
                //SetCurrentValue(MaxValueProperty, 100d);
                //  protected override Type StyleKeyOverride => typeof(ArcGauge);
            }

            private void InitTick()
            {
                // 画大刻度
                for (int i = 0; i < 9; i++)
                {
                    Line line = new Line();
                    //line.X1 = 0;
                    //line.Y1 = 0;
                    //line.X2 = 0;
                    //line.Y2 = 12;
                    line.Stroke = Brushes.White;
                    line.StrokeThickness = 2;
                    //  line.HorizontalAlignment = HorizontalAlignment.Center;
                    //  line.RenderTransformOrigin = new Point(0.5, 0.5);
                    line.RenderTransform = new RotateTransform() { Angle = -140 + i * 35 };
                    //  bdGrid.Children.Add(line);
                    DrawText();
                }            // 画小刻度
                for (int i = 0; i < 8; i++)
                {
                    var start = -140 + 35 * i + 3.5;
                    for (int j = 0; j < 9; j++)
                    {
                        Line line = new Line();
                        //line.X1 = 0;
                        //line.Y1 = 0;
                        //line.X2 = 0;
                        //line.Y2 = 6;
                        line.Stroke = Brushes.White;
                        line.StrokeThickness = 1;
                        //line.HorizontalAlignment = HorizontalAlignment.Center;
                        //line.RenderTransformOrigin = new Point(0.5, 0.5);
                        line.RenderTransform = new RotateTransform() { Angle = start + j * 3.5 };
                        //  bdGrid.Children.Add(line);
                    }
                }
            }
            List<TextBlock> textLabels = new List<TextBlock>();
            private void DrawText()
            {
                foreach (var item in textLabels)
                {
                    //  bdGrid.Children.Remove(item);
                }
                textLabels.Clear();
                var per = MaxValue / 8;
                for (int i = 0; i < 9; i++)
                {
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = $"{MinValue + (per * i)}";
                    //textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    //textBlock.RenderTransformOrigin = new Point(0.5, 0.5);
                    textBlock.RenderTransform = new RotateTransform() { Angle = -140 + i * 35 };
                    textBlock.Margin = new Thickness(12);
                    textBlock.Foreground = Brushes.White;
                    bdGrid.Children.Add(textBlock);
                    textLabels.Add(textBlock);
                }
            }
          
        

        
            RotateTransform rotateTransform;
            Grid bdGrid;
            public override void ApplyTemplate()
            {
                base.ApplyTemplate();

                //rotateTransform = GetTemplateChild("PointRotate") as RotateTransform;
                //bdGrid = GetTemplateChild("bdGrid") as Grid;

             //   rotateTransform = this.FindControl<RotateTransform>("PointRotate");
                Refresh();
                InitTick();
            }
            

            [Category("值设定")]
            public double Value
            {
                get { return (double)GetValue(ValueProperty); }
                set { SetValue(ValueProperty, value); }
            }
            public static readonly StyledProperty<double> ValueProperty =
                AvaloniaProperty.Register< ArcGauge,double>(nameof(Value),  coerce:OnValueChanged);

            private static double OnValueChanged(AvaloniaObject @object, double arg2)
            {
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
            private void Refresh()
            {
                if (rotateTransform == null)
                    return;
                // DoubleAnimation da = new DoubleAnimation();
                Animation da = new Animation();
                da.Duration = TimeSpan.FromMilliseconds(350);
                KeyFrame keyFrame = new KeyFrame();
               
                keyFrame.PropertyChanged += (s, e) =>
                {
                    if (Value > MaxValue)
                    {
                        rotateTransform.Angle = 140;
                      
                    }
                    else if (Value < MinValue)
                    {
                        rotateTransform.Angle = -140;
                    
                    }
                    else
                    {
                        var range = MaxValue - MinValue;
                        var process = Value / range;
                        var tAngle = process * 280 - 140;
                        rotateTransform.Angle = tAngle;
                       
                    }
                };
             
               // rotateTransform.(RotateTransform.AngleProperty, da);
            }
        }
       
    }
}
