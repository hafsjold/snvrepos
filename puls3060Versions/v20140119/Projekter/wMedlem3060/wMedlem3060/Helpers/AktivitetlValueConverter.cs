namespace wMedlem3060
{
    using System;
    using System.Windows.Data;

    /// <summary>
    /// Two way IValueConverter that lets you bind a property on a bindable object that can be an empty string value to a dependency property that should be set to null in that case.
    /// </summary>
    public class AktivitetValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts <c>null</c> or empty strings to <c>null</c>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The expected type of the result (ignored).</param>
        /// <param name="parameter">Optional parameter (ignored).</param>
        /// <param name="culture">The culture for the conversion (ignored).</param>
        /// <returns>If the <paramref name="value"/>is <c>null</c> or empty, this method returns <c>null</c> otherwise it returns the <paramref name="value"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int Akt_id;
            try
            {
                Akt_id = (int)value;
            }
            catch (Exception)
            {
                Akt_id = 0;
            }
            string Aktivitet = null;
            switch (Akt_id)
            {
                case 10:
                    Aktivitet = "Indmeldelses dato";
                    break;
                case 20:
                    Aktivitet = "PBS opkrævnings dato";
                    break;
                case 21:
                    Aktivitet = "E-mail rykker dato";
                    break;
                case 30:
                    Aktivitet = "Kontingent betalt til";
                    break;
                case 40:
                    Aktivitet = "PBS betaling tilbageført";
                    break;
                case 50:
                    Aktivitet = "Udmeldelses dato";
                    break;
             }
            return string.IsNullOrEmpty(Aktivitet) ? null : Aktivitet;
        }

        /// <summary>
        /// Converts <c>null</c> back to <see cref="String.Empty"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The expected type of the result (ignored).</param>
        /// <param name="parameter">Optional parameter (ignored).</param>
        /// <param name="culture">The culture for the conversion (ignored).</param>
        /// <returns>If <paramref name="value"/> is <c>null</c>, it returns <see cref="String.Empty"/> otherwise <paramref name="value"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ?? string.Empty;
        }
    }
}
