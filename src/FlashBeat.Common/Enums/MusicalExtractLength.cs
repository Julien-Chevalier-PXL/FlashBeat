namespace FlashBeat.Common.Enums;

using System.ComponentModel;

/// <summary>
/// Enumerates the lengths of a musical extract.
/// </summary>
public enum MusicalExtractLength
{
    [Description("0.1 seconds")]
    PointOneSeconds = 100,
    
    [Description("0.5 seconds")]
    PointFiveSeconds = 500,
    
    [Description("2 seconds")]
    TwoSeconds = 2_000,
    
    [Description("4 seconds")]
    FourSeconds = 4_000,
    
    [Description("8 seconds")]
    EightSeconds = 8_000,
    
    [Description("15 seconds")]
    FifteenSeconds = 15_000,
}
