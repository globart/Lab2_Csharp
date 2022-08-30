using lab2.ConsoleProcessors;
using lab2.Seeders;
using lab2.ViewModels;

var seeder = new DataSeeder();
seeder.Seed();


var printer = new Printer();
printer.PrintAllFiles();

printer.PrintQueries();

printer.PrintEnteringEntities();
printer.PrintAllFiles();