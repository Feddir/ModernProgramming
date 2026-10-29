
using FitnessTracker.Domain;
using FitnessTracker.Domain.Enums;

var athleteRepo = new InMemoryRepository<Athlete>();
var workoutRepo = new InMemoryRepository<Workout>();

var athlete1 = new Athlete { Name = "Олександр", Email = "alex@gmail.com", Notes = "Професіонал" };
var athlete2 = new Athlete { Name = "Марія", Email = "maria@gmail.com", Notes = null };
var athlete3 = new Athlete { Name = "Іван", Email = "ivan@gmail.com", Notes = "Новачок" };

athleteRepo.Add(athlete1);
athleteRepo.Add(athlete2);
athleteRepo.Add(athlete3);

string[] exerciseNames =
{
    "Присідання", "Жим лежачи", "Станова тяга",
    "Підтягування", "Прес"
};

var rand = new Random(42);

for (int i = 1; i <= 15; i++)
{
    var workout = new Workout($"Тренування #{i}");

    for (int j = 0; j < 3; j++)
    {
        var exercise = new Exercise(
            exerciseNames[rand.Next(exerciseNames.Length)]);

        for (int k = 0; k < 3; k++)
        {
            double weight = rand.Next(40, 130);
            int reps = rand.Next(6, 12);

            exercise.AddSet(weight, reps);
        }

        workout.AddExercise(exercise);
    }

    workout.Start();

    if (i % 3 == 0)
        workout.Cancel();
    else
        workout.Complete();

    workoutRepo.Add(workout);

    if (i % 3 == 1)
        athlete1.AddWorkout(workout);
    else if (i % 3 == 2)
        athlete2.AddWorkout(workout);
    else
        athlete3.AddWorkout(workout);
}

var volumeCalculator = new StrengthVolumeCalculator();

Console.WriteLine("=== LINQ ЗАПИТИ ===");

var completedWorkouts = workoutRepo.GetAll()
    .Where(w => w.Status == WorkoutStatus.Completed)
    .ToList();

Console.WriteLine("\n1. Завершені тренування:");
foreach (var w in completedWorkouts)
    Console.WriteLine(w.Title);

var top5Workouts = workoutRepo.GetAll()
    .OrderByDescending(w => volumeCalculator.Calculate(w))
    .Take(5)
    .ToList();

Console.WriteLine("\n2. Топ-5 тренувань за об'ємом:");
foreach (var w in top5Workouts)
    Console.WriteLine($"{w.Title}: {volumeCalculator.Calculate(w)} кг");

var totalVolume = workoutRepo.GetAll()
    .Sum(w => volumeCalculator.Calculate(w));

Console.WriteLine($"\n3. Загальний training volume: {totalVolume} кг");

var avgSets = workoutRepo.GetAll()
    .Average(w => w.Exercises.Sum(e => e.Sets.Count));

Console.WriteLine($"\n4. Середня кількість sets: {avgSets:F1}");

Console.WriteLine("\n5. Кількість тренувань кожного атлета:");

foreach (var a in athleteRepo.GetAll())
    Console.WriteLine($"{a.Name}: {a.Workouts.Count}");

var allExercises = athleteRepo.GetAll()
    .SelectMany(a => a.Workouts)
    .SelectMany(w => w.Exercises)
    .ToList();

Console.WriteLine($"\n6. Загальна кількість exercises: {allExercises.Count}");

var top3Exercises = allExercises
    .GroupBy(e => e.Name)
    .Select(g => new
    {
        Name = g.Key,
        TotalReps = g.Sum(e => e.Sets.Sum(s => s.Repetitions))
    })
    .OrderByDescending(e => e.TotalReps)
    .Take(3)
    .ToList();

Console.WriteLine("\n7. Топ-3 вправи за repetitions:");

foreach (var e in top3Exercises)
    Console.WriteLine($"{e.Name}: {e.TotalReps} повторень");

var heavyExercises = allExercises
    .Where(e => e.Sets.Any(s => s.Weight > 100))
    .Select(e => e.Name)
    .Distinct()
    .ToList();

Console.WriteLine("\n8. Вправи з вагою понад 100 кг:");

foreach (var name in heavyExercises)
    Console.WriteLine(name);

bool hasMassiveWorkout = workoutRepo.GetAll()
    .Any(w => volumeCalculator.Calculate(w) > 10000);

Console.WriteLine(
    $"\n9. Є тренування з об'ємом понад 10000 кг? {(hasMassiveWorkout ? "Так" : "Ні")}");

var maxWeightDictionary = allExercises
    .GroupBy(e => e.Name)
    .ToDictionary(
        g => g.Key,
        g => g.Max(e => e.Sets.Max(s => s.Weight))
    );

Console.WriteLine("\n10. Максимальна вага для кожної вправи:");

foreach (var item in maxWeightDictionary)
    Console.WriteLine($"{item.Key}: {item.Value} кг");