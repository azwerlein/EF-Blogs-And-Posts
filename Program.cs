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
                        DisplayPosts(db);
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
        var query = db.Blogs.OrderBy(b => b.BlogId);

        Console.WriteLine("Please select the blog you would like to post to.");
        foreach (var item in query)
        {
            Console.WriteLine($"{item.BlogId}) {item.Name}");
        }
        
        string response = Console.ReadLine();
        int number;
        if (!int.TryParse(response, out number))
        {
            Logger.Error("Invalid blog ID: {response}", response);
            return;
        }

        Blog blog = query.Where(b => b.BlogId == number).FirstOrDefault();
        if (blog == null)
        {
            Logger.Error("There are no blogs saved with the ID {response}", response);
            return;
        }

        Console.WriteLine("Enter the post title");
        string title = Console.ReadLine();
        if (title == "")
        {
            Logger.Error("Post title cannot be null");
            return;
        }

        Console.WriteLine("Enter the post content");
        string content = Console.ReadLine();

        Post post = new Post { Title = title, Content = content, BlogId = blog.BlogId };
        // blog.Posts.Add(post);
        db.AddPost(post);

        Logger.Info("Post added - {title}", title);
    }

    private static void DisplayPosts(BloggingContext db)
    {
        IQueryable<Blog> query = db.Blogs.OrderBy(b => b.BlogId);

        Console.WriteLine("Select the blog's posts to display:");
        Console.WriteLine("0) Posts from all blogs");
        foreach (var item in query)
        {
            Console.WriteLine($"{item.BlogId}) Posts from {item.Name}");
        }

        string response = Console.ReadLine();
        int number = 0; // If there's an invalid response, default to 0.
        int.TryParse(response, out number);

        IQueryable<Post> posts;
        if (number != 0)
        {
            posts = db.Posts.Where(b => b.BlogId == number);
            foreach (var post in posts)
            {
                Console.WriteLine($"Blog: {post.Blog.Name}");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Content: {post.Content}");
                Console.WriteLine();
            }
        } else {
            // TODO: No Posts
        }

        
    }
}
