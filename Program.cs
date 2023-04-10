using NLog;

public class BlogPosts
{
    public static Logger Logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");

        Console.WriteLine("Please enter your selection.");
        Console.WriteLine("1) Display all blogs");
        Console.WriteLine("2) Add blog");
        Console.WriteLine("3) Create Post");
        Console.WriteLine("4) Display Posts");

        string response = Console.ReadLine();

        switch (response)
        {
            case "1":
                Console.WriteLine("[Display blogs]");
                break;
            case "2":
                CreateBlog();
                break;
            case "3":
                Console.WriteLine("[Create post]");
                break;
            case "4":
                Console.WriteLine("[Display posts]");
                break;
            default:
                Console.WriteLine("Default response");
                break;
        }

        Console.WriteLine("Have a good one!");
    }

    private static void CreateBlog()
    {
        Console.Write("Enter a name for the new Blog: ");
        string name = Console.ReadLine();

        Blog blog = new Blog { Name = name };

        BloggingContext db = new BloggingContext();
        db.AddBlog(blog);
        Logger.Info("Blog added - {name}", name);
    }
}
