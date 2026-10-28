
using FitnessTracker.Domain;
using FitnessTracker.Domain.Interface;

var athlete = new Athlete("Олександр");
var workout = new Workout("День ніг");

var squat = new Exercise("Присідання зі штангою");
squat.AddSet(100.0, 10);
squat.AddSet(110.0, 8);

workout.AddExercise(squat);
athlete.AddWorkout(workout);

workout.Start();
workout.Complete();

IWorkoutMetricCalculator volumeCalculator = new StrengthVolumeCalculator();
IWorkoutMetricCalculator repCalculator = new RepetitionCalculator();

double totalVolume = volumeCalculator.Calculate(workout);
double totalReps = repCalculator.Calculate(workout);

Console.WriteLine($"Атлет: {athlete.Name}");
Console.WriteLine($"Тренування: {workout.Title} [Статус: {workout.Status}]");
Console.WriteLine($"Загальний об'єм піднятої ваги: {totalVolume} кг");
Console.WriteLine($"Загальна кількість повторень: {totalReps}");