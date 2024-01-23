using Microsoft.EntityFrameworkCore;
using ObjectManagementApp.Data;

namespace ObjectManagementApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ObjectManagementAppContext(serviceProvider.GetRequiredService<DbContextOptions<ObjectManagementAppContext>>()))
            {
                if (context.CustomObject.Any())
                {
                    return;
                }

                var desktop = new CustomObject
                {
                    Name = "Desktop",
                    Description = "Desktop computer",
                    Type = "Computer"
                };
                var laptop = new CustomObject
                {
                    Name = "Laptop",
                    Description = "laptop",
                    Type = "Computer"
                };
                var monitor = new CustomObject
                {
                    Name = "Monitor",
                    Description = "computer monitor",
                    Type = "Computer"
                };
                var keyboard = new CustomObject
                {
                    Name = "Keyboard",
                    Description = "mechanical keyboard",
                    Type = "Computer"
                };
                var mouse = new CustomObject
                {
                    Name = "Mouse",
                    Description = "mouse",
                    Type = "Computer"
                };

                context.CustomObject.AddRange(desktop, laptop, monitor, keyboard, mouse);

                context.Orders.AddRange(
                    new Order { Number = "12345", Date = DateTime.Today, CustomObjects = { desktop, laptop } },
                     new Order { Number = "12346", Date = DateTime.Today, CustomObjects = { keyboard, mouse } },
                     new Order { Number = "12347", Date = DateTime.Today, CustomObjects = { keyboard, mouse, desktop, laptop, monitor } }
                );

                context.SaveChanges();
            }
        }
    }
}
