using FitnessPortalAPI.Entities;

namespace FitnessPortalAPI.Seeding
{
    public static class TrainingsSeeder
    {
        public static IEnumerable<Training> GetSampleTrainings(List<User> users)
        {
            var trainings = new List<Training>()
            {
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 8, 17),
                    NumberOfSeries = 4,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "barbell rows", NumberOfReps = 6, Payload = 35.5f },
                        new Exercise() { Name = "upright rows", NumberOfReps = 6, Payload = 15.5f },
                        new Exercise() { Name = "leg press", NumberOfReps = 6, Payload = 118.5f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 23.5f },
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 105.5f },
                        new Exercise() { Name = "squat", NumberOfReps = 10, Payload = 75.5f },
                        new Exercise() { Name = "biceps curl", NumberOfReps = 7, Payload = 18.5f },
                        new Exercise() { Name = "chest flyes", NumberOfReps = 8, Payload = 45.5f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 8, 17),
                    NumberOfSeries = 5,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "upright rows", NumberOfReps = 6, Payload = 17.5f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 25.5f },
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 107.5f },
                        new Exercise() { Name = "squat", NumberOfReps = 10, Payload = 78.5f },
                        new Exercise() { Name = "biceps curl", NumberOfReps = 7, Payload = 21.5f },
                        new Exercise() { Name = "chest flyes", NumberOfReps = 8, Payload = 53.5f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 8, 18),
                    NumberOfSeries = 4,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "bench press", NumberOfReps = 8, Payload = 65.0f },
                        new Exercise() { Name = "lat pulldowns", NumberOfReps = 8, Payload = 45.0f },
                        new Exercise() { Name = "leg extensions", NumberOfReps = 10, Payload = 80.0f },
                        new Exercise() { Name = "bicep curls", NumberOfReps = 8, Payload = 20.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 25.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 8, 21),
                    NumberOfSeries = 3,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 110.0f },
                        new Exercise() { Name = "shoulder press", NumberOfReps = 8, Payload = 30.0f },
                        new Exercise() { Name = "leg curls", NumberOfReps = 10, Payload = 65.0f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 22.5f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 25.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 8, 25),
                    NumberOfSeries = 5,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 110.0f },
                        new Exercise() { Name = "shoulder press", NumberOfReps = 8, Payload = 35.0f },
                        new Exercise() { Name = "leg curls", NumberOfReps = 10, Payload = 62.5f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 22.5f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 9, 28),
                    NumberOfSeries = 4,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "leg press", NumberOfReps = 8, Payload = 150.0f },
                        new Exercise() { Name = "bench press", NumberOfReps = 8, Payload = 75.0f },
                        new Exercise() { Name = "lat pulldowns", NumberOfReps = 10, Payload = 60.0f },
                        new Exercise() { Name = "bicep curls", NumberOfReps = 8, Payload = 25.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 9, 29),
                    NumberOfSeries = 3,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 120.0f },
                        new Exercise() { Name = "shoulder press", NumberOfReps = 8, Payload = 35.0f },
                        new Exercise() { Name = "leg curls", NumberOfReps = 10, Payload = 70.0f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 20.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 9, 30),
                    NumberOfSeries = 5,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "push-ups", NumberOfReps = 12, Payload = 0.0f },
                        new Exercise() { Name = "pull-ups", NumberOfReps = 6, Payload = 15.0f },
                        new Exercise() { Name = "squats", NumberOfReps = 12, Payload = 0.0f },
                        new Exercise() { Name = "planks", NumberOfReps = 3, Payload = 0.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 1),
                    NumberOfSeries = 4,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "bench press", NumberOfReps = 8, Payload = 70.0f },
                        new Exercise() { Name = "lat pulldowns", NumberOfReps = 8, Payload = 50.0f },
                        new Exercise() { Name = "leg extensions", NumberOfReps = 10, Payload = 85.0f },
                        new Exercise() { Name = "bicep curls", NumberOfReps = 8, Payload = 26.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 30.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 2),
                    NumberOfSeries = 3,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 115.0f },
                        new Exercise() { Name = "shoulder press", NumberOfReps = 8, Payload = 33.0f },
                        new Exercise() { Name = "leg curls", NumberOfReps = 10, Payload = 72.5f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 24.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 30.0f }

                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 4),
                    NumberOfSeries = 5,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "squats", NumberOfReps = 8, Payload = 105.0f },
                        new Exercise() { Name = "bench press", NumberOfReps = 10, Payload = 70.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 30.0f },
                        new Exercise() { Name = "lat pulldowns", NumberOfReps = 10, Payload = 60.0f },
                        new Exercise() { Name = "bicep curls", NumberOfReps = 8, Payload = 25.0f },
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 9, 30),
                    NumberOfSeries = 3,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "shoulder press", NumberOfReps = 8, Payload = 35.0f },
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 120.0f },
                        new Exercise() { Name = "leg curls", NumberOfReps = 10, Payload = 70.0f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 20.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 1),
                    NumberOfSeries = 5,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "pull-ups", NumberOfReps = 6, Payload = 15.0f },
                        new Exercise() { Name = "squats", NumberOfReps = 12, Payload = 0.0f },
                        new Exercise() { Name = "push-ups", NumberOfReps = 12, Payload = 0.0f },
                        new Exercise() { Name = "planks", NumberOfReps = 3, Payload = 0.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 2),
                    NumberOfSeries = 4,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "bench press", NumberOfReps = 8, Payload = 70.0f },
                        new Exercise() { Name = "lat pulldowns", NumberOfReps = 8, Payload = 50.0f },
                        new Exercise() { Name = "leg extensions", NumberOfReps = 10, Payload = 85.0f },
                        new Exercise() { Name = "bicep curls", NumberOfReps = 8, Payload = 26.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 30.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 5),
                    NumberOfSeries = 3,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "deadlift", NumberOfReps = 6, Payload = 115.0f },
                        new Exercise() { Name = "shoulder press", NumberOfReps = 8, Payload = 33.0f },
                        new Exercise() { Name = "leg curls", NumberOfReps = 10, Payload = 72.5f },
                        new Exercise() { Name = "hammer curls", NumberOfReps = 8, Payload = 24.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 25.0f }
                    }
                },
                new Training()
                {
                    DateOfTraining = new DateTime(2023, 10, 7),
                    NumberOfSeries = 5,
                    Exercises = new List<Exercise>()
                    {
                        new Exercise() { Name = "squats", NumberOfReps = 8, Payload = 105.0f },
                        new Exercise() { Name = "bench press", NumberOfReps = 10, Payload = 70.0f },
                        new Exercise() { Name = "bicep curls", NumberOfReps = 8, Payload = 25.0f },
                        new Exercise() { Name = "tricep dips", NumberOfReps = 8, Payload = 30.0f },
                        new Exercise() { Name = "lat pulldowns", NumberOfReps = 10, Payload = 60.0f }
                    }
                }
                // Add more sample trainings here
            };

            // Calculate totalPayload for each training
            foreach (var training in trainings)
            {
                float totalPayload = 0;
                foreach (var exercise in training.Exercises)
                {
                    totalPayload += exercise.NumberOfReps * exercise.Payload;
                }
                training.TotalPayload = totalPayload;
                training.UserId = users[new Random().Next(users.Count)].Id; // Assign a random user
            }

            return trainings;
        }
    }
}
