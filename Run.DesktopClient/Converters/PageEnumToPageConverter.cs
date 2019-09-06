using System;
using System.Globalization;

namespace Run.DesktopClient
{
    public class PageEnumToPageConverter : BaseConverter<PageEnumToPageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Core.Page page)
                return new Uri($"../Pages/{page}Page.xaml", UriKind.Relative);

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
