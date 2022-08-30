using System.Xml;
using System.Xml.Linq;

namespace lab2.Parsers
{
    public static class DefaultParser
    {
        public static int ToInt(this XElement element)
        {
            int result = 0;

            if(element != null) int.TryParse(element.Value, out result);

            return result;
        }

        public static int ToInt(this XmlElement element)
        {
            int result = 0;

            if (element != null) int.TryParse(element.InnerText, out result);

            return result;
        }

        public static double ToDouble(this XElement element)
        {
            double result = 0;

            if (element != null) double.TryParse(element.Value, out result);

            return result;
        }

        public static double ToDouble(this XmlElement element)
        {
            double result = 0;

            if (element != null) double.TryParse(element.InnerText, out result);

            return result;
        }

        public static decimal ToDecimal(this XElement element)
        {
            decimal result = 0;

            if (element != null) decimal.TryParse(element.Value, out result);

            return result;
        }

        public static decimal ToDecimal(this XmlElement element)
        {
            decimal result = 0;

            if (element != null) decimal.TryParse(element.InnerText, out result);

            return result;
        }

        public static DateTime ToDateTime(this XElement element)
        {
            DateTime result = DateTime.MinValue;

            if (element != null) DateTime.TryParse(element.Value, out result);

            return result;
        }

        public static DateTime ToDateTime(this XmlElement element)
        {
            DateTime result = DateTime.MinValue;

            if (element != null) DateTime.TryParse(element.InnerText, out result);

            return result;
        }
    }
}
