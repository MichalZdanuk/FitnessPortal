using System.Text.Json.Serialization;

namespace FitnessPortalAPI.Constants
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TrainingPeriod
    {
        Month,
        Quarter,
        HalfYear
    }
}
