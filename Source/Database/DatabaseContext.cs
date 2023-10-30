using Home.Models.Entities.Money;
using Home.Models.Entities.Other;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Home.Source.Database
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountActivity> AccountActivities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SuperCategory> SuperCategories { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>().ToTable("People");
            builder.Entity<Event>().ToTable("Events");
            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<AccountActivity>().ToTable("AccountActivities");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<SuperCategory>().ToTable("SuperCategories");
            builder.Entity<Entry>().ToTable("Entries");

            builder.Entity<User>(e =>
            {
                e.HasMany(p => p.People).WithOne(p => p.User).OnDelete(DeleteBehavior.Cascade);
                e.HasMany(p => p.Activities).WithOne(p => p.User).OnDelete(DeleteBehavior.Cascade);
                e.HasMany(p => p.Accounts).WithOne(p => p.User).OnDelete(DeleteBehavior.Cascade);                
                e.HasMany(p => p.SuperCategories).WithOne(p => p.User).OnDelete(DeleteBehavior.Cascade);
                e.HasMany(p => p.Entries).WithOne(p => p.User).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Person>(e =>
            {
                e.Property(p => p.Id).HasColumnName("PersonId");

                e.Property(p => p.FirstName).IsRequired(required: true).HasMaxLength(25);
                e.Property(p => p.LastName).IsRequired(required: true).HasMaxLength(25);

                e.HasOne(p => p.User).WithMany(p => p.People).HasForeignKey(p => p.UserId);
                e.HasMany(p => p.Events).WithOne(p => p.Person).OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(p => new { p.UserId, p.FirstName, p.LastName }).IsUnique();
            });

            builder.Entity<Event>(e =>
            {
                e.Property(p => p.Id).HasColumnName("EventId");

                e.Property(p => p.Description).IsRequired(required: true).HasMaxLength(250);
                e.Property(p => p.RunningTime).IsRequired(required: true);
                e.Property(p => p.RemainingTime).IsRequired(required: true);
                e.Property(p => p.Date).IsRequired(required: true).HasColumnType("datetime");
                e.Property(p => p.Time).IsRequired(required: false).HasColumnType("time");
                e.Property(p => p.ReminderTime).IsRequired(required: false);

                e.HasOne(p => p.Person).WithMany(p => p.Events).HasForeignKey(p => p.PersonId);

                e.HasIndex(p => new { p.PersonId, p.Description, p.Date }).IsUnique().HasFilter(null);
            });

            builder.Entity<Activity>(e =>
            {
                e.Property(p => p.Id).HasColumnName("ActivityId");

                e.Property(p => p.Tag).IsRequired(required: true).HasMaxLength(25);
                e.Property(p => p.Description).IsRequired(required: true).HasMaxLength(250);
                e.Property(p => p.Date).IsRequired(required: true).HasColumnType("datetime");
                e.Property(p => p.Time).IsRequired(required: false).HasColumnType("time");
                e.Property(p => p.IsDone).IsRequired(required: true);

                e.HasOne(p => p.User).WithMany(p => p.Activities).HasForeignKey(p => p.UserId);

                e.HasIndex(p => new { p.UserId, p.Tag, p.Description, p.Date }).IsUnique().HasFilter(null);
            });

            builder.Entity<Account>(e =>
            {
                e.Property(p => p.Id).HasColumnName("AccountId");

                e.Property(p => p.Name).IsRequired(required: true).HasMaxLength(50);
                e.Property(e => e.Amount).IsRequired(required: true).HasColumnType("money");
                e.Property(p => p.Description).IsRequired(required: false).HasMaxLength(100);
                e.Property(p => p.Owner).IsRequired(required: true).HasMaxLength(25);

                e.HasOne(p => p.User).WithMany(p => p.Accounts).HasForeignKey(p => p.UserId);
                e.HasMany(p => p.Entries).WithOne(p => p.Account);
                e.HasMany(p => p.AccountActivities).WithOne(p => p.Account).OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(p => new { p.UserId, p.Name }).IsUnique();
            });


            builder.Entity<AccountActivity>(e =>
            {
                e.Property(p => p.Id).HasColumnName("AccountActivityId");

                e.Property(e => e.Amount).IsRequired(required: true).HasColumnType("money");
                e.Property(e => e.Date).IsRequired(required: true).HasColumnType("datetime");
                
                e.HasOne(p => p.Account).WithMany(p => p.AccountActivities).HasForeignKey(p => p.AccountId);
            });

            builder.Entity<Category>(e =>
            {
                e.Property(p => p.Id).HasColumnName("CategoryId");

                e.Property(e => e.Budget).IsRequired(required: true).HasColumnType("money");
                e.Property(e => e.Name).IsRequired(required: true).HasMaxLength(25);

                e.HasOne(p => p.SuperCategory).WithMany(p => p.Categories).HasForeignKey(p => p.SuperCategoryId);
                e.HasMany(p => p.Entries).WithOne(p => p.Category);

                e.HasIndex(p => new { p.SuperCategoryId, p.Name }).IsUnique();
            });

            builder.Entity<SuperCategory>(e =>
            {
                e.Property(p => p.Id).HasColumnName("SuperCategoryId");

                e.Property(e => e.Name).IsRequired(required: true).HasMaxLength(25);
                e.Property(e => e.IsRegular).IsRequired(required: true);
                e.Property(e => e.IsService).IsRequired(required: true);

                e.HasOne(p => p.User).WithMany(p => p.SuperCategories).HasForeignKey(p => p.UserId);
                e.HasMany(p => p.Categories).WithOne(p => p.SuperCategory).OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(p => new { p.UserId, p.Name }).IsUnique();
            });

            builder.Entity<Entry>(e =>
            {
                e.Property(p => p.Id).HasColumnName("EntryId");

                e.Property(e => e.Amount).IsRequired(required: true).HasColumnType("money");
                e.Property(e => e.Date).IsRequired(required: true).HasColumnType("datetime");
                e.Property(p => p.Description).IsRequired(required: false).HasMaxLength(100);

                e.HasOne(p => p.User).WithMany(p => p.Entries).HasForeignKey(p => p.UserId);//.OnDelete(DeleteBehavior.NoAction);
                e.HasOne(p => p.Category).WithMany(p => p.Entries).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.NoAction);
                e.HasOne(p => p.Account).WithMany(p => p.Entries).HasForeignKey(p => p.AccountId).OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
