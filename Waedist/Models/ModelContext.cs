using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Waedist.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Orderdelivary> Orderdelivaries { get; set; }
        public virtual DbSet<Orderproduct> Orderproducts { get; set; }
        public virtual DbSet<Orderr> Orderrs { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Productstore> Productstores { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Testmoninal> Testmoninals { get; set; }
        public virtual DbSet<Useraccount> Useraccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_USER98;PASSWORD=W@edW@ed12;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER98")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.HasKey(e => e.Aboutid)
                    .HasName("SYS_C00211608");

                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Aboutid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ABOUTID");

                entity.Property(e => e.Headline)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("HEADLINE");

                entity.Property(e => e.Image)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE");

                entity.Property(e => e.Text1)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT2");

                entity.Property(e => e.Text3)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT3");

                entity.Property(e => e.Text4)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT4");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLE");

                entity.Property(e => e.Tittlepic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLEPIC");

                entity.Property(e => e.Video)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("VIDEO");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.Payid)
                    .HasName("SYS_C00211594");

                entity.ToTable("BANK");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");

                entity.Property(e => e.Expiration)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRATION");

                entity.Property(e => e.Ownername)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("OWNERNAME");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("BLOG");

                entity.Property(e => e.Blogid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BLOGID");

                entity.Property(e => e.Headline1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("HEADLINE1");

                entity.Property(e => e.Headline2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("HEADLINE2");

                entity.Property(e => e.Pic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PIC");

                entity.Property(e => e.Text1)
                    .HasColumnType("CLOB")
                    .HasColumnName("TEXT1");

                entity.Property(e => e.Text2)
                    .HasColumnType("CLOB")
                    .HasColumnName("TEXT2");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLE");

                entity.Property(e => e.Tittlepic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLEPIC");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORYNAME");

                entity.Property(e => e.Pic)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PIC");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.HasKey(e => e.Contid)
                    .HasName("SYS_C00211602");

                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Contid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CONTID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Subject)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("SUBJECT");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLE");

                entity.Property(e => e.Tittlepic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLEPIC");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Homeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("HOMEID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Asidertittle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ASIDERTITTLE");

                entity.Property(e => e.Companyemail)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYEMAIL");

                entity.Property(e => e.Companylogo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYLOGO");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYNAME");

                entity.Property(e => e.Companyphone)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("COMPANYPHONE");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Headertext)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("HEADERTEXT");

                entity.Property(e => e.Tittle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLE");

                entity.Property(e => e.Tittlepic)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TITTLEPIC");
            });

            modelBuilder.Entity<Orderdelivary>(entity =>
            {
                entity.HasKey(e => e.Odlivaryid)
                    .HasName("SYS_C00241013");

                entity.ToTable("ORDERDELIVARY");

                entity.Property(e => e.Odlivaryid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ODLIVARYID");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Postnumber)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("POSTNUMBER");

                entity.Property(e => e.Shippingmethod)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("SHIPPINGMETHOD");

                entity.Property(e => e.Useraddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("USERADDRESS");

                entity.Property(e => e.Useremail)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("USEREMAIL");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USERID");

                entity.Property(e => e.Username)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.Property(e => e.Userphone)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("USERPHONE");
            });

            modelBuilder.Entity<Orderproduct>(entity =>
            {
                entity.ToTable("ORDERPRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Qty)
                    .HasColumnType("NUMBER")
                    .HasColumnName("QTY");

                entity.Property(e => e.Status)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Stoid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STOID");

                entity.Property(e => e.Storename)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("STORENAME");

                entity.Property(e => e.Totalamount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("TOTALAMOUNT");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PRODUCTFK");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PRODUCT");
            });

            modelBuilder.Entity<Orderr>(entity =>
            {
                entity.HasKey(e => e.Orderid)
                    .HasName("SYS_C00211613");

                entity.ToTable("ORDERR");

                entity.Property(e => e.Orderid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ORDERID");

                entity.Property(e => e.Display)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DISPLAY")
                    .IsFixedLength(true);

                entity.Property(e => e.Dte)
                    .HasColumnType("DATE")
                    .HasColumnName("DTE");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orderrs)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKUSER");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Payid)
                    .HasName("SYS_C00211596");

                entity.ToTable("PAYMENT");

                entity.Property(e => e.Payid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PAYID");

                entity.Property(e => e.Amount)
                    .HasColumnType("FLOAT")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Datee)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("USERSFK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Pic)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PIC");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Productname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTNAME");

                entity.Property(e => e.Productsize)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCTSIZE");

                entity.Property(e => e.Productvalue)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRODUCTVALUE");
            });

            modelBuilder.Entity<Productstore>(entity =>
            {
                entity.ToTable("PRODUCTSTORE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Productid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCTID");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STOREID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productstores)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FKPRODUCT");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Productstores)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("F_CUSTOMER");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("REVIEW");

                entity.Property(e => e.Reviewid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REVIEWID");

                entity.Property(e => e.Name)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Pic)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PIC");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("STOREID");

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Storeid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK1_STORE_ID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLES");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("STORE");

                entity.Property(e => e.Storeid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STOREID");

                entity.Property(e => e.Categoryid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORYID");

                entity.Property(e => e.Headpic)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("HEADPIC");

                entity.Property(e => e.Monthlyrent)
                    .HasColumnType("FLOAT")
                    .HasColumnName("MONTHLYRENT");

                entity.Property(e => e.Smallpic)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("SMALLPIC");

                entity.Property(e => e.Storename)
                    .HasMaxLength(126)
                    .IsUnicode(false)
                    .HasColumnName("STORENAME");

                entity.Property(e => e.Text)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Totalsales)
                    .HasColumnType("FLOAT")
                    .HasColumnName("TOTALSALES");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("CATEGOFK");
            });

            modelBuilder.Entity<Testmoninal>(entity =>
            {
                entity.ToTable("TESTMONINAL");

                entity.Property(e => e.Testmoninalid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TESTMONINALID");

                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Testimage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TESTIMAGE");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USERID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testmoninals)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("TESTM");
            });

            modelBuilder.Entity<Useraccount>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("SYS_C00211591");

                entity.ToTable("USERACCOUNT");

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Phonenumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PHONENUMBER");

                entity.Property(e => e.Pic)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("PIC");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Useraccounts)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ROLEFK");
            });

            modelBuilder.HasSequence("DEP_SEQ");

            modelBuilder.HasSequence("SEQ_EMP_");

            modelBuilder.HasSequence("SEQ_EMP_ID");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
