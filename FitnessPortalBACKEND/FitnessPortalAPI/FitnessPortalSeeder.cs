﻿using FitnessPortalAPI.Constants;
using FitnessPortalAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitnessPortalAPI
{
    public class FitnessPortalSeeder
    {
        private readonly FitnessPortalDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private Calculator _calculator;
        public FitnessPortalSeeder(FitnessPortalDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _calculator = new Calculator();
        }
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                SeedRoles();
                SeedUsers();
                SeedArticles();
                SeedBMI();
                SeedTrainings();
            }
        }

        private void SeedRoles()
        {
            if (!_context.Roles.Any())
            {
                var roles = GetRoles();
                _context.Roles.AddRange(roles);
                _context.SaveChanges();
            }
        }

        private void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var users = GetUsersWithHashedPasswords();
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }

        private void SeedArticles()
        {
            if (!_context.Articles.Any())
            {
                var users = _context.Users.ToList();

                var articles = GetArticles(users);
                _context.Articles.AddRange(articles);
                _context.SaveChanges();
            }
        }

        private void SeedBMI()
        {
            if (!_context.BMIs.Any())
            {
                var users = _context.Users.ToList();

                var bmis = GetBMIRecords(users);
                _context.BMIs.AddRange(bmis);
                _context.SaveChanges();
            }
        }

        private void SeedTrainings()
        {
            if (true)//!_context.Trainings.Any())
            {
                var users = _context.Users.ToList();

                var trainings = GetSampleTrainings(users);
                _context.Trainings.AddRange(trainings);
                _context.SaveChanges();
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }

        private IEnumerable<User> GetUsersWithHashedPasswords()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email = "john@doe.com",
                    Username = "JohnDoe",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Weight = 75,
                    Height = 180,
                    RoleId = 1,
                    PasswordHash = HashPassword("johndoe"),
                },
                new User()
                {
                    Email = "jane@smith.com",
                    Username = "JaneSmith",
                    DateOfBirth = new DateTime(1985, 8, 20),
                    Weight = 68,
                    Height = 170,
                    RoleId = 1,
                    PasswordHash = HashPassword("janesmith"),
                },
                new User()
                {
                    Email = "alice@johnson.com",
                    Username = "AliceJohnson",
                    DateOfBirth = new DateTime(1992, 10, 10),
                    Weight = 62,
                    Height = 165,
                    RoleId = 1,
                    PasswordHash = HashPassword("alicejohnson"),
                },
            };
            return users;
        }

        private IEnumerable<Article> GetArticles(List<User> users)
        {
            var articles = new List<Article>()
            {
                new Article()
                {
                    Title = "The Key Elements of a Well-Balanced Diet for Optimal Health",
                    ShortDescription = "A well-balanced diet is the foundation for maintaining good health and achieving optimal wellness. It provides the body with the necessary nutrients, vitamins, and minerals it needs to function at its best. In this article, we delve into the key elements of a well-balanced diet and explore how it can support your overall well-being.",
                    Content = "Include a Variety of Nutrient-Dense Foods: A well-balanced diet should consist of a wide range of nutrient-dense foods, including fruits, vegetables, whole grains, lean proteins, and healthy fats. These foods are rich in essential vitamins, minerals, and antioxidants, providing the body with the fuel it needs to thrive. Prioritize Portion Control: While the quality of food is important, portion control plays a significant role in maintaining a well-balanced diet. Pay attention to portion sizes and listen to your body's hunger and satiety cues. Opt for smaller, frequent meals throughout the day to keep energy levels stable and prevent overeating. Emphasize Whole Grains and Fiber: Whole grains, such as brown rice, quinoa, and whole wheat bread, are excellent sources of fiber. Fiber aids in digestion, promotes feelings of fullness, and helps regulate blood sugar levels. Aim to include whole grains in your meals to support a well-balanced diet. Incorporate Lean Proteins: Protein is essential for building and repairing tissues, supporting immune function, and maintaining muscle mass. Opt for lean protein sources like chicken, fish, beans, tofu, and low-fat dairy products. Including protein with each meal can help you feel satisfied and maintain stable energy levels. Don't Forget Healthy Fats: Healthy fats, such as those found in avocados, nuts, seeds, and olive oil, are crucial for brain health, hormone production, and nutrient absorption. Incorporate these sources of healthy fats into your diet in moderation to support a well-rounded approach to nutrition.Hydration is Key: Remember to stay properly hydrated by drinking an adequate amount of water throughout the day. Water helps maintain bodily functions, supports digestion, and aids in nutrient absorption. Make it a habit to carry a water bottle with you and sip water regularly. Conclusion: A well-balanced diet is not about strict restrictions or fad diets; it is about nourishing your body with wholesome, nutrient-dense foods. By incorporating a variety of fruits, vegetables, whole grains, lean proteins, and healthy fats into your daily meals, you can support your overall health and well-being. Remember, small, sustainable changes to your eating habits can make a significant impact on your long-term health goals.",
                    Category = "Well-balanced diet",
                    DateOfPublication = new DateTime(2023, 2, 22),
                    CreatedById = users[0].Id,
                },
                new Article()
                {
                    Title = "Unlocking the Secrets to a Restful Night's Sleep: Your Guide to Quality Sleep for Optimal Fitness",
                    ShortDescription = "Quality sleep plays a vital role in overall health and well-being, particularly when it comes to achieving fitness goals. In this article, we uncover the secrets to a restful night's sleep and explore how proper sleep habits can enhance your physical performance, recovery, and mental focus.",
                    Content = "Establish a Consistent Sleep Schedule: Creating a regular sleep routine helps regulate your body's internal clock, making it easier to fall asleep and wake up naturally. Aim to go to bed and wake up at the same time every day, even on weekends, to optimize your sleep-wake cycle. Create a Sleep-Friendly Environment: Transform your bedroom into a peaceful sanctuary conducive to sleep. Keep the room dark, quiet, and cool, and invest in a comfortable mattress and pillows that support your body's needs. Consider using earplugs, eye masks, or white noise machines to block out any disruptions. Unwind with a Bedtime Ritual: Establish a relaxing routine before bed to signal to your body that it's time to wind down. This can include activities such as taking a warm bath, reading a book, practicing meditation or deep breathing exercises, or listening to calming music. Avoid stimulating activities or screens (e.g., smartphones, laptops) that can interfere with sleep. Watch Your Diet and Hydration: Be mindful of your food and drink choices, especially close to bedtime. Avoid consuming heavy meals, caffeine, nicotine, and alcohol, as they can disrupt sleep patterns. Opt for light, balanced evening snacks and stay hydrated throughout the day, but reduce liquid intake closer to bedtime to minimize nighttime disruptions. Exercise Regularly, but Timing is Key: Engaging in regular physical activity promotes better sleep, but the timing of exercise matters. Aim to complete your workouts at least a few hours before bedtime to allow your body temperature and adrenaline levels to normalize, promoting a more restful sleep. Manage Stress and Relaxation Techniques: High levels of stress can interfere with sleep quality. Incorporate stress management techniques into your daily routine, such as practicing mindfulness, journaling, or engaging in activities that help you relax and unwind. These techniques can help calm your mind and prepare your body for a good night's sleep. Limit Screen Time Before Bed: The blue light emitted by electronic devices can disrupt your body's natural sleep-wake cycle. Minimize exposure to screens at least an hour before bed and consider using blue light filters on your devices or wearing blue light-blocking glasses to mitigate the impact on your sleep. Conclusion: A good night's sleep is not only essential for recovery and rejuvenation but also for optimizing your fitness journey. By implementing healthy sleep habits, creating a soothing environment, and prioritizing relaxation techniques, you can improve the quality and duration of your sleep. Remember, quality sleep is a powerful ally in your pursuit of overall fitness and well-being.",
                    Category = "Sleep",
                    DateOfPublication = new DateTime(2023, 3, 11),
                    CreatedById = users[1].Id,
                },
                new Article()
                {
                    Title = "Unlocking Your Full Potential: Strategies to Improve Fitness Performance",
                    ShortDescription = "Whether you're an athlete or a fitness enthusiast, maximizing your performance is a common goal. In this article, we explore effective strategies to enhance your fitness performance, boost endurance, increase strength, and achieve new heights in your fitness journey.",
                    Content = "Set Clear and Specific Goals: Define clear and specific performance goals that align with your overall fitness objectives. Establishing measurable targets, such as improving running speed, increasing weightlifting capacity, or enhancing flexibility, provides focus and motivation to push yourself beyond your limits. Prioritize Progressive Overload: To improve performance, it's crucial to gradually increase the demands placed on your body. Progressive overload involves progressively challenging your muscles, cardiovascular system, or skill level over time. This can be achieved by gradually increasing resistance, intensity, duration, or complexity in your workouts. Incorporate Periodization: Periodization is a training approach that divides your program into specific phases or cycles to optimize performance gains. This technique includes periods of high intensity, moderate intensity, and rest to prevent plateaus and overtraining. Structured periodization allows for optimal adaptation, improved strength, and reduced risk of injury. Focus on Strength and Resistance Training: Strength training is a cornerstone for improving overall fitness performance. Incorporate compound exercises, such as squats, deadlifts, and bench presses, to target multiple muscle groups and increase strength. Utilize progressive overload principles by gradually increasing weights and adjusting rep ranges for continuous progress. Enhance Cardiovascular Conditioning: Cardiovascular endurance is crucial for activities that require sustained effort. Include cardio exercises like running, cycling, or swimming in your routine to improve aerobic capacity. Vary intensity levels with interval training, long-distance runs, and steady-state cardio to challenge your cardiovascular system. Optimize Recovery and Rest: Adequate recovery is essential for optimizing performance. Allow your body enough time to rest and repair through proper sleep, nutrition, and active recovery techniques like foam rolling and stretching. Listen to your body's signals and avoid overtraining, as it can hinder performance gains. Fine-tune Nutrition: Proper nutrition plays a critical role in optimizing performance. Ensure you're fueling your body with a well-balanced diet that includes adequate protein for muscle repair, carbohydrates for energy, and healthy fats for overall health. Stay hydrated and consider incorporating supplements when necessary to support performance and recovery. Mental Conditioning and Visualization: Mental preparation is as important as physical training. Develop a positive mindset, set realistic expectations, and utilize visualization techniques to enhance performance. Visualize successful performances, overcome mental obstacles, and stay focused and motivated during workouts or competitions. Conclusion: Improving fitness performance requires a holistic approach that encompasses training, recovery, nutrition, and mental conditioning. By setting clear goals, incorporating progressive overload, prioritizing strength and cardio training, optimizing recovery, and fine-tuning nutrition, you can unlock your full potential and achieve new levels of performance. Remember, consistent effort, dedication, and a growth mindset are key to reaching your fitness performance goals.",
                    Category = "Performance",
                    DateOfPublication = new DateTime(2023, 4, 12),
                    CreatedById = users[2].Id,
                },
                new Article()
                {
                    Title = "The Power of Plant-Based Nutrition: Harnessing the Benefits of Eating Vegetables for Optimal Fitness",
                    ShortDescription = "Incorporating vegetables into your diet is a game-changer when it comes to fueling your fitness journey. In this article, we delve into the immense benefits of eating vegetables and how they can enhance your overall health, support weight management, and improve athletic performance.",
                    Content = "A Nutrient Powerhouse: Vegetables are packed with essential vitamins, minerals, antioxidants, and dietary fiber that are vital for optimal health. They provide a wide range of nutrients, including vitamin C, vitamin K, potassium, and folate, which support immune function, bone health, and cardiovascular health. Boost Energy and Recovery: Vegetables provide a natural source of carbohydrates, which are the body's primary energy source. Incorporating vegetables into your meals fuels your workouts and helps replenish glycogen stores for better performance and faster recovery. Leafy greens, such as spinach and kale, are particularly rich in iron, supporting oxygen transport and combating fatigue. Weight Management and Satiation: Vegetables are low in calories and high in fiber, making them an excellent choice for weight management. Their high water content adds volume to your meals, promoting satiety and reducing the chances of overeating. Including a variety of vegetables in your diet can help you feel fuller for longer, supporting weight loss or maintenance goals. Enhance Digestion and Gut Health: Vegetables are rich in dietary fiber, which plays a crucial role in maintaining a healthy digestive system. Fiber aids in regulating bowel movements, preventing constipation, and promoting a healthy gut microbiome. A diverse range of vegetables can provide prebiotic fibers that feed beneficial gut bacteria, promoting overall gut health. Reduce Inflammation and Support Recovery: Many vegetables contain natural anti-inflammatory compounds and antioxidants that help combat inflammation caused by intense workouts. These properties aid in reducing muscle soreness, supporting joint health, and enhancing post-exercise recovery. Colorful vegetables like bell peppers, tomatoes, and beets are particularly rich in antioxidants. Versatility and Culinary Creativity: Vegetables offer endless possibilities for culinary creativity, allowing you to experiment with different flavors, textures, and cooking methods. From roasted vegetables to stir-fries and salads, incorporating a variety of vegetables in your meals can make healthy eating enjoyable and sustainable. Practical Tips for Increasing Vegetable Intake: Start by gradually increasing your vegetable intake, aiming for at least 5 servings per day. Include a variety of colors and types to maximize nutrient diversity. Experiment with new recipes, try different cooking techniques, and consider incorporating vegetables in smoothies or as snacks for added convenience. Conclusion: Eating vegetables is a powerful strategy for enhancing your fitness journey. Their nutrient density, ability to support weight management, boost energy, aid in recovery, and promote overall health make them an essential component of a well-rounded diet. By incorporating a colorful array of vegetables into your meals and exploring different cooking methods, you can harness the benefits of plant-based nutrition and optimize your fitness goals.",
                    Category = "Diet",
                    DateOfPublication = new DateTime(2023, 4, 15),
                    CreatedById = users[2].Id,
                },
                new Article()
                {
                    Title = "Meditation: Cultivating Inner Strength for Optimal Fitness and Well-being",
                    ShortDescription = "Meditation is a practice that goes beyond stress reduction and relaxation; it has profound benefits for both the mind and body, making it a valuable tool in your fitness journey. In this article, we explore the transformative power of meditation and how incorporating this practice can enhance your mental focus, emotional well-being, and overall physical performance.",
                    Content = "Mental Focus and Clarity: Regular meditation practice cultivates a heightened sense of mental focus and clarity. By training the mind to stay present and redirecting attention to the present moment, you can improve your ability to concentrate during workouts, stay focused on your fitness goals, and overcome mental obstacles. Stress Reduction and Emotional Balance: Chronic stress can hinder progress and impact overall well-being. Meditation is an effective technique for managing stress, reducing anxiety, and promoting emotional balance. Through mindful awareness and acceptance, meditation helps regulate stress hormones, calms the nervous system, and enhances resilience in the face of challenges. Enhancing Mind-Body Connection: Fitness is not just about physical strength; it's also about developing a strong mind-body connection. Meditation allows you to tune in to your body's sensations, become aware of muscle tension, and improve your ability to listen to your body's needs. This awareness can help prevent injuries, optimize movement patterns, and enhance overall physical performance. Boosting Recovery and Sleep Quality: Rest and recovery are vital for fitness progress. Regular meditation practice has been shown to improve sleep quality, reduce insomnia, and enhance the body's ability to recover. By promoting relaxation and calming the mind, meditation supports the body's natural healing processes, facilitating faster recovery from workouts. Developing Resilience and Mental Toughness: Fitness journeys often come with setbacks and challenges. Meditation helps build resilience and mental toughness by training the mind to stay focused, cultivate patience, and maintain a positive mindset in the face of obstacles. This mindset can enhance your ability to stay committed, bounce back from setbacks, and persevere through plateaus. Mindful Eating for Nutritional Support: Mindful eating is an extension of meditation that involves paying attention to the present moment while consuming food. By practicing mindful eating, you can develop a healthier relationship with food, improve portion control, and enhance your ability to make nourishing choices that support your fitness goals. Getting Started with Meditation: Begin by setting aside a dedicated time and space for meditation practice. Start with short sessions, gradually increasing the duration as you become more comfortable. Experiment with different meditation techniques, such as focused attention, loving-kindness meditation, or body scan meditation, to find what resonates with you. Conclusion: Meditation is a powerful practice that supports overall well-being and enhances your fitness journey. By incorporating meditation into your routine, you can improve mental focus, reduce stress, enhance emotional balance, and develop a stronger mind-body connection. Embrace meditation as a valuable tool to cultivate inner strength, optimize your fitness performance, and promote holistic well-being.",
                    Category = "Meditation",
                    DateOfPublication = new DateTime(2023, 4, 17),
                    CreatedById = users[0].Id,
                },
                new Article()
                {
                    Title = "Calisthenics: Unlocking Your Full Potential with Bodyweight Fitness",
                    ShortDescription = "Calisthenics is a dynamic and versatile form of exercise that utilizes your bodyweight to build strength, flexibility, and endurance. In this article, we dive into the world of calisthenics and explore how this discipline can transform your fitness journey, sculpt your physique, and enhance overall functional fitness.",
                    Content = "Building Functional Strength: Calisthenics movements focus on compound exercises that engage multiple muscle groups simultaneously. By mastering exercises like push-ups, pull-ups, squats, and planks, you develop functional strength that translates into real-life movements, improving your ability to perform daily tasks and excel in other physical activities. No Equipment, No Problem: One of the greatest advantages of calisthenics is its accessibility. You can perform these exercises virtually anywhere, as they require little to no equipment. Whether you're at home, in a park, or traveling, you can engage in a challenging calisthenics workout using only your bodyweight, making it a convenient and cost-effective fitness option. Progression and Adaptability: Calisthenics allows for progressive overload, enabling you to continuously challenge your muscles and progress at your own pace. From beginner variations to advanced movements like handstands, muscle-ups, and human flags, there are endless progressions to keep you motivated and ensure consistent gains in strength and skill. Body Control and Kinesthetic Awareness: Calisthenics emphasizes body control, balance, and kinesthetic awareness. Through movements like handstands, L-sits, and pistol squats, you develop a deep understanding of your body's positioning and movement in space. This enhanced body awareness carries over to other physical activities, improving coordination and reducing the risk of injury. Full-Body Conditioning: Calisthenics workouts engage multiple muscle groups simultaneously, providing a comprehensive full-body workout. By incorporating exercises that target upper body, lower body, and core, you develop balanced muscular strength, endurance, and stability, leading to a well-rounded physique. Mobility and Flexibility: Calisthenics incorporates movements that promote mobility and flexibility, improving joint range of motion and overall functional movement patterns. Exercises like deep squats, bridge variations, and leg raises enhance flexibility in the hips, shoulders, and spine, increasing athletic performance and reducing the risk of injuries. Mind-Body Connection and Mindfulness: Calisthenics encourages a strong mind-body connection. By focusing on the execution of precise movements and maintaining proper form, you cultivate mindfulness and concentration. This connection enhances body awareness, mental focus, and the overall mind-body harmony during workouts. Community and Support: Calisthenics has a vibrant and supportive community. Whether through local meetups, online forums, or social media groups, connecting with fellow calisthenics enthusiasts can provide motivation, inspiration, and valuable resources for progressing in your fitness journey. Conclusion: Calisthenics offers a unique and effective approach to fitness that emphasizes bodyweight movements, functional strength, and versatility. By incorporating calisthenics into your routine, you can build strength, improve flexibility, enhance body control, and enjoy a dynamic and challenging workout experience. Embrace the power of calisthenics to unlock your full potential and embark on a transformative fitness journey.",
                    Category = "Workout",
                    DateOfPublication = new DateTime(2023, 5, 1),
                    CreatedById = users[0].Id,
                },
                new Article()
                {
                    Title = "Cardiovascular Workouts for Weight Loss",
                    ShortDescription = "Discover effective cardio workouts to accelerate weight loss and boost your overall health.",
                    Content = "Cardiovascular exercises, commonly known as cardio, play a pivotal role in weight loss and overall health. These workouts focus on elevating your heart rate, burning calories, and improving your stamina. Whether you're a beginner or an experienced fitness enthusiast, incorporating effective cardio workouts into your routine can help you achieve your weight loss goals. One of the key benefits of cardio workouts is their versatility. You can choose from various activities such as running, cycling, swimming, or even dancing. The variety keeps your workouts interesting and prevents boredom. To maximize weight loss, it's essential to find a cardio routine that suits your preferences and lifestyle. High-intensity interval training (HIIT) is an excellent choice for those looking to burn calories quickly. HIIT involves short bursts of intense exercise followed by brief recovery periods, making it efficient and time-saving. Additionally, longer, steady-state cardio sessions, like jogging or cycling, can also contribute to weight loss when done consistently. These workouts improve endurance and help you build a strong cardiovascular system. It's important to combine cardio workouts with a balanced diet to see significant results. Proper nutrition provides the energy needed for these exercises and supports muscle recovery. Remember to stay hydrated and listen to your body's signals during your cardio sessions.",
                    Category = "Fitness",
                    DateOfPublication = new DateTime(2023, 5, 5),
                    CreatedById = users[1].Id,
                },
                new Article()
                {
                    Title = "Nutrition Essentials for Muscle Gain",
                    ShortDescription = "Learn essential nutrition tips to support muscle gain and enhance your fitness journey.",
                    Content = "Achieving muscle gain and optimizing post-workout recovery relies heavily on nutrition. To build and repair muscle tissue effectively, your body requires the right nutrients. Here are some essential nutrition tips for those looking to enhance their muscle-building journey. Protein Intake: Protein is the cornerstone of muscle growth. Incorporate lean protein sources like chicken, fish, tofu, and beans into your diet. Consider consuming protein-rich snacks before and after workouts to support muscle recovery. Carbohydrates: Carbs provide the energy necessary for intense workouts. Opt for complex carbohydrates such as whole grains, sweet potatoes, and brown rice to sustain your energy levels throughout the day. Healthy Fats: Don't overlook healthy fats like avocados, nuts, and olive oil. They aid in hormone production, which plays a role in muscle growth. Hydration: Proper hydration is crucial for muscle function and recovery. Drink plenty of water to prevent dehydration, which can hinder your performance. Timing Matters: Consume a balanced meal with a mix of protein and carbs within two hours of your workout to promote muscle recovery. Supplements: While whole foods should be your primary source of nutrients, consider supplements like whey protein or creatine if you struggle to meet your dietary needs. Rest and Sleep: Adequate sleep is essential for muscle repair and growth. Aim for 7-9 hours of quality sleep each night. By paying attention to your nutrition and incorporating these tips, you can support muscle gain, reduce recovery time, and optimize your fitness results.",
                    Category = "Fitness & Nutrition",
                    DateOfPublication = new DateTime(2023, 5, 12),
                    CreatedById = users[2].Id,
                },
                new Article()
                {
                    Title = "Yoga's Mind and Body Benefits",
                    ShortDescription = "Explore the holistic benefits of yoga for physical and mental well-being.",
                    Content = "Yoga is more than just a physical exercise; it's a holistic practice that offers a multitude of benefits for both the mind and body. Whether you're a seasoned yogi or new to the practice, exploring the advantages of yoga can transform your overall well-being. Physical Benefits: Flexibility: Regular yoga practice increases flexibility by stretching and lengthening muscles. Strength: Many yoga poses require bodyweight resistance, which helps build lean muscle. Balance: Yoga poses enhance stability and improve posture. Pain Relief: Yoga can alleviate pain in conditions like arthritis and back pain. Injury Prevention: Increased flexibility and strength reduce the risk of injury. Mental Benefits: Stress Reduction: Yoga encourages relaxation and stress management through deep breathing and meditation. Mental Clarity: Practicing mindfulness during yoga enhances focus and concentration. Emotional Balance: Yoga fosters emotional stability and a positive outlook on life. Better Sleep: Yoga can improve sleep quality and combat insomnia. Overall Well-Being: Body-Mind Connection: Yoga fosters a strong connection between the body and mind. Self-Awareness: Practicing yoga encourages self-reflection and self-discovery. Mindful Living: Yoga philosophy promotes mindful and balanced living. Whether you're seeking physical strength, mental calmness, or a combination of both, yoga has something to offer. Its accessibility makes it suitable for individuals of all fitness levels, and you can practice it almost anywhere.",
                    Category = "Yoga & Wellness",
                    DateOfPublication = new DateTime(2023, 6, 3),
                    CreatedById = users[1].Id,
                },
                new Article()
                {
                    Title = "The Benefits of Strength Training",
                    ShortDescription = "Explore the advantages of incorporating strength training into your fitness routine.",
                    Content = "Strength training, often referred to as resistance training, is a vital component of a well-rounded fitness regimen. This article delves into the numerous benefits of incorporating strength training exercises into your workout routine. Building Muscle Mass: Strength training stimulates muscle growth, leading to increased muscle mass. As your muscles develop, you'll experience improved strength and endurance. Boosting Metabolism: Muscle tissue requires more energy to maintain than fat tissue, so as you gain muscle through strength training, your resting metabolism increases. This can contribute to effective weight management. Enhanced Bone Health: Strength training helps increase bone density, reducing the risk of osteoporosis and fractures. Joint Health: Strengthening the muscles around your joints can improve joint stability and reduce the risk of injury. Posture and Balance: Strength training exercises that target core and stabilizing muscles can enhance posture and balance. Functional Strength: The strength gained through resistance training translates into improved performance in everyday activities. Whether you're carrying groceries or playing sports, you'll notice enhanced functional strength. Weight Management: Combining strength training with cardiovascular workouts can accelerate weight loss and promote a leaner physique. Mental Health Benefits: Strength training has been linked to reduced symptoms of anxiety and depression. Additionally, the sense of accomplishment from reaching strength goals can boost self-esteem and confidence. Customizable Workouts: Strength training offers a variety of exercises that can be tailored to your fitness level and goals. It's suitable for beginners and experienced fitness enthusiasts alike. With a balanced fitness routine that includes strength training, you can achieve your fitness goals while reaping these numerous health benefits.",
                    Category = "Fitness & Strength Training",
                    DateOfPublication = new DateTime(2023, 6, 9),
                    CreatedById = users[0].Id,
                },
                new Article()
                {
                    Title = "The Power of Mindfulness Meditation",
                    ShortDescription = "Explore the transformative effects of mindfulness meditation on mental well-being.",
                    Content = "Mindfulness meditation is a practice that focuses on cultivating awareness of the present moment without judgment. It has gained widespread popularity for its positive impact on mental well-being. In this article, we delve into the transformative effects of mindfulness meditation. Stress Reduction: Mindfulness meditation techniques, such as deep breathing and body scanning, reduce stress levels and promote relaxation. Improved Emotional Regulation: Regular practice enhances emotional regulation, allowing individuals to respond to life's challenges with greater resilience. Enhanced Concentration: Mindfulness meditation boosts concentration and attention span, making it easier to stay focused on tasks. Anxiety Management: The practice has been proven effective in managing symptoms of anxiety disorders. Reduced Symptoms of Depression: Mindfulness meditation can reduce symptoms of depression and contribute to a more positive outlook on life. Enhanced Self-Awareness: Through mindfulness, individuals gain a deeper understanding of their thoughts, emotions, and behaviors. Pain Management: Mindfulness meditation has been used as a complementary therapy for pain management, helping individuals cope with chronic pain conditions. Improved Sleep: The relaxation techniques associated with mindfulness can lead to better sleep quality and reduced insomnia. Greater Resilience: Practicing mindfulness fosters resilience and the ability to bounce back from challenges. Enhanced Relationships: Mindfulness encourages empathy and better communication in relationships. Accessible Practice: Anyone can practice mindfulness meditation, and it can be done almost anywhere, making it a versatile tool for enhancing mental well-being. The transformative effects of mindfulness meditation extend beyond stress reduction; it can lead to profound changes in how individuals perceive and navigate the world.",
                    Category = "Wellness & Mental Health",
                    DateOfPublication = new DateTime(2023, 6, 18),
                    CreatedById = users[0].Id,
                },
                new Article()
                {
                    Title = "The Joy of Outdoor Activities",
                    ShortDescription = "Discover the joy of outdoor activities and their positive impact on physical and mental health.",
                    Content = "Spending time outdoors and engaging in recreational activities not only provides enjoyment but also offers numerous physical and mental health benefits. In this article, we celebrate the joy of outdoor activities and their positive impact on overall well-being. Physical Fitness: Outdoor activities like hiking, cycling, and kayaking offer excellent cardiovascular workouts while allowing you to explore natural landscapes. Vitamin D: Sunlight exposure during outdoor activities contributes to increased vitamin D production, which is essential for bone health and immunity. Stress Reduction: Being in nature and engaging in outdoor activities has been linked to reduced stress levels and improved mood. Mental Clarity: The tranquility of outdoor settings promotes mental clarity and relaxation, allowing you to disconnect from the demands of daily life. Enhanced Creativity: Spending time outdoors can boost creativity and problem-solving skills. Social Connection: Many outdoor activities, such as group hikes or team sports, offer opportunities for social interaction and bonding. Connection with Nature: Outdoor enthusiasts often develop a deeper connection with the natural world, fostering an appreciation for environmental conservation. Variety of Choices: From trail running to birdwatching, there's an outdoor activity to suit every interest and fitness level. Accessibility: Outdoor activities can be enjoyed by people of all ages and abilities, making them an inclusive form of recreation. Adventure and Exploration: Exploring new trails, landscapes, and environments adds an element of adventure to outdoor activities. Whether you're seeking an adrenaline rush or a peaceful escape, outdoor activities provide a wealth of options to connect with nature, stay active, and improve your overall well-being.",
                    Category = "Fitness & Outdoor Recreation",
                    DateOfPublication = new DateTime(2023, 7, 4),
                    CreatedById = users[2].Id,
                }
            };
            return articles;
        }

        private IEnumerable<BMI> GetBMIRecords(List<User> users)
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
                _calculator.CalculateBMI(bmi.Height, bmi.Weight, out float bmiIndex, out BMICategory bmiCategory);
                bmi.BMIScore = bmiIndex;
                bmi.BMICategory = bmiCategory;
            }

            return bmis;
        }

        private IEnumerable<Training> GetSampleTrainings(List<User> users)
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

        private string HashPassword(string password)
        {
            var user = new User();
            return _passwordHasher.HashPassword(user, password);
        }
    }
}
