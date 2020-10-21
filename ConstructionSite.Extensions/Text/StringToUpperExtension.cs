using System.Text;

namespace ConstructionSite.Extensions.Text
{
    public static class StringToUpperExtension
    {
        #region .::FISTUPPER::

        public static string FistUpper(this string str)
        {
            StringBuilder builder = new StringBuilder();
            string strFirstLetter = str
                   .Substring(0, 1)
                   .ToUpper();
            var strFulllanguages = builder.Append(strFirstLetter + str.Substring(1)).ToString();
            // string strFulllanguages = strFirstLetter + str.Substring(1);
            return strFulllanguages;
        }

        #endregion .::FISTUPPER::
    }
}