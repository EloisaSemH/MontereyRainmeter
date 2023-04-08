using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using uWidgets.Models;
using uWidgets.Utilities;

namespace uWidgets.Widgets;

public partial class Calendar : Widget
{
    public Calendar(WidgetLayout layout, Settings settings, Locale locale) 
        : base(layout, settings,locale)
    {
        InitializeComponent();
        FillMonthCalendar(settings);
        Show();
    }

    private void FillMonthCalendar(Settings settings)
    {
        
        var now = DateTime.Now.Date;
        var daysCount = DateTime.DaysInMonth(now.Year, now.Month);
        var font = (FontFamily)Application.Current.Resources["SfProMedium"];
        var row = 1;

        MonthCalendar.RowDefinitions.Add(new RowDefinition());
        MonthName.Text = now.ToString("MMMM", new CultureInfo(settings.Region.Language)).ToUpper();
        
        MonthCalendar.RowDefinitions.Add(new RowDefinition());

        for (var weekDay = 0; weekDay < 7; weekDay++)
        {
            MonthCalendar.With(new Viewbox
            {
                Child = new TextBlock
                {
                    Text = weekDay.ToString(),
                    FontFamily = font,
                    Opacity = weekDay < 5 ? 1 : 0.5
                }
            }, weekDay, 1);
        }

        for (var day = 1; day <= daysCount; day++)
        {
            var column = ((int)new DateTime(now.Year, now.Month, day).DayOfWeek + 6) % 7;

            if (column == 0 || day == 1)
            {
                MonthCalendar.RowDefinitions.Add(new RowDefinition());
                row++;
            }

            MonthCalendar.With(new Viewbox
            {
                Child = new TextBlock
                {
                    Text = day.ToString(),
                    FontFamily = font,
                    Opacity = column < 5 ? 1 : 0.5,
                }
            }, column, row);
        }
    }
}