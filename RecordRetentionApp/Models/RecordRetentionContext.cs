namespace RecordRetentionApp.Models
{

    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RecordRetentionContext : DbContext
    {
        public RecordRetentionContext()
            : base("name=RecordRetentionDB")
        {
        }

        public virtual DbSet<retention_schedule> retention_schedule { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<retention_schedule>()
        //        .Property(e => e.File_Number)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<retention_schedule>()
        //        .Property(e => e.department)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<retention_schedule>()
        //        .Property(e => e.description)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<retention_schedule>()
        //        .Property(e => e.Folder_Name)
        //        .IsUnicode(false);

        //    modelBuilder.Entity<retention_schedule>()
        //       .Property(e => e.record_status)
        //       .IsUnicode(false);

        //    modelBuilder.Entity<retention_schedule>()
        //       .Property(e => e.username)
        //       .IsUnicode(false);
        //}


    }
}