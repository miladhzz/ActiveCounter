using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveCounter
{
    public class TimerRecord
    {
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public TimeSpan WorkDuration { get; set; }
        public TimeSpan StopDuration { get; set; }

        // برای نمایش صحیح مدت زمانها در DataGridView
        public string WorkDurationString => WorkDuration.ToString(@"hh\:mm\:ss");
        public string StopDurationString => StopDuration.ToString(@"hh\:mm\:ss");
    }
}
