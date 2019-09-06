using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Run.DesktopClient
{
    public abstract class BaseConverter<Converter> : MarkupExtension, IValueConverter
        where Converter : new()
    {
        public static Converter Instance { get; } = new Converter();

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public override object ProvideValue(IServiceProvider serviceProvider) => Instance;
    }
}
