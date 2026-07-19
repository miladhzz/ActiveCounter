using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveCounter
{
    public class TimerRecord
    {
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime StopTime { get; set; } = DateTime.MinValue;
        public TimeSpan WorkDuration { get; set; } = TimeSpan.Zero;
        public TimeSpan StopDuration { get; set; } = TimeSpan.Zero;

        // برای نمایش صحیح مدت زمانها در DataGridView
        public string WorkDurationString => WorkDuration != TimeSpan.Zero ? WorkDuration.ToString("hh\\:mm\\:ss") : string.Empty;
        public string StopDurationString => StopDuration != TimeSpan.Zero ? StopDuration.ToString("hh\\:mm\\:ss") : string.Empty;
    }
}
