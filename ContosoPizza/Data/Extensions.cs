namespace ContosoPizza.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        Console.WriteLine("in CreateDbIfNotExists");
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PizzaContext>();
                Console.WriteLine("in using of CreateDbIfNotExists");
                if (!context.Database.EnsureCreated())
                {
                    Console.WriteLine("in context.Database.EnsureCreated()");
                    DbInitializer.Initialize(context);
                }
            }
        }
    }
}