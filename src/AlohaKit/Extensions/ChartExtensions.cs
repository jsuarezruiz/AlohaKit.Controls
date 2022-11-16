using System.Globalization;

namespace AlohaKit.Extensions
{
	public static class ChartExtensions
    {
        private static readonly long _trillionMultiplicator = 1000000000000000000;
        private static readonly int _billiomMultiplicator = 1000000000;
        private static readonly int _millionMultiplicator = 1000000;
        private static readonly int _thousandMultiplicator = 1000;

        /// <summary>
        /// Compute the space between lines
        /// </summary>
        /// <param name="minValue">Minimum value in negative axis</param>
        /// <param name="maxValue">Maximum value in positive axis</param>
        /// <returns></returns>
        public static float GetStepForAxisPositiveAndNegative(this float minValue, float maxValue)
        {
            double positiveRange = (Math.Abs(maxValue) - 0);
            double negativeRange = (Math.Abs(minValue) - 0);
            double range = 0;

            if (positiveRange > negativeRange)
            {
                range = positiveRange;
            }
            else
            {
                range = negativeRange;
            }

            int tickCount = 4;
            double unroundedTickSize = range / (tickCount - 1);
            double x = Math.Ceiling(Math.Log10(unroundedTickSize) - 1);
            double pow10x = Math.Pow(10, x);
            double roundedTickRange = Math.Ceiling(unroundedTickSize / pow10x) * pow10x;
            return (float)roundedTickRange;
        }

        /// <summary>
        /// Calculate the maximum range value for horizontal or vertical lines for charts with Positive
        /// and negative axis
        /// </summary>
        /// <param name="minValue">point minimum from values</param>
        /// <param name="maxValue">point maximum from values</param>
        /// <returns>get the maximum range for the entire chart</returns>
        public static float GetRangeValuesPositiveAndNegative(this float minValue, float maxValue)
        {
            if (maxValue == 0)
            {
                return 0;
            }
            var step = minValue.GetStepForAxisPositiveAndNegative(maxValue);
            int tickCount = 8;
            return step * (tickCount - 1);
        }

        /// <summary>
        /// Compute the space between lines
        /// </summary>
        /// <param name="maxValue">point maximum from values</param>
        /// <returns>Get the soace between lines</returns>
        public static float GetStepForPositiveAxis(this float maxValue)
        {
            double maximumValueLabel = (double)maxValue;
            double minimumValueLabel = 0;

            double range = maximumValueLabel - minimumValueLabel;
            int tickCount = 4;
            double unroundedTickSize = range / (tickCount - 1);
            double x = Math.Ceiling(Math.Log10(unroundedTickSize) - 1);
            double pow10x = Math.Pow(10, x);
            double roundedTickRange = Math.Ceiling(unroundedTickSize / (double)pow10x) * pow10x;

            return (float)roundedTickRange;
        }

        /// <summary>
        /// Calculate the maximum value for horizontal or vertical lines for charts with Positive axis
        /// </summary>
        /// <param name="maxValue">point maximum from values</param>
        /// <returns>get the maximum value for the last line</returns>
        public static float GetMaxValueForPositiveAxis(this float maxValue)
        {
            if (maxValue == 0)
            {
                return 0;
            }
            var step = maxValue.GetStepForPositiveAxis();
            int tickCount = 4;
            return step * (tickCount - 1);
        }

        /// <summary>
        ///  Cleans extra 0 from a string when needed. Example 40.0 is parsed to 40
        /// </summary>
        /// <param name="number">number string representation</param>
        /// <returns>String without extra 0 decimal</returns>
        public static string Remove0AfterPoint(this string number)
        {
            if (number.Contains('.'))
            {
                var decimalNumber = Convert.ToDecimal(number.Substring(number.IndexOf('.') + 1));
                if (decimalNumber <= 0)
                {
                    var textToRemove = number.Substring(number.IndexOf('.'));
                    number = number.Replace(textToRemove, "");
                }
            }

            return number;
        }

