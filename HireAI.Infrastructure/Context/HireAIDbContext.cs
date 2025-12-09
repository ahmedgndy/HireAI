using HireAI.Data.Helpers.Enums;
using HireAI.Data.Models;
using HireAI.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;


namespace HireAI.Infrastructure.Context
{
    public class HireAIDbContext : IdentityDbContext<ApplicationUser>
    {
        public HireAIDbContext(DbContextOptions options): base(options)
        {
        }

        // DbSets for concrete entities
        public DbSet<Applicant> Applicants { get; set; } = default!;
        public DbSet<HR> HRs { get; set; } = default!;
        public DbSet<JobPost> JobPosts { get; set; } = default!;
        public DbSet<Application> Applications { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<CV> CVs { get; set; } = default!;
        public DbSet<Answer> Answers { get; set; } = default!;
        public DbSet<ApplicantSkill> ApplicantSkills { get; set; } = default!;
        public DbSet<ApplicantResponse> ApplicantResponses { get; set; } = default!;
        public DbSet<Exam> Exams { get; set; } = default!;
        public DbSet<ExamEvaluation> ExamEvaluations { get; set; } = default!;
        public DbSet<ExamSummary> ExamSummarys { get; set; } = default!;
        public DbSet<JobSkill> JobSkills { get; set; } = default!;
        public DbSet<QuestionEvaluation> QuestionEvaluations { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Skill> Skills { get; set; } = default!;

        //DBsets 
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //payment method
            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethods");
                entity.HasKey(e => e.Id);
            });


            //payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.Id);

                entity.Property(p => p.Amount).HasPrecision(18, 2);

                entity.HasOne(p => p.PaymentMethod)
                      .WithMany()
                      .HasForeignKey(p => p.PaymentMethodId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            //plan
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plans");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.Property(p => p.MonthlyPrice).HasPrecision(18, 2);
                entity.Property(p => p.Title).IsRequired();
            });


            //Subscription
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("Subscriptions");
                entity.HasKey(s => s.Id);

                // No PricePerCycle property anymore

                // Configure relationship using a shadow FK column "PlanId" (PlanId will still exist in DB
                // but is not exposed on the Subscription CLR type).
                entity.HasOne(s => s.Plan)
                      .WithMany()
                      .HasForeignKey("PlanId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property<string>("PlanTitle").IsRequired(false);
                entity.Property<string>("BillingCycle").IsRequired().HasDefaultValue("Monthly");
                entity.Property<string>("Status").IsRequired().HasDefaultValue("Active");
            });
            // TPC (Table Per Concrete class) 

            modelBuilder.Entity<User>().UseTpcMappingStrategy();
     
            modelBuilder.Entity<HR>().ToTable("HRs");
            modelBuilder.Entity<Applicant>().ToTable("Applicants");

            // Apply configuration classes from this assembly (IEntityTypeConfiguration implementations)
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HireAIDbContext).Assembly);

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
