using NLog;
using System.Linq;

public class BlogPosts
{
    public static Logger Logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome!");

        BloggingContext db = new BloggingContext();

        bool loop = true;
        while (loop)
        {
            Console.WriteLine("Please enter your selection.");
            Console.WriteLine("1) Display all blogs");
            Console.WriteLine("2) Add blog");
            Console.WriteLine("3) Create post");
            Console.WriteLine("4) Display posts");
            Console.WriteLine("5) Exit the program");

            string response = Console.ReadLine();
            try
            {
                switch (response)
                {
                    case "1":
                        DisplayBlogs(db);
                        break;
                    case "2":
                        CreateBlog(db);
                        break;
                    case "3":
                        Console.WriteLine("[Create post]");
                        break;
                    case "4":
                        Console.WriteLine("[Display posts]");
                        break;
                    case "5":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response!");
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        Console.WriteLine("Have a good one!");
    }

    private static void DisplayBlogs(BloggingContext db)
    {
        var query = db.Blogs.OrderBy(b => b.Name);

        Console.WriteLine("All blogs in the database:");
        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }
    }

    private static void CreateBlog(BloggingContext db)
    {
        Console.Write("Enter a name for the new Blog: ");
        string name = Console.ReadLine();

        Blog blog = new Blog { Name = name };
        db.AddBlog(blog);
        Logger.Info("Blog added - {name}", name);
    }
}
