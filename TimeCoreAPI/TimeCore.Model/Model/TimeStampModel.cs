using System;

namespace Model
{
    public class TimeStampModel : BasicModel
    {
        public int TimeStampYear { get; set; }
        public int TimeStampMonth { get; set; }
        public int TimeStampDay { get; set; }
        public int TimeStampHour { get; set; }
        public int TimeStampMinute { get; set; }
        public int TimeStampSecond { get; set; }
        public AccountModel Account { get; set; }
    }
}
