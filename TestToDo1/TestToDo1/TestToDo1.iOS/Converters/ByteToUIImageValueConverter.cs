using Foundation;
using MvvmCross.Platform.Converters;
using System;
using System.Globalization;
using UIKit;

namespace TestToDo1.Core.Converters
{
    public class ByteToUIImageValueConverter : MvxValueConverter<byte[], Object>
    {
        protected override Object Convert(byte[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            NSData data = NSData.FromArray(value);
            UIImage img = UIImage.LoadFromData(data);
            return img;
        }
    }
}
