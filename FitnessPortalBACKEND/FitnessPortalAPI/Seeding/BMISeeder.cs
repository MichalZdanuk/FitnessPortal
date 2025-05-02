using FitnessPortalAPI.Utils;

namespace FitnessPortalAPI.Seeding
{
	public static class BMISeeder
    {
        public static IEnumerable<BMI> GetBMIRecords(List<User> users)
        {
            var bmis = new List<BMI>()
            {
                new BMI()
                {
                    Date = new DateTime(2023, 08, 01),
                    Height = 175,
                    Weight = 70,
                    UserId = users[0].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 08, 02),
                    Height = 180,
                    Weight = 85,
                    UserId = users[1].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 08, 03),
                    Height = 160,
                    Weight = 60,
                    UserId = users[2].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 09, 01),
                    Height = 175,
                    Weight = 72,
                    UserId = users[0].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 09, 05),
                    Height = 180,
                    Weight = 72,
                    UserId = users[1].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 09, 03),
                    Height = 160,
                    Weight = 72,
                    UserId = users[2].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 09, 07),
                    Height = 175,
                    Weight = 77,
                    UserId = users[0].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 09, 12),
                    Height = 180,
                    Weight = 74,
                    UserId = users[1].Id,
                },
                new BMI()
                {
                    Date = new DateTime(2023, 09, 05),
                    Height = 160,
                    Weight = 82,
                    UserId = users[2].Id,
                }
            };

            // Calculate BMIScore and BMICategory for each BMI record
            foreach (var bmi in bmis)
            {
				Calculator.CalculateBMI(bmi.Height, bmi.Weight, out float bmiIndex, out BMICategory bmiCategory);
                bmi.BMIScore = bmiIndex;
                bmi.BMICategory = bmiCategory;
            }

            return bmis;
        }
    }
}
