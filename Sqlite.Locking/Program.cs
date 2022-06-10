using Microsoft.EntityFrameworkCore;
using Sqlite.Locking;

Actions.AddItem();
GC.Collect();
var lockers = FileUtil.WhoIsLocking(MyContext.DbFile);
var locker  = lockers.Any() ? lockers.First().ProcessName : "NONE"; 
Console.WriteLine($"Locked by: {locker}");
Console.ReadKey();

public class Foo
{
    public int    Id   { get; set; }
    public string Bar { get; set; }
}

public class MyContext : DbContext
{
    ~MyContext()
    {
        Console.WriteLine("Finaliser was called.");
    }

    public override void Dispose()
    {
        base.Dispose();
        Console.WriteLine("Dispose was called.");
    }

    public static readonly string DbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "_temp.db");

    public DbSet<Foo> Summaries { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Foo>().HasKey(nameof(Foo.Id));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbFile};Pooling=false");
    }
}

public static class Actions
{
    public static void AddItem()
    {
        using var ctx = new MyContext();
        ctx.Database.EnsureCreated();
        ctx.Summaries.Add(new Foo {Bar = "Foo"});
        ctx.SaveChanges();        
    }
}
