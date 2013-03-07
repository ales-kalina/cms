using NuGet;

namespace CMS.Core
{

    public sealed class PackageManagerMessage
    {

        private MessageLevel mLevel;
        private string mText;

        public MessageLevel Level
        {
            get
            {
                return mLevel;
            }
        }

        public string Text
        {
            get
            {
                return mText;
            }
        }

        public PackageManagerMessage(MessageLevel level, string text)
        {
            mLevel = level;
            mText = text;
        }

    }

}