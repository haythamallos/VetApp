namespace Vetapp.Engine.BusinessAccessLayer
{
    internal class BusValidationExpressions
    {
        // Looks for any character in an entire string that has a character outside the specified Ascii range
        // of 0x20 - 0x126 and is not a tab or a new line character. The pattern checks from the beginning of
        // the string to the end.
        // "This string will not match (which is a valid case) because all the characters are in the specified range.
        // "This string will match because the backspace character is (not valid) for the TEXT type validation pattern. \b"
        public const string REGEX_TYPE_PATTERN_TEXT = @"^([^ -~\t\r\n])$";

        // Looks for any character outside the specified range of A to Z upper and lower case letters
        // and the number range 0 to 9, tabs, returns and linefeeds or for strings over 255 characters long.
        // The pattern checks from the beginning of the string to the end.
        // "This string will not match (which is a valid case) because the ; is not in the specified range."
        // "This string will match which is an invalid case because all characters are in the specified range"
        public const string REGEX_TYPE_PATTERN_NVARCHAR255 = @"^([^a-zA-Z0-9\t\r\n]{1,255}|.{256,257})$";

        // Looks for a match of one to ten digits from the beginning of the string to the end.
        // "0123456789" Successful match case, is a valid case.
        // "12.4" Does not match, is invalid case.
        // "2310" Successful match case, is a valid case.
        // "0x20" Does not match, is invalid case.
        public const string REGEX_TYPE_PATTERN_NUMERIC10 = @"^(\d{1,10})$";

        // Looks for a single "0" or "1" in the string. No other characters are allowed.
        //"1" Successful match case, is valid case.
        //"0" Successful match case, is valid case.
        //"10" Does not match, is invalid case.
        //"0110" Does not match, is invalid case.
        //"" Does not match, is invalid case.
        //"A" Does not match, is invalid case.
        public const string REGEX_TYPE_PATTERN_BIT = @"^(0|1)$";

        // Looks for 0 to 1 negative signs, followed by one to 13 digits from the beginning of
        // the string to the end.
        //"0123456789012" Successful match case, is valid case.
        //"0" Successful match case, is valid case.
        //"-2013" Successful match case, is valid case.
        //"1.2" Does not match, is invalid case.
        //"" Does not match, is invalid case.
        //"01234567890123" Does not match, is invalid case.
        //"A" Does not match, is invalid case.
        //"-" Does not match, is invalid case.
        public const string REGEX_TYPE_PATTERN_INT = @"^-{0,1}\d{1,13}$";
    }
}