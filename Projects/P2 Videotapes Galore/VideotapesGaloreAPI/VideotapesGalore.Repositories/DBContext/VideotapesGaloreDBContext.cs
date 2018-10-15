namespace VideotapesGalore.Repositories.DBContext
{
    using System;
    using System.Linq;
    using VideotapesGalore.Models.Entities;
    using Microsoft.EntityFrameworkCore;

    public class VideotapesGaloreDBContext : DbContext
    {
        public VideotapesGaloreDBContext(DbContextOptions options): base(options) { }
        public virtual DbSet<Tape> Tapes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<BorrowRecord> BorrowRecords { get; set; }
    }
}