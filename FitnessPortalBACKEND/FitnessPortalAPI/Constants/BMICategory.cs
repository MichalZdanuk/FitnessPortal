using System.Text.Json.Serialization;

namespace FitnessPortalAPI.Constants;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BMICategory
{
	Underweight,
	Normalweight,
	Overweight,
	Obesity
}
