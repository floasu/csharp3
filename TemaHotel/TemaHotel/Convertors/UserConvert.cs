using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TemaHotel.ViewModel;

namespace TemaHotel.Convertors
{
    public class UserConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length == 5)
            {
                return new UserUtils()
                {
                    
                    Username = values[0].ToString(),
                    Name = values[1].ToString(),
                    Email = values[2].ToString(),
                    Password = values[3].ToString(),
                    ConfirmPassword = values[4].ToString()
                };
            }
            else if (values.Length == 2)
            {
                return new UserUtils()
                {
                    Username = values[0].ToString(),
                    Password = values[1].ToString(),
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            UserUtils pers = value as UserUtils;
            object[] result = new object[2] { pers.Password, pers.ConfirmPassword };
            return result;
        }
    }
}
