using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TechShopConsole
{
    #region Models

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public bool IsAdmin { get; set; }
        public List<int> OrderHistory { get; set; } = new();
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public bool Availability { get; set; } = true;

        public virtual string ShortInfo() => $"{Id}. {Name} – {Brand} {Model} – {Price:C}";
        public virtual string FullInfo() => $"{ShortInfo()}\nКатегорія: {Category}\nОпис: {Description}\nНаявність: {(Availability ? "так" : "ні")}";
    }

    public class Smartphone : Product
    {
        public double ScreenSize { get; set; }
        public string Resolution { get; set; } = "";
        public string Processor { get; set; } = "";
        public int Ram { get; set; }
        public int Storage { get; set; }
        public int CameraMp { get; set; }
        public int BatteryCapacity { get; set; }
        public string OperatingSystem { get; set; } = "";
        public string Color { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nДіагональ: {ScreenSize}\" | Роздільна здатність: {Resolution}\nПроцесор: {Processor} | RAM: {Ram}GB | Пам'ять: {Storage}GB\nКамера: {CameraMp}MP | Батарея: {BatteryCapacity}mAh | OS: {OperatingSystem} | Колір: {Color}";
    }

    public class Laptop : Product
    {
        public double ScreenSize { get; set; }
        public string ProcessorType { get; set; } = "";
        public double ProcessorSpeed { get; set; }
        public int RamSize { get; set; }
        public string StorageType { get; set; } = "";
        public int StorageCapacity { get; set; }
        public string GraphicsCard { get; set; } = "";
        public int BatteryLife { get; set; }
        public double Weight { get; set; }

        public override string FullInfo() => base.FullInfo() +
            $"\nДіагональ: {ScreenSize}\" | Процесор: {ProcessorType} {ProcessorSpeed}GHz\nRAM: {RamSize}GB | Накопичувач: {StorageType} {StorageCapacity}GB\nВідеокарта: {GraphicsCard} | Батарея: {BatteryLife}год | Вага: {Weight}кг";
    }

    public class Television : Product
    {
        public double ScreenSize { get; set; }
        public string Resolution { get; set; } = "";
        public string DisplayTechnology { get; set; } = "";
        public bool SmartTv { get; set; }
        public bool HdrSupport { get; set; }
        public int RefreshRate { get; set; }
        public string Connectivity { get; set; } = "";
        public string EnergyClass { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nДіагональ: {ScreenSize}\" | Роздільна здатність: {Resolution}\nТехнологія: {DisplayTechnology} | Smart TV: {(SmartTv ? "так" : "ні")}\nHDR: {(HdrSupport ? "так" : "ні")} | Частота: {RefreshRate}Hz | Клас енергоефективності: {EnergyClass}";
    }

    public class Tablet : Product
    {
        public double ScreenSize { get; set; }
        public string Resolution { get; set; } = "";
        public string Processor { get; set; } = "";
        public int Ram { get; set; }
        public int Storage { get; set; }
        public string OperatingSystem { get; set; } = "";
        public string Connectivity { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nДіагональ: {ScreenSize}\" | Роздільна здатність: {Resolution}\nПроцесор: {Processor} | RAM: {Ram}GB | ПЗП: {Storage}GB\nOS: {OperatingSystem} | Зв'язок: {Connectivity}";
    }

    public class Headphones : Product
    {
        public string Type { get; set; } = "";
        public bool Wireless { get; set; }
        public bool NoiseCancellation { get; set; }
        public string FrequencyResponse { get; set; } = "";
        public int BatteryLife { get; set; }
        public string BluetoothVersion { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nТип: {Type} | Бездротові: {(Wireless ? "так" : "ні")} | Шумопригнічення: {(NoiseCancellation ? "так" : "ні")}\nЧастотна: {FrequencyResponse} | Автономність: {BatteryLife} год | Bluetooth: {BluetoothVersion}";
    }

    public class Camera : Product
    {
        public string CameraType { get; set; } = "";
        public double Megapixels { get; set; }
        public string SensorSize { get; set; } = "";
        public string VideoResolution { get; set; } = "";
        public string LensMount { get; set; } = "";
        public double Weight { get; set; }

        public override string FullInfo() => base.FullInfo() +
            $"\nТип: {CameraType} | МП: {Megapixels}MP | Матриця: {SensorSize}\nВідео: {VideoResolution} | Байонет: {LensMount} | Вага: {Weight} кг";
    }

    public class GameConsole : Product
    {
        public string Generation { get; set; } = "";
        public int StorageCapacity { get; set; }
        public string SupportedResolution { get; set; } = "";
        public bool BackwardCompatibility { get; set; }
        public string OnlineService { get; set; } = "";
        public string ControllerType { get; set; } = "";
        public string ExclusiveGames { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nПокоління: {Generation} | Пам'ять: {StorageCapacity}GB | Підтримка: {SupportedResolution}\nЗворотна сумісність: {(BackwardCompatibility ? "так" : "ні")}\nОнлайн сервіс: {OnlineService} | Контролер: {ControllerType}\nЕксклюзиви: {ExclusiveGames}";
    }

    public class SmartWatch : Product
    {
        public string DisplayType { get; set; } = "";
        public double DisplaySize { get; set; }
        public string OperatingSystem { get; set; } = "";
        public string BatteryLife { get; set; } = "";
        public string WaterResistance { get; set; } = "";
        public string HealthSensors { get; set; } = "";
        public bool GPS { get; set; }
        public string Connectivity { get; set; } = "";
        public string StrapMaterial { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nДисплей: {DisplayType} {DisplaySize}\" | OS: {OperatingSystem}\nБатарея: {BatteryLife} | Водозахист: {WaterResistance} | GPS: {(GPS ? "так" : "ні")}\nДатчики: {HealthSensors} | Ремінець: {StrapMaterial}";
    }

    public class Router : Product
    {
        public string WifiStandard { get; set; } = "";
        public string MaxSpeed { get; set; } = "";
        public string FrequencyBands { get; set; } = "";
        public int AntennaCount { get; set; }
        public int EthernetPorts { get; set; }
        public bool MeshSupport { get; set; }
        public string CoverageArea { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nСтандарт: {WifiStandard} | Швидкість: {MaxSpeed}\nЧастоти: {FrequencyBands} | Антени: {AntennaCount} | Ethernet порти: {EthernetPorts}\nMesh: {(MeshSupport ? "так" : "ні")} | Покриття: {CoverageArea}";
    }

    public class Printer : Product
    {
        public string PrintTechnology { get; set; } = "";
        public string PrintSpeed { get; set; } = "";
        public string MaxResolution { get; set; } = "";
        public string PaperSizes { get; set; } = "";
        public string Connectivity { get; set; } = "";
        public bool DuplexPrinting { get; set; }
        public string InkType { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nТехнологія: {PrintTechnology} | Швидкість: {PrintSpeed} | Роздільна здатність: {MaxResolution}\nФормати паперу: {PaperSizes} | Duplex: {(DuplexPrinting ? "так" : "ні")}\nЧорнило: {InkType} | Підключення: {Connectivity}";
    }

    public class PowerBank : Product
    {
        public int Capacity { get; set; }
        public string OutputPower { get; set; } = "";
        public int UsbPortsCount { get; set; }
        public bool FastCharging { get; set; }
        public bool WirelessCharging { get; set; }
        public double Weight { get; set; }

        public override string FullInfo() => base.FullInfo() +
            $"\nЄмність: {Capacity} mAh | Порти USB: {UsbPortsCount}\nВихідна потужність: {OutputPower} | Швидка зарядка: {(FastCharging ? "так" : "ні")}\nБездротова зарядка: {(WirelessCharging ? "так" : "ні")} | Вага: {Weight}г";
    }

    public class Accessory : Product
    {
        public string Compatibility { get; set; } = "";
        public string Material { get; set; } = "";
        public string Color { get; set; } = "";
        public string WarrantyPeriod { get; set; } = "";
        public string SpecialFeatures { get; set; } = "";

        public override string FullInfo() => base.FullInfo() +
            $"\nСумісність: {Compatibility} | Матеріал: {Material} | Колір: {Color}\nГарантія: {WarrantyPeriod} | Особливості: {SpecialFeatures}";
    }

    public class CartItem
    {
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string CustomerName { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Очікує оцінки";
        public string DeliveryAddress { get; set; } = "";
        public List<OrderItem> Items { get; set; } = new();
        public int? Rating { get; set; }
        public string ShopLocation { get; set; } = "TechShop";
    }

    public class OrderItem
    {
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    #endregion

    class Program
    {
        static List<User> users = new();
        static List<Product> catalog = new();
        static List<Order> orders = new();
        static User? currentUser = null;
        static List<CartItem> cart = new();

        const string USERS_FILE = "users.json";
        const string ORDERS_FILE = "orders.json";

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            LoadData();
            SeedCatalog();

            while (true)
            {
                if (currentUser == null)
                {
                    LoginMenu();
                }
                else
                {
                    if (currentUser.IsAdmin)
                        AdminMenu();
                    else
                        MainMenu();
                }
            }
        }

        #region Data Management

        static void LoadData()
        {
            if (File.Exists(USERS_FILE))
            {
                try
                {
                    var json = File.ReadAllText(USERS_FILE);
                    users = JsonSerializer.Deserialize<List<User>>(json) ?? new();
                }
                catch { users = new(); }
            }

            if (users.Count == 0)
            {
                users = new List<User>
                {
                    new User { UserId = 1, Username = "viktor", Password = "pass123", Email = "viktor.tushynskyi@techshop.ua", FullName = "Віктор Тушинський", IsAdmin = false },
                    new User { UserId = 2, Username = "vlad", Password = "vlad2024", Email = "vlad.khmil@techshop.ua", FullName = "Влад Хмільовський", IsAdmin = false },
                    new User { UserId = 3, Username = "oleksandr", Password = "malynskyi!", Email = "oleksandr.mal@techshop.ua", FullName = "Олександр Малинський", IsAdmin = false },
                    new User { UserId = 4, Username = "admin", Password = "admin2024", Email = "admin@techshop.ua", FullName = "Адміністратор", IsAdmin = true }
                };
                SaveUsers();
            }

            if (File.Exists(ORDERS_FILE))
            {
                try
                {
                    var json = File.ReadAllText(ORDERS_FILE);
                    orders = JsonSerializer.Deserialize<List<Order>>(json) ?? new();
                }
                catch { orders = new(); }
            }
        }

        static void SaveUsers()
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(USERS_FILE, json);
        }

        static void SaveOrders()
        {
            var json = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ORDERS_FILE, json);
        }

        #endregion

        #region Authentication

        static void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║   ІНТЕРНЕТ-МАГАЗИН ТЕХНІКИ - АВТОРИЗАЦІЯ   ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine("\n1. Увійти");
            Console.WriteLine("2. Вийти з програми");
            Console.Write("\nВиберіть опцію: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Login(); break;
                case "2": Environment.Exit(0); break;
            }
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              ВХІД В СИСТЕМУ                ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            Console.Write("Логін: ");
            var username = Console.ReadLine();
            Console.Write("Пароль: ");
            var password = Console.ReadLine();

            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                currentUser = user;
                Console.WriteLine($"\n✓ Вітаємо, {user.FullName}!");
                System.Threading.Thread.Sleep(1500);
            }
            else
            {
                Console.WriteLine("\n✗ Невірний логін або пароль!");
                Console.WriteLine("Натисніть Enter...");
                Console.ReadLine();
            }
        }

        static void Logout()
        {
            currentUser = null;
            cart.Clear();
        }

        #endregion

        #region Admin Menu

        static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║          ПАНЕЛЬ АДМІНІСТРАТОРА             ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.WriteLine($"\nВи увійшли як: {currentUser!.FullName}");
                Console.WriteLine("\n1. Переглянути всі замовлення");
                Console.WriteLine("2. Переглянути користувачів");
                Console.WriteLine("3. Статистика");
                Console.WriteLine("4. Вийти з акаунту");
                Console.Write("\nВиберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewAllOrders(); break;
                    case "2": ViewAllUsers(); break;
                    case "3": ViewStatistics(); break;
                    case "4":
                        CheckPendingOrders();
                        Logout();
                        return;
                }
            }
        }

        static void CheckPendingOrders()
        {
            var pendingOrders = orders.Where(o => o.Status == "Очікує оцінки" && !o.Rating.HasValue).ToList();

            if (pendingOrders.Any())
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║        УВАГА: ОЧІКУЮЧІ ЗАМОВЛЕННЯ!         ║");
                Console.WriteLine("╚════════════════════════════════════════════╝\n");

                var groupedByUser = pendingOrders.GroupBy(o => o.CustomerName);

                foreach (var group in groupedByUser)
                {
                    var orderCount = group.Count();
                    var orderWord = orderCount == 1 ? "замовлення" : orderCount < 5 ? "замовлення" : "замовлень";
                    Console.WriteLine($"► {group.Key} очікує {orderCount} свого {orderWord}");
                    Console.WriteLine($"  Оцінка: 5 (Вони приходили в {group.First().ShopLocation})");
                    Console.WriteLine();
                }

                Console.WriteLine("\nНатисніть Enter для продовження...");
                Console.ReadLine();
            }
        }

        static void ViewAllOrders()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║           ВСІ ЗАМОВЛЕННЯ В СИСТЕМІ         ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            if (!orders.Any())
            {
                Console.WriteLine("Замовлень поки немає.");
            }
            else
            {
                foreach (var order in orders.OrderByDescending(o => o.OrderDate))
                {
                    Console.WriteLine($"═══════════════════════════════════════════");
                    Console.WriteLine($"Замовлення #{order.OrderId}");
                    Console.WriteLine($"Клієнт: {order.CustomerName}");
                    Console.WriteLine($"Дата: {order.OrderDate:dd.MM.yyyy HH:mm}");
                    Console.WriteLine($"Сума: {order.TotalAmount:C}");
                    Console.WriteLine($"Статус: {order.Status}");
                    Console.WriteLine($"Адреса: {order.DeliveryAddress}");
                    Console.WriteLine($"Магазин: {order.ShopLocation}");
                    if (order.Rating.HasValue)
                        Console.WriteLine($"Оцінка: {order.Rating}/5");
                    Console.WriteLine($"\nТовари:");
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"  • {item.ProductName} x{item.Quantity} = {item.Price * item.Quantity:C}");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }

        static void ViewAllUsers()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║        КОРИСТУВАЧІ В СИСТЕМІ               ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            foreach (var user in users.OrderBy(u => u.UserId))
            {
                Console.WriteLine($"═══════════════════════════════════════════");
                Console.WriteLine($"ID: {user.UserId}");
                Console.WriteLine($"Ім'я: {user.FullName}");
                Console.WriteLine($"Логін: {user.Username}");
                Console.WriteLine($"Email: {user.Email}");
                Console.WriteLine($"Роль: {(user.IsAdmin ? "Адміністратор" : "Користувач")}");
                Console.WriteLine($"Замовлень: {user.OrderHistory.Count}");
                Console.WriteLine();
            }

            Console.WriteLine("Натисніть Enter...");
            Console.ReadLine();
        }

        static void ViewStatistics()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              СТАТИСТИКА                    ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            Console.WriteLine($"Всього користувачів: {users.Count(u => !u.IsAdmin)}");
            Console.WriteLine($"Всього замовлень: {orders.Count}");
            Console.WriteLine($"Загальна сума продажів: {orders.Sum(o => o.TotalAmount):C}");
            Console.WriteLine($"Середній чек: {(orders.Any() ? orders.Average(o => o.TotalAmount) : 0):C}");
            Console.WriteLine($"\nЗамовлень з оцінкою: {orders.Count(o => o.Rating.HasValue)}");
            Console.WriteLine($"Очікують оцінки: {orders.Count(o => !o.Rating.HasValue && o.Status == "Очікує оцінки")}");

            if (orders.Any(o => o.Rating.HasValue))
            {
                Console.WriteLine($"Середня оцінка: {orders.Where(o => o.Rating.HasValue).Average(o => o.Rating.Value):F2}/5");
            }

            Console.WriteLine("\n\nТоп-3 активних клієнтів:");
            var topUsers = users.Where(u => !u.IsAdmin)
                .OrderByDescending(u => u.OrderHistory.Count)
                .Take(3);

            int pos = 1;
            foreach (var user in topUsers)
            {
                Console.WriteLine($"{pos}. {user.FullName} - {user.OrderHistory.Count} замовлень");
                pos++;
            }

            Console.WriteLine("\n\nНатисніть Enter...");
            Console.ReadLine();
        }

        #endregion

        #region Main Menu

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║      ІНТЕРНЕТ-МАГАЗИН ТЕХНІКИ              ║");
                Console.WriteLine("╚════════════════════════════════════════════╝");
                Console.WriteLine($"\nВітаємо, {currentUser!.FullName}!");
                Console.WriteLine($"Email: {currentUser.Email}");
                Console.WriteLine("\n1. Переглянути каталог");
                Console.WriteLine("2. Пошук товару");
                Console.WriteLine("3. Кошик");
                Console.WriteLine("4. Мої замовлення");
                Console.WriteLine("5. Вийти з акаунту");
                Console.Write("\nВиберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ShowCategories(); break;
                    case "2": SearchByName(); break;
                    case "3": ShowCart(); break;
                    case "4": ViewMyOrders(); break;
                    case "5": Logout(); return;
                }
            }
        }

        #endregion

        #region Catalog

        static void ShowCategories()
        {
            var cats = catalog.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║              КАТЕГОРІЇ ТОВАРІВ             ║");
                Console.WriteLine("╚════════════════════════════════════════════╝\n");

                for (int i = 0; i < cats.Count; i++)
                    Console.WriteLine($"{i + 1}. {cats[i]}");

                Console.WriteLine("0. Назад");
                Console.Write("\nВиберіть категорію: ");

                if (!int.TryParse(Console.ReadLine(), out int sel) || sel < 0 || sel > cats.Count)
                    continue;

                if (sel == 0) return;

                ShowProductsByCategory(cats[sel - 1]);
            }
        }

        static void ShowProductsByCategory(string category)
        {
            var items = catalog.Where(p => p.Category == category).ToList();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"╔════════════════════════════════════════════╗");
                Console.WriteLine($"║  {category.ToUpper().PadRight(42)}║");
                Console.WriteLine($"╚════════════════════════════════════════════╝\n");

                foreach (var p in items)
                    Console.WriteLine(p.ShortInfo());

                Console.WriteLine("\nВведіть ID товару для перегляду або 0 для повернення");
                Console.Write("ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id)) continue;
                if (id == 0) return;

                var prod = catalog.FirstOrDefault(x => x.Id == id);
                if (prod == null)
                {
                    Console.WriteLine("Товар не знайдено. Натисніть Enter...");
                    Console.ReadLine();
                    continue;
                }

                ShowProductDetails(prod);
            }
        }

        static void ShowProductDetails(Product p)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║            ДЕТАЛІ ТОВАРУ                   ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");
            Console.WriteLine(p.FullInfo());
            Console.WriteLine("\n\n1. Додати в кошик");
            Console.WriteLine("0. Назад");
            Console.Write("\nВибір: ");

            var c = Console.ReadLine();
            if (c == "1")
            {
                Console.Write("Кількість: ");
                if (!int.TryParse(Console.ReadLine(), out int q) || q <= 0) q = 1;
                AddToCart(p, q);
                Console.WriteLine("\n✓ Товар додано в кошик!");
                System.Threading.Thread.Sleep(1000);
            }
        }

        static void AddToCart(Product p, int q)
        {
            var existing = cart.FirstOrDefault(ci => ci.Product.Id == p.Id);
            if (existing != null)
                existing.Quantity += q;
            else
                cart.Add(new CartItem { Product = p, Quantity = q });
        }

        static void SearchByName()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║              ПОШУК ТОВАРУ                  ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");
            Console.Write("Введіть назву товару: ");

            var q = Console.ReadLine()?.ToLower() ?? "";
            var results = catalog.Where(p => p.Name.ToLower().Contains(q) ||
                                             p.Brand.ToLower().Contains(q) ||
                                             p.Model.ToLower().Contains(q)).ToList();

            Console.Clear();
            Console.WriteLine($"Знайдено товарів: {results.Count}\n");

            if (!results.Any())
            {
                Console.WriteLine("Нічого не знайдено.");
                Console.WriteLine("\nНатисніть Enter...");
                Console.ReadLine();
                return;
            }

            foreach (var r in results)
                Console.WriteLine(r.ShortInfo());

            Console.WriteLine("\nВведіть ID для перегляду або 0 для повернення");
            Console.Write("ID: ");

            if (int.TryParse(Console.ReadLine(), out int id) && id != 0)
            {
                var prod = catalog.FirstOrDefault(x => x.Id == id);
                if (prod != null)
                    ShowProductDetails(prod);
            }
        }

        #endregion

        #region Cart

        static void ShowCart()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║                КОШИК                       ║");
                Console.WriteLine("╚════════════════════════════════════════════╝\n");

                if (!cart.Any())
                {
                    Console.WriteLine("Кошик порожній.");
                    Console.WriteLine("\nНатисніть Enter...");
                    Console.ReadLine();
                    return;
                }

                decimal total = 0;
                Console.WriteLine("Товари в кошику:\n");

                foreach (var ci in cart)
                {
                    var subtotal = ci.Product.Price * ci.Quantity;
                    total += subtotal;
                    Console.WriteLine($"{ci.Product.Id}. {ci.Product.Name}");
                    Console.WriteLine($"   {ci.Product.Brand} {ci.Product.Model}");
                    Console.WriteLine($"   {ci.Product.Price:C} x {ci.Quantity} = {subtotal:C}\n");
                }

                Console.WriteLine($"{'─',50}");
                Console.WriteLine($"ЗАГАЛОМ: {total:C}");
                Console.WriteLine($"{'─',50}\n");

                Console.WriteLine("1. Видалити товар");
                Console.WriteLine("2. Очистити кошик");
                Console.WriteLine("3. Оформити замовлення");
                Console.WriteLine("0. Назад");
                Console.Write("\nВибір: ");

                var c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        Console.Write("Введіть ID товару для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var item = cart.FirstOrDefault(x => x.Product.Id == id);
                            if (item != null)
                            {
                                cart.Remove(item);
                                Console.WriteLine("✓ Товар видалено!");
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                        break;
                    case "2":
                        cart.Clear();
                        Console.WriteLine("✓ Кошик очищено!");
                        System.Threading.Thread.Sleep(1000);
                        break;
                    case "3":
                        Checkout();
                        return;
                    case "0":
                        return;
                }
            }
        }

        static void Checkout()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║         ОФОРМЛЕННЯ ЗАМОВЛЕННЯ              ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            if (!cart.Any())
            {
                Console.WriteLine("Кошик порожній!");
                Console.WriteLine("\nНатисніть Enter...");
                Console.ReadLine();
                return;
            }

            decimal total = cart.Sum(ci => ci.Product.Price * ci.Quantity);

            Console.WriteLine($"Покупець: {currentUser!.FullName}");
            Console.WriteLine($"Email: {currentUser.Email}");
            Console.WriteLine($"Сума замовлення: {total:C}\n");

            Console.Write("Введіть адресу доставки: ");
            var address = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(address))
            {
                Console.WriteLine("\n✗ Адреса не може бути порожньою!");
                Console.WriteLine("Натисніть Enter...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════════════════╗");
            Console.WriteLine("║           ПІДТВЕРДЖЕННЯ ЗАМОВЛЕННЯ         ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.WriteLine($"\nКлієнт: {currentUser.FullName}");
            Console.WriteLine($"Адреса: {address}");
            Console.WriteLine($"Сума: {total:C}");
            Console.WriteLine("\nТовари:");

            foreach (var ci in cart)
            {
                Console.WriteLine($"  • {ci.Product.Name} x{ci.Quantity} = {(ci.Product.Price * ci.Quantity):C}");
            }

            Console.Write("\nПідтвердити замовлення? (y/n): ");
            var conf = Console.ReadLine();

            if (conf?.ToLower() == "y")
            {
                var order = new Order
                {
                    OrderId = orders.Any() ? orders.Max(o => o.OrderId) + 1 : 1,
                    UserId = currentUser.UserId,
                    CustomerName = currentUser.FullName,
                    OrderDate = DateTime.Now,
                    TotalAmount = total,
                    Status = "Очікує оцінки",
                    DeliveryAddress = address,
                    ShopLocation = "TechShop",
                    Items = cart.Select(ci => new OrderItem
                    {
                        ProductName = ci.Product.Name,
                        Quantity = ci.Quantity,
                        Price = ci.Product.Price
                    }).ToList()
                };

                orders.Add(order);
                currentUser.OrderHistory.Add(order.OrderId);

                SaveOrders();
                SaveUsers();

                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════╗");
                Console.WriteLine("║              ДЯКУЄМО!                      ║");
                Console.WriteLine("╚════════════════════════════════════════════╝\n");
                Console.WriteLine($"✓ Замовлення #{order.OrderId} успішно оформлено!");
                Console.WriteLine($"\nДата: {order.OrderDate:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"Сума: {order.TotalAmount:C}");
                Console.WriteLine($"Адреса доставки: {order.DeliveryAddress}");
                Console.WriteLine($"Статус: {order.Status}");
                Console.WriteLine($"\nМагазин: {order.ShopLocation}");
                Console.WriteLine("\nОчікуйте на доставку!");

                cart.Clear();

                Console.WriteLine("\n\nНатисніть Enter...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n✗ Замовлення скасовано.");
                Console.WriteLine("Натисніть Enter...");
                Console.ReadLine();
            }
        }

        #endregion

        #region Orders

        static void ViewMyOrders()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║            МОЇ ЗАМОВЛЕННЯ                  ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            var myOrders = orders.Where(o => o.UserId == currentUser!.UserId)
                                 .OrderByDescending(o => o.OrderDate)
                                 .ToList();

            if (!myOrders.Any())
            {
                Console.WriteLine("У вас поки немає замовлень.");
                Console.WriteLine("\nНатисніть Enter...");
                Console.ReadLine();
                return;
            }

            foreach (var order in myOrders)
            {
                Console.WriteLine($"═══════════════════════════════════════════");
                Console.WriteLine($"Замовлення #{order.OrderId}");
                Console.WriteLine($"Дата: {order.OrderDate:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"Сума: {order.TotalAmount:C}");
                Console.WriteLine($"Статус: {order.Status}");
                Console.WriteLine($"Адреса: {order.DeliveryAddress}");
                Console.WriteLine($"Магазин: {order.ShopLocation}");

                if (order.Rating.HasValue)
                {
                    Console.WriteLine($"Ваша оцінка: {order.Rating}/5 ⭐");
                }
                else
                {
                    Console.WriteLine("Оцінка: не залишена");
                }

                Console.WriteLine("\nТовари:");
                foreach (var item in order.Items)
                {
                    Console.WriteLine($"  • {item.ProductName} x{item.Quantity} = {item.Price * item.Quantity:C}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nБажаєте залишити оцінку для замовлення? (y/n)");
            var rate = Console.ReadLine();

            if (rate?.ToLower() == "y")
            {
                Console.Write("Введіть номер замовлення: ");
                if (int.TryParse(Console.ReadLine(), out int orderId))
                {
                    var order = myOrders.FirstOrDefault(o => o.OrderId == orderId);
                    if (order != null)
                    {
                        if (order.Rating.HasValue)
                        {
                            Console.WriteLine("\n✗ Ви вже залишили оцінку для цього замовлення!");
                        }
                        else
                        {
                            Console.Write("Введіть оцінку (1-5): ");
                            if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 5)
                            {
                                order.Rating = rating;
                                order.Status = "Завершено";
                                SaveOrders();
                                Console.WriteLine($"\n✓ Дякуємо! Ви оцінили замовлення на {rating}/5 ⭐");
                            }
                            else
                            {
                                Console.WriteLine("\n✗ Невірна оцінка!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n✗ Замовлення не знайдено!");
                    }
                }
            }

            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }

        #endregion

        #region Seed Data

        static void SeedCatalog()
        {
            if (catalog.Any()) return;

            int id = 1;

            // Смартфони
            catalog.Add(new Smartphone
            {
                Id = id++,
                Name = "Galaxy S24 Ultra",
                Brand = "Samsung",
                Model = "S24U",
                Category = "Смартфони",
                Description = "Флагманський смартфон з потужним процесором",
                Price = 42999m,
                Availability = true,
                ScreenSize = 6.8,
                Resolution = "3088x1440",
                Processor = "Snapdragon 8 Gen 3",
                Ram = 12,
                Storage = 256,
                CameraMp = 200,
                BatteryCapacity = 5000,
                OperatingSystem = "Android 14",
                Color = "Чорний"
            });

            catalog.Add(new Smartphone
            {
                Id = id++,
                Name = "iPhone 15 Pro",
                Brand = "Apple",
                Model = "15Pro",
                Category = "Смартфони",
                Description = "Преміум iPhone з титановим корпусом",
                Price = 46999m,
                Availability = true,
                ScreenSize = 6.1,
                Resolution = "2556x1179",
                Processor = "A17 Pro",
                Ram = 8,
                Storage = 256,
                CameraMp = 48,
                BatteryCapacity = 3274,
                OperatingSystem = "iOS 17",
                Color = "Титановий синій"
            });

            // Ноутбуки
            catalog.Add(new Laptop
            {
                Id = id++,
                Name = "MacBook Pro 14",
                Brand = "Apple",
                Model = "MBP14M3",
                Category = "Ноутбуки",
                Description = "Потужний ноутбук для професіоналів",
                Price = 79999m,
                Availability = true,
                ScreenSize = 14.2,
                ProcessorType = "Apple M3 Pro",
                ProcessorSpeed = 3.5,
                RamSize = 18,
                StorageType = "SSD",
                StorageCapacity = 512,
                GraphicsCard = "Інтегрована M3 Pro",
                BatteryLife = 18,
                Weight = 1.55
            });

            catalog.Add(new Laptop
            {
                Id = id++,
                Name = "ThinkPad X1 Carbon",
                Brand = "Lenovo",
                Model = "X1C11",
                Category = "Ноутбуки",
                Description = "Бізнес-ноутбук преміум класу",
                Price = 54999m,
                Availability = true,
                ScreenSize = 14,
                ProcessorType = "Intel Core i7-1355U",
                ProcessorSpeed = 5.0,
                RamSize = 16,
                StorageType = "SSD",
                StorageCapacity = 512,
                GraphicsCard = "Intel Iris Xe",
                BatteryLife = 12,
                Weight = 1.12
            });

            // Телевізори
            catalog.Add(new Television
            {
                Id = id++,
                Name = "OLED C3 65",
                Brand = "LG",
                Model = "OLEDC365",
                Category = "Телевізори",
                Description = "Преміум OLED телевізор з AI процесором",
                Price = 69999m,
                Availability = true,
                ScreenSize = 65,
                Resolution = "3840x2160",
                DisplayTechnology = "OLED",
                SmartTv = true,
                HdrSupport = true,
                RefreshRate = 120,
                Connectivity = "HDMI 2.1, Wi-Fi 6, Bluetooth 5.0",
                EnergyClass = "A"
            });

            catalog.Add(new Television
            {
                Id = id++,
                Name = "Neo QLED QN90C",
                Brand = "Samsung",
                Model = "QN90C55",
                Category = "Телевізори",
                Description = "Яскравий QLED з технологією Mini LED",
                Price = 59999m,
                Availability = true,
                ScreenSize = 55,
                Resolution = "3840x2160",
                DisplayTechnology = "Neo QLED",
                SmartTv = true,
                HdrSupport = true,
                RefreshRate = 144,
                Connectivity = "HDMI 2.1, Wi-Fi 6E",
                EnergyClass = "A+"
            });

            // Планшети
            catalog.Add(new Tablet
            {
                Id = id++,
                Name = "iPad Pro 12.9",
                Brand = "Apple",
                Model = "iPadPro12.9M2",
                Category = "Планшети",
                Description = "Професійний планшет з M2 чіпом",
                Price = 49999m,
                Availability = true,
                ScreenSize = 12.9,
                Resolution = "2732x2048",
                Processor = "Apple M2",
                Ram = 8,
                Storage = 256,
                OperatingSystem = "iPadOS 17",
                Connectivity = "Wi-Fi 6E, 5G"
            });

            catalog.Add(new Tablet
            {
                Id = id++,
                Name = "Galaxy Tab S9",
                Brand = "Samsung",
                Model = "TabS9",
                Category = "Планшети",
                Description = "Потужний Android планшет",
                Price = 34999m,
                Availability = true,
                ScreenSize = 11,
                Resolution = "2560x1600",
                Processor = "Snapdragon 8 Gen 2",
                Ram = 12,
                Storage = 256,
                OperatingSystem = "Android 13",
                Connectivity = "Wi-Fi 6E, 5G"
            });

            // Навушники
            catalog.Add(new Headphones
            {
                Id = id++,
                Name = "AirPods Pro 2",
                Brand = "Apple",
                Model = "APP2USBC",
                Category = "Навушники",
                Description = "TWS навушники з активним шумозаглушенням",
                Price = 10999m,
                Availability = true,
                Type = "Вкладні TWS",
                Wireless = true,
                NoiseCancellation = true,
                FrequencyResponse = "20Hz-20kHz",
                BatteryLife = 6,
                BluetoothVersion = "5.3"
            });

            catalog.Add(new Headphones
            {
                Id = id++,
                Name = "WH-1000XM5",
                Brand = "Sony",
                Model = "WH1000XM5",
                Category = "Навушники",
                Description = "Накладні навушники з найкращим ANC",
                Price = 13999m,
                Availability = true,
                Type = "Накладні",
                Wireless = true,
                NoiseCancellation = true,
                FrequencyResponse = "4Hz-40kHz",
                BatteryLife = 30,
                BluetoothVersion = "5.2"
            });

            // Камери
            catalog.Add(new Camera
            {
                Id = id++,
                Name = "Alpha A7 IV",
                Brand = "Sony",
                Model = "ILCE7M4",
                Category = "Фотокамери",
                Description = "Універсальна повнокадрова камера",
                Price = 94999m,
                Availability = true,
                CameraType = "Бездзеркальна",
                Megapixels = 33,
                SensorSize = "Full Frame",
                VideoResolution = "4K 60fps",
                LensMount = "Sony E",
                Weight = 0.658
            });

            catalog.Add(new Camera
            {
                Id = id++,
                Name = "EOS R6 Mark II",
                Brand = "Canon",
                Model = "R6II",
                Category = "Фотокамери",
                Description = "Швидка камера для фото і відео",
                Price = 109999m,
                Availability = true,
                CameraType = "Бездзеркальна",
                Megapixels = 24,
                SensorSize = "Full Frame",
                VideoResolution = "4K 60fps",
                LensMount = "Canon RF",
                Weight = 0.670
            });

            // Ігрові консолі
            catalog.Add(new GameConsole
            {
                Id = id++,
                Name = "PlayStation 5",
                Brand = "Sony",
                Model = "PS5Slim",
                Category = "Ігрові консолі",
                Description = "Консоль нового покоління",
                Price = 21999m,
                Availability = true,
                Generation = "9",
                StorageCapacity = 1000,
                SupportedResolution = "8K, 4K 120fps",
                BackwardCompatibility = true,
                OnlineService = "PlayStation Plus",
                ControllerType = "DualSense",
                ExclusiveGames = "Spider-Man 2, God of War Ragnarök"
            });

            catalog.Add(new GameConsole
            {
                Id = id++,
                Name = "Xbox Series X",
                Brand = "Microsoft",
                Model = "SeriesX",
                Category = "Ігрові консолі",
                Description = "Найпотужніша консоль Xbox",
                Price = 23999m,
                Availability = true,
                Generation = "9",
                StorageCapacity = 1000,
                SupportedResolution = "8K, 4K 120fps",
                BackwardCompatibility = true,
                OnlineService = "Xbox Game Pass",
                ControllerType = "Xbox Wireless",
                ExclusiveGames = "Forza Horizon 5, Halo Infinite"
            });

            // Розумні годинники
            catalog.Add(new SmartWatch
            {
                Id = id++,
                Name = "Apple Watch Series 9",
                Brand = "Apple",
                Model = "AWS9",
                Category = "Розумні годинники",
                Description = "Розумний годинник з яскравим дисплеєм",
                Price = 17999m,
                Availability = true,
                DisplayType = "OLED",
                DisplaySize = 1.9,
                OperatingSystem = "watchOS 10",
                BatteryLife = "18 годин",
                WaterResistance = "50м",
                HealthSensors = "ЕКГ, пульсоксиметр, температура",
                GPS = true,
                Connectivity = "Bluetooth 5.3, Wi-Fi, LTE",
                StrapMaterial = "Силікон"
            });

            catalog.Add(new SmartWatch
            {
                Id = id++,
                Name = "Galaxy Watch 6",
                Brand = "Samsung",
                Model = "GW6",
                Category = "Розумні годинники",
                Description = "Стильний Android годинник",
                Price = 12999m,
                Availability = true,
                DisplayType = "Super AMOLED",
                DisplaySize = 1.5,
                OperatingSystem = "Wear OS 4",
                BatteryLife = "40 годин",
                WaterResistance = "50м",
                HealthSensors = "ЕКГ, пульсоксиметр, аналіз сну",
                GPS = true,
                Connectivity = "Bluetooth 5.3, Wi-Fi, LTE",
                StrapMaterial = "Шкіра"
            });

            // Роутери
            catalog.Add(new Router
            {
                Id = id++,
                Name = "RT-AX88U Pro",
                Brand = "ASUS",
                Model = "RTAX88UPRO",
                Category = "Маршрутизатори",
                Description = "Потужний ігровий роутер",
                Price = 14999m,
                Availability = true,
                WifiStandard = "Wi-Fi 6E",
                MaxSpeed = "6000 Мбіт/с",
                FrequencyBands = "2.4GHz, 5GHz, 6GHz",
                AntennaCount = 8,
                EthernetPorts = 8,
                MeshSupport = true,
                CoverageArea = "До 300 кв.м"
            });

            catalog.Add(new Router
            {
                Id = id++,
                Name = "Orbi WiFi 6E",
                Brand = "NETGEAR",
                Model = "RBKE963",
                Category = "Маршрутизатори",
                Description = "Mesh система для великих будинків",
                Price = 29999m,
                Availability = true,
                WifiStandard = "Wi-Fi 6E",
                MaxSpeed = "10800 Мбіт/с",
                FrequencyBands = "2.4GHz, 5GHz, 6GHz",
                AntennaCount = 12,
                EthernetPorts = 4,
                MeshSupport = true,
                CoverageArea = "До 600 кв.м"
            });

            // Принтери
            catalog.Add(new Printer
            {
                Id = id++,
                Name = "LaserJet Pro M404dn",
                Brand = "HP",
                Model = "M404dn",
                Category = "Принтери",
                Description = "Монохромний лазерний принтер",
                Price = 9999m,
                Availability = true,
                PrintTechnology = "Лазерний",
                PrintSpeed = "38 стор/хв",
                MaxResolution = "1200x1200 dpi",
                PaperSizes = "A4, Letter",
                Connectivity = "USB, Ethernet",
                DuplexPrinting = true,
                InkType = "Тонер"
            });

            catalog.Add(new Printer
            {
                Id = id++,
                Name = "PIXMA G6040",
                Brand = "Canon",
                Model = "G6040",
                Category = "Принтери",
                Description = "МФУ з системою СНПЧ",
                Price = 12999m,
                Availability = true,
                PrintTechnology = "Струменевий",
                PrintSpeed = "13 стор/хв",
                MaxResolution = "4800x1200 dpi",
                PaperSizes = "A4, Letter, 10x15",
                Connectivity = "USB, Wi-Fi, Ethernet",
                DuplexPrinting = true,
                InkType = "Пігментний + водорозчинний"
            });

            // Павербанки
            catalog.Add(new PowerBank
            {
                Id = id++,
                Name = "SuperCharger Pro",
                Brand = "Anker",
                Model = "PowerCore 26800",
                Category = "Павербанки",
                Description = "Потужний павербанк на 26800mAh",
                Price = 2499m,
                Availability = true,
                Capacity = 26800,
                OutputPower = "45W PD",
                UsbPortsCount = 3,
                FastCharging = true,
                WirelessCharging = false,
                Weight = 495
            });

            catalog.Add(new PowerBank
            {
                Id = id++,
                Name = "Qi Wireless 10K",
                Brand = "Xiaomi",
                Model = "WPB10000",
                Category = "Павербанки",
                Description = "Павербанк з бездротовою зарядкою",
                Price = 1299m,
                Availability = true,
                Capacity = 10000,
                OutputPower = "18W QC3.0",
                UsbPortsCount = 2,
                FastCharging = true,
                WirelessCharging = true,
                Weight = 220
            });

            // Аксесуари
            catalog.Add(new Accessory
            {
                Id = id++,
                Name = "MagSafe зарядний пристрій",
                Brand = "Apple",
                Model = "MagSafe15W",
                Category = "Аксесуари",
                Description = "Бездротова зарядка для iPhone",
                Price = 1999m,
                Availability = true,
                Compatibility = "iPhone 12-15",
                Material = "Алюміній, пластик",
                Color = "Білий",
                WarrantyPeriod = "1 рік",
                SpecialFeatures = "Магнітне кріплення, 15W"
            });

            catalog.Add(new Accessory
            {
                Id = id++,
                Name = "USB-C Hub 7в1",
                Brand = "Ugreen",
                Model = "CM512",
                Category = "Аксесуари",
                Description = "Багатофункціональний хаб",
                Price = 1499m,
                Availability = true,
                Compatibility = "MacBook, ноутбуки з USB-C",
                Material = "Алюміній",
                Color = "Сірий",
                WarrantyPeriod = "2 роки",
                SpecialFeatures = "HDMI 4K, USB 3.0, SD/microSD, PD 100W"
            });
        }

        #endregion
    }
}