        /// <summary>
        ///  Formats a number to thousands (K),millions (M) , billions (B) and trillions (T)
        /// </summary>
        /// <param name="number">string number representation to format</param>
        /// <param name="isRounded">check if it is needed the rounding</param>
        /// <param name="isPercentage">the value is percentage</param>
        /// <returns>Formatted KMB number representation</returns>s
        public static string ToKMBString(this decimal number, bool isRounded = false, bool isPercentage = false)
        {
            var stringNumber = string.Empty;

            //Always perform rounding to nearest integer for values < 1000
            if (isPercentage)
            {
                stringNumber = number.ToString("0.0", CultureInfo.InvariantCulture).Remove0AfterPoint();
            }
            else
            {
                stringNumber = Math.Round(number, 0, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture).Remove0AfterPoint();
            }

            //Trillion
            if (number > _trillionMultiplicator || number < (_trillionMultiplicator * -1))
            {
                return PrepareKMBString(number, isRounded, 11, _trillionMultiplicator, "T");
            }
            //Billion
            if (number > _billiomMultiplicator || number < (_billiomMultiplicator * -1))
            {
                return PrepareKMBString(number, isRounded, 8, _billiomMultiplicator, "B");
            }
            //Million
            if (number > (_millionMultiplicator - 1) || number < ((_millionMultiplicator - 1) * -1))
            {
                return PrepareKMBString(number, isRounded, 5, _millionMultiplicator, "M");
            }
            //Thousands
            if (number > (_thousandMultiplicator - 1) || number < ((_thousandMultiplicator - 1) * -1))
            {
                return PrepareKMBString(number, isRounded, 2, _thousandMultiplicator, "K");
            }

            return stringNumber;

            /// <summary>
            /// This code is here because it only is useful in the context of this function. It is in charge of
            /// formatting KMB with a specific number of decimal positions and a suffix depending on multiplicator.
            /// </summary>
            /// <param name="number">Number to be rounded</param>
            /// <param name="isRounded">Number needs to be rounded</param>
            /// <param name="decimalsToRound">Only used if isRounded is true. Specifies number of decimals to be rounded</param>
            /// <param name="multiplicator">Indicates if number will be transformed to Thousands,Millions, Trillions</param>
            /// <param name="suffix">Suffix for the resulting string</param>
            /// <returns>number rounded</returns>
            string PrepareKMBString(decimal number, bool isRounded, int decimalsToRound, long multiplicator, string suffix)
            {
                var stringNumber = isRounded ? DinamycDecimalsToRound(number, decimalsToRound, multiplicator)
                        : Math.Round(number / multiplicator, 0, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture).Remove0AfterPoint();

                if (stringNumber.Contains('.'))
                {
                    var numbersAfterPoint = stringNumber.Substring(stringNumber.LastIndexOf('.') + 1);
                    stringNumber = stringNumber.Substring(0, (stringNumber.Length - numbersAfterPoint.Length) + 1).Remove0AfterPoint();
                }

                return stringNumber + suffix;
            }
        }


        /// <summary>
        /// Compute the decimals to be rounded. for example 23.0 - 23.499 => 23 or 23.5 - 23.999 => 24
        /// </summary>
        /// <param name="number">Number to be rounded</param>
        /// <param name="decimals">decimal to round the number</param>
        /// <param name="divider">Number to divide the number</param>
        /// <returns>number rounded</returns>
        private static string DinamycDecimalsToRound(decimal number, int decimals, long divider)
        {
            var moduleDividerString = "1E" + decimals;
            var moduleDivider = decimal.Parse(moduleDividerString, NumberStyles.AllowExponent);
            var module = number % moduleDivider;
            var limitNoRounding = "49";
            for (var index = 1; index <= decimals - 2; index++)
            {
                limitNoRounding += "9";
            }
            if (module >= 0 && module <= long.Parse(limitNoRounding))
            {
                number -= module;
            }
            number = Math.Round(number / divider, decimals, MidpointRounding.AwayFromZero);
            var stringNumber = number.ToString("0.#");
            return stringNumber;
        }

        /// <summary>
        ///  Formats a number to thousands (K),millions (M), billions (B) and trillions (T)
        /// </summary>
        /// <param name="number">number to format</param>
        /// <param name="decimalsToRound">number of decimal places to apply the rounding</param>
        /// <param name="isPercentage">the value is percentage</param>
        /// <returns>Formatted KMB number</returns>s
        public static decimal ToKMBNumber(this decimal number, int decimalsToRound = 0, bool isPercentage = false)
        {
            if (number > _trillionMultiplicator || number < (_trillionMultiplicator * -1))
            {
                return Math.Round(number / _trillionMultiplicator, decimalsToRound, MidpointRounding.AwayFromZero) * _trillionMultiplicator;
            }

            if (number > _billiomMultiplicator || number < (_billiomMultiplicator * -1))
            {
                return Math.Round(number / _billiomMultiplicator, decimalsToRound, MidpointRounding.AwayFromZero) * _billiomMultiplicator;
            }

            if (number > (_millionMultiplicator - 1) || number < ((_millionMultiplicator - 1) * -1))
            {
                return Math.Round(number / _millionMultiplicator, decimalsToRound, MidpointRounding.AwayFromZero) * _millionMultiplicator;
            }

            if (number > (_thousandMultiplicator - 1) || number < ((_thousandMultiplicator - 1) * -1))
            {
                return Math.Round(number / _thousandMultiplicator, decimalsToRound, MidpointRounding.AwayFromZero) * _thousandMultiplicator;
            }

            //Always perform rounding to nearest integer for values < 1000
            return Math.Round(number, isPercentage ? decimalsToRound : 0, MidpointRounding.AwayFromZero);
        }
    }
}
