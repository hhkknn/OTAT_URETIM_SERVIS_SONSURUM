using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace UVTService.Helper
{
    public class parseNumber
    {
        private static CultureInfo _parsCult;
        public static T parservalues<T>(string val) where T : struct
        {

            object returnvals = 0;
            try
            {
                if (val.StartsWith("0"))
                {
                    val = val.Replace(".", ",");
                }

                bool negatif = val.StartsWith("-") ? true : false;
                if (string.IsNullOrEmpty(val)) return (T)Convert.ChangeType(returnvals, typeof(T));

                val = Regex.Replace(val, @"[^0-9.,]+", "").Trim();

                CultureInfo parser = parsCult;
                if (new Regex("^(?!0|\\.00)[0-9]+(,\\d{3})*([.][0-9]{0,9})$").IsMatch(val))
                {
                    parser = System.Globalization.CultureInfo.InvariantCulture;
                }

                if (typeof(T) == typeof(decimal))
                {
                    if (!negatif)
                    {
                        returnvals = decimal.Parse(val, parser);
                    }
                    else
                    {
                        returnvals = decimal.Parse(val, parser) * -1;
                    }

                }
                else if (typeof(T) == typeof(double))
                {
                    if (!negatif)
                    {
                        returnvals = double.Parse(val, parser);
                    }
                    else
                    {
                        returnvals = double.Parse(val, parser) * -1;
                    }
                }

                else if (typeof(T) == typeof(float))
                {
                    if (!negatif)
                    {
                        returnvals = float.Parse(val, parser);
                    }
                    else
                    {
                        returnvals = float.Parse(val, parser) * -1;
                    }
                }
                else if (typeof(T) == typeof(int))
                {
                    if (!negatif)
                    {
                        returnvals = (int)decimal.Parse(val, parser);
                    }
                    else
                    {
                        returnvals = (int)decimal.Parse(val, parser) * -1;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return (T)returnvals;

        }

        private static CultureInfo parsCult
        {
            get
            {
                if (_parsCult == null)
                {
                    CultureInfo ci = CultureInfo.InvariantCulture;
                    _parsCult = (CultureInfo)ci.Clone();

                    _parsCult.NumberFormat.CurrencyDecimalSeparator = ",";
                    _parsCult.NumberFormat.NumberDecimalSeparator = ",";
                    _parsCult.NumberFormat.PercentDecimalSeparator = ",";
                    _parsCult.NumberFormat.CurrencyGroupSeparator = ".";
                    _parsCult.NumberFormat.NumberGroupSeparator = ".";
                    _parsCult.NumberFormat.PercentGroupSeparator = ".";
                }

                return _parsCult;

            }
        }
    }
}