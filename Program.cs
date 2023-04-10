using NLog;
using System.Linq;

public class BlogPosts
{
    public static Logger Logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
    static void Main(string[] args)
    {
        Logger.Info("Program started");
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
            Console.WriteLine("Enter q to quit");

            string response = Console.ReadLine().ToLower();
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
                        CreatePost(db);
                        break;
                    case "4":
                        Console.WriteLine("[Display posts]");
                        break;
                    case "q":
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
            finally
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine("Have a good one!");
    }

    private static void DisplayBlogs(BloggingContext db)
    {
        var query = db.Blogs.OrderBy(b => b.Name);

        Console.WriteLine($"{query.Count()} blogs returned: ");
        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }
    }

    private static void CreateBlog(BloggingContext db)
    {
        Console.Write("Enter a name for a new Blog: ");
        string name = Console.ReadLine();
        if (name == "")
        {
            Logger.Error("Blog name cannot be null");
            return;
        }

        Blog blog = new Blog { Name = name };
        db.AddBlog(blog);
        Logger.Info("Blog added - {name}", name);
    }

    private static void CreatePost(BloggingContext db)
    {
        // var query = db.Blogs.OrderBy(b => b.BlogId);
    }

    private static void DisplayPosts(BloggingContext db)
    {

    }
}
