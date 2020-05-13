namespace RS.Common.Constants
{
    /// <summary>
    /// The regular expression constants.
    /// </summary>
    public class RegularExpressionConstants
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public const string Guid = @"^[A-Fa-f0-9]{32}$|^({|\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\))?$";

        /// <summary>
        /// The URL.
        /// </summary>
        public const string Url = @"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";
    }
}